// Author(s): 
// Ashima Bahl, asbahl@cisco.com 26-feb
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

        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Srvr")]
        public string Server { get; set; }

        [Parameter]
        [Alias("ignorecerts")]
        public SwitchParameter IgnoreCertificateWarnings { get; set; }

        public static Dictionary<string, dynamic> storageKeyDictionary;

        //public DisconnectHXServer()
        //{
        //    if (storageKeyDictionary == null)
        //    {
        //        storageKeyDictionary = new Dictionary<string, dynamic>();
        //    }
        //}

        //
        // Cmdlet body
        //

        protected override void ProcessSPRecord()
        {
            ValidateParameters();

            // Callback method for handling the certificates returned by each
            // Server to which we are trying to establish a session

            //if (Server == null)
            //{
            //    throw new InvalidProgramException("Please enter a valid IP Address of a server to continue");

            //}
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

                    var vARR = ConnectHXServer.storageKeyDictionary.ToArray();
                    var vARR_cnt = vARR.Count();
                    //WriteObject(vARR);
                    //WriteObject(vARR_cnt);
                    
                    for (int i =0;i<= (vARR_cnt - 1);i++)
                    {

                        WriteObject(vARR[i].Key);
                        // WriteObject("Server IP#" + storageKeyDictionary[i]);
                    }
                }

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
