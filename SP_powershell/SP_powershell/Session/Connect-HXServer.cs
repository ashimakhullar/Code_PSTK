// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Configuration;
using System.Collections.Generic;

namespace Cisco.Runbook
{
    /// <summary>
    /// The Connect-HXServer PowerShell CmdLet is used to establish sessions with 
    /// HXServer instances.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "HXServer")]
    [OutputType(typeof(VirtualMachine))]
    [CmdletBinding]
    public class ConnectHXServer : SPCmdlet
    {
        // 
        // Properties (PowerShell Parameters) to be defined below
        //

        //credentials to be inputed
        //WIP -not implemented
        [Parameter(Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true,
            ParameterSetName = "PSCredentialObject")]
        [ValidateNotNull]
        [Alias("cred")]
        public PSCredential Credential { get; set; }

        //Server IP address
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Srv")]
        public string Server { get; set; }

        //Username for the Server IP entered
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("user")]
        public string Username { get; set; }

        //Password for the Server IP entered
        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("pwd")]
        public string Password { get; set; }

        //Switch paramter defining whether to ignore certificate warnings
        [Parameter]
        [Alias("ignorecerts")]
        public SwitchParameter IgnoreCertificateWarnings { get; set; }

        public new static Dictionary<string, dynamic> storageKeyDictionary = 
                                            new Dictionary<string, dynamic>();

        //
        // Cmdlet body
        //
        /// <summary>
        /// ProcessRecord
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        protected override void ProcessSPRecord()
        {
            ValidateParameters();
            // Callback method for handling the certificates returned by each
            // Server to which we are trying to establish a session
            try
            {
                //attempt for regular login using ip address
                ResolveCredentials();
                if (Username != null && Password != null)
                {
                    Username = Username;
                    Password = Password;
                }
                else
                {
                    Username = Credential.UserName.ToString();
                    Password = Credential.GetNetworkCredential().Password.ToString();
                }
                AccessTokenEnvelope resultToken = GetAccessToken();

                if (resultToken != null)
                // Access Token for each server connected is maintained in Dictionary object
                //storageKeyDictionary-server ip is key while response token is value;
                {
                    dynamic dictServerCnnctd = ConnectHXServer.storageKeyDictionary.
                                    FirstOrDefault(x => x.Key == Server.ToString()).Value;
                    if (dictServerCnnctd != null)
                    {
                        throw new Exception("Server is already connected!");
                    }
                }
                WriteObject(resultToken, true);
                storageKeyDictionary.Add(Server.ToString(), resultToken);
            }
            catch (ApiException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Correct Credentials not provided.", ErrorCategory.AuthenticationError,
                           e.Message);
                WriteError(psErrRecord);
            }
            catch (Exception e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
        }

        private AccessTokenEnvelope GetAccessToken()
        {
            try
            {
                string exeConfigPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.Configuration.Configuration configFile = ConfigurationManager.
                                    OpenExeConfiguration(exeConfigPath);
                var clientId = configFile.AppSettings.Settings["client_id"].Value;
                var clientSecret = configFile.AppSettings.Settings["client_secret"].Value;
                var redirectUri = configFile.AppSettings.Settings["redirect_uri"].Value;
                Debug.Assert(Username != null);

                if (Username == null || Password == null)
                {
                    throw new ArgumentException("Username/Password has to be provided");
                }
                UserCredentials body = new UserCredentials(Username.ToString().Trim(), 
                     Password.ToString().Trim(), clientId.Trim(), clientSecret.Trim(), redirectUri.Trim());
                var apiInstance = new ObtainAccessTokenApi(Server);
                AccessTokenEnvelope responseAccessToken = apiInstance.ObtainAccessToken("password", body);
                return responseAccessToken;
            }
            catch (Exception e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
                throw new Exception(e.Message);
            }
        }

        protected internal override bool ValidateParameters()
        {
            // Leave this here so that we can add more checks if needed
            // and return all errors if there are multiple without returning
            // on the first one we find.
            return true;
        }

        /// <summary>
        /// In case of a regular login, resolve the login credentials and make sure
        /// that the Credentials object is not null.
        /// </summary>
        private void ResolveCredentials()
        {
            // If we do not have a valid Credential object, or username and password,
            // PromptForCredential() will return true (requiring user input).

            if (RequirePromptForCredentials())
            {
                // PowerShell PromptForCredential() will return an initialized
                // PSCredential object if the user doesn't cancel the dialog.
                Credential = Host.UI.PromptForCredential(
                    "Credentials Required",
                    "Please enter a valid username and password to connect to the Cisco HyperFlex Connect",
                    string.IsNullOrEmpty(Username)
                        ? "local/root"
                        : Username,
                    string.Empty, PSCredentialTypes.Generic, PSCredentialUIOptions.ValidateUserNameSyntax);

                if (Credential == null)
                {
                    // The user has cancelled/not entered credentials.
                    ThrowTerminatingError(TCmdLetEx.GetAuthErrorRecord(
                        "Please retry with valid credentials."));
                }
            }

        }

        /// <summary>
        /// Checks to see if we need to prompt for credentials.
        /// </summary>
        /// <returns><c>true</c> if we must prompt for credentials, 
        /// <c>false</c> otherwise.</returns>
        /// <remarks>If the PSCredential property is null, or if we
        /// do not have BOTH a username and a password, this method
        /// will return true to its name.</remarks>
        private bool RequirePromptForCredentials()
        {
            var userName = string.IsNullOrEmpty(Username);
            var passWord = Password == null;

            bool[] userNamePassword =
            {
                userName,
                passWord
            };

            if (!string.IsNullOrEmpty(Credential?.UserName) &&
                Credential.Password.Length > 0)
            {
                return false;
            }
            return !userNamePassword.All(tf => tf == false);
        }
    }
}
