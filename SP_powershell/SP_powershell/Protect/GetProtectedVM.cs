// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace Cisco.Runbook
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

        [Parameter()]
        [ValidateNotNull]
        [Alias("srvr")]
        public string Server { get; set; }
        
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("vmid")]
        public string VMUid { get; set; }

        //VMname will pass the VM name
        [Parameter()]
        [SupportsWildcards()]
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
            AccessToken accToken = new AccessToken();
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
                var apiInstance = new ProtectApi(Server.Trim());
                
                //
                // Find a specific Protected VM
                //
                if (VMUid != null)
                {
                    var VmSpecific = VMUid.ToString();
                    //GetSpecific VM;
                    ProtectedVMInfo respVMGet = apiInstance.OpDpVmVmidGet1(VmSpecific, accessTkn, "en-US");
                    WriteObject(respVMGet, true);
                    return;
                }
                var respVMListGet = apiInstance.OpDpVmGet(null, accessTkn, "en-US");
                List<ProtectedVMInfo> respVMList1 = apiInstance.OpDpVmGet(null, accessTkn, "en-US");
                SessionState valc = new SessionState();
                List<VMHXobject> respVMList2 = GetVirtualMachineResources(respVMListGet);
                List<containerHXobject> respVMList3 = GetVirtualMachineDetail(respVMListGet);
                //find the vm details matching to the vmName provided as parameter
                if (VMname != null)
                {
                    Debug.Assert(!string.IsNullOrWhiteSpace(VMname));
                    var cleanedHostName =   Regex.Escape(VMname).
                                          Replace("\\*", "").
                                          Replace("\\?", "") ;

                    var namePatternMatchFound = respVMList1
                               .Where(ts => ts.Er.Name.IndexOf(
                                   cleanedHostName, StringComparison.OrdinalIgnoreCase) >= 0)
                               .ToList();
                    var vmMatch = respVMList1.FirstOrDefault(vm => vm.Er.Name == VMname.ToString());
                    WriteObject(namePatternMatchFound, true);
                    return;
                }
                //find the vm details matching to the state provided as parameter-"ACTIVE", "HALTED", "RECOVERED", "FAILED", "IN_PROGRESS"
                if (State != null)
                {
                    List<ProtectedVMInfo> vmStateMatch = respVMList1.FindAll(vm => vm.ProtectionStatus.ToString() == State.ToString());
                    WriteObject(vmStateMatch, true);
                    return;
                }
                // WriteContainerecord(myresult3);
                WriteObject(respVMList1, true);
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
                //WriteWarning(psErrRecord.Exception.Message);
                //WriteErrorRecord(e,"Exception when calling apiInstance.OpDpVmGet: ", ErrorCategory.ConnectionError, e.Message);
            }

        }


        private void WriteContainerecord(List<containerHXobject> myresult3)
        {
            foreach (var resultset in myresult3)
            {
                if (resultset.VMHXobject != null)
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
                if (resultset.Er != null)
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
                    if (virtualMachineHostResource.ClusterEr == null)
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
