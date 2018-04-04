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

namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Set, "TestFailover26")]
    [OutputType(typeof(VirtualMachine))]

    public class SetTestFailover26 : SPCmdlet
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

        //[Parameter(ParameterSetName = "networkMap")]
        [Parameter(Position = 4)]
        [ValidateNotNullOrEmpty]
        [Alias("NWMap")]
        public string NetworkMap { get; set; }

        //[Parameter(ParameterSetName = "newName")]
        [Parameter(Position = 5)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

        //[Parameter(ParameterSetName = "PowerOn")]
        [Parameter( Position = 6)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter PowerOn { get; set; }

        //[Parameter(ParameterSetName = "Async")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Async { get; set; }

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
            
            

            
            try
            {
                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
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
                var apiInstance = new RecoveryApi(apiString);


                var num = 0;
                string accessTkn = GetAccessToken(Server.ToString());
                

                dynamic dictServerCnnctd = null;
                if (ConnectHXServer.storageKeyDictionary != null)
                {
                    num = ConnectHXServer.storageKeyDictionary.Count();
                    if (num == 1)
                    {

                        dictServerCnnctd = ConnectHXServer.storageKeyDictionary.First(x => x.Key == Server.ToString()).Value;

                    }
                    else
                    {
                        dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                    }
                }
                else
                {
                    num = 0;
                    throw new Exception("Please connect to a server.");
                }

                if (dictServerCnnctd != null)
                {
                    accessTkn = dictServerCnnctd.TokenType + " " + dictServerCnnctd.AccessToken;
                }
                else
                {
                    throw new Exception("The Server is not connected;Please check the IP address of Server");
                }
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
                if (TestNetwork != null) { objTestNetwork = TestNetwork.ToString(); }
                bool vPowerOn = false;
                if (PowerOn == true)
                {
                    vPowerOn = true;
                }
                // List<NetworkMapping> dictNetworkMap=null;
                string newName = null;
                if (NewName != null) { newName = NewName.ToString(); }
               
 

                var jsonPool = JsonConvert.SerializeObject(objResPoolJson);
                var jsonFolder = JsonConvert.SerializeObject(objFolderJson);
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

                string result1;
                if (VMId != null)
                {
                    if (Async != true)
                    {

                        
                        result1 = apiInstance.OpDpVmRecoveryTestPut(VMId.ToString(), body, accessTkn.ToString());

                        WriteVerbose("Test Failover of VM done");
                        WriteObject(result1, true);
                        return;
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                       // WriteVerbose("The Vm has been halted");
                        result1 = apiInstance.OpDpVmRecoveryTestPut(VMId.ToString(), body, accessTkn.ToString());
                        var taskID = JsonConvert.SerializeObject(result1);
                        
                        WriteVerbose("Test Failover of VM done");
                        List<IO.Swagger.Model.Job> result2 = apiInstance.OpDpVmRecoveryJobsJobIdGet(result1.ToString(), accessTkn.ToString(), "en-US");//43d80de4-f438-4ff0-a3de-b89a62a3ac1f
                        //var result2 = apiInstance.OpDpVmRecoveryJobsJobIdGet("43d80de4-f438-4ff0-a3de-b89a62a3ac1f", accessTkn.ToString(), "en-US");
                        DateTime oneMinutesFromNow = GetOneMinutesFromNow();

                        //////////{
                        //////////    //Continue on
                        //////////}
                        //////////IO.Swagger.Client.ApiResponse<string> response = null;
                        if (result2[0].State.ToString() == "EXCEPTION")
                        {
                            WriteVerbose("Exception in Test Failover of VM");
                            WriteObject(result2, true);

                        }
                        while (result2 != null && now < oneMinutesFromNow)
                        {
                            List<IO.Swagger.Model.Job> check1 = apiInstance.OpDpVmRecoveryJobsJobIdGet(result1.ToString(), accessTkn.ToString(), "en-US");
                            // ApiResponse check = api.GetResponse(RequestID);
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


                private string GetAccessToken(string Server)
        {
            var num = 0;
            String accessTkn = "";
            dynamic dictServerCnnctd = null;
            if (ConnectHXServer.storageKeyDictionary != null)
            {
                num = ConnectHXServer.storageKeyDictionary.Count();
                if (num == 1)
                {
                dictServerCnnctd = ConnectHXServer.storageKeyDictionary.First(x => x.Key == Server.ToString()).Value;
                }
                else
                {
                dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                }
            }
            else
            {
                num = 0;
                throw new Exception("Please connect to a server.");
            }
            if (dictServerCnnctd != null)
            {
                accessTkn = dictServerCnnctd.TokenType + " " + dictServerCnnctd.AccessToken;
            }
            else
            {
                throw new Exception("The Server is not connected;Please check the IP address of Server");
            }
            return accessTkn;
        }
    }
   
}
