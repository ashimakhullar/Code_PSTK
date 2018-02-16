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

        //[Parameter(ParameterSetName = "HXName")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("vmid1")]
        public string VMId { get; set; }
       
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
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("user")]
        public string UserName { get; set; }

        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        [Alias("pwd")]
        public string Password { get; set; }


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
            if (UserName != null && Password != null)
            {
                Configuration.Default.Username = UserName.ToString();
                Configuration.Default.Password = Password.ToString();
            }
            else
            {
                throw new ArgumentException("Username/Password has to be provided");
            }
            var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
            var apiInstance = new ProtectApi(apiString);
            var jparams = "";
            
            try
            {
                if (VMName == null)
                {
                 jparams = "VMName";
                }
                if (VMId == null)
                {
                 jparams += ",VMId";
                }
                if (VMidtype == null)
                {
                 jparams += ",VMidtype";
                }
                if (jparams != "")
                {
                    throw new ArgumentException("Please enter the following params"+ jparams);
                }
                var objVmJson = new VMDet
                {
                    name = VMName.ToString(),
                    type = "DP_VM",
                    id = VMId.ToString(),
                    idtype = VMidtype.ToString(),
                    confignum="0"
                };


           
                var json = JsonConvert.SerializeObject(objVmJson);


                EntityRef objEF = JsonConvert.DeserializeObject<EntityRef>(json);
      
                ProtectedVMSpec body = new ProtectedVMSpec(objEF);
                                 
                ProtectedVMInfo result1 = apiInstance.OpDpVmPost(body);
                WriteObject(result1, true);
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
