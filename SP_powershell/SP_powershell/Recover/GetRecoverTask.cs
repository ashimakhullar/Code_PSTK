// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;



namespace SP_powershell
{
    [Cmdlet(VerbsCommon.Get, "RecoverTask")]
    [OutputType(typeof(VirtualMachine))]

    public class GetRecoverTask : SPCmdlet
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

        //VMUid will pass the VM uid

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("vm_id")]
        public string VMID { get; set; }

        //TaskId will pass the taskID uid

        [Parameter()]
        [ValidateNotNullOrEmpty]
        [Alias("task_id")]
        public string TaskID { get; set; }
        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {

            if (ValidateParameters() == false)
                return;
            // Configure access token for authorization
            AccessToken accToken = new AccessToken();

            try
            {
                if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                {
                    throw new Exception("No server is connected.");
                }
                WriteDebug("The Server connected : " + Server.ToString());
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
                WriteDebug("apiString : " + apiString.ToString());
                var apiInstance = new RecoverApi(apiString);
                string accessTkn = accToken.GetAccessToken(Server.ToString());
       
                if (TaskID != null)
                {
                    //GetSpecific TaskID;
                    var TaskSpecific = TaskID.ToString();
                    WriteDebug("TaskSpecific : " + TaskSpecific.ToString());
                    List<IO.Swagger.Model.Job> respTaskList = apiInstance.OpDpVmTasksGet(accessTkn, VMID.ToString(), TaskSpecific, null, null, "en-US");
                    WriteObject(respTaskList, true);
                    return;
                }

                //api response returning the task for the vmid passed
                List<IO.Swagger.Model.Job> respVMTask = apiInstance.OpDpVmTasksGet(accessTkn, VMID.ToString(), null, null, null, "en-US");
                //WriteTaskrecord(output);
                WriteObject(respVMTask, true);


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
            }

        }
        /// <summary>
        /// Write Task record.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when null values </exception>
        /// <param name="vmTasks">Tasks for each vm</param>
        /// <returns></returns>
        private void WriteTaskrecord(List<IO.Swagger.Model.Job> vmTasks)
        {
            double val;
            double timeStarted=0;
            foreach (var resultset in vmTasks)
            {
                var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
                if (resultset.State != null)
                {
                    WriteObject("STATE : " + resultset.State.ToString());
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.MethodName != null)
                {
                    WriteObject("METHOD NAME : " + resultset.MethodName);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.Jobid != null)
                {
                    WriteObject("JOB ID : " + resultset.Jobid);
                    WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.TimeStartedMillis != null)
                {
                    timeStarted = (double)resultset.TimeStartedMillis;
                    WriteObject("TIME STARTED " + posixTime.AddMilliseconds(timeStarted));
                    //WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.TimeElapsedMillis != null)
                {
                    val = (double)resultset.TimeElapsedMillis;
                    val = timeStarted + val;
                    WriteObject("TIME ELAPSED : " + posixTime.AddMilliseconds(val));
                    //WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.TimeSubmittedMillis != null)
                {
                    val = (double)resultset.TimeSubmittedMillis;
                    WriteObject("TIME SUBMITTED : " + posixTime.AddMilliseconds(val));
                    //WriteObject("---------------------------------------------------------------------");
                }
                if (resultset.LifetimeAfterExitMillis != null)
                {
                    val = (double)resultset.LifetimeAfterExitMillis;
                    WriteObject("LIFETIME AFTER EXIT : " + posixTime.AddMilliseconds(val));
                }
                WriteObject("=====================================================================");
            }
        }
        /// <summary>
        /// Convert unixTimestamp to DateTime format
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when null values </exception>
        /// <param name="unixTimeStamp">double</param>
        /// <returns></returns>

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
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
