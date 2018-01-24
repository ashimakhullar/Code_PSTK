﻿using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;


namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "ProtectedVM")]
    [OutputType(typeof(VirtualMachine))]

    public class GetProtectedVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        List<string> listParamtersetName = new List<string>();
       

        //Server Parameter contains the Cisco HXConnect IP
        [Parameter(Mandatory =true)]
        [ValidateNotNull]
        [Alias("srvr")]
        public string Server { get; set; }
        
        //UserName Parameter contains the Cisco HXConnect UserName
        [Parameter(Position = 1, ParameterSetName = "UserNamePassword")]
        [ValidateNotNullOrEmpty]
        [Alias("user")]
        public string UserName { get; set; }

        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Position = 2, ParameterSetName = "UserNamePassword")]
        [ValidateNotNullOrEmpty]
        [Alias("pwd")]
        public string Password { get; set; }

        //VMUid will pass the VM uid
        //[Parameter(ParameterSetName = "HXUid")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("vmid")]
        public string VMUid { get; set; }

        //VMname will pass the VM name
        //[Parameter(ParameterSetName = "HXVMname")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string VMname { get; set; }
        //VMState will pass the VM name
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [ValidateSet("ACTIVE", "HALTED", "RECOVERED", "FAILED", "IN_PROGRESS")]
        [Alias("VMState")]
        public string State { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            if (ValidateParameters() == false)
                return;
            // Configure access token for authorization
            
           
            try
            {
                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                
                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";

                var apiInstance = new ProtectApi(apiString);

                //
                // Find a specific Protected VM
                //

                if (VMUid != null)
                {
                    var VmSpecific = VMUid.ToString();
                    //GetSpecific VM;
                    ProtectedVMInfo result1 = apiInstance.OpDpVmVmidGet(VmSpecific);
                    WriteObject(result1, true);
                    return;
                }


                // Find All Protected VMs
                ////////////VMHXobject objVmJson = new VMHXobject();

                var num=ConnectHXServer.storageKeyDictionary.Count();
                dynamic dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                String accessTkn=null;
                if (dictServerCnnctd!=null)
                {
                    accessTkn = dictServerCnnctd.TokenType + " " + dictServerCnnctd.AccessToken;
                }
                else
                {
                    throw new Exception("The Server is not connected;Please check the IP address of Server");
                }


                var output = apiInstance.OpDpVmGet(null, accessTkn.ToString(), "en-US");
               
                List<ProtectedVMInfo> result = apiInstance.OpDpVmGet(null, accessTkn.ToString(), "en-US");
                SessionState valc = new SessionState();
                List<VMHXobject> myresult2 = GetVirtualMachineResources(output);
                List<containerHXobject> myresult3 = GetVirtualMachineDetail(output);
                //find the vm details matching to the vmName provided as parameter
                if (VMname!=null)
                { 
                    var vmMatch= result.FirstOrDefault(vm => vm.Er.Name == VMname.ToString());
                    WriteObject(vmMatch, true);
                    return;
                }
                //find the vm details matching to the state provided as parameter-"ACTIVE", "HALTED", "RECOVERED", "FAILED", "IN_PROGRESS"
                if (State!=null)
                {
                    List<ProtectedVMInfo> vmStateMatch = result.FindAll(vm => vm.ProtectionStatus.ToString() == State.ToString());
                    WriteObject(vmStateMatch, true);
                    return;
                }
               // WriteContainerecord(myresult3);
                WriteObject(result, true);
               // WriteVMrecord(result);
            }
            catch (ApiException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Correct Credentials not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch (ArgumentException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Arguments not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch (Exception e)
            {
               
                ErrorRecord psErrRecord = new ErrorRecord(
                          e,
                          "Exception when calling apiInstance.OpDpVmGet: ",
                          ErrorCategory.NotSpecified,
                          e.Message);
                WriteError(psErrRecord);
                WriteWarning(psErrRecord.Exception.Message);
                //WriteErrorRecord(e,"Exception when calling apiInstance.OpDpVmGet: ", ErrorCategory.ConnectionError, e.Message);
            }
            
        }

        private void WriteContainerecord(List<containerHXobject> myresult3)
        {
            foreach (var resultset in myresult3)
            {   if (resultset.VMHXobject != null)
                { 
                    WriteObject(resultset.VMHXobject);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.ClusterHXobject.clusterEr != null)
                {
                    WriteObject(resultset.ClusterHXobject.clusterEr);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.VmInfoHXobject.vmCurrentProtectionInfo != null)
                {
                    WriteObject(resultset.VmInfoHXobject.vmCurrentProtectionInfo.VmInfo);
                }
                WriteObject("=====================================================================");
            }
        }
        /// <summary>
        /// Write VM record.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when null values </exception>
        /// <param name="body">Protect VM Spec</param>
        /// <returns></returns>
        private void WriteVMrecord(List<ProtectedVMInfo> myresult)
        {
            foreach (var resultset in myresult)
            {
                if(resultset.Er!=null)
                {
                    WriteObject(resultset.Er.ToString());
                    WriteObject(resultset.Er);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.ClusterEr != null)
                {
                    WriteObject(resultset.ClusterEr);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.ProtectionInfo != null)
                {
                    WriteObject(resultset.ProtectionInfo.VmCurrentProtectionInfo.VmInfo);
                }
                WriteObject("=====================================================================");
            }
        }

        private List<containerHXobject> GetVirtualMachineDetail(List<ProtectedVMInfo> output)
        {
            
                List<containerHXobject> list = new List<containerHXobject>();
            try
            {
                foreach (var virtualMachineHostResource in output)
                {
                    containerHXobject obj = new containerHXobject();
                    obj.VMHXobject = new VMHXobject(virtualMachineHostResource.Er.Name, virtualMachineHostResource.Er.Id, virtualMachineHostResource.Er.Type);
                    if (virtualMachineHostResource.ClusterEr==null)
                    {
                        obj.ClusterHXobject = new ClusterHXobject();
                    }
                    else
                    { 
                        obj.ClusterHXobject = new ClusterHXobject(virtualMachineHostResource.ClusterEr);
                    }
                    if (virtualMachineHostResource.ProtectionInfo == null)
                    {
                        obj.VmInfoHXobject = new VmInfoHXobject();
                    } 
                    else
                    { 
                        obj.VmInfoHXobject = new VmInfoHXobject(virtualMachineHostResource.ProtectionInfo.VmCurrentProtectionInfo);
                    }
                    //list.Add(new VMHXobject(virtualMachineHostResource.Er.Name, virtualMachineHostResource.Er.Id, virtualMachineHostResource.Er.Type)); // Read variables from item...
                    list.Add(obj);

                                                                                                                                                       ////    virtualMachineHostResource.ApplianceUuid = tintriServer.Uuid;
                }
                return list;

            }
            catch (Exception e)
            {

                ErrorRecord psErrRecord = new ErrorRecord(
                          e,
                          "##Exception when calling apiInstance.OpDpVmGet: ",
                          ErrorCategory.NotSpecified,
                          e.Message);
                WriteError(psErrRecord);
                return list;
            }
        }

        private List<VMHXobject> GetVirtualMachineResources(List<ProtectedVMInfo> output)
        {

            //var ts = serverMap[tintriServer];
            //var hostResources = vminfo;
            List<VMHXobject> list = new List<VMHXobject>(); 
            // var hostResources = ts.Datastore.GetVMHostResources().Result.ToList();

            //if (!string.IsNullOrEmpty(tintriServer.ApplianceHostName) &&
            //    !string.IsNullOrEmpty(tintriServer.Uuid))
            //{
            foreach (var virtualMachineHostResource in output)
            {
                list.Add(new VMHXobject(virtualMachineHostResource.Er.Name, virtualMachineHostResource.Er.Id, virtualMachineHostResource.Er.Type)); // Read variables from item...
                                                                                                                                                    ////virtualMachineHostResource.ApplianceHostName = tintriServer.ApplianceHostName;
                                                                                                                                                    ////    virtualMachineHostResource.ApplianceUuid = tintriServer.Uuid;
            }
            //}

            return list;
        }

        private void WriteErrorRecord(Exception e, string v, ErrorCategory connectionError, string message)
        {
            throw new NotImplementedException();
        }

        protected internal override bool ValidateParameters()
        {


            // Leave this here so that we can add more checks if needed
            // and return all errors if there are multiple without returning
            // on the first one we find.
            return true;
        }
    }
    public class VMHXobject
    {
        public string name;
        public EntityRef.TypeEnum? type;
        public string id;

        public VMHXobject()
        {
        }

        public VMHXobject(string name, string id, EntityRef.TypeEnum? type1)
        {
            this.name = name;
            this.id = id;
            this.type = type1;
        }
    }

    public class ClusterHXobject
    {
       
        public EntityRef clusterEr;

        public ClusterHXobject()
        {
        }

        public ClusterHXobject(EntityRef clusterEr)
        {
            this.clusterEr = clusterEr;
        }
    }


    public class VmInfoHXobject
    {

        public SnapshotInfo vmCurrentProtectionInfo;

        public VmInfoHXobject()
        {
        }
        
        public VmInfoHXobject(SnapshotInfo vmCurrentProtectionInfo)
        {
            this.vmCurrentProtectionInfo = vmCurrentProtectionInfo;
        }
    }

    public class containerHXobject
    {
       
        public VMHXobject VMHXobject { get; set; }
        public ClusterHXobject ClusterHXobject { get; set; }
        public VmInfoHXobject VmInfoHXobject { get; set; }


        public containerHXobject()
        {
        }


    }

    
}
