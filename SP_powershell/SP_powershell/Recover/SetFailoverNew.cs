// Author(s): 
// Ashima Bahl, asbahl@cisco.com 

using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Set, "FailoverNew")]
    [OutputType(typeof(VirtualMachine))]

    public class SetFailoverNew : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string VMName { get; set; }

        //VMUid will pass the VM uid
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("vm")]
        public string VMId { get; set; }

        //ResourcePoolName will pass the ResourcePoolName
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("RPName")]
        public string ResourcePoolName { get; set; }

        //ResourcePoolID
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("RPID")]
        public string ResourcePoolID { get; set; }

        //FolderName 
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("FName")]
        public string FolderName { get; set; }

        //FolderID will pass the folder id
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("FID")]
        public string FolderID { get; set; }

        //NetworkMap defines the mapping
        [Parameter(Position = 3)]
        [ValidateNotNullOrEmpty]
        [Alias("testNW")]
        public string TestNetwork { get; set; }

        //VMUid will pass the VM uid
        [Parameter(Position = 4)]
        [ValidateNotNullOrEmpty]
        [Alias("NWMap")]
        public string NetworkMap { get; set; }

        //NewName will change the name of the existing vm to newname after failover
        [Parameter(Position = 5)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

        //PowerOn will pass if the VM has to be powered on after failover
        [Parameter(Position = 6)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter PowerOn { get; set; }

        //Async will return the task id for asynchronous call to cmdlet-switch parameter
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Async { get; set; }

        //Server will pass the Server for which api call has to be made using the corresponding token
        [Parameter(Mandatory = true)]
        [Alias("srv")]
        public string Server { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            //Server authentication to be done
            //ValidateServerSessions();
            if (ValidateParameters() == false)
            
            try
            {
                AccessToken accToken = new AccessToken();
                // to determine the access Token
                string accessTkn = accToken.GetAccessToken(Server.ToString());
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }

                if (Server != null)
                {
                    Server = Server.Trim();
                }
                else if (ConnectHXServer.storageKeyDictionary != null)
                {
                    var firstElement = ConnectHXServer.storageKeyDictionary.FirstOrDefault();
                    Server = firstElement.Key;
                }
                
                var apiInstance = new RecoverApi(Server);

                // to determine the various parameters of payload sent to api call
                string vResourcePoolName = string.Empty;
                string vResourcePoolID = string.Empty;
                if (ResourcePoolName != null) { vResourcePoolName = ResourcePoolName; }
                if (ResourcePoolID != null) { vResourcePoolID = ResourcePoolID; }
                var objResPoolJson = new EntityDetail
                {
                    name = vResourcePoolName,
                    type = erType.DP_VM,
                    id = vResourcePoolID,
                    idtype = erIDType.VCMOID,
                    confignum = "0"
                };
                string vFolderName = string.Empty;
                string vFolderID = string.Empty;
                if (FolderName != null) { vFolderName = FolderName; }
                if (FolderID != null) { vFolderID = FolderID; }

                var objFolderJson = new EntityDetail
                {
                    name = vFolderName,
                    type = erType.DP_VM,
                    id = vFolderID,
                    idtype = erIDType.VCMOID,
                    confignum = "0"
                };
                string objTestNetwork = string.Empty;
                if (TestNetwork != null) { objTestNetwork = TestNetwork.ToString(); }
                bool vPowerOn = false;
                if (PowerOn == true)
                {
                    vPowerOn = true;
                }

                string newName = string.Empty;
                if (NewName != null) { newName = NewName.ToString(); }
                var jsonPool = JsonConvert.SerializeObject(objResPoolJson);
                var jsonFolder = JsonConvert.SerializeObject(objFolderJson);
                EntityRef objEF_pool = JsonConvert.DeserializeObject<EntityRef>(jsonPool);
                EntityRef objEF_folder = JsonConvert.DeserializeObject<EntityRef>(jsonFolder);
                RecoverVmOptions body = new RecoverVmOptions(objEF_pool, objEF_folder, TestNetwork, vPowerOn, null, newName);
                //check if either resource pool name or id is provided then pass it to body
                if (ResourcePoolName != null || ResourcePoolName != null)
                {
                    objEF_pool = JsonConvert.DeserializeObject<EntityRef>(jsonPool);
                    body.ResourcePoolEr = objEF_pool;
                }
                else
                {
                    objEF_pool = null;
                    body.ResourcePoolEr = null;
                }
                //check if either Folder name or id is provided then pass it to body
                if (FolderName != null || FolderID != null)
                {
                    objEF_folder = JsonConvert.DeserializeObject<EntityRef>(jsonFolder);
                    body.FolderEr = objEF_folder;
                }
                else
                {
                    objEF_pool = null;
                    body.FolderEr = null;
                }
                body.NetworkMap = null;
                body.NewName = null;
                body.PowerOn = false;
                if (NewName != null)
                {
                    body.NewName = NewName.ToString();
                }
                if (PowerOn == true)
                {
                    body.PowerOn = true;
                }

                string respVMFailover = string.Empty;
                if (VMId != null)
                {
                    // to determine if async task then return the task after failover
                    if (Async == true)
                    {
                        // to determine if async task then return the task after failover
                        respVMFailover = apiInstance.OpDpVmFailoverPut(VMId, accessTkn, body, "en-US");
                        WriteVerbose("The Vm has been failed over");
                        WriteObject(respVMFailover, true);
                        return;
                    }
                    else
                    {
                        // to determine if not async task then loop till task is completed.
                        DateTime now = DateTime.Now;
                        respVMFailover = apiInstance.OpDpVmFailoverPut(VMId, accessTkn, body, "en-US");//OpDpVmFailoverPut
                        JObject joResponse = JObject.Parse(respVMFailover);
                        JValue ojObject = (JValue)joResponse["taskId"];
                        WriteVerbose("The Vm has been failed over");
                        List<IO.Swagger.Model.Job> respVMTaskGet = apiInstance.
                                OpDpVmTasksGet(accessTkn, VMId, ojObject.ToString());
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
                            // to check the task and break if status is "COMPLETED" or "EXCEPTION"
                            List<IO.Swagger.Model.Job> checkVMTask = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());
                            if (checkVMTask[0].State.ToString() == "COMPLETED")
                            {
                                respVMTaskGet = checkVMTask;
                                break;
                            }
                            else if (checkVMTask[0].State.ToString() == "EXCEPTION")
                            {
                                respVMTaskGet = checkVMTask;
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

                WriteObject("Exception when calling apiInstance.OpDpVmRecoveryFailoverPut: " + e.Message);
            }

        }


        /// <summary>
        /// Get value of time after one minute
        /// </summary>
        /// <returns>DateTime</returns>
        private DateTime GetOneMinutesFromNow()
        {
            DateTime otherDate = DateTime.Now.AddMinutes(2);
            return otherDate;
        }


        /// <summary>
        /// Validates the Parameters entered are correct
        /// </summary>
        /// <returns>bool</returns>
        protected internal override bool ValidateParameters()
        {
            // Leave this here so that we can add more checks if needed
            // and return all errors if there are multiple without returning
            // on the first one we find.
            return true;
        }
    }

}
