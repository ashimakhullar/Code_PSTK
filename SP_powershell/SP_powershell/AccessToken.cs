// Author(s): 
// Ashima Bahl, abahl@cisco.com 
using System;
using System.Linq;


namespace Cisco.Runbook
{
    public class AccessToken
    {
        private string username;
        private string password;
        private string client_id;
        private string client_secret;
        private string redirect_uri;


        public string GetAccessToken(string Server)
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
    }

}
