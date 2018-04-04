// Author(s): 
// Ashima Bahl, asbahl@cisco.com 26-feb
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace Cisco.Runbook
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

        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Srvr")]
        public string Server { get; set; }

        [Parameter]
        [Alias("ignorecerts")]
        public SwitchParameter IgnoreCertificateWarnings { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            ValidateParameters();
            try
            {
                if (ConnectHXServer.storageKeyDictionary == null)
                {
                    throw new Exception("No server is connected.");
                }
                else if (ConnectHXServer.storageKeyDictionary != null)
                {
                    var firstElement = ConnectHXServer.storageKeyDictionary.FirstOrDefault();
                    Server = firstElement.Key;

                    var arrayServer = ConnectHXServer.storageKeyDictionary.ToArray();
                    var serverCount = arrayServer.Count();
                    for (int i =0;i<= (serverCount - 1);i++)
                    {
                        WriteObject(arrayServer[i].Key);
                    }
                }
            }
            catch (ApiException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Correct Credentials not provided.", 
                           ErrorCategory.AuthenticationError, e.Message);
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
