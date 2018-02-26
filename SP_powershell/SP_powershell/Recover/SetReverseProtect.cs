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
using Newtonsoft.Json.Linq;

namespace SP_powershell
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

        //[Parameter(ParameterSetName = "HXName")]
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }
        
        [Parameter(Mandatory = true)]
        [Alias("srvr")]
        public string Server { get; set; }

        //[Parameter(ParameterSetName = "Async")]
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

            // Configure OAuth2 access token for authorization
            
           

            try
            {
                //Configuration.Default = new Configuration();
                //Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
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

                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                var apiInstance = new RecoverApi(apiString);

                string accessTkn = accToken.GetAccessToken(Server.ToString());
               string result1 = "";
                if (VMId != null)
                {
                if (Async == true)
                {
                    result1 = apiInstance.OpDpVmReverseProtectPut(VMId.ToString(), accessTkn.ToString(),"en-US");
                    WriteVerbose("The Vm has been reverse protected!");
                    WriteObject(result1, true);
                }
                else
                {
                    DateTime now = DateTime.Now;
                    //apiInstance.OpDpVmHaltPut(VMId.ToString(), accessTkn.ToString(), "en-US");
                    //WriteVerbose("The Vm has been halted");
                    result1 = apiInstance.OpDpVmReverseProtectPut(VMId.ToString(), accessTkn.ToString(), "en-US");
                    JObject joResponse = JObject.Parse(result1);
                    JValue ojObject = (JValue)joResponse["taskId"];
                    WriteVerbose("The Vm has been failed over");
                    List<IO.Swagger.Model.Job> result2 = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());
                    
                    DateTime oneMinutesFromNow = GetOneMinutesFromNow();
                    if (result2[0].State.ToString() == "EXCEPTION")
                    {
                        WriteVerbose("Exception in Test Failover of VM");
                        WriteObject(result2, true);

                    }
                    if (result2[0].State.ToString() == "COMPLETED")
                    {
                        WriteVerbose("Test Failover of VM done");
                        WriteObject(result2, true);
                    }

                    while (result2 != null && now < oneMinutesFromNow)
                    {
                        List<IO.Swagger.Model.Job> check1 = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());
                        if (check1[0].State.ToString() == "COMPLETED")
                        {
                            result2 = check1;
                            break;
                        }
                        else if (check1[0].State.ToString() == "EXCEPTION")
                        {
                            result2 = check1;
                            break;
                        }
                        else
                        {
                            //Wait for 3 seconds
                            System.Threading.Thread.Sleep(3000);
                        }
                    }
                    WriteObject(result2, true);
                }
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
