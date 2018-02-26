// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;


namespace SP_powershell
{
    /// <summary>
    /// The Connect-HXServer PowerShell CmdLet is used to establish sessions with 
    /// HXServer instances.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "HXServer")]
    [OutputType(typeof(VirtualMachine))]
    [CmdletBinding]
    public class GetHXServer : SPCmdlet
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

        [Parameter]
        [Alias("ignorecerts")]
        public SwitchParameter IgnoreCertificateWarnings { get; set; }

        public static Dictionary<string, dynamic> storageKeyDictionary;

        public DisconnectHXServer()
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
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }
            
                if (Server != null)
                {
                    Server = Server.ToString().Trim();
                }
                else if (ConnectHXServer.storageKeyDictionary != null)
                {
                    var firstElement = ConnectHXServer.storageKeyDictionary.FirstOrDefault();
                    Server = firstElement.Key;
                }
                var num = 0;
                String accessTkn = "";
                dynamic dictServerCnnctd = null;
                if (ConnectHXServer.storageKeyDictionary != null)
                {
                    num = ConnectHXServer.storageKeyDictionary.Count();
                    if (num == 1)
                    {
                     dictServerCnnctd = ConnectHXServer.storageKeyDictionary.First(x => x.Key == Server.ToString()).Value;
                    }
                    else
                    {
                     dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                    }
                }
                else
                {
                    num = 0;
                    throw new Exception("Please connect to a server.");
                }
                
                if (dictServerCnnctd != null)
                {
                    accessTkn = dictServerCnnctd.TokenType + " " + dictServerCnnctd.AccessToken;
                }
                else
                {
                    throw new Exception("The Server is not connected;Please check the IP address of Server");
                }
                Configuration.Default = new Configuration();
                
                 var apiString = "https://" + Server.ToString().Trim() + "/aaa/v1";
                
                 RevokeTokenEnvelope body = new RevokeTokenEnvelope(dictServerCnnctd.AccessToken.ToString(), dictServerCnnctd.RefreshToken.ToString(), dictServerCnnctd.TokenType.ToString());
                var apiInstance = new RevokeTokenApi(apiString);
                apiInstance.RevokeToken1(body, accessTkn.ToString());
                ConnectHXServer.storageKeyDictionary.Remove(Server.ToString());
                WriteObject(Server.ToString() + "is Disconnected.", true);

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
        /// Checks to see if the Server exists (by its IP Address key) in collections.
        /// </summary>
        /// <param name="HXServer">An HX ConnectServer instance.</param>
        /// <returns><c>true</c> if the IP address exists as a key in the dictionaries, 
        /// <c>false</c> otherwise.</returns>
        private bool HXServerExists(IHXServer tintriServer)
        {
           return true;
        }

    }

    


  
}
