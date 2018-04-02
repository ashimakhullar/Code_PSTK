// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;



namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Get, "RecoveryJobs")]
    [OutputType(typeof(VirtualMachine))]

    public class GetRecoveryJobs : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        List<string> listParamtersetName = new List<string>();


        //Server Parameter contains the Cisco HXConnect IP
  
        [Parameter()] 
        [ValidateNotNull]
        [Alias("srvr")]
        public string Server { get; set; }
        
        //UserName Parameter contains the Cisco HXConnect UserName
        [Parameter(Position = 1, ParameterSetName = "UserNamePassword")]
        [ValidateNotNullOrEmpty]
        [Alias("user")]
        public string UserName { get; set; }

        //Password Parameter contains the Cisco HXConnect Password
        [Parameter(Position = 2, ParameterSetName = "UserNamePassword")]
        [ValidateNotNullOrEmpty]
        [Alias("pwd")]
        public string Password { get; set; }

        //VMUid will pass the VM uid
        //[Parameter(ParameterSetName = "HXUid")]
        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("job_id")]
        public string JobID { get; set; }

      
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
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }
                //var valServer = "";
                if (Server!=null )
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
                var num=0;
               String accessTkn = "";

                dynamic dictServerCnnctd = null;
                if (ConnectHXServer.storageKeyDictionary != null)
                {
                    num= ConnectHXServer.storageKeyDictionary.Count();
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

                //
                // Find a specific Protected VM
                //


                if (JobID != null)
                {
                    //GetSpecific JobID;
                    var jobSpecific = JobID.ToString();
                    //GetSpecific VM;
                    List<IO.Swagger.Model.Job> result1 = apiInstance.OpDpVmRecoveryJobsJobIdGet(jobSpecific, accessTkn,"en-US");
                    WriteObject(result1, true);
                    return;
                }
                
                
                var output = apiInstance.OpDpVmRecoveryJobsGet(accessTkn.ToString(), "en-US");


                WriteObject(output, true);
              
              
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
                //WriteWarning(psErrRecord.Exception.Message);
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
