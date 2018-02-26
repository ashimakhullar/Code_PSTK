
 #
 # Copyright 2013-2016 Illumio, Inc. All Rights Reserved.
 #
 param (
    [switch]$logs
)

Add-Type @"

using Microsoft.Win32;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Reflection;
namespace TNScope
{
    internal class UserMessage
    {
        internal const String TNerrRegEntry = "TN Service registry entries are not set properly.";
        internal const String TNerrInstall = "TN Service package is not properly installed. Please check installation.";
        internal const String TNerrConfigMsg = "MSI installer is not proeprly pre-configured by IT Admin for {0} property.";
        internal const String TNerrpropNotSet = "The {0} is not set correctly. Please contact IT Administrator";
        internal const String TNerrInvalidChars = "MSI installer is not proeprly pre-configured by IT Admin for {0} property, it contains invalid characters.";
        internal const String TNerrAdminNotSet = "The {0} is not set correctly. Please contact IT Administrator";

        internal const String TNerrPropOutofRange = "MSI installer is not proeprly pre-configured by IT Admin for {0} property, Its value should be between {1} and {2} inclusive.";

        internal const String TNevtRegistrationFailed = "Failed to register events. Program may not work as expected.";
        internal const String TNevtIPAddressChangeMsg = "IP Address change, event detected. This event will occur per network interface.";
        internal const String TNevtKeepAliveMsg = "KeepAlive event occured. Sending request to Telemetry Node Service.";
        internal const String TNevtLogOffMsg = "Logoff event detected...";

        internal const String TNMsginterrupt = "User interrupted the script. Performing script cleanup operations.\nPlease do not press Ctrl + C or close Powershell window until cleanup completes, \notherwise it will leave the process in unclean mode.";

        internal const String TNMsgunregEvents = "Unregistering events, hence no further events will be handled by TN client. \nPlease wait unregistering events may take some time...";
        internal const String TNMsgretry = "Retrying connection to Telemetry Node Service in {0} seconds";
        internal const String TNMsgretryNetworkAvailablity = "Checking for network availability in {0} seconds.";
        internal const String TNMsgnwUnavailable = "Network unavailable. Failed to connect to Telemetry Node Service.";
        internal const String TNMsgfailedtoConn = "Failed to connect to Telemetry Node Service.";
        internal const String TNMsgvalidatingSSL = "Validating SSL certificate.";
        internal const String TNMsgsslValidity = "Telemetry Node Service has {0} SSL certificate.";
        internal const String TNMsgsendingInfo = "Sending logged in user information to VEN server.";
        internal const String TNMsghttpSendStatus = "Sending logged in user information {0} to VEN server.";
        internal const string TNMsgConfig = "Telemetry Node Service is properly configured.";
        internal const string TNMsgRegReadFailed = "Registry read failed. Exiting Telemetry Node script...";
        internal const string TNMsgRestartPowerShell = "Please close powershell session and start again. Exiting...";
        internal const String TNMsgquickEditDisabled = "Quick Edit Mode Disabled.";
        internal const String TNMsgquickEditNote = "NOTE: Running in QuickEdit mode will pause the script.\nPress Enter Key to resume script execution.\nDisable QuickEdit mode to avoid this behavior.";
		// Warning Message for IdentityNotMappedException 
		internal const String TNMsgADGroupChange = "There was a change in AD group membership. Please logout and log back in for the latest changes to take effect.";

        internal const String TNMsginitialStatus = "Reading and validating Telemetry Node Service properties.";
        internal const String TNMsginstanceRunning = "Telemetry Node Instance is already running.";
        internal const String TNMsgconnectingTNS = "Connecting To Telemetry Node Service at FQDN {0} on Port {1}";
        internal const String TNMsgregisteredLogOff = "Registered User log off event.";
        internal const String TNMsgregisteredIpChange = "Registered IP Address Change event.";
        internal const String TNMsgregisteredKeepAlive = "Registered Keep Alive event.";
        internal const String TNMsgkeepAliveStarted = "KeepAlive timer started.";
        internal const String TNMsgscriptRunning = "Telemetry Node script is running successfully...";
        internal const String TNMsgeventsActivatedMsg = "Activated event Listener.";
        internal const String TNMsgServerFQDNMalFormed = "ServerFQDN url is not well formed.";
        internal const String TNMsgGOOD_BYE = "Exiting script. Good Bye.";
    }

    internal enum TNRequestType
    {
        PUT, DELETE
    }

    internal class GroupInfo
    {
        public string name;
        public string sid;
    }

    internal class IPInfo
    {
        public string ip;
        public string cidr_block;
    }
    /// <TNClientInfo>
    /// This class will have all the required functions to build the
    /// JSON message to be send to VEN server.
    /// Same JSON message will be send in all communication with VEN server.
    /// This class will be used by TNJsonMsg to build the JSON string to
    /// be send to VEN agent.
    /// </TNClientInfo>
    internal class TNClientInfo
    {
        internal Boolean GetSubnetMask(IPAddress address, out String subnetMask)
        {
            subnetMask = string.Empty;
            try
            {
                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (unicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (address.Equals(unicastIPAddressInformation.Address))
                            {
                                subnetMask = unicastIPAddressInformation.IPv4Mask.ToString();
                                break;
                            }
                        }
                    }
                    if (!String.IsNullOrEmpty(subnetMask))
                    {
                        break;
                    }
                }

                if (String.IsNullOrEmpty(subnetMask))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetHostIPAddresses(out List<IPInfo> list)
        {
            list = new List<IPInfo>();
            try
            {
                IPAddress[] ipList = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip in ipList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IPInfo ipInfo = new IPInfo();
                        ipInfo.ip = ip.ToString();
                        String subnetmask;
                        if (!GetSubnetMask(ip, out subnetmask))
                        {
                            continue;
                        }
                        ipInfo.cidr_block = subnetmask;
                        list.Add(ipInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetADGroupsUserBelongsTo(out List<GroupInfo> result)
        {
            result = new List<GroupInfo>();
            try
            {
                IdentityReferenceCollection groupColl = WindowsIdentity.GetCurrent().Groups;
                foreach (IdentityReference currentGroup in groupColl)
                {
                    try
                    {
                        SecurityIdentifier objSecId = new SecurityIdentifier(currentGroup.Value);
                        IdentityReference objNTIdentityRef = objSecId.Translate(typeof(NTAccount));
                        GroupInfo gi = new GroupInfo();
                        gi.name = TNHelper.ExtractUserName(objNTIdentityRef.Value);
                        gi.sid = currentGroup.Value;
                        result.Add(gi);
                    }
					catch(IdentityNotMappedException)
					{
						Boolean bIsWarnMsg = true;
						TNHelper.DisplayUserMessage(UserMessage.TNMsgADGroupChange, bIsWarnMsg);
					}
                    catch (Exception gxExp)
                    {
                        TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name + " foreach loop:", gxExp);
                    }
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean LoggedInUserName(out String userName)
        {
            userName = String.Empty;
            try
            {
                userName = TNHelper.ExtractUserName(WindowsIdentity.GetCurrent().Name);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetMachineId(out String machineId)
        {
            machineId = String.Empty;
            try
            {
                if (String.IsNullOrEmpty(TNClientConfig.MachineID))
                {
                    return false;
                }
                machineId = TNClientConfig.MachineID;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetServiceSharedSecret(out String secretKey)
        {
            secretKey = String.Empty;
            try
            {
                if (!TNRegistryInfo.SingleInstance.GetServerSecretKey(out secretKey))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }
    }

    /// <TNJsonMsg>
    /// This class will have all the required functions to build JSON 
    /// request. All the required information to build JSON response 
    /// will be built using class TNClientInfo.
    /// </TNJsonMsg>
    internal class TNJsonMsg
    {
        private string service_shared_secret;
        private string username;
        private string machine_id;
        private List<GroupInfo> groups;
        private List<IPInfo> interfaces;
        String comma = ",";
        String openSquareBracket = "[";
        String closeSquareBracket = "]";
        String opencurlybrace = "{";
        String closecurlybrace = "}";
        System.Text.StringBuilder m_sbJason = null;

        internal void AddColon()
        {
            String space = " ";
            String colon = ":";
            try
            {
                m_sbJason.Append(colon);
                m_sbJason.Append(space);
                m_sbJason.Append(space);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        internal void AddString(String strToAdd)
        {
            String quote = "\"";
            try
            {
                m_sbJason.Append(quote);
                m_sbJason.Append(strToAdd);
                m_sbJason.Append(quote);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        internal void AddKeyValue(String Key, String Value, bool bAddComma)
        {
            try
            {
                AddString(Key);
                AddColon();
                AddString(Value);
                if (bAddComma)
                {
                    m_sbJason.Append(comma);
                }
                m_sbJason.Append(Environment.NewLine);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal void AddGroupsArray()
        {
            const String arraygroups = "groups";
            const String nameMember = "name";
            const String sidMember = "sid";
            try
            {
                AddString(arraygroups);
                AddColon();
                m_sbJason.Append(openSquareBracket);
                m_sbJason.Append(Environment.NewLine);
                for (int index = 0; index < groups.Count; index++)
                {

                    m_sbJason.Append(opencurlybrace);
                    m_sbJason.Append(Environment.NewLine);
                    GroupInfo currGroup = groups[index];

                    AddKeyValue(nameMember, currGroup.name, true);
                    AddKeyValue(sidMember, currGroup.sid, false);

                    m_sbJason.Append(closecurlybrace);
                    if ((index + 1) != groups.Count)
                    {
                        m_sbJason.Append(comma);
                    }
                    m_sbJason.Append(Environment.NewLine);
                }
                m_sbJason.Append(closeSquareBracket);
                m_sbJason.Append(comma);
                m_sbJason.Append(Environment.NewLine);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal void AddIPsArray()
        {
            const String arrayinterfaces = "interfaces";
            const String ipMember = "ip";
            const String cidr_blockMember = "cidr_block";
            try
            {
                AddString(arrayinterfaces);
                AddColon();
                m_sbJason.Append(openSquareBracket);
                m_sbJason.Append(Environment.NewLine);
                for (int index = 0; index < interfaces.Count; index++)
                {
                    m_sbJason.Append(opencurlybrace);
                    m_sbJason.Append(Environment.NewLine);
                    IPInfo currIPinfo = interfaces[index];

                    AddKeyValue(ipMember, currIPinfo.ip, true);
                    AddKeyValue(cidr_blockMember, currIPinfo.cidr_block, false);

                    m_sbJason.Append(closecurlybrace);
                    if ((index + 1) != interfaces.Count)
                    {
                        m_sbJason.Append(comma);
                    }
                    m_sbJason.Append(Environment.NewLine);
                }
                m_sbJason.Append(closeSquareBracket);

                m_sbJason.Append(Environment.NewLine);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal Boolean InitJsonFields()
        {
            try
            {
                TNClientInfo clientInfo = new TNClientInfo();
                String sharedSecret;
                if (!clientInfo.GetServiceSharedSecret(out sharedSecret))
                {
                    return false;
                }
                service_shared_secret = sharedSecret;
                String user;
                if (!clientInfo.LoggedInUserName(out user))
                {
                    return false;
                }
                username = user;
                String mid;
                if (!clientInfo.GetMachineId(out mid))
                {
                    return false;
                }
                machine_id = mid;
                List<GroupInfo> groupInfo;
                if (!clientInfo.GetADGroupsUserBelongsTo(out groupInfo))
                {
                    return false;
                }
                groups = groupInfo;
                List<IPInfo> ipAddrList;
                if (!clientInfo.GetHostIPAddresses(out ipAddrList))
                {
                    return false;
                }
                interfaces = ipAddrList;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        public Boolean BuildTNJsonMsg(out String jasonString)
        {
            jasonString = String.Empty;
            const String SHAREDSECRET = "service_shared_secret";
            const String USERNAME = "username";
            const String MACHINE_ID = "machine_id";
            try
            {
                if (!InitJsonFields())
                {
                    return false;
                }
                m_sbJason = new System.Text.StringBuilder();
                m_sbJason.Append(opencurlybrace);
                m_sbJason.Append(Environment.NewLine);
                AddKeyValue(SHAREDSECRET, service_shared_secret, true);
                m_sbJason.Append(Environment.NewLine);
                AddKeyValue(USERNAME, username, true);
                m_sbJason.Append(Environment.NewLine);
                AddKeyValue(MACHINE_ID, machine_id, true);
                m_sbJason.Append(Environment.NewLine);
                AddGroupsArray();
                AddIPsArray();
                m_sbJason.Append(closecurlybrace);
                jasonString = m_sbJason.ToString();
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return true;
        }
    }

    internal class TNHelper
    {
        internal static bool LogsOn = false;

        internal static void DisplayDebugMessage(String DebugMessage, Exception ex = null)
        {
            try
            {
                String timeStamp = DateTime.Now.ToString("[yyyy-MM-ddTHH:mm:ss.fff]");

                if (LogsOn && !String.IsNullOrEmpty(DebugMessage))
                {
                    Console.WriteLine("{0}  DEBUG -- {1}", timeStamp, DebugMessage);
                }

                if (null != ex)
                {
                    Console.WriteLine("{0}  ERROR -- {1}", timeStamp, ex.Message);
                }

                if (LogsOn && null != ex)
                {
                    Console.WriteLine("{0}  ERROR -- {1}", timeStamp, ex.StackTrace);
                }
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message);
            }
        }

        internal static void DisplayUserMessage(String UserMessage, Boolean bIsWarnMsg = false)
        {
            try
            {
                if (String.IsNullOrEmpty(UserMessage))
                {
                    return;
                }

                String timeStamp = DateTime.Now.ToString("[yyyy-MM-ddTHH:mm:ss.fff]");
				
				String logLevel = ((bIsWarnMsg)?"WARN":"INFO");
				
				Console.WriteLine("{0}   {1} -- {2}", timeStamp, logLevel , UserMessage);
				
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void DisplayHttpRespText(String errRespText)
        {
            if (String.IsNullOrEmpty(errRespText))
            {
                return;
            }
            try
            {
                String[] arrResponses = errRespText.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (null != arrResponses && arrResponses.Length > 0)
                {
                    TNHelper.DisplayUserMessage("HTTP error response:");
                    foreach (String response in arrResponses)
                    {
                        String[] arrRespFields = response.Split("=>".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (null != arrRespFields && arrRespFields.Length > 0 && arrRespFields[0].IndexOf(":message") != -1)
                        {
                            String message = arrRespFields[1].Replace("\"", String.Empty).Replace("}", String.Empty).Replace("]", String.Empty);
                            TNHelper.DisplayUserMessage(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal static String ExtractUserName(String strToUpdate)
        {
            try
            {
                String slash = @"\";
                if (strToUpdate.IndexOf(slash) == -1)
                {
                    return strToUpdate;
                }

                String[] strArrComp = strToUpdate.Split(slash.ToCharArray(),
                                                          StringSplitOptions.RemoveEmptyEntries);
                if (null != strArrComp && strArrComp.Length == 1)
                {
                    strToUpdate = strArrComp[0];
                }
                else
                    if (null != strArrComp && strArrComp.Length > 1)
                    {
                        strToUpdate = strArrComp[1];
                    }
            }
            catch (Exception ex)
            {
                DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return strToUpdate;
        }
    }

    /// <TNMessageOps>
    /// This class will have all the required functions to perform request for 
    /// add or remove user access in VEN server.
    /// It will also have retry logic for add or remove user operations.
    /// Apart from this it will contain HTTP related functions for request and 
    /// response handling.
    /// This class will use TNJsonMsg to get JSON message to be send to VEN server.
    /// </summary>
    internal class TNMessageOps
    {
        private enum TNHttpResult
        {
            TN_HTTP_SUCCESS = 0, TN_HTTP_FAIL, TN_HTTP_RETRY
        }

        private static readonly object PutReqLock = new object();
        private const String HTTPS = "https://";
        private const String COLON = ":";
        private String m_healthCheckURL = String.Empty;
        private String m_MsgURL = String.Empty;
        private String m_evtSourceMsg = String.Empty;
        private const int Timeout = 60000 * 3;
        // 5 sec  for 723 times = 1 hr  15 sec
        // 2 sec  for   2 times =        4 sec
        // Total  Sleep  time   = 1 hr 19 sec
        private const int iRetryTimes = 725;  
 
        //declaring as volatile
        //so that The system always reads the current value of a volatile IsDeleteInProgress at the point it is requested
        //Also, the value of the IsDeleteInProgress is written immediately on assignment. 
        internal static volatile bool IsDeleteInProgress = false;
        internal static volatile bool IsPutInProgress = false;
        internal TNMessageOps()
        {

        }

        internal TNMessageOps(String evtSourceMsg)
        {
            m_evtSourceMsg = evtSourceMsg;
        }

        private Boolean initMsgURL()
        {
            try
            {
                const int iInitialCapacity = 80;
                const String MSG_INTERFACE = "/api/v1/user_sessions";//"/async/interface"
                String serverFQDN = String.Empty;
                String serverPort = String.Empty;
                System.Text.StringBuilder sb = new System.Text.StringBuilder(iInitialCapacity);
                sb.Append(HTTPS);

                if (!TNRegistryInfo.SingleInstance.GetServerFQDN(out serverFQDN))
                {
                    return false;
                }
                sb.Append(serverFQDN);
                sb.Append(COLON);

                if (!TNRegistryInfo.SingleInstance.GetServerPort(out serverPort))
                {
                    return false;
                }
                sb.Append(serverPort);

                sb.Append(MSG_INTERFACE);
                m_MsgURL = sb.ToString();

                if (!Uri.IsWellFormedUriString(m_MsgURL, UriKind.Absolute))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsgServerFQDNMalFormed);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        private Boolean initHealthCheckURL()
        {
            try
            {
                const String GET_INTERFACE = "/hello";
                const int iInitialCapacity = 80;
                String serverFQDN = String.Empty;
                String serverPort = String.Empty;
                System.Text.StringBuilder sb = new System.Text.StringBuilder(iInitialCapacity);

                sb.Append(HTTPS);

                if (!TNRegistryInfo.SingleInstance.GetServerFQDN(out serverFQDN))
                {
                    return false;
                }
                sb.Append(serverFQDN);
                sb.Append(COLON);
                if (!TNRegistryInfo.SingleInstance.GetServerPort(out serverPort))
                {
                    return false;
                }
                sb.Append(serverPort);
                sb.Append(GET_INTERFACE);

                m_healthCheckURL = sb.ToString();

                if (!Uri.IsWellFormedUriString(m_healthCheckURL, UriKind.Absolute))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsgServerFQDNMalFormed);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean Send(TNRequestType TNMethod)
        {
            try
            {
                String strReqType = Convert.ToString(TNMethod);

                int iRetry = iRetryTimes;
                //wait while network is available 
                while (iRetry > 0 && CanProcessHttpRequest(strReqType) && !NetworkInterface.GetIsNetworkAvailable())
                {                   
                    ExecuteTNMsgRetryDelay(iRetry, true);
                    --iRetry;
                }

                if (!CanProcessHttpRequest(strReqType))
                {
                    return false;
                }

                if (0 == iRetry)
                {
                    String cleanupMsg = UserMessage.TNMsgnwUnavailable;
                    Boolean bSendDeleteReq = false;
                    TNClientConfig.SingleInstance.Cleanup(cleanupMsg, bSendDeleteReq);
                    return false;
                }

                if (TNHttpResult.TN_HTTP_FAIL == ValidateSSLCertificate(strReqType))
                {
                    return false;
                }

                return SendTNHttpRequest(strReqType);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return false;
        }

        internal Boolean AddUser()
        {
            Boolean bSendStatus = false;
            if (IsPutInProgress || !TNClientConfig.IsScriptRunning)
            {
                //return if another PUT request 
                //or script in not running
                //is already in progress
                return bSendStatus;
            }
            lock (PutReqLock)
            {
                IsPutInProgress = true;
                try
                {
                    if (CanProcessHttpRequest(Convert.ToString(TNRequestType.PUT)))
                    {
                        TNHelper.DisplayUserMessage(m_evtSourceMsg);

                        bSendStatus = Send(TNRequestType.PUT);
                    }
                }
                catch (Exception ex)
                {
                    TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                    bSendStatus = false;
                }
                finally
                {
                    IsPutInProgress = false;

                    //If UserAdded and TNClientConfig.IsAddUseSuccessful is false
                    //set TNClientConfig.IsAddUseSuccessful to true 
                    if (bSendStatus && !TNClientConfig.IsAddUserSuccessful)
                    {
                        TNClientConfig.IsAddUserSuccessful = true;
                    }
                }
            }
            return bSendStatus;
        }

        private void ExecuteTNMsgRetryDelay(int iRetry, bool bShowTNNetworkAvailablityMsg = false)
        { 
            int iSleep;
            try
            {
                if (iRetry > (iRetryTimes - 2))         //first 2 retry with 2 sec sleep  
                {
                    iSleep = 2000;
                }
                else   //remaining all retries with 5 sec sleep  
                {
                    iSleep = 5000;
                }

                if (bShowTNNetworkAvailablityMsg)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNMsgretryNetworkAvailablity, (iSleep / 1000)));
                }
                else
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNMsgretry, (iSleep / 1000)));
                }

                Thread.Sleep(iSleep);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        //HTTP operations
        private Boolean GetHttpWebResp(ref int iResponseCode, ref HttpWebResponse errorResponse, ref HttpWebRequest httpWebRequest, ref String strRespText)
        {
            Boolean bIsSucces = false;
            if (!TNClientConfig.IsScriptRunning)
            {
                return bIsSucces;
            }
            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strRespText = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();
                    TNHelper.DisplayDebugMessage("Telemetry Node Service response is : \n" + strRespText);
                }

                iResponseCode = Convert.ToInt32(httpResponse.StatusCode);
                TNHelper.DisplayDebugMessage("Status Code " + Convert.ToString(iResponseCode));
                bIsSucces = true;
            }
            catch (WebException e)
            {
                errorResponse = e.Response as HttpWebResponse;
                try
                {
                    if (null != errorResponse)
                    {
                        using (StreamReader streamReader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            strRespText = streamReader.ReadToEnd();
                            streamReader.Close();
                            streamReader.Dispose();
                        }
                        iResponseCode = Convert.ToInt32(errorResponse.StatusCode);
                        TNHelper.DisplayDebugMessage("Status Code " + Convert.ToString(iResponseCode), e);
                    }
                }
                catch (Exception ex)
                {
                    TNHelper.DisplayDebugMessage("while displaying response body", ex);
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return bIsSucces;
        }

        private Boolean CreateHttpReq(out HttpWebRequest httpWebRequest, String strReqType, String reqURL)
        {
            httpWebRequest = null;
            try
            {
                if (!CanProcessHttpRequest(strReqType))
                {
                    return false;
                }
                httpWebRequest = (HttpWebRequest)WebRequest.Create(reqURL);
                httpWebRequest.Credentials = null;
                httpWebRequest.Timeout = Timeout;
                httpWebRequest.AllowAutoRedirect = true;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        #region GetHttpResponseStatus
        ///<summary>
        ///Checks HttpStatusCode for success
        ///</summary>
        ///<param name="StatusCode"></param>
        ///<returns></returns>
        private TNHttpResult GetHttpResponseStatus(int StatusCode, HttpWebResponse errorResponse, Boolean bIsSucces)
        {
            try
            {
                if (null == errorResponse && !bIsSucces)
                {
                    return TNHttpResult.TN_HTTP_RETRY;
                }
                //Status code range for TNHttpResult.TN_HTTP_SUCCESS 
                //200 to 299 inclusive
                if ((StatusCode >= 200) && (StatusCode <= 299))
                {
                    return TNHttpResult.TN_HTTP_SUCCESS;
                }
                else if ((StatusCode == 429) || ((StatusCode >= 500) && (StatusCode <= 599)))//Status code range for TNHttpResult.TN_HTTP_RETRY 500 to 599 inclusive or 429
                {
                    return TNHttpResult.TN_HTTP_RETRY;
                }
                else
                {
                    return TNHttpResult.TN_HTTP_FAIL;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return TNHttpResult.TN_HTTP_FAIL;
        }
        #endregion

        private TNHttpResult ProcessHttpRespCode(int iResponseCode, HttpWebResponse errorResponse, Boolean bIsSucces,
                                                 ref Boolean bIsReachable, ref int retry, String errRespText)
        {
            TNHttpResult httpResult = TNHttpResult.TN_HTTP_FAIL;
            if (!TNClientConfig.IsScriptRunning)
            {
                return TNHttpResult.TN_HTTP_FAIL;
            }
            try
            {
                httpResult = GetHttpResponseStatus(iResponseCode, errorResponse, bIsSucces);

                switch (httpResult)
                {
                    case TNHttpResult.TN_HTTP_RETRY:
                        {
                            ExecuteTNMsgRetryDelay(retry);
                            --retry;
                            //for last retry failure retry == 0
                            //display error response text to user
                            if (retry == 0)
                            {
                                TNHelper.DisplayHttpRespText(errRespText);
                            }
                        }
                        break;
                    case TNHttpResult.TN_HTTP_FAIL:
                        {
                            TNHelper.DisplayHttpRespText(errRespText);
                            retry = 0;
                        }
                        break;
                    case TNHttpResult.TN_HTTP_SUCCESS:
                        {
                            bIsReachable = true;
                        }
                        break;
                    default:
                        {
                            retry = 0;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return httpResult;
        }

        private TNHttpResult ValidateSSLCertificate(String strReqType)
        {
            Boolean bIsValid = false;
            Boolean bIsReachable = false;
            HttpWebRequest httpWebRequest = null;
            int retry = iRetryTimes;
            try
            {
                if (!initHealthCheckURL())
                {
                    //error while building health check url
                    return TNHttpResult.TN_HTTP_FAIL;
                }
                while (retry > 0 && CanProcessHttpRequest(strReqType) && !bIsReachable)
                {
                    int iResponseCode = -1;
                    String errRespText = String.Empty;
                    HttpWebResponse errorResponse = null;
                    Boolean bIsSucces = false;

                    if (!CreateHttpReq(out httpWebRequest, strReqType, m_healthCheckURL))
                    {
                        return TNHttpResult.TN_HTTP_FAIL;
                    }
                    if (!CanProcessHttpRequest(strReqType))
                    {
                        return TNHttpResult.TN_HTTP_FAIL;
                    }

                    bIsSucces = GetHttpWebResp(ref iResponseCode, ref errorResponse, ref httpWebRequest, ref errRespText);

                    if (TNHttpResult.TN_HTTP_RETRY == ProcessHttpRespCode(iResponseCode, errorResponse, bIsSucces,
                                                                          ref bIsReachable, ref retry, errRespText))
                    {
                        httpWebRequest = null;
                    }
                }

                if (!bIsReachable)
                {
                    String cleanupMsg = UserMessage.TNMsgfailedtoConn;
                    Boolean bSendDeleteReq = false;
                    TNClientConfig.SingleInstance.Cleanup(cleanupMsg, bSendDeleteReq);
                    return TNHttpResult.TN_HTTP_FAIL;
                }


                TNHelper.DisplayUserMessage(UserMessage.TNMsgvalidatingSSL);
                if (null != httpWebRequest.ServicePoint.Certificate &&
                    IntPtr.Zero != httpWebRequest.ServicePoint.Certificate.Handle)
                {
                    IntPtr certHandle = httpWebRequest.ServicePoint.Certificate.Handle;
                    X509Chain chain = new X509Chain();
                    X509Certificate2 objX509Certificate2 = new X509Certificate2(certHandle);
                    try
                    {
                        bIsValid = chain.Build(objX509Certificate2);
                        chain.Reset();
                    }
                    catch(Exception inner) 
                    {
                        TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name + " inner ", inner);
                        // The certificate is unreadable.
                        bIsValid = false;
                    }
                }
                if (CanProcessHttpRequest(strReqType))
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNMsgsslValidity, (bIsValid) ? "valid" : "invalid"));
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                httpWebRequest = null;
            }
            return (bIsValid) ? TNHttpResult.TN_HTTP_SUCCESS : TNHttpResult.TN_HTTP_FAIL;
        }

        private Boolean CanProcessHttpRequest(String strTNMethod)
        {
            if (!TNClientConfig.IsScriptRunning)
            {
                return false;
            }
            try
            {
                //strTNMethod    IsDeleteInProgress      return Value
                //------------------------------------------------
                //PUT                true                 false
                //PUT                false                true
                //DELETE             true                 true
                //DELETE             false                true          

                if (Convert.ToString(TNRequestType.PUT).Equals(strTNMethod, StringComparison.OrdinalIgnoreCase))
                {
                    if (IsDeleteInProgress)
                    {
                        //don't allow PUT request 
                        //as LogOff is in Progress
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                    if (Convert.ToString(TNRequestType.DELETE).Equals(strTNMethod, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        //this else will never execute
                        //as strTNMethod can be either PUT OR DELETE
                        return false;
                    }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private Boolean SendTNHttpRequest(String strRequestType)
        {
            Boolean bIsReachable = false;
            int retry = iRetryTimes;
            HttpWebRequest httpWebRequest;
            try
            {
                if (!initMsgURL())
                {
                    //error while building MsgURL
                    return false;
                }
                while (retry > 0 && CanProcessHttpRequest(strRequestType) && !bIsReachable)
                {
                    String errRespText = String.Empty;
                    Boolean bIsSucces = false;
                    ServicePoint servicePoint = null;
                    HttpWebResponse errorResponse = null;
                    Stream requestStream = null;
                    int iResponseCode = -1;
                    try
                    {
                        TNHelper.DisplayUserMessage(UserMessage.TNMsgsendingInfo);
                        TNHelper.DisplayDebugMessage("Using method type : " + strRequestType);

                        Uri objUri = new Uri(m_MsgURL);
                        servicePoint = ServicePointManager.FindServicePoint(objUri);

                        if (!CreateHttpReq(out httpWebRequest, strRequestType, m_MsgURL))
                        {
                            return false;
                        }

                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = strRequestType;

                        if (!CanProcessHttpRequest(strRequestType))
                        {
                            httpWebRequest.Abort();
                            return false;
                        }
                        String jsonString;
                        TNJsonMsg objJason = new TNJsonMsg();
                        if (!objJason.BuildTNJsonMsg(out jsonString))
                        {
                            return false;
                        }
                        requestStream = httpWebRequest.GetRequestStream();

                        TNHelper.DisplayDebugMessage("Sending httpWebRequest");
                        using (StreamWriter streamWriter = new StreamWriter(requestStream))
                        {
                            streamWriter.Write(jsonString);
                            streamWriter.Flush();
                            streamWriter.Close();
                            streamWriter.Dispose();
                            TNHelper.DisplayDebugMessage(String.Format("JSON object:\n {0} \n", jsonString));
                        }

                        bIsSucces = GetHttpWebResp(ref iResponseCode, ref errorResponse, ref httpWebRequest, ref errRespText);
                        
                    }
                    catch (Exception innerEx)
                    {
                        TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name + " inside retry loop", innerEx);
                    }
                    finally
                    {
                        if (null != requestStream)
                        {
                            requestStream.Dispose();
                        }

                        if (null != servicePoint)
                        {
                            servicePoint.CloseConnectionGroup(String.Empty);
                        }
                    }

                    if (TNHttpResult.TN_HTTP_RETRY == ProcessHttpRespCode(iResponseCode, errorResponse, bIsSucces,
                                                                            ref bIsReachable, ref retry, errRespText))
                    {
                        httpWebRequest = null;
                    }
                }

                if (CanProcessHttpRequest(strRequestType))
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNMsghttpSendStatus, (bIsReachable) ? "successful" : "failed"));
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                httpWebRequest = null;
            }
            return bIsReachable;
        }

    }

    /// <TNRegistryInfo>
    /// This class implements functions to read registry parameters related to
    /// TN Client.
    /// </TNRegistryInfo>
    internal class TNRegistryInfo
    {
        private static TNRegistryInfo Instance = null;
        private static readonly object ObjLock = new object();
        private String ServiceSharedSecret = String.Empty;
        private String Server = String.Empty;
        private String Port = String.Empty;
        private String KeepAliveSeconds = String.Empty;

        private TNRegistryInfo()
        {
        }

        internal static TNRegistryInfo SingleInstance
        {
            get
            {
                try
                {
                    if (Instance == null)
                    {
                        lock (ObjLock)
                        {
                            if (Instance == null)
                            {
                                Instance = new TNRegistryInfo();
                            }
                        }
                    }
                    return Instance;
                }
                catch (Exception ex)
                {
                    TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                }
                return null;
            }
        }

        private Boolean ReadRegValue(String key, ref String value)
        {
            const String REG_PATH_32 = @"SOFTWARE\\Illumio\\TelemetryNode";
            RegistryKey view32 = null;
            Boolean bReadStatus = false;
            try
            {
                view32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

                using (RegistryKey localKey = view32.OpenSubKey(REG_PATH_32, false))
                {
                    value = localKey.GetValue(key).ToString();
                    localKey.Close();
                    localKey.Dispose();
                }
                if (String.IsNullOrEmpty(value))
                {
                    bReadStatus = false;
                }
                else
                {
                    bReadStatus = true;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                bReadStatus = false;
            }
            finally
            {
                if (null != view32)
                {
                    view32.Dispose();
                }
            }
            return bReadStatus;
        }

        internal Boolean CheckAndReadRegistry()
        {
            try
            {
                TNHelper.DisplayUserMessage(UserMessage.TNMsginitialStatus);
                ServiceSharedSecret = String.Empty;
                Server = String.Empty;
                Port = String.Empty;
                KeepAliveSeconds = String.Empty;

                //pass local variables as out parameter
                String serverFQDN;
                if (!GetServerFQDN(out serverFQDN))
                {
                    return false;
                }

                String localPort;
                if (!GetServerPort(out localPort))
                {
                    return false;
                }

                String secretKey;
                if (!GetServerSecretKey(out secretKey))
                {
                    return false;
                }

                String keepAlive;
                if (!GetKeepAliveInterval(out keepAlive))
                {
                    return false;
                }
                TNHelper.DisplayUserMessage(UserMessage.TNMsgConfig);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetServerFQDN(out String serverFQDN)
        {
            serverFQDN = String.Empty;
            try
            {
                //check if serverFQDN is already read in
                //member field Server if read return that value
                if (!String.IsNullOrEmpty(Server))
                {
                    //assign value from member field Server 
                    serverFQDN = Server;
                    return true;
                }

                const string SUBKEY_SERVER_FQDN = "ServiceFQDN";
                //Reading SUBKEY_SERVER_FQDN from registry
                if (!ReadRegValue(SUBKEY_SERVER_FQDN, ref serverFQDN))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNerrInstall);
                    TNHelper.DisplayDebugMessage(UserMessage.TNerrRegEntry);
                    return false;
                }

                if (String.Compare(serverFQDN, "none", true) == 0)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_SERVER_FQDN));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrConfigMsg, SUBKEY_SERVER_FQDN));
                    return false;
                }
                //save serverFQDN in member field Server
                Server = serverFQDN;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetServerPort(out String serverPort)
        {
            const int iPORT_MIN_VALUE = 1;
            const int iPORT_MAX_VALUE = 65535;
            serverPort = String.Empty;
            try
            {
                //check if serverPort is already read in
                //member field Port 
                if (!String.IsNullOrEmpty(Port))
                {
                    //assign value from member field Port and return
                    serverPort = Port;
                    return true;
                }

                const string SUBKEY_PORT = "ServicePort";
                //Reading SUBKEY_PORT from registry
                if (!ReadRegValue(SUBKEY_PORT, ref serverPort))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNerrInstall);
                    return false;
                }
                //Data validation 
                int iPort;
                if (!int.TryParse(serverPort, out iPort))
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_PORT));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrConfigMsg, SUBKEY_PORT));
                    return false;
                }

                if (iPort < iPORT_MIN_VALUE || iPort > iPORT_MAX_VALUE)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_PORT));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrPropOutofRange, SUBKEY_PORT, iPORT_MIN_VALUE, iPORT_MAX_VALUE));
                    return false;
                }
                //save serverPort in member field Port
                Port = serverPort;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetKeepAliveInterval(out String keepAlive)
        {
            const double dKEEPALIVE_MIN_SECONDS = 1;
            // 8 hours - 1 min hrs in seconds 28800s - 60s = 28740
            // substracting 1 min since TNServer has 8 hrs window for User access 
            const double dKEEPALIVE_MAX_SECONDS = 28740;
            keepAlive = String.Empty;
            try
            {
                //check if keepAlive is already read in
                //member field KeepAliveSeconds 
                if (!String.IsNullOrEmpty(KeepAliveSeconds))
                {
                    //assign value from member field KeepAliveSeconds and return
                    keepAlive = KeepAliveSeconds;
                    return true;
                }

                const string SUBKEY_KEEPALIVESECS = "IntervalKeepAliveSec";
                //Reading SUBKEY_KEEPALIVESECS from registry
                if (!ReadRegValue(SUBKEY_KEEPALIVESECS, ref keepAlive))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNerrInstall);
                    return false;
                }
                double dkeepAliveSecs;
                if (!double.TryParse(keepAlive, out dkeepAliveSecs))
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_KEEPALIVESECS));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrConfigMsg, SUBKEY_KEEPALIVESECS));
                    return false;
                }
                if (dkeepAliveSecs < dKEEPALIVE_MIN_SECONDS || dkeepAliveSecs > dKEEPALIVE_MAX_SECONDS)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_KEEPALIVESECS));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrPropOutofRange, SUBKEY_KEEPALIVESECS, dKEEPALIVE_MIN_SECONDS, dKEEPALIVE_MAX_SECONDS));
                    return false;
                }
                KeepAliveSeconds = keepAlive;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        internal Boolean GetServerSecretKey(out String secretKey)
        {
            secretKey = String.Empty;
            try
            {
                //Reading SUBKEY_API_KEY from registry
                if (!String.IsNullOrEmpty(ServiceSharedSecret))
                {
                    //assign value from member field ServiceSharedSecret and return
                    secretKey = ServiceSharedSecret;
                    return true;
                }
                const string SUBKEY_API_KEY = "ServiceSharedSecret";
                if (!ReadRegValue(SUBKEY_API_KEY, ref secretKey))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNerrInstall);
                    return false;
                }
                //Data validation 
                if (String.Compare(secretKey, "none", true) == 0)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_API_KEY));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrConfigMsg, SUBKEY_API_KEY));
                    return false;
                }
                if (secretKey.IndexOf(@"\") != -1)
                {
                    TNHelper.DisplayUserMessage(String.Format(UserMessage.TNerrpropNotSet, SUBKEY_API_KEY));
                    TNHelper.DisplayDebugMessage(String.Format(UserMessage.TNerrInvalidChars, SUBKEY_API_KEY));
                    return false;
                }
                //save secretKey in member field ServiceSharedSecret
                ServiceSharedSecret = secretKey;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }
    }
    /// <TNClientConfig>
    /// This class implements configuration related routines to properly configure 
    /// TNClient.
    /// </TNClientConfig>
    public class TNClientConfig
    {
        //An enumerated type for the control messages
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0, CTRL_BREAK_EVENT, CTRL_CLOSE_EVENT, CTRL_LOGOFF_EVENT = 5, CTRL_SHUTDOWN_EVENT
        }
        private static TNClientConfig Instance = null;
        private static readonly object ObjLock = new object();

        private System.Timers.Timer m_KeepAliveTimer = null;
        internal static String MachineID = String.Empty;

        Mutex InstanceMutex = null;
        private String AppGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        //declaring as volatile
        //so that The system always reads the current value of a volatile IsScriptRunning at the point it is requested
        //Also, the value of the IsScriptRunning is written immediately on assignment. 
        public static volatile bool IsScriptRunning = true;
        internal static volatile bool IsAddUserSuccessful = false;

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        #region FOR Powershell Window Close
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);
        public static HandlerRoutine s_handler = null;
        #endregion

        private TNClientConfig()
        {
        }

        public static TNClientConfig SingleInstance
        {
            get
            {
                try
                {
                    if (Instance == null)
                    {
                        lock (ObjLock)
                        {
                            if (Instance == null)
                            {
                                Instance = new TNClientConfig();
                            }
                        }
                    }
                    return Instance;
                }
                catch (Exception ex)
                {
                    TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                }
                return null;
            }
        }

        Boolean DisableQuickEditMode()
        {
            IntPtr consoleHandle = IntPtr.Zero;
            UInt32 consoleMode;
            const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
            const int STD_INPUT_HANDLE = -10;
            try
            {
                consoleHandle = GetStdHandle(STD_INPUT_HANDLE);
                if (IntPtr.Zero == consoleHandle)
                    return false;

                // get current console mode
                if (!GetConsoleMode(consoleHandle, out consoleMode))
                {
                    TNHelper.DisplayDebugMessage("Unable to get console mode.");
                    return false;
                }
                // Clear the quick edit bit in the mode flags
                consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
                // set the new mode
                if (!SetConsoleMode(consoleHandle, consoleMode))
                {
                    TNHelper.DisplayDebugMessage("Unable to set console mode");
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        // Will be called through powershell script
        public Boolean InitializeTN(String machineId, Boolean bLogsOn)
        {
            try
            {
                //return if tried to Initialize 
                //without Acquiring Mutex
                if (null == InstanceMutex)
                {
                    return false;
                }
                IsAddUserSuccessful = false;
                IsScriptRunning = true;
                TNMessageOps.IsDeleteInProgress = false;
                TNMessageOps.IsPutInProgress = false;
                MachineID = machineId;
                TNHelper.LogsOn = bLogsOn;
                // Disable quick edit mode
                if (!DisableQuickEditMode())
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsgquickEditNote);
                }
                else
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsgquickEditDisabled);
                }

                // Read registry entries
                if (!TNRegistryInfo.SingleInstance.CheckAndReadRegistry())
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsgRegReadFailed);
                    IsScriptRunning = false;
                    return false;
                }

                // create keepalive timer
                // perform event registration for logoff,  Ctrl and IP change
                if (!RegisterEvents())
                {
                    Boolean bSendDeleteReq = false;
                    Cleanup(UserMessage.TNMsgRestartPowerShell, bSendDeleteReq);
                    return false;
                }

                // Invoke AddUser() function to send first PUT request
                // to VEN server.
                TNMessageOps objTNMsgOps = new TNMessageOps();
                if (!objTNMsgOps.AddUser())
                {
                    Boolean bSendDeleteReq = false;
                    Cleanup(String.Empty, bSendDeleteReq);
                    return false;
                }
                TNHelper.DisplayUserMessage(UserMessage.TNMsgscriptRunning);
                TNHelper.DisplayUserMessage(UserMessage.TNMsgeventsActivatedMsg);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                IsScriptRunning = false;
                return false;
            }
            return true;
        }
        internal Boolean RegisterEvents()
        {
            Boolean bIsValid = false;
            try
            {
                String serverFQDN;
                if (!TNRegistryInfo.SingleInstance.GetServerFQDN(out serverFQDN))
                {
                    //error while reading ServerFQDN
                    TNHelper.DisplayUserMessage(UserMessage.TNevtRegistrationFailed);
                    bIsValid = false;
                    return bIsValid;
                }

                String serverPort;
                if (!TNRegistryInfo.SingleInstance.GetServerPort(out serverPort))
                {
                    //error while reading ServerPort
                    TNHelper.DisplayUserMessage(UserMessage.TNevtRegistrationFailed);
                    bIsValid = false;
                    return bIsValid;
                }
                TNHelper.DisplayUserMessage(String.Format(UserMessage.TNMsgconnectingTNS, serverFQDN,
                                            serverPort));
                TNClientConfig.SetHandler();
                SystemEvents.SessionEnding += new SessionEndingEventHandler(this.logOffEventHandler);
                TNHelper.DisplayUserMessage(UserMessage.TNMsgregisteredLogOff);
                NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(this.IpAddressChangedCallback);
                TNHelper.DisplayUserMessage(UserMessage.TNMsgregisteredIpChange);
                m_KeepAliveTimer = new System.Timers.Timer();
                m_KeepAliveTimer.AutoReset = true;
                String keepAliveValue;
                if (!TNRegistryInfo.SingleInstance.GetKeepAliveInterval(out keepAliveValue))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNevtRegistrationFailed);
                    return false;
                }
                m_KeepAliveTimer.Interval = TimeSpan.FromSeconds(double.Parse(keepAliveValue)).TotalMilliseconds;
                m_KeepAliveTimer.Elapsed += this.KeepAliveTimerHandler;
                TNHelper.DisplayUserMessage(UserMessage.TNMsgregisteredKeepAlive);
                m_KeepAliveTimer.Start();
                TNHelper.DisplayUserMessage(UserMessage.TNMsgkeepAliveStarted);
                bIsValid = true;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                bIsValid = false;
            }

            if (!bIsValid)
            {
                TNHelper.DisplayUserMessage(UserMessage.TNevtRegistrationFailed);
            }

            return bIsValid;
        }

        internal void UnregisterEvents()
        {
            TNHelper.DisplayUserMessage(UserMessage.TNMsgunregEvents);
            try
            {
                if (null != m_KeepAliveTimer)
                {
                    m_KeepAliveTimer.AutoReset = false;
                    m_KeepAliveTimer.Enabled = false;
                    m_KeepAliveTimer.Elapsed -= KeepAliveTimerHandler;
                    m_KeepAliveTimer.Stop();
                    m_KeepAliveTimer.Close();
                    m_KeepAliveTimer = null;
                }

                NetworkChange.NetworkAddressChanged -= new NetworkAddressChangedEventHandler(this.IpAddressChangedCallback);

                SystemEvents.SessionEnding -= new SessionEndingEventHandler(this.logOffEventHandler);

                TNClientConfig.SetConsoleCtrlHandler(null, false);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal Boolean Cleanup(String cleanupMsg, Boolean bSendDeleteReq = true)
        {
            if (TNMessageOps.IsDeleteInProgress || !IsScriptRunning)
            {
                return false;
            }
            Boolean bIsClean = false;
            try
            {
                if (bSendDeleteReq)
                {

                    //don't allow any PUT request after 
                    //IsDeleteInProgress is set to true
                    TNMessageOps.IsDeleteInProgress = true;
                    TNMessageOps objTNMsgOps = new TNMessageOps();
                    bIsClean = objTNMsgOps.Send(TNRequestType.DELETE);
                }
                TNHelper.DisplayUserMessage(cleanupMsg);
                UnregisterEvents();
                TNHelper.DisplayUserMessage(UserMessage.TNMsgGOOD_BYE);
                IsScriptRunning = false;
                bIsClean = true;
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                bIsClean = false;
            }
            return bIsClean;
        }

        void IpAddressChangedCallback(object sender, EventArgs e)
        {
            try
            {
                TNMessageOps objTNMsgOps = new TNMessageOps(UserMessage.TNevtIPAddressChangeMsg);
                objTNMsgOps.AddUser();
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void KeepAliveTimerHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                TNMessageOps objTNMsgOps = new TNMessageOps(UserMessage.TNevtKeepAliveMsg);
                objTNMsgOps.AddUser();
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void logOffEventHandler(object sender, Microsoft.Win32.SessionEndingEventArgs e)
        {
            try
            {
                String cleanupMsg = UserMessage.TNevtLogOffMsg;
                Cleanup(cleanupMsg);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        //static void Main(string[] args)
        //{
        //    TNClientConfig objEventMon = new TNClientConfig();
        //    if (objEventMon.AcquirePSMutex())
        //    {
        //        if (objEventMon.InitializeTN("machineId", false))
        //        {
        //            while (TNClientConfig.IsScriptRunning)
        //            {
        //                // Add a pause so the loop doesn't run super fast and use lots of CPU    
        //                Thread.Sleep(30000);
        //            }
        //        }
        //        objEventMon.ReleasePSMutex();
        //    }
        //}
        public Boolean AcquirePSMutex()
        {
            try
            {
                // Acquire PS single instance mutex. If already acquired then fail the function.
                InstanceMutex = new Mutex(false, AppGuid);
                if (!InstanceMutex.WaitOne(0, false))
                {
                    TNHelper.DisplayUserMessage(UserMessage.TNMsginstanceRunning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        public void ReleasePSMutex()
        {
            try
            {
                if (null != InstanceMutex)
                {
                    InstanceMutex.ReleaseMutex();
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (null != InstanceMutex)
                {
                    InstanceMutex.Close();
                    InstanceMutex.Dispose();
                }
            }
        }

        static void SetHandler()
        {
            try
            {
                if (s_handler == null)
                {
                    s_handler = new HandlerRoutine(ConsoleCtrlCheck);
                    SetConsoleCtrlHandler(s_handler, true);
                }
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            try
            {
                switch (ctrlType)
                {
                    case CtrlTypes.CTRL_C_EVENT:
                    case CtrlTypes.CTRL_BREAK_EVENT:
                    case CtrlTypes.CTRL_CLOSE_EVENT:
                        String cleanupMsg = UserMessage.TNMsginterrupt;
                        Boolean bSendDeleteReq = false;
                        if (IsAddUserSuccessful)
                        {
                            bSendDeleteReq = true;
                        }
                        SingleInstance.Cleanup(cleanupMsg, bSendDeleteReq);
                        break;
                    case CtrlTypes.CTRL_LOGOFF_EVENT:
                    case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                        break;
                }
                SetConsoleCtrlHandler(null, false);
            }
            catch (Exception ex)
            {
                TNHelper.DisplayDebugMessage(MethodBase.GetCurrentMethod().Name, ex);
            }
            return false;
        }
    }
}
"@

$IsMutexAcquired =[TNScope.TNClientConfig]::SingleInstance.AcquirePSMutex()

if ($IsMutexAcquired -eq $false)
{
    Write-Host "Exiting..."
    exit       
} 

$machineId =  WmiObject Win32_ComputerSystemProduct | Select-Object -ExpandProperty UUID

$TNconfigStatus = [TNScope.TNClientConfig]::SingleInstance.InitializeTN( $machineId, $logs )

if ($TNconfigStatus -eq $false)
{
    [TNScope.TNClientConfig]::SingleInstance.ReleasePSMutex()
    exit       
}         

try
{
    While ([TNScope.TNClientConfig]::IsScriptRunning)
    {   
        # Add a pause so the loop doesn't run super fast and use lots of CPU    
        Start-Sleep -Seconds 30  
    }
}
finally 
{
    [TNScope.TNClientConfig]::SingleInstance.ReleasePSMutex()
}

