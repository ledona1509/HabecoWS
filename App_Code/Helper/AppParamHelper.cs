using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Helper
{
    /// <summary>
    /// Summary description for AppParamHelper
    /// </summary>
    public static class AppParamHelper
    {
        private const string AppParamPath = "~/App_Data/AppParam.xml";

        // Change Sessionkey Time Expired
        public static void ChangeTimeExpired(int newTime)
        {
            // Impplement later
        }

        // Add new client ip to whitelist IP
        public static void AddNewClientIp(String newClientIp)
        {
            // Impplement later        
        }
    
        // Delete client ip from whitelits IP
        public static void DeleteClientIp(String clientIp)
        {
            // Impplement later        
        }

        public static int GetSessionKeyTimeExpired()
        {
            int result = 0;
            using (XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(AppParamPath)))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "SessionKeyTimeExpired":
                                if (reader.Read())
                                    result = Convert.ToInt32(reader.Value.Trim());
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public static List<String> GetListClientIp()
        {
            var result = new List<string>();
            using (XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(AppParamPath)))
            {
                while(reader.Read())
                {
                    if(reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "IP":
                                if (reader.Read())
                                    result.Add(reader.Value.Trim());
                                break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
