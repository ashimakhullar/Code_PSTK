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
    [Cmdlet(VerbsCommon.Get, "Script")]
    [OutputType(typeof(MapPairErToReplicationPeerInfo), typeof(ReplicationDatastore))]

    public class GetScript : SPCmdlet
    {

        // 
        // Properties (PowerShell Parameters) to be defined below
        //

        [Parameter(Mandatory = true)]
        [Alias("srvr")]
        public string Server { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Primary")]
        public string PrServer { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Secondary")]
        public string ReServer { get; set; }

        //VMUid will pass the VM uid
        [Parameter()]
        [ValidateNotNullOrEmpty]
          public string VM { get; set; }

        //
        // Cmdlet body
        //
        protected override void ProcessSPRecord()
        {
            if (ValidateParameters() == false)
                return;
            try
            {
                Configuration.Default = new Configuration();
                Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";
                //if ((Server == null) && (ConnectHXServer.storageKeyDictionary == null))
                //{
                //    throw new Exception("No server is connected.");
                //}
                ////var valServer = "";
                //if (Server != null)
                //{
                //    Server = Server.ToString().Trim();
                //}
                //else if (ConnectHXServer.storageKeyDictionary != null)
                //{
                //    var firstElement = ConnectHXServer.storageKeyDictionary.FirstOrDefault();
                //    Server = firstElement.Key;
                //}

                //var apiString = "https://" + Server.ToString().Trim() + "/dataprotection/v1";
                //var apiInstance = new RecoveryApi(apiString);
                //var num = 0;
                //String accessTkn = "";

                //dynamic dictServerCnnctd = null;
                //if (ConnectHXServer.storageKeyDictionary != null)
                //{
                //    num = ConnectHXServer.storageKeyDictionary.Count();
                //    if (num == 1)
                //    {

                //        dictServerCnnctd = ConnectHXServer.storageKeyDictionary.First(x => x.Key == Server.ToString()).Value;

                //    }
                //    else
                //    {
                //        dictServerCnnctd = ConnectHXServer.storageKeyDictionary.FirstOrDefault(x => x.Key == Server.ToString()).Value;
                //    }
                //}
                //else
                //{
                //    num = 0;
                //    throw new Exception("Please connect to a server.");
                //}




                //if (dictServerCnnctd != null)
                //{
                //    accessTkn = dictServerCnnctd.TokenType + " " + dictServerCnnctd.AccessToken;
                //}
                //else
                //{
                //    throw new Exception("The Server is not connected;Please check the IP address of Server");
                //}

                var lineToWrite = "";

                string[] lines = { "First line", "Second line", "Third line" };
                List<string> cmdList = new List<string>();

                // Add items using Add method 
               
                if (PrServer != null)
                {
                    lineToWrite = "$Primary=Connect-HXServer -Server "+ PrServer.ToString() + " -Username local/root -Password Cisco123";
                    cmdList.Add(lineToWrite);
                }
                if (ReServer != null)
                {
                    lineToWrite = "$Secondary=Connect-HXServer -Server " + ReServer.ToString() + " -Username local/root -Password Cisco123";
                    cmdList.Add(lineToWrite);
                }
                if (VM != null)
                {
                    lineToWrite = "$vm=Get-ProtectedVM -Server "+ PrServer.ToString()  + " -VMname  " + VM.ToString() ;
                    cmdList.Add(lineToWrite);
                }

                cmdList.Add("Set-TestFailovernew -Server 10.198.5.221 -VMId $vm[0].er.id -TestNetwork \"VM Network\" -NewName ah");
                cmdList.Add("#fail for primary");
                cmdList.Add("Set-PrepareFailover -Server 10.198.5.219 -VMId $vm[0].er.id ");


                cmdList.Add("#new name");
                cmdList.Add("Set-Failovernew -Server 10.198.5.221 -VMId $vm[0].er.id -NetworkMap \"Storage Controller Data Network: VM Network\" -NewName ash-failover");


                cmdList.Add("Set-PrepareReverseProtect -Server 10.198.5.221 -VMId $vm[0].er.id ");

                cmdList.Add("Get-RecoverTask -Server 10.198.5.221 -VMId $vm[0].er.id  | Format-Table");

                cmdList.Add("Get-RecoverTask -Server 10.198.5.219 -VMId $vm[0].er.id  | Format-Table");

                cmdList.Add("Set-ReverseProtect -Server 10.198.5.219 -VMId $vm[0].er.id ");
                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllLines(@"C:\temp\script.txt", cmdList);


                // Example #2: Write one string to a text file.
                string text = "A class is the most powerful data type in C#. Like a structure, " +
                               "a class defines the data and behavior of the data type. ";
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllText(@"C:\temp\WriteText.txt", text);

                // Example #3: Write only some strings in an array to a file.
                // The using statement automatically flushes AND CLOSES the stream and calls 
                // IDisposable.Dispose on the stream object.
                // NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
                // encodes the output as text.
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\temp\WriteLines2.txt"))
                {
                    foreach (string line in lines)
                    {
                        // If the line doesn't contain the word 'Second', write the line to the file.
                        if (!line.Contains("Second"))
                        {
                            file.WriteLine(line);
                        }
                    }
                }

                // Example #4: Append new text to an existing file.
                // The using statement automatically flushes AND CLOSES the stream and calls 
                // IDisposable.Dispose on the stream object.
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\temp\WriteLines2.txt", true))
                {
                    file.WriteLine("Fourth line");
                }
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

        private void WritePeerRecord(List<MapPairErToReplicationPeerInfo> result)
        {
            List<MapPairErToReplicationPeerInfo> list = new List<MapPairErToReplicationPeerInfo>();

            foreach (var item in result)
            {
                if (item.PeerInfo != null)
                {
                    WriteObject(item.PeerInfo);
                    WriteObject("---------------------------------------------------------------------");
                }
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
