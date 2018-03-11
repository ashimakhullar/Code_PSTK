// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Set, "PrepareFailover")]
    [OutputType(typeof(VirtualMachine))]

    public class SetPrepareFailover : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        //VMName will pass the VM name to the cmdlet
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string VMName { get; set; }

        //VMId will pass the VM uid
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }

        //Server Parameter contains the Cisco HXConnect IP
        [Parameter(Mandatory = true)]
        [Alias("srvr")]
        public string Server { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            //Server authentication to be done
            //ValidateServerSessions();
            if (ValidateParameters() == false)
                return;

            // Configure OAuth2 access token for authorization
            
            AccessToken accToken = new AccessToken();
                       
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

                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                var apiInstance = new RecoverApi(apiString);

                string accessTkn = accToken.GetAccessToken(Server.ToString());
                
                if (VMId != null)
                {
                  string result2 = apiInstance.OpDpVmPrepareFailoverPut(VMId.ToString(), accessTkn.ToString(), "en-US");//43d80de4-f438-4ff0-a3de-b89a62a3ac1f
                }
            }
            catch (ArgumentException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Arguments not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch(Exception e)
            {
                WriteObject("Exception when calling apiInstance.OpDpVmRecoveryFailoverPut: " + e.Message);
            }
         
        }

        private DateTime GetOneMinutesFromNow()
        {
            DateTime otherDate = DateTime.Now.AddMinutes(2);
            return otherDate;
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
