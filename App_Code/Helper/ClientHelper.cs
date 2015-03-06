using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Model;

namespace Helper
{
    /// <summary>
    /// Summary description for ClientHelper
    /// </summary>
    public class ClientHelper
    {
        private const string ClientDataPath = "~/App_Data/ClientData.xml";

        /// <summary>
        /// Get List Client from file
        /// </summary>
        /// <returns>List Client object</returns>
        public static List<Client> LoadClientDataHelper()
        {
            return SerializeObjectHelper.DeserializeObject<List<Client>>(ClientDataPath);
        }

        /// <summary>
        /// Save List Client to file
        /// </summary>
        /// <param name="clients">List clients</param>
        public static void SaveClientDataHelper(List<Client> clients)
        {
            SerializeObjectHelper.SerializeObject(clients, ClientDataPath);
        }

        /// <summary>
        /// Update List ClientIP along with SessionKey from file
        /// </summary>
        /// <param name="clientIp">IP of client</param>
        /// <param name="newSessionKey">New SessionKey to update</param>
        public static void UpdateListClientIpWithSessionKey(string clientIp, string newSessionKey)
        {
            // Get List ClientIP
            var clients = LoadClientDataHelper();
            // Add new List ClientIP when it is empty
            if (clients == null)
            {
                clients = new List<Client>();
                var client = new Client(clientIp, newSessionKey,
                                        DateTime.Now.AddMinutes(AppParamHelper.GetSessionKeyTimeExpired()));
                clients.Add(client);
            }
            // Update List ClientIP when it is not empty
            else
            {
                var isExist = false;
                // ClientIP has existed in List ClientIP
                foreach (var client in clients.Where(client => client.ClientIp == clientIp))
                {
                    client.SessionKey = newSessionKey;
                    client.TimeExpired = DateTime.Now.AddMinutes(AppParamHelper.GetSessionKeyTimeExpired());
                    isExist = true;
                    break;
                }
                // ClientIP not exist in List ClientIP => Add new
                if(!isExist)
                {
                    var client = new Client(clientIp, newSessionKey,
                                           DateTime.Now.AddMinutes(AppParamHelper.GetSessionKeyTimeExpired()));
                    clients.Add(client);
                    
                }
            }
            // Save List ClientIP to file
            SaveClientDataHelper(clients);
        }

        /// <summary>
        /// Check IP of Client is valid or not
        /// </summary>
        /// <param name="clientIp">IP of client</param>
        /// <returns>True or False</returns>
        public static bool IsClientIpValid(string clientIp)
        {
            var listIp = AppParamHelper.GetListClientIp();
            return listIp.Contains(clientIp);
        }

        /// <summary>
        /// Check Client's SessionKey is valid or not
        /// </summary>
        /// <param name="sessionKey">SessionKey</param>
        /// <returns>True or False</returns>
        public static bool IsSessionKeyValid(string sessionKey)
        {
            var clients = LoadClientDataHelper();
            return clients != null && clients.Any(client => client.SessionKey == sessionKey);
        }

        /// <summary>
        /// Check Client's SessionKey is expired or not
        /// </summary>
        /// <param name="sessionKey">SessionKey</param>
        /// <returns>True of False</returns>
        public static bool IsSessionKeyHasExpired(string sessionKey)
        {
            var clients = LoadClientDataHelper();
            return clients != null && clients.Any(client => client.SessionKey == sessionKey && DateTime.Compare(client.TimeExpired, DateTime.Now) < 0);
        }
    }
}
