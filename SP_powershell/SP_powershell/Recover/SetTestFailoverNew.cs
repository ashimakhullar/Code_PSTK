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

namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Set, "TestFailoverNew")]
    [OutputType(typeof(VirtualMachine))]

    public class SetTestFailoverNew : SPCmdlet
    {
        

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        //[Parameter(ParameterSetName = "HXName")]
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }
        //[Parameter(ParameterSetName = "HXresourcePool")]
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("resourcePoolEr")]
        public string ResourcePool { get; set; }

        //[Parameter(ParameterSetName = "HXfolder")]
        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("folderEr")]
        public string Folder { get; set; }
        //[Parameter(ParameterSetName = "RPName")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("RPName")]
        public string ResourcePoolName { get; set; }
        //[Parameter(ParameterSetName = "RPID")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("RPID")]
        public string ResourcePoolID { get; set; }
        //[Parameter(ParameterSetName = "FolderName")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("FName")]
        public string FolderName { get; set; }
        //[Parameter(ParameterSetName = "RPID")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("FID")]
        public string FolderID { get; set; }
        //[Parameter(ParameterSetName = "testNetwork")]
        [Parameter(Position =3)]
        [ValidateNotNullOrEmpty]
        [Alias("testNW")]
        public string TestNetwork { get; set; }

        //Either TestNetwork or NetworkMap is provided
        //NetworkMap defines the mapping
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        public string[] NetworkMap
        {
            get { return nwMaps; }
            set { nwMaps = value; }
        }
        private string[] nwMaps;


        //NewName will change the name of the existing vm to newname after failover
        [Parameter(Position = 5)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

        //PowerOn will pass if the VM has to be powered on after Test failover
        [Parameter( Position = 6)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter PowerOn { get; set; }

        //Async will return the task id for asynchronous call to cmdlet-switch parameter
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Async { get; set; }
        //Server will pass the Server for which api call has to be made using the corresponding token
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

                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                var apiInstance = new RecoverApi(apiString);

                
                //check if either resource pool name or id is provided then pass it to body
                var vResourcePoolName = "";
                var vResourcePoolID = "";
                if (ResourcePoolName != null){ vResourcePoolName = ResourcePoolName.ToString(); }
                if (ResourcePoolID != null) { vResourcePoolID = vResourcePoolID.ToString(); }
                var objResPoolJson = new EntityDetail
                    {
                        name = vResourcePoolName,
                        type = erType.DP_VM,
                        id = ResourcePoolID,
                        idtype = erIDType.VCMOID,
                        confignum = "0"
                    };
                var vFolderName = "";
                var vFolderID = "";
                if (FolderName != null) { vFolderName = FolderName.ToString(); }
                if (FolderID != null) { vFolderID = FolderID.ToString(); }

                var objFolderJson = new EntityDetail
                {
                    name = vFolderName,
                    type = erType.DP_VM,
                    id = vFolderID,
                    idtype = erIDType.VCMOID,
                    confignum = "0"
                };
                
                string objTestNetwork = null;
                Dictionary<string, string> mapDictionary=null;
                if (NetworkMap != null)
                {
                    mapDictionary = this.NetworkMap
                                                    .Select(x => x.Split(':'))
                                                    .ToDictionary(x => x[0], x => x[1]);

                }
                if (TestNetwork != null) { objTestNetwork = TestNetwork.ToString() ; }
                
                bool vPowerOn = false;
                if (PowerOn == true)
                {
                    vPowerOn = true;
                }
               
                string newName = null;
                if (NewName != null) { newName = NewName.ToString(); }
               
                var jsonPool = JsonConvert.SerializeObject(objResPoolJson);
                var jsonFolder = JsonConvert.SerializeObject(objFolderJson);
                var jsonNW = JsonConvert.SerializeObject(mapDictionary);
                EntityRef objEF_pool = JsonConvert.DeserializeObject<EntityRef>(jsonPool);
                EntityRef objEF_folder = JsonConvert.DeserializeObject<EntityRef>(jsonFolder);
               
                RecoverVmOptions body = new RecoverVmOptions(objEF_pool,objEF_folder,TestNetwork,vPowerOn,null, newName);
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
                List<NetworkMapping> cont1 = new List<NetworkMapping>();
                if (mapDictionary != null)
                {
                    var obj_nw = JsonConvert.DeserializeObject(jsonNW);
                    foreach (KeyValuePair<string, string> entry in mapDictionary)
                    {
                        NetworkMapping varNW = new NetworkMapping(entry.Key,entry.Value);
                        cont1.Add(varNW);
                        // do something with entry.Value or entry.Key
                    }
                    body.NetworkMap = cont1;
                    
                }
                else
                {
                    body.NetworkMap = null;
                }
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

                string respVMTestFailover=string.Empty;
                string accessTkn = accToken.GetAccessToken(Server.ToString());
                if (VMId != null)
                {
                    if (Async == true)
                    {
                        respVMTestFailover = apiInstance.OpDpVmTestFailoverPut(VMId.ToString(),  accessTkn.ToString(), body,"en-US");
                        WriteVerbose("Test Failover of VM done");
                        WriteObject(respVMTestFailover, true);
                        return;
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        // WriteVerbose("The Vm has been halted");
                        respVMTestFailover = apiInstance.OpDpVmTestFailoverPut(VMId.ToString(), accessTkn.ToString(), body, "en-US");
                       
                        JObject joResponse = JObject.Parse(respVMTestFailover);
                        
                        JValue ojObject = (JValue)joResponse["taskId"];
                       
                        
                        WriteVerbose("Test Failover of VM done");
                        List<IO.Swagger.Model.Job> respVMTaskGet = apiInstance.OpDpVmTasksGet(accessTkn.ToString(), VMId.ToString(), ojObject.ToString());
                      
                        DateTime oneMinutesFromNow = GetOneMinutesFromNow();

                        if (respVMTaskGet[0].State.ToString() == "EXCEPTION")
                        {
                            WriteVerbose("Exception in Test Failover of VM");
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
                        if (respVMTaskGet[0].State.ToString() == "EXCEPTION")
                        {
                            WriteVerbose("Exception in Test Failover of VM");
                            WriteObject(respVMTaskGet, true);

                        }
                        if (respVMTaskGet[0].State.ToString() == "COMPLETED")
                        {
                            WriteVerbose("Test Failover of VM Completed");
                            WriteObject(respVMTaskGet, true);
                        }


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
        /// get one minute from the current time
        /// </summary>
        /// <returns></returns>
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
