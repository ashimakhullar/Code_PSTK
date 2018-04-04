// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Cisco.Runbook
{
    /// <summary>
    /// The Disconnect-HXServer PowerShell CmdLet is used to revoke/disconnect sessions with 
    /// HXServer instances.
    /// </summary>
    [Cmdlet(VerbsCommunications.Disconnect, "HXServer")]
    [OutputType(typeof(VirtualMachine))]
    [CmdletBinding]
    public class DisconnectHXServer : SPCmdlet
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

        //public static Dictionary<string, dynamic> storageKeyDictionary;

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
            try
            {
                if (Server != null)
                {
                    Server = Server.Trim();
                }
                dynamic value = "";
                if (ConnectHXServer.storageKeyDictionary.TryGetValue(Server,
                                                                   out value))
                {
                    Console.WriteLine("Server = \"{0}\", will be disconnected.", Server);
                }
                else
                {
                    Console.WriteLine("Server = \"{0}\" is not found.", Server);
                }
                string accessTkn = value.TokenType + " " + value.AccessToken;
                string vAccessToken = value.AccessToken.ToString();
                string vRefreshToken = value.RefreshToken.ToString();
                string vTokenType = value.TokenType.ToString();
                Configuration.Default = new Configuration();
                RevokeTokenEnvelope body = new RevokeTokenEnvelope(
                                vAccessToken, vRefreshToken,
                                vTokenType);
                var apiInstance = new RevokeTokenApi(Server.Trim());
                apiInstance.RevokeToken1(body, accessTkn);
                ConnectHXServer.storageKeyDictionary.Remove(Server);
                WriteObject(Server + " is Disconnected.", true);
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
        protected internal override bool ValidateParameters()
        {
            // Leave this here so that we can add more checks if needed
            // and return all errors if there are multiple without returning
            // on the first one we find.
            return true;
        }
    }
}
