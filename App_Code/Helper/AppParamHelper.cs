using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Helper
{
    /// <summary>
    /// AppParam.xml contain list IP can access (or using) API and a time number (in minute)
    /// to identify when the session key will be expired.
    /// AppParamHelper will manipulate AppParam.xml file
    /// </summary>
    public static class AppParamHelper
    {
        /// <summary>
        /// Location of AppParam.xml in system
        /// </summary>
        private const string AppParamPath = "~/App_Data/AppParam.xml";

        /// <summary>
        /// Change SessionKey's Time expired (in Minute) in AppParam.xml
        /// </summary>
        /// <param name="newTime">new value time (in Minute)</param>
        public static void ChangeTimeExpired(int newTime)
        {
            // Implement later
        }

        /// <summary>
        /// Add new Client Ip to white list in AppParam.xml
        /// </summary>
        /// <param name="newClientIp">New client Ip</param>
        public static void AddNewClientIp(String newClientIp)
        {
            // Implement later        
        }
    
        /// <summary>
        /// Delete client ip from white list in AppParam.xml
        /// </summary>
        /// <param name="clientIp">New client Ip</param>
        public static void DeleteClientIp(String clientIp)
        {
            // Implement later        
        }

        /// <summary>
        /// Get Session key's Time expired
        /// </summary>
        /// <returns>Time expired (in minute)</returns>
        public static int GetSessionKeyTimeExpired()
        {
            var result = 0;
            using (var reader = XmlReader.Create(HttpContext.Current.Server.MapPath(AppParamPath)))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement()) continue;
                    switch (reader.Name)
                    {
                        case "SessionKeyTimeExpired":
                            if (reader.Read())
                                result = Convert.ToInt32(reader.Value.Trim());
                            break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get list Client Ips can access (or using) API
        /// </summary>
        /// <returns>List Client Ip (String)</returns>
        public static List<String> GetListClientIp()
        {
            var result = new List<string>();
            using (var reader = XmlReader.Create(HttpContext.Current.Server.MapPath(AppParamPath)))
            {
                while(reader.Read())
                {
                    if (!reader.IsStartElement()) continue;
                    switch (reader.Name)
                    {
                        case "IP":
                            if (reader.Read())
                                result.Add(reader.Value.Trim());
                            break;
                    }
                }
            }
            return result;
        }
    }
}
