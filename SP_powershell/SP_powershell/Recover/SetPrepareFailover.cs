// Author(s): 
// Ashima Bahl, asbahl@cisco.com 

using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System;
using System.Linq;


namespace Cisco.Runbook
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
            
            try
            {
                // Configure access token for authorization

                if (Server != null)
                {
                    Server = Server.Trim();
                }
                dynamic value = "";
                if (ConnectHXServer.storageKeyDictionary.TryGetValue(Server,
                                                                   out value))
                {
                    Console.WriteLine("Server = \"{0}\", is connected.", Server);
                }
                else
                {
                    Console.WriteLine("Server = \"{0}\" is not found.", Server);
                }
                string accessTkn = value.TokenType + " " + value.AccessToken;

                var apiInstance = new RecoverApi(Server);
                
                if (VMId != null)
                {
                    string respPrepareFailoverPut = apiInstance.OpDpVmPrepareFailoverPut(VMId.Trim(),
                                                            accessTkn.Trim(), "en-US");
                    WriteVerbose("Test Failover of VM done");
                    WriteObject(respPrepareFailoverPut, true);
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

        protected internal override bool ValidateParameters()
        {
            // Leave this here so that we can add more checks if needed
            // and return all errors if there are multiple without returning
            // on the first one we find.
            return true;
        }
    }
    
}
