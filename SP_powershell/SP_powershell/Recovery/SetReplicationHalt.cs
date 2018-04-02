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
    [Cmdlet(VerbsCommon.Set, "ReplicationHalt")]
    [OutputType(typeof(VirtualMachine))]

    public class SetReplicationHalt : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        [Parameter(ParameterSetName = "HXName")]
        [ValidateNotNullOrEmpty]
        [Alias("name")]
        public string VMName { get; set; }

        //[Parameter(ParameterSetName = "HXName")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }
        //commented out as DP_VM is default being used-reference stcli
        ////[Parameter(ParameterSetName = "HXName")]
        ////[ValidateNotNullOrEmpty]
        ////[ValidateSet("DISK", "PNODE", "NODE", "CLUSTER", "DATASTORE", "VIRTNODE", "VIRTCLUSTER", "VIRTDATASTORE", "VIRTMACHINE", "PDISK", "PDATASTORE", "VIRTMACHINESNAPSHOT", "FOLDER", "RESOURCEPOOL", "FILE", "VIRTDATACENTER", "REPLICATION_APPLIANCE", "REPLICATION_JOB", "IP_POOL", "DP_VM_SNAPSHOT", "DP_VMGROUP_SNAPSHOT", "DP_VM", "DP_VMGROUP", "DP_VM_CONFIG", "DP_VM_SNAPSHOT_POINT", "CLUSTER_PAIR")]
        ////[Alias("vmtyp")]
        ////public string VMType { get; set; }

        //[Parameter(ParameterSetName = "HXName")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [ValidateSet("VCMOID", "VMBIOSUUID", "VMDSPATH")]
        [Alias("vmidty")]
        public string VMidtype { get; set; }
        
        [Parameter(Mandatory = true)]
        [Alias("srvr")]
        public string Server { get; set; }

        //UserName Parameter contains the Cisco HXConnect UserName
        //[Parameter(Mandatory = true, Position = 1)]
        //[ValidateNotNullOrEmpty]
        //[Alias("user")]
        //public string UserName { get; set; }

        ////Password Parameter contains the Cisco HXConnect Password
        //[Parameter(Mandatory = true, Position = 2)]
        //[ValidateNotNullOrEmpty]
        //[Alias("pwd")]
        //public string Password { get; set; }


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
               
               
                
                apiInstance.OpDpVmHaltPutCustom("4218d69f-bbbf-5632-229d-d1a299478f3a", "en-US", accessTkn.ToString());
                WriteObject("Halted Replication", true);
                return;
                
            }
            catch (ArgumentException e)
            {
                ErrorRecord psErrRecord = new ErrorRecord(
                           e, "Arguments not provided.", ErrorCategory.AuthenticationError, e.Message);
                WriteError(psErrRecord);
            }
            catch (Exception e)
            {
                
                WriteObject("Exception when calling apiInstance.OpDpVmRecoveryFailoverPut: " + e.Message);
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
    
}
