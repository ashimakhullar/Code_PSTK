// Author(s): 
// Ashima Bahl, asbahl@cisco.com 

using System.Management.Automation;
using IO.Swagger.Api;
using IO.Swagger.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Get, "ProtectionGroup")]
    [OutputType(typeof(VirtualMachine))]

    public class ProtectionGroup : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        //Server Parameter contains the Cisco HXConnect IP

        [Parameter()]
        [ValidateNotNull]
        [Alias("srvr")]
        public string Server { get; set; }
       
        //Groupid will pass the Group uid
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
                
                List<ProtectionGroupInfo> result = apiInstance.OpDpGroupGet( accessTkn.ToString(), null, null, "en-US");

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
                if (GroupName != null)
                {
                    var grpMatch = result.FirstOrDefault(vm => vm.Er.Name == GroupName.ToString());
                    WriteObject(grpMatch, true);
                    return;
                }


                WriteObject(result, true);
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
