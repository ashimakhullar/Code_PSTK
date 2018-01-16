using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;


namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "ProtectedVM")]
    [OutputType(typeof(VirtualMachine))]

    public class GetProtectedVM : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        List<string> listParamtersetName = new List<string>();
       

        //Server Parameter contains the Cisco HXConnect IP
        [Parameter(Mandatory =true)]
        [ValidateNotNull]
        [Alias("srvr")]
        public string Server { get; set; }
        
        //UserName Parameter contains the Cisco HXConnect UserName
        [Parameter(Mandatory = true , Position = 1, ParameterSetName = "UserNamePassword")]
        [ValidateNotNullOrEmpty]
        [Alias("user")]
        public string UserName { get; set; }

        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Mandatory = true , Position = 2, ParameterSetName = "UserNamePassword")]
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
        [Alias("VMState")]
        public string State { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            if (ValidateParameters() == false)
                return;
            // Configure access token for authorization
            
           
            try
            {
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

                //
                // Find a specific Protected VM
                //

                if (VMUid != null)
                {
                    var VmSpecific = VMUid.ToString();
                    //GetSpecific VM;
                    ProtectedVMInfo result1 = apiInstance.OpDpVmVmidGet(VmSpecific);
                    WriteObject(result1, true);
                    return;
                }


                // Find All Protected VMs
                List<ProtectedVMInfo> result = apiInstance.OpDpVmGet();

               
                //find the vm details matching to the vmName provided as parameter
                if (VMname!=null)
                { 
                    var vmMatch= result.FirstOrDefault(vm => vm.Er.Name == VMname.ToString());
                    WriteObject(vmMatch, true);
                    return;
                }
                
                WriteObject(result, true);
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
                WriteWarning(psErrRecord.Exception.Message);
                //WriteErrorRecord(e,"Exception when calling apiInstance.OpDpVmGet: ", ErrorCategory.ConnectionError, e.Message);
            }
            
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

}
