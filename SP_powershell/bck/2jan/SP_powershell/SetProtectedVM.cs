using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using IO.Swagger.Model;
using IO.Swagger.Client;
using IO.Swagger.Api;
using System.Diagnostics;

namespace SP_powershell
{
    class SetProtectedVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter()]
        [Alias("ID")]
        [ValidateNotNullOrEmpty]
        public string ID { get; set; }

        [Parameter()]
        [Alias("Name")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter()]
        [Alias("State")]
        [ValidateNotNullOrEmpty]
        public string State { get; set; }

        [Parameter()]
        [Alias("prt")]
        public SwitchParameter Protect { get; set; }
        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            //Server authentication to be done
            //ValidateServerSessions();
            if (ValidateParameters() == false)
                return;

            VirtualMachineEx VMex = new VirtualMachineEx();
            //fetches the list of vms as per the filter criteria set
            // var ListOfVms = VMex.GetVMs();
            // var ListOfVms = VMex.GetVMs();
            Configuration.Default.AddApiKey("api_key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("api_key", "Bearer");

            var apiInstance = new ProtectApi();
            // var petId = 789;  // long? | ID of pet to return

            try
            {
                // Find pet by ID
                List<ProtectedVMInfo> result = apiInstance.OpDpVmGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling apiInstance.OpDpVmGet: " + e.Message);
            }
            //////if (ListOfVms != null)
            //////{
            //////    //display the list of vms 
            //////    WriteObject(ListOfVms);
            //////}

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