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
using static IO.Swagger.Model.Job;

namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Set, "Failover")]
    [OutputType(typeof(VirtualMachine))]

    public class SetFailover : RecoveryCmdlet
    {
        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        //VMUid will pass the VM uid
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("VM")]
        public string VMId { get; set; }

        //ResourcePool object
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("resourcePoolEr")]
        public string ResourcePool { get; set; }

        //Folder object
        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("folderEr")]
        public string Folder { get; set; }

        //Resource Pool Name
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("RPName")]
        public string ResourcePoolName { get; set; }

        //ResourcePool Id
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

        //TestNetwork
        [Parameter(Position = 3)]
        [ValidateNotNullOrEmpty]
        [Alias("TestNW")]
        public string TestNetwork { get; set; }

        //Either TestNetwork or NetworkMap is provided
        //NetworkMap defines the mapping
        [Parameter(Position = 4)]
        [ValidateNotNullOrEmpty]
		[Alias("NWMap")]
        public string[] NetworkMap { get; set; }

        //NewName will change the name of the existing vm to newname after 
        //failover
        [Parameter(Position = 5)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

        //PowerOn will pass if the VM has to be powered on after Test failover
        [Parameter(Position = 6)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter PowerOn { get; set; }

        //Async will return the task id for asynchronous call to cmdlet-switch
        //parameter
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Async { get; set; }

        //Server will pass the Server for which api call has to be made using the
        //corresponding token
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
                //create body for the API Call
                RecoverVmOptions bodyPayload = CreatePayload();

                string respVMFailover = string.Empty;
                
                if (VMId != null)
                {
                    // to determine if async task then return the task after failover
                    if (Async == true)
                    {
                        respVMFailover = apiInstance.OpDpVmFailoverPut(VMId,
                                                        accessTkn, bodyPayload, "en-US");
                        WriteVerbose("Test Failover of VM done");
                        WriteObject(respVMFailover, true);
                        return;
                    }
                    else
                    {
                        //if switch parameter is false then keep pinging the api for X minutes
                        //exit if status ="COMPLETED", "EXCEPTION"
                        //"SUSPENDED", "SHUTTING_DOWN", "TERMINATED", "CANCELLED", "EXCEPTION","STALLED"
                        //For "NEW", "STARTING", "RUNNING" status after every 3 seconds ping the API 

                        DateTime now = DateTime.Now;
                        respVMFailover = apiInstance.OpDpVmFailoverPut(VMId,
                                                          accessTkn, bodyPayload, "en-US");
                        //check if not null
                        JObject joResponse = JObject.Parse(respVMFailover);
                        JValue ojObject = (JValue)joResponse["taskId"];

                        WriteVerbose("Test Failover of VM done");
                        List<IO.Swagger.Model.Job> respVMTaskGet = apiInstance.OpDpVmTasksGet(accessTkn,
                                                                        VMId.Trim(), ojObject.ToString());
                        //get time X minutes from now
                        DateTime timeMinutesFromNow = GetTimeMinutesFromNow();
                        //Loop while the repVMTaskGet is not null and X minutes from now is not over
                        while (respVMTaskGet != null && now < timeMinutesFromNow)
                        {
                            List<IO.Swagger.Model.Job> checkVMTaskGet = apiInstance.OpDpVmTasksGet(accessTkn,
                                                                                VMId.Trim(), ojObject.ToString());
                            //check if state is   SUSPENDED,SHUTTING_DOWN,TERMINATED,CANCELLED,COMPLETED,EXCEPTION,STALLED
                            if (new string[] {"SUSPENDED", "SHUTTING_DOWN", "TERMINATED", "CANCELLED",
                                                   "COMPLETED", "EXCEPTION","STALLED"}.Contains(checkVMTaskGet[0].State.ToString()))
                            {
                                respVMTaskGet = checkVMTaskGet;
                                break;
                            }
                            //check if state is         NEW,STARTING,RUNNING
                            else if (new string[] { "NEW", "STARTING", "RUNNING" }.Contains(checkVMTaskGet[0].State.ToString()))
                            {
                                {
                                    //Wait for 3 seconds
                                    System.Threading.Thread.Sleep(3000);
                                    now = DateTime.Now;
                                }
                            }
                         }
						 //call show status to display the status to the user
                         ShowStatus(respVMTaskGet);
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

                WriteObject("Exception when calling apiInstance.OpDpVmRecoveryTestPut: " + e.Message);
            }

        }

        /// <summary>
        /// Create PAyload for the API call
        /// </summary>
        /// <returns>RecoverVmOptions</returns>
        private RecoverVmOptions CreatePayload()
        {
            //check if either resource pool name or id is provided then pass it to body
            string vResourcePoolName = string.Empty;
            string vResourcePoolID = string.Empty;
            string vFolderName = string.Empty;
            string vFolderID = string.Empty;

            if (ResourcePoolName != null) { vResourcePoolName = ResourcePoolName.Trim(); }
            if (ResourcePoolID != null) { vResourcePoolID = ResourcePoolID.Trim(); }
            var objResPoolJson = new EntityDetail
            {
                name = vResourcePoolName,
                type = erType.DP_VM,
                id = ResourcePoolID,
                idtype = erIDType.VCMOID,
                confignum = "0"
            };

            if (FolderName != null) { vFolderName = FolderName.Trim(); }
            if (FolderID != null) { vFolderID = FolderID.Trim(); }

            var objFolderJson = new EntityDetail
            {
                name = vFolderName,
                type = erType.DP_VM,
                id = vFolderID,
                idtype = erIDType.VCMOID,
                confignum = "0"
            };

            string objTestNetwork = null;
            Dictionary<string, string> mapDictionary = null;
            if (NetworkMap != null)
            {
                mapDictionary = this.NetworkMap
                                                .Select(x => x.Split(':'))
                                                .ToDictionary(x => x[0], x => x[1]);
            }
            if (TestNetwork != null) { objTestNetwork = TestNetwork.Trim(); }

            bool vPowerOn = false;
            if (PowerOn == true)
            {
                vPowerOn = true;
            }

            string newName = null;
            if (NewName != null) { newName = NewName.Trim(); }

            var jsonPool = JsonConvert.SerializeObject(objResPoolJson);
            var jsonFolder = JsonConvert.SerializeObject(objFolderJson);
            var jsonNW = JsonConvert.SerializeObject(mapDictionary);
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
            List<NetworkMapping> nwMappings = new List<NetworkMapping>();
            if (mapDictionary != null)
            {
                var obj_nw = JsonConvert.DeserializeObject(jsonNW);
                foreach (KeyValuePair<string, string> entry in mapDictionary)
                {
                    NetworkMapping varNW = new NetworkMapping(entry.Key, entry.Value);
                    nwMappings.Add(varNW);
                    // do something with entry.Value or entry.Key
                }
                body.NetworkMap = nwMappings;
            }
            else
            {
                body.NetworkMap = null;
            }
            body.NewName = null;
            body.PowerOn = false;
            if (NewName != null)
            {
                body.NewName = NewName.Trim();
            }
            if (PowerOn == true)
            {
                body.PowerOn = true;
            }
            return body;
        }

        /// <summary>
        /// Show Status of the Task corresponding to the state 
        /// </summary>
        /// <param name="List<IO.Swagger.Model.Job>">An instance of IO.Swagger.Model.Job</param>
        /// <returns></returns>
        protected override void ShowStatus(List<IO.Swagger.Model.Job> respVMTaskGet)
        {
            //switch case to display 
            switch (respVMTaskGet[0].State)
            {
                case StateEnum.CANCELLED:
                    WriteObject("Test Failover of VM Cancelled");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.COMPLETED:
                    WriteObject("Test Failover of VM Completed");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.EXCEPTION:
                    WriteObject("Test Failover of VM faced an Exception");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.SHUTTINGDOWN:
                    WriteObject("Test Failover of VM Shutting Down.");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.STALLED:
                    WriteObject("Test Failover of VM Stalled.");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.SUSPENDED:
                    WriteObject("Test Failover of VM Suspended.");
                    WriteObject(respVMTaskGet, true);
                    break;
                case StateEnum.TERMINATED:
                    WriteObject("Test Failover of VM Terminated.");
                    WriteObject(respVMTaskGet, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Status not existing");
            }
        }

        /// <summary>
        /// gets two minute from the current time
        /// </summary>
        /// <returns></returns>
        private DateTime GetTimeMinutesFromNow()
        {
            //time after 2 minutes from the current time
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
