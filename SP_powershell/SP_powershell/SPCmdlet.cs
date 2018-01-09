using System;
using System.Management.Automation;
using System.Net.Http;

namespace SP_powershell
{
    /// <summary>
    /// This is the base class for all CmdLets, which pulls in the
    /// PSCmdLet base class from which all others derive their Cmdletness.
    /// </summary>

    public abstract class SPCmdlet : PSCmdlet
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
            catch (HttpRequestException e)
            {
                WriteErrorRecord(e, "", ErrorCategory.ConnectionError, "");
            }
            catch (PipelineStoppedException e)
            {
                // Obtrusive to write this message.
            }
            catch (Exception e)
            {
                WriteErrorRecord(e, "", ErrorCategory.NotSpecified, "");
            }
        }

        protected abstract void ProcessSPRecord();
        
        

        // 
        // Properties
        //


        //protected internal abstract bool ValidateParameters();

        /// <summary>
        /// If the servers PS session variable doesn't exist (no sessions),
        /// CmdLet's that require existing sessions can easily check with this
        /// method.
        /// </summary>
        private void WriteErrorRecord(Exception e, string errorCode, ErrorCategory category, string errorMessage)
        {
            var errRecord =
                new ErrorRecord(e, errorCode, category, null)
                {
                    ErrorDetails = new ErrorDetails(errorMessage)
                };

            WriteError(errRecord);
        }
        /// <summary>
        /// Abstract method for parameter validation that is more complex than
        /// using the PowerShell parameter validation attributes.
        /// </summary>
        protected internal abstract bool ValidateParameters();
    }
}