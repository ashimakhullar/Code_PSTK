// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Cisco.Runbook
{
    [Cmdlet(VerbsCommon.Get, "RecoverTask")]
    [OutputType(typeof(VirtualMachine))]

    public class GetRecoverTask : SPCmdlet
    {
        // 
        // Properties (PowerShell Parameters) to be defined below
        //
        //List<string> listParamtersetName = new List<string>();

        //Server Parameter contains the Cisco HXConnect IP

        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        [Alias("srv")]
        public string Server { get; set; }

        //VMID will pass the VM uid

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
        /// <summary>
        /// Process Record.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when null values </exception>
        /// <returns></returns>
        protected override void ProcessSPRecord()
        {

            if (ValidateParameters() == false)
                return;
            // Configure access token for authorization
            AccessToken accToken = new AccessToken();
            try
            {
                if (ConnectHXServer.storageKeyDictionary == null)
                {
                    throw new Exception("No server is connected.");
                }
                else if (ConnectHXServer.storageKeyDictionary != null)
                {
                   Server = ConnectHXServer.storageKeyDictionary.FirstOrDefault().Key;
                }
                WriteDebug("The Server connected : " + Server);
                if (Server != null)
                {
                    Server = Server.Trim();
                }
                
                WriteDebug("Server : " + Server);
                var apiInstance = new RecoverApi(Server);
                string accessTkn = accToken.GetAccessToken(Server.ToString());
       
                if (TaskID != null)
                {
                    //GetSpecific TaskID;
                    string TaskSpecific = TaskID.ToString();
                    WriteDebug("TaskSpecific : " + TaskSpecific);
                    List<IO.Swagger.Model.Job> respTaskList = apiInstance.OpDpVmTasksGet(accessTkn, VMID.ToString(), TaskSpecific, null, null, "en-US");
                    WriteObject(respTaskList, true);
                    return;
                }
                //api response returning the tasks for the vmid passed
                List<IO.Swagger.Model.Job> respVMTaskList = apiInstance.OpDpVmTasksGet(accessTkn, VMID.ToString(), null, null, null, "en-US");
                //WriteTaskrecord(output);
                WriteObject(respVMTaskList, true);
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
