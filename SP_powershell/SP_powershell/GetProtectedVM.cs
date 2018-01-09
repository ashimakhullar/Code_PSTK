﻿using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "ProtectedVM")]
    [OutputType(typeof(VirtualMachine))]

    public class GetProtectedVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string DisplayName { get; set; }

        //[Parameter(Mandatory = false,ParameterSetName = "HXUid")]
        //[ValidateNotNullOrEmpty]
        //[Alias("vmid")]
        //public string VMUUid { get; set; }

        
        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            // Configure OAuth2 access token for authorization: petstore_auth
            //Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
            Configuration.Default = new Configuration();
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
            var apiInstance = new ProtectApi("https://10.198.2.214/dataprotection/v1");
             // long? | Pet id to delete
                              //  var apiKey = apiKey_example;  // string |  (optional) 

            Func<IEnumerable<VirtualMachine>, VirtualMachine> selectByDisplayName = (VMList) =>
              VMList.FirstOrDefault(hvds => String.Equals(hvds.Name, DisplayName,
                    StringComparison.OrdinalIgnoreCase));

            try
            {
                //
                // Find a specific Protected VM
                //

                if (ParameterSetName == "HXName")
                {
                    var VmSpecific = DisplayName.ToString();
                    //    //GetSpecific VM;
                    ProtectedVMInfo result1 = apiInstance.OpDpVmVmidGet(VmSpecific);
                    WriteObject(result1, true);
                    return;
                }
                //if (ParameterSetName == "HXName")
                //{
                //    var VmSpecific = VMUUid.ToString();
                //    //    //GetSpecific VM;
                //    ProtectedVMInfo result1 = apiInstance.OpDpVmVmidGet(VmSpecific);
                //    WriteObject(result1, true);
                //    return;
                //}

                // Find All Protected VMs
                List<ProtectedVMInfo> result = apiInstance.OpDpVmGet();
                WriteObject(result,true);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling apiInstance.OpDpVmGet: " + e.Message);
            }
            //////try
            //////{
            //////    // Deletes a pet
            //////    //apiInstance.DeletePet(petId, apiKey);
            //////    apiInstance.OpDpVmPost(petId, null);
            //////}
            //////catch (Exception e)
            //////{
            //////    throw new NotImplementedException();
            //////   // WriteError(e.Message);
            //////   // Debug.Print("Exception when calling PetApi.DeletePet: " + e.Message);
            //////}

            /*
            //Server authentication to be done
            //ValidateServerSessions();
            if (ValidateParameters() == false)
                return;

            VirtualMachineEx VMex = new VirtualMachineEx();
            //fetches the list of vms as per the filter criteria set
            var ListOfVms = VMex.GetVMs();

            if (ListOfVms != null)
            {
                //display the list of vms 
                WriteObject(ListOfVms);
            }
            */
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
