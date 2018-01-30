// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "Peers")]
    [OutputType(typeof(MapPairErToReplicationPeerInfo), typeof(ReplicationDatastore))]

    public class Peers : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //

        [Parameter(Mandatory = true)]
        [Alias("srvr")]
        public string Server { get; set; }

        //UserName Parameter contains the Cisco HXConnect UserName
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "UserNamePassword")]
        [Alias("user")]
        public string UserName { get; set; }

        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "UserNamePassword")]
        [Alias("pwd")]
        public string Password { get; set; }

        //VMUid will pass the VM uid
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("peerclusterid")]
        public string ClusterID { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            if (ValidateParameters() == false)
                return;
            try
            {
                // Configure OAuth2 access token for authorization: petstore_auth
                //Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                Configuration.Default.Username = UserName.ToString();
                Configuration.Default.Password = Password.ToString();
                var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                var apiInstance = new ReplicationApi(apiString);

                //if (ParameterSetName == "HXClusterid")
                if (ClusterID != null)
                {
                    var clusterSpecific = ClusterID.ToString();
                    //GetSpecific VM;
                    List<ReplicationDatastore> result1 = apiInstance.OpReplicationPeerPeerClusterIdDatastoresGet(clusterSpecific);
                    WriteObject(result1, true);
                    return;
                }
                // Find Replication peers of the current cluster


                List<MapPairErToReplicationPeerInfo> result = apiInstance.OpReplicationPeerGet();
                //WriteObject(result,true);
                WritePeerRecord(result);
            }
            catch (Exception e)
            {

                ErrorRecord psErrRecord = new ErrorRecord(
                          e,
                          "Exception when calling apiInstance.OpDpVmGet: ",
                          ErrorCategory.NotSpecified,
                          e.Message);
                WriteError(psErrRecord);

            }

        }

        private void WritePeerRecord(List<MapPairErToReplicationPeerInfo> result)
        {
            List<MapPairErToReplicationPeerInfo> list = new List<MapPairErToReplicationPeerInfo>();

            foreach (var item in result)
            {
                if (item.PeerInfo != null)
                {
                    WriteObject(item.PeerInfo);
                    WriteObject("---------------------------------------------------------------------");
                }
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
