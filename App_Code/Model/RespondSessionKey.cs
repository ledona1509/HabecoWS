namespace Model
{
    /// <summary>
    /// ResponseSessionKey contain SessionKey information when Client request a new connection to server
    /// </summary>
    public class ResponseSessionKey
    {
        public string SessionKey { get; set; }
        public int ResponseCode { get; set; }
        public ResponseSessionKey(string sessionKey, int respondCode)
        {
            SessionKey = sessionKey;
            ResponseCode = respondCode;
        }
        public ResponseSessionKey(){}
    }
}
