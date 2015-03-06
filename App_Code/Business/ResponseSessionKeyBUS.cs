using System;
using Helper;
using Model;

namespace Business
{
    /// <summary>
    /// Summary description for ResponseSessionKeyBUS
    /// </summary>
    public class ResponseSessionKeyBUS
    {
        public ResponseSessionKeyBUS(){}

        /// <summary>
        /// Generate new SessionKey for Client based on GUID in .NET
        /// </summary>
        /// <returns>ResponseSessionKey object</returns>
        public ResponseSessionKey GenerateSessionKey()
        {
            var result = new ResponseSessionKey();
            var guid = Guid.NewGuid();
            result.SessionKey = guid.ToString();
            return result;                
        }
    }
}
