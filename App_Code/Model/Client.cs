using System;

namespace Model
{
    /// <summary>
    /// Client object representative for a client include : ip, session key and the time
    /// when session key will be expired
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
