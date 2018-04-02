// Author(s): 
// Ashima Bahl, asbahl@cisco.com 

using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Set, "ReverseProtect")]
    [OutputType(typeof(VirtualMachine))]

    public class SetReverseProtect : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
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

        //Async will return the task id for asynchronous call to cmdlet-switch parameter
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Async { get; set; }

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
                // Configure OAuth2 access token for authorization
                AccessToken accToken = new AccessToken();
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

                //var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                var apiInstance = new RecoverApi(Server);

                string accessTkn = accToken.GetAccessToken(Server.ToString());
                string respVMReverseProtect = "";
                if (VMId != null)
                {
                    if (Async == true)
                    {
                        respVMReverseProtect = apiInstance.OpDpVmReverseProtectPut(VMId.ToString(), accessTkn.ToString(), "en-US");
                        WriteVerbose("The Vm has been reverse protected!");
                        WriteObject(respVMReverseProtect, true);
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        respVMReverseProtect = apiInstance.OpDpVmReverseProtectPut(VMId.ToString(), accessTkn.ToString(), "en-US");
                        JObject joResponse = JObject.Parse(respVMReverseProtect);
                        JValue ojObject = (JValue)joResponse["taskId"];
                        WriteVerbose("The Vm has been failed over");
                        List<IO.Swagger.Model.Job> respVMTaskGet = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());

                        DateTime oneMinutesFromNow = GetOneMinutesFromNow();
                        if (respVMTaskGet[0].State.ToString() == "EXCEPTION")
                        {
                            WriteVerbose("Exception in Test Failover of VM");
                            WriteObject(respVMTaskGet, true);

                        }
                        if (respVMTaskGet[0].State.ToString() == "COMPLETED")
                        {
                            WriteVerbose("Test Failover of VM done");
                            WriteObject(respVMTaskGet, true);
                        }

                        while (respVMTaskGet != null && now < oneMinutesFromNow)
                        {
                            List<IO.Swagger.Model.Job> checkVMTaskGet = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());
                            if (checkVMTaskGet[0].State.ToString() == "COMPLETED")
                            {
                                respVMTaskGet = checkVMTaskGet;
                                break;
                            }
                            else if (checkVMTaskGet[0].State.ToString() == "EXCEPTION")
                            {
                                respVMTaskGet = checkVMTaskGet;
                                break;
                            }
                            else
                            {
                                //Wait for 3 seconds
                                System.Threading.Thread.Sleep(3000);
                            }
                        }
                        WriteObject(respVMTaskGet, true);
                    }
                }

            }
            catch (ArgumentException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Arguments not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch (Exception e)
            {

                WriteObject("Exception when calling apiInstance.OpDpVmReverseProtectPut: " + e.Message);
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
