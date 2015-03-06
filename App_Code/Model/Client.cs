using System;

namespace Model
{
    /// <summary>
    /// Summary description for Client
    /// </summary>
    public class Client
    {
        public string ClientIp { get; set; }
        public string SessionKey { get; set; }
        public DateTime TimeExpired { get; set; }

        public Client(){}

        public Client(string clientIp, string sessionKey, DateTime timeExpired)
        {
            ClientIp = clientIp;
            SessionKey = sessionKey;
            TimeExpired = timeExpired;
        }
    }
}
