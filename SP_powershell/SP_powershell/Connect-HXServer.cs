﻿using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace SP_powershell
{
    [Cmdlet(VerbsCommunications.Connect, "HXServer")]
    [OutputType(typeof(VirtualMachine))]
    //[Category(CmdletCategory.Session)]
    [CmdletBinding]
    public class ConnectHXServer : SPCmdlet
    {
        SessionState se1 = new SessionState();




        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true,
            ParameterSetName = "PSCredentialObject")]
        [ValidateNotNull]
        [Alias("cred")]
        public PSCredential Credential { get; set; }

        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Uname")]
        public string Username { get; set; }

        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("Pswrd")]
        public string Password { get; set; }

        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("Srvr")]
        public string Server { get; set; }



        //
        // Cmdlet body
        //
        
    protected override void ProcessSPRecord()
        {
            // Configure OAuth2 access token for authorization: petstore_auth
            //Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
            Configuration.Default = new Configuration();
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
            Configuration.Default.Username = "root";
            Configuration.Default.Password = "Cisco123";
            //$PsCmdlet.SessionState.PSVariable.Set("_Server", Server);
            var apiInstance = new AboutApi("https://10.198.2.214/dataprotection/v1");
            // var apiInstance = new ProtectApi("https://10.198.2.214/dataprotection/v1");
            ////////var petId = 789;  // long? | Pet id to delete
            ////////                  //  var apiKey = apiKey_example;  // string |  (optional) 

            ////////Func<IEnumerable<VirtualMachine>, VirtualMachine> selectByDisplayName = (VMList) =>
            ////////  VMList.FirstOrDefault(hvds => String.Equals(hvds.Name, DisplayName,
            ////////        StringComparison.OrdinalIgnoreCase));

            try
            {
                //
                // Find a specific Protected VM
                //

                ////if (ParameterSetName == DisplayName)
                ////{
                ////    var VmSpecific = DisplayName.ToString();
                ////    //GetSpecificHypervisorDatastore();
                ////    return;
                ////}

                // Find All Protected VMs
                AboutInfo result = apiInstance.OpAboutGet();
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
