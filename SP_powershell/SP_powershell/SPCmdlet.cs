// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Linq;

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

        protected abstract void ProcessSPRecord();
        /// <summary>
        ///Get AccessToken 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="jobId"></param>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <param name="accessToken"></param>
        /// <returns>ApiResponse of List&lt;Job&gt;</returns>
        protected string getAccessToken(string Server)
        {
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
            return accessTkn;
        }



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