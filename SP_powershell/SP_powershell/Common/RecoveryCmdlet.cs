// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Collections.Generic;

namespace Cisco.Runbook
{
    /// <summary>
    /// This is the base class for Recover CmdLets, which pulls in the
    /// SPCmdlet class from which all others common cmdlets derive their Cmdletness.
    /// </summary>

    public abstract class RecoveryCmdlet : SPCmdlet
    {
        protected override void ProcessRecord()
        {

            /// <summary>
            /// Gets the instance of session state for the current runspace.
            /// </summary>

            try
            {
                ProcessSPRecord();

            }
            catch (IO.Swagger.Client.ApiException e)
            {
                WriteErrorRecord(e, "", ErrorCategory.NotSpecified, "");
            }
            catch (HttpRequestException e)
            {
                WriteErrorRecord(e, "", ErrorCategory.ConnectionError, "");
            }
            catch (PipelineStoppedException e)
            {
                // Obtrusive to write this message.
            }
            catch (ArgumentException e)
            {
                WriteErrorRecord(e, "", ErrorCategory.InvalidArgument, "");
            }
            catch (Exception e)
            {
                WriteErrorRecord(e, "", ErrorCategory.NotSpecified, "");
            }
        }
   
        protected abstract void ShowStatus(List<IO.Swagger.Model.Job> respVMTaskGet);
    }
}