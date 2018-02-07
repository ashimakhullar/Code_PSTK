// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Vestris.VMWareLib;
using System.Management.Automation.Runspaces;
using VMware.VimAutomation.ViCore.Types.V1;
using VMware.Vim;
using VMware.VimAutomation.ViCore.Types;
using VMware.VimAutomation.ViCore.Types.V1.Inventory;
using System.Collections.Specialized;
using VMware.VimAutomation.ViCore.Types.V1.Cluster;
using VMware.VimAutomation.ViCore.Types.V1.VM;
using VMware.VimAutomation.ViCore.Types.V1.VM.Guest;
using VMware.VimAutomation.Sdk.Types.V1;
using VMware.VimAutomation.ViCore;
using VMware.VimAutomation.ViCore.Types.V1.DatastoreManagement;
using VMware.VimAutomation.ViCore.Cmdlets;
using VMware.VimAutomation.ViCore.Cmdlets.Commands;
using System.Collections;

namespace SP_powershell
{
    public class Demo

    {

        public IEnumerable GetVMs()
        {
            ConnectVIServer ConnectServer = new ConnectVIServer();
            ConnectServer.User = "administrator@vsphere.local";
            ConnectServer.Password = "Cisco123";

            ConnectServer.Protocol = "https";

            ConnectServer.Server = new string[] { "10.198.2.215/sdk" };

            ConnectServer.Invoke();
            GetVM getVM = new GetVM();
            getVM.Name = new string[] { "vm_src11" };
            var vm1 = getVM.Invoke();
            // Call the PowerShell.Create() method to create an 
            // empty pipeline.
            PowerShell ps = PowerShell.Create();

            // Call the PowerShell.AddCommand(string) method to add 
            // the Get-Process cmdlet to the pipeline. Do 
            // not include spaces before or after the cmdlet name 
            // because that will cause the command to fail.
            //ps.AddCommand("Get-Module -Name VMware.VimAutomation.Core");
            //ps.AddCommand("Import-Module -Name VMware.VimAutomation.Core");
            ps.AddCommand("Get-Process");

            Console.WriteLine("Process                 Id");
            Console.WriteLine("----------------------------");

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (PSObject result in ps.Invoke())
            {
                Console.WriteLine(
                        "{0,-24}{1}",
                        result.Members["ProcessName"].Value,
                        result.Members["Id"].Value);
            }
            Console.WriteLine(vm1);
            return vm1;
           
        }

    }
    [Cmdlet(VerbsCommon.Get, "SPVM")]
    [OutputType(typeof(VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine))]

    public class GetSPVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined belowvim
        //
        List<string> listParamtersetName = new List<string>();
       

        //Server Parameter contains the Cisco HXConnect IP
  
        [Parameter()] 
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
            try
            {

                VimClient vimClient = new VimClientImpl();

                List<EntityViewBase> vmlist = new List<EntityViewBase>();
                vimClient.Connect("https://10.198.2.215/sdk");
                vimClient.Login("administrator@vsphere.local", "Cisco123");
                vm_123 vmobj = new vm_123();



               // vmlist = vimClient.FindEntityViews(typeof(VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine), null, null, null);
                vmlist = vimClient.FindEntityViews(typeof(VMware.Vim.VirtualMachine), null, null, null);
                //VMware.VimAutomation.ViCore.Types.V1;
                //List<VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine> vmnew = new List<VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine>();
                //vmobj = vmlist[1];
                //VMware.Vim.VirtualMachine obj2 = new VMware.Vim.VirtualMachine();
                foreach (VMware.VimAutomation.Types.VirtualMachine vm in vmlist)
                {
                    WriteObject(vm, true);
                }

                Demo obj = new Demo();
                var outVar=obj.GetVMs();

            if (ValidateParameters() == false)
                return;
            // Configure access token for authorization
            
           
           
                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }
                //var valServer = "";
                if (Server!=null )
                {
                    Server = Server.ToString().Trim();
                }
                else if (ConnectHXServer.storageKeyDictionary != null)
                {
                    var firstElement = ConnectHXServer.storageKeyDictionary.FirstOrDefault();
                    Server = firstElement.Key;
                }

                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";

                var apiInstance = new ProtectApi(apiString);

              
                

                // Find All Protected VMs
                ////////////VMHXobject objVmJson = new VMHXobject();
                var num=0;
                //if (ConnectHXServer.storageKeyDictionary == null)
                //{
                //    num = 0;

                //}
                //else
                //{
                //    num = ConnectHXServer.storageKeyDictionary.Count();
                //check the server ip provided by user if it exists in dictionary containing the list of all servers connected
               // dynamic dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                String accessTkn = "";

                dynamic dictServerCnnctd = null;
                if (ConnectHXServer.storageKeyDictionary != null)
                {
                    num= ConnectHXServer.storageKeyDictionary.Count();
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

                //
                // Find a specific Protected VM
                //


                if (VMUid != null)
                {
                    var VmSpecific = VMUid.ToString();
                    //GetSpecific VM;
                    ProtectedVMInfo result1 = apiInstance.OpDpVmVmidGet1(VmSpecific, accessTkn,"en-US");
                    WriteObject(result1, true);
                    return;
                }
               // VMware.Vim.VirtualMachine vm = new VMware.Vim.VirtualMachine();
               
                var output = apiInstance.OpDpVmGet(null, accessTkn.ToString(), "en-US");
                //List<VMware.VimAutomation.ViCore.Types.V1.VM.Guest.VMGuest> result = apiInstance.OpDpVmGet(null, accessTkn.ToString(), "en-US");
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
                    //WriteObject(vmStateMatch, true);
                    WriteObject(result, true);
                    return;
                }
               // WriteContainerecord(myresult3);
                //WriteObject(result, true);

                WriteObject(outVar, true);
                // WriteVMrecord(result);
                //Execute_vCenter_Tasks("10.198.2.215", "administrator@vsphere.local", "Cisco123");
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

        private static void Execute_vCenter_Tasks(string vCenterIp, string vCenterUsername, string vCenterPassword)
        {
          
                var shell = PowerShell.Create();



                string PsCmd = "Get-Module -Name VMware.VimAutomation.Core; $vCenterServer = '" + vCenterIp + "';$vCenterAdmin = '" + vCenterUsername + "' ;$vCenterPassword = '" + vCenterPassword + "';" + System.Environment.NewLine;



                PsCmd = PsCmd + "$VIServer = Connect-VIServer -Server $vCenterServer -User $vCenterAdmin -Password $vCenterPassword;" + System.Environment.NewLine;

                shell.Commands.AddScript(PsCmd);
          
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
    public partial class vm_123: VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine
    {


        [JsonConstructorAttribute]
        public vm_123() { }

        public PowerState PowerState => throw new NotImplementedException();

        public string Notes => throw new NotImplementedException();

        public VMGuest Guest => throw new NotImplementedException();

        public int NumCpu => throw new NotImplementedException();

        public int CoresPerSocket => throw new NotImplementedException();

        public decimal MemoryMB => throw new NotImplementedException();

        public decimal MemoryGB => throw new NotImplementedException();

        public string VMHostId => throw new NotImplementedException();

        public VMHost VMHost => throw new NotImplementedException();

        public VApp VApp => throw new NotImplementedException();

        public string FolderId => throw new NotImplementedException();

        public VMware.VimAutomation.ViCore.Types.V1.Inventory.Folder Folder => throw new NotImplementedException();

        public string ResourcePoolId => throw new NotImplementedException();

        public VMware.VimAutomation.ViCore.Types.V1.Inventory.ResourcePool ResourcePool => throw new NotImplementedException();

        public HARestartPriority? HARestartPriority => throw new NotImplementedException();

        public HAIsolationResponse? HAIsolationResponse => throw new NotImplementedException();

        public DrsAutomationLevel? DrsAutomationLevel => throw new NotImplementedException();

        public VMSwapfilePolicy? VMSwapfilePolicy => throw new NotImplementedException();

        public VMResourceConfiguration VMResourceConfiguration => throw new NotImplementedException();

        public VMVersion Version => throw new NotImplementedException();

        public string PersistentId => throw new NotImplementedException();

        public string GuestId => throw new NotImplementedException();

        public decimal UsedSpaceGB => throw new NotImplementedException();

        public decimal ProvisionedSpaceGB => throw new NotImplementedException();

        public object ExtensionData => throw new NotImplementedException();

        public string[] DatastoreIdList => throw new NotImplementedException();

        PowerState VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.PowerState => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.Notes => throw new NotImplementedException();

        VMGuest VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.Guest => throw new NotImplementedException();

        int VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.NumCpu => throw new NotImplementedException();

        int VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.CoresPerSocket => throw new NotImplementedException();

        decimal VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.MemoryMB => throw new NotImplementedException();

        decimal VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.MemoryGB => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.VMHostId => throw new NotImplementedException();

        VMHost VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.VMHost => throw new NotImplementedException();

        VApp VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.VApp => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.FolderId => throw new NotImplementedException();

        VMware.VimAutomation.ViCore.Types.V1.Inventory.Folder VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.Folder => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.ResourcePoolId => throw new NotImplementedException();

        VMware.VimAutomation.ViCore.Types.V1.Inventory.ResourcePool VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.ResourcePool => throw new NotImplementedException();

        HARestartPriority? VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.HARestartPriority => throw new NotImplementedException();

        HAIsolationResponse? VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.HAIsolationResponse => throw new NotImplementedException();

        DrsAutomationLevel? VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.DrsAutomationLevel => throw new NotImplementedException();

        VMSwapfilePolicy? VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.VMSwapfilePolicy => throw new NotImplementedException();

        VMResourceConfiguration VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.VMResourceConfiguration => throw new NotImplementedException();

        VMVersion VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.Version => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.PersistentId => throw new NotImplementedException();

        string VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.GuestId => throw new NotImplementedException();

        decimal VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.UsedSpaceGB => throw new NotImplementedException();

        decimal VMware.VimAutomation.ViCore.Types.V1.Inventory.VirtualMachine.ProvisionedSpaceGB => throw new NotImplementedException();

        string VIObject.Id => throw new NotImplementedException();

        string VIObject.Name => throw new NotImplementedException();

        string NamedObject.Name => throw new NotImplementedException();

        string VIObjectCore.Uid => throw new NotImplementedException();

        object ExtensionData.ExtensionData => throw new NotImplementedException();

        string[] DatastoreUser.DatastoreIdList => throw new NotImplementedException();

        public void LockUpdates()
        {
            throw new NotImplementedException();
        }

        public void UnlockUpdates()
        {
            throw new NotImplementedException();
        }

        void ExtensionData.LockUpdates()
        {
            throw new NotImplementedException();
        }

        void ExtensionData.UnlockUpdates()
        {
            throw new NotImplementedException();
        }
    }



}

   


    


