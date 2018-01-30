// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Security.Principal;

namespace SP_powershell
{
    /// <summary>
    /// The Connect-HXServer PowerShell CmdLet is used to establish sessions with 
    /// HXServer instances.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "HXServer")]
    [OutputType(typeof(VirtualMachine))]
    //[Category(CmdletCategory.Session)]
    [CmdletBinding]
    public class ConnectHXServer : SPCmdlet
    {



        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true,
            ParameterSetName = "PSCredentialObject")]
        [ValidateNotNull]
        [Alias("cred")]
        public PSCredential Credential { get; set; }


        [Parameter(Position = 0,Mandatory =true)]
        [ValidateNotNullOrEmpty]
        [Alias("Srvr")]
        public string Server { get; set; }

        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("Uname")]
        public string Username { get; set; }

        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("Pswrd")]
        public string Password { get; set ; }


        [Parameter]
        [Alias("ignorecerts")]
        public SwitchParameter IgnoreCertificateWarnings { get; set; }

        public static Dictionary<string, dynamic> storageKeyDictionary;

        public ConnectHXServer()
        {
            if (storageKeyDictionary == null)
            {
                storageKeyDictionary = new Dictionary<string, dynamic>();
            }
        }



        //
        // Cmdlet body
        //

        protected override void ProcessSPRecord()
        {
            ValidateParameters();

            // Callback method for handling the certificates returned by each
            // Server to which we are trying to establish a session

            if (Server == null)
            {
                throw new InvalidProgramException("Please enter a valid IP Address of a server to continue");

            }
            try
            {
                //attempt for regular login using ip address
                ResolveCredentials();
                if (Username != null && Password != null)
                {
                    Username=Username.ToString();
                    Password = Password.ToString();
                }
                else
                {
                    Username = Credential.UserName.ToString();
                    Password = Credential.GetNetworkCredential().Password.ToString();
                }
                var client_id = "HxGuiClient";
                var client_secret = "Sunnyvale";
                var redirect_uri = "http://localhost/aaa/redirect";
                //};

                Debug.Assert(Username != null);

                Configuration.Default = new Configuration();
                
                if (Username != null && Password != null)
                {
                    Configuration.Default.Username = Username.ToString();
                    Configuration.Default.Password = Password.ToString();
                }
                else
                {
                    throw new ArgumentException("Username/Password has to be provided");
                }
                var apiString = "https://" + Server.ToString().Trim() + "/aaa/v1";
                               
                UserCredentials body = new UserCredentials(Username.ToString().Trim(), Password.ToString().Trim(), client_id.Trim(), client_secret.Trim(), redirect_uri.Trim());
                var apiInstance = new ObtainAccessTokenApi(apiString);
                AccessTokenEnvelope result = apiInstance.ObtainAccessToken("password", body);
                


                // Access Token for each server connected is maintained in Dictionary object
                //storageKeyDictionary-server ip is key while response token is value;
                Token vtoken = new Token();
                vtoken.AccessToken = result.AccessToken;
                vtoken.RefreshToken = result.RefreshToken;
                vtoken.TokenType = result.TokenType;
                dynamic dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                if (dictServerCnnctd != null)
                {
                    throw new Exception("Server is already connected!");
                }
                WriteObject(result, true);
                storageKeyDictionary.Add(Server.ToString(), vtoken);
                

            }
            catch (ApiException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Correct Credentials not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch (Exception e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
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
                    string.Empty, PSCredentialTypes.Generic,PSCredentialUIOptions.ValidateUserNameSyntax);

                if (Credential == null)
                {
                    // The user has cancelled/not entered credentials.
                    ThrowTerminatingError(TCmdLetEx.GetAuthErrorRecord(
                        "Please retry with valid credentials."));
                }
            }
            else
            {
                if (Password!=null)
                {
                    //
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

        /// <summary>
        /// Checks to see if the Server exists (by its IP Address key) in collections.
        /// </summary>
        /// <param name="HXServer">An HX ConnectServer instance.</param>
        /// <returns><c>true</c> if the IP address exists as a key in the dictionaries, 
        /// <c>false</c> otherwise.</returns>
        private bool HXServerExists(IHXServer tintriServer)
        {
            //if (tintriServer.) { }
                return true;
        }

    }

    


    internal class IHXServer
    {
    }
}
