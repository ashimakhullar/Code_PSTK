// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Client;
using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "ProtectionGroup")]
    [OutputType(typeof(VirtualMachine))]

    public class ProtectionGroup : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //

        [Parameter()]
        [Alias("srvr")]
        public string Server { get; set; }
        //UserName Parameter contains the Cisco HXConnect UserName
        [Parameter(Position = 1, ParameterSetName = "UserNamePassword")]
        [Alias("user")]
        public string UserName { get; set; }
        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Position = 2, ParameterSetName = "UserNamePassword")]
        [Alias("pwd")]
        public string Password { get; set; }
        //Groupid will pass the Group uid
        //[Parameter(ParameterSetName = "HXGrUid")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("Grpid")]
        public string Groupid { get; set; }

        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("GrpName")]
        public string GroupName { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            if (ValidateParameters() == false)
                return;

            try
            {
                // Configure OAuth2 access token for authorization

                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }
                //var valServer = "";
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
                var apiInstance = new ProtectApi(apiString);
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

               

                List<ProtectionGroupInfo> result = apiInstance.OpDpGroupGet(null, accessTkn.ToString(), "en-US");

                //
                // Find a specific Protected Group
                //
                if (Groupid != null)
                {
                    var groupSpecific = Groupid.ToString();
                    ProtectionGroupInfo result1 = apiInstance.OpDpGroupGroupidGet(groupSpecific);
                    WriteObject(result1, true);
                    return;
                }
                // Find Protection Groups

               // List<ProtectionGroupInfo> result = apiInstance.OpDpGroupGet();
                //find the Group details matching to the GroupName provided as parameter
                if (GroupName != null)
                {
                    var grpMatch = result.FirstOrDefault(vm => vm.Er.Name == GroupName.ToString());
                    WriteObject(grpMatch, true);
                    return;
                }

               
                WriteObject(result,true); 
            }
            catch (Exception e)
            {

                ErrorRecord psErrRecord = new ErrorRecord(
                          e,
                          "Exception when calling apiInstance.OpDpGroupGet: ",
                          ErrorCategory.NotSpecified,
                          e.Message);
                WriteError(psErrRecord);
                
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
