using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Set, "ProtectedVM")]
    [OutputType(typeof(VirtualMachine))]

    public class SetProtectedVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string VMName { get; set; }

        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }
        //commented out as DP_VM is default being used-reference stcli
        ////[Parameter(ParameterSetName = "HXName")]
        ////[ValidateNotNullOrEmpty]
        ////[ValidateSet("DISK", "PNODE", "NODE", "CLUSTER", "DATASTORE", "VIRTNODE", "VIRTCLUSTER", "VIRTDATASTORE", "VIRTMACHINE", "PDISK", "PDATASTORE", "VIRTMACHINESNAPSHOT", "FOLDER", "RESOURCEPOOL", "FILE", "VIRTDATACENTER", "REPLICATION_APPLIANCE", "REPLICATION_JOB", "IP_POOL", "DP_VM_SNAPSHOT", "DP_VMGROUP_SNAPSHOT", "DP_VM", "DP_VMGROUP", "DP_VM_CONFIG", "DP_VM_SNAPSHOT_POINT", "CLUSTER_PAIR")]
        ////[Alias("vmtyp")]
        ////public string VMType { get; set; }
        
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [ValidateSet("VCMOID", "VMBIOSUUID", "VMDSPATH")]
        [Alias("vmidty")]
        public string VMidtype { get; set; }
        
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
            
            Configuration.Default = new Configuration();
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
            var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
            var apiInstance = new ProtectApi(apiString);

            //Func<IEnumerable<VirtualMachine>, VirtualMachine> selectByDisplayName = (VMList) =>
            //  VMList.FirstOrDefault(hvds => String.Equals(hvds.Name, DisplayName,
            //        StringComparison.OrdinalIgnoreCase));

            try
            {
                var jName = VMName.ToString().Trim();
                var jIdtype = VMidtype.ToString();
                var objVmJson = new VMDet
                {
                    name = jName,
                    type = "DP_VM",
                    id = VMId.ToString(),
                    idtype = jIdtype,
                    confignum="0"
                };


                ////string json1 = @"{
                ////    ""name"": ""vm_src12"",
                ////    ""type"": ""DP_VM"",
                ////    ""id"": ""vm-53"",
                ////    ""idtype"": ""VCMOID"",
                ////    ""confignum"": 0
                ////    }";


                var json = JsonConvert.SerializeObject(objVmJson);


                EntityRef objEF = JsonConvert.DeserializeObject<EntityRef>(json);
                //objEF.Name = "vm_src11";
                //objEF.Idtype = EntityRef.IdtypeEnum.VCMOID;
                //objEF.Type = EntityRef.TypeEnum.DPVM;
                //objEF.Id = "421875b8-f66b-2e88-41e3-997abc2c4f38";
                //objEF.Confignum = null;
                ProtectedVMSpec body = new ProtectedVMSpec(objEF);
                                 
                ProtectedVMInfo result1 = apiInstance.OpDpVmPost(body);
                WriteObject(result1, true);
                return;
                
            }
            catch (Exception e)
            {
                
                WriteObject("Exception when calling apiInstance.OpDpVmGet: " + e.Message);
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
    public class VMDet
    {
        public string name;
        public string type;
        public string id;
        public string idtype;
        public string confignum;
    }
}
