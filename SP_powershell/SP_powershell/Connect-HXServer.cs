using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;

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

        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Uname")]
        public string Username { get; set; }

        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("Pswrd")]
        public string Password { get; set; }

        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("Srvr")]
        public string Server { get; set; }

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
            // TintriServer to which we are trying to establish a session

            if (Server == null)
            {
                throw new InvalidProgramException("Please enter a valid IP Address of a server to continue");

            }
            try
            {
                //var objAccessTokenJson = new AccessToken
                //{
                //username="local/root",
                //password= "Cisco123",
                var client_id = "HxGuiClient";
                var client_secret = "Sunnyvale";
                var redirect_uri = "http://localhost/aaa/redirect";
                //};

                Debug.Assert(Username != null);

                Configuration.Default = new Configuration();
                //Configuration.Default.AccessToken = objAccessTokenJson;
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

                // var json = JsonConvert.SerializeObject(objAccessTokenJson);


                //  var objEF = JsonConvert.DeserializeObject<UserCredentials>(json);
                UserCredentials body = new UserCredentials(Username.ToString().Trim(), Password.ToString().Trim(), client_id.Trim(), client_secret.Trim(), redirect_uri.Trim());
                //  TokenInfo body = new TokenInfo(objEF);
                //ProtectedVMInfo result1 = apiInstance.OpDpVmPost(body);
                var apiInstance = new ObtainAccessTokenApi(apiString);
                AccessTokenEnvelope result = apiInstance.ObtainAccessToken("password", body);
                WriteObject(result, true);
                // return;

                // Check if connection present in the dic
                //if(HXServerExists!=true)
                //{ 
                    Token vtoken = new Token();
                    vtoken.AccessToken = result.AccessToken;
                    vtoken.RefreshToken = result.RefreshToken;
                    vtoken.TokenType = result.TokenType;
                    storageKeyDictionary.Add(Server.ToString(), vtoken);
                //}

                // { { Server.ToString(),vtoken } };
                //Token.ServerConnected = new Dictionary<string, object>();
                //Token.ServerConnected.Add(Server.ToString(), Token.AccessToken.ToString());
                //var localserverDict = new Dictionary<String, String>;
                ////////  Dictionary<string, object> storageKeyDictionary = new Dictionary<string, object>() { { Server.ToString(), Token.AccessToken.ToString() }};

                //SessionState ss = new SessionState(); } };

                //Storage.Store.Add(clientId, storageKeyDictionary);

                //// private static Dictionary<string, string> ServerConnected;

                //localserverDict.Add(Server.ToString(),Token.AccessToken);
                //SessionState ss = new SessionState();
                //ss.InvokeCommand.InvokeScript("new-variable -scope global -name gtest -value 3");
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling apiInstance.OpDpVmGet: " + e.Message);
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
