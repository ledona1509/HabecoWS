using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Business;
using Helper;
using Model;

namespace Controller
{
    /// <summary>
    /// Summary description for Worker
    /// </summary>
    public class Worker
    {
        enum ERespondSessionKey
        {
            Success = 1, InvalidIp
        }

        enum EResponseStockTotalCode
        {
            Success = 1
            ,Fail = 0
            ,SessionKeyInvalid = 2
            ,DFromInvalid = 3
            ,DToInvalid = 4
            ,STiteInvalid = 5
            ,CUnitInvalid = 6
            ,ConnectionFail = 20
            ,TimeOut = 7
        }

        enum EResponseDebitTotalCode
        {
            Success = 1
            ,Fail = 0
            ,SessionKeyInvalid = 2
            ,DFromInvalid = 3
            ,DToInvalid = 4
            ,CUnitInvalid = 6
            ,CCustInvalid = 8
            ,ConnectionFail = 20
            ,TimeOut = 7
        }

        #region SetConnection
        /// <summary>
        /// Set new connection for Client
        /// </summary>
        /// <param name="clientIp">IP of Client</param>
        /// <returns>ResponseSessionKey</returns>
        public ResponseSessionKey SetConnection(string clientIp)
        {
            var result = new ResponseSessionKey();

            // Check client ip
            if(ClientHelper.IsClientIpValid(clientIp))
            {
                // ClientIP is valid
                var responseSessionKeyBUS = new ResponseSessionKeyBUS();
                result = responseSessionKeyBUS.GenerateSessionKey();
                result.ResponseCode = (int)ERespondSessionKey.Success;

                ClientHelper.UpdateListClientIpWithSessionKey(clientIp, result.SessionKey);
            }
            // ClientIP is invalid
            else
            {
                result.SessionKey = String.Empty;
                result.ResponseCode = (int)ERespondSessionKey.InvalidIp;
            }
            return result;
        }
        #endregion

        #region VerifySessionKey
        /// <summary>
        /// Verify session key is valid or not
        /// </summary>
        /// <param name="sessionKey">sessionkey</param>
        /// <returns>bool type</returns>
        private static bool VerifySessionKey(string sessionKey)
        {
            return ClientHelper.IsSessionKeyValid(sessionKey) && !ClientHelper.IsSessionKeyHasExpired(sessionKey);
        }
        #endregion


        /// <summary>
        /// Get debit total
        /// </summary>
        /// <param name="dFrom">From date format dd/MM/yyyy</param>
        /// <param name="dTo">To date format dd/MM/yyyy</param>
        /// <param name="cUnit">Branch code</param>
        /// <param name="cCust">Customer code</param>
        /// <param name="sessionKey">Session key</param>
        /// <returns>ResponseDebitTotal object</returns>
        public ResponseDebitTotal GetDebitTotal(string dFrom, string dTo, string cUnit, string cCust, string sessionKey)
        {
            var responseDebitTotal = new ResponseDebitTotal();
            
            // Check session key
            if (!VerifySessionKey(sessionKey))
            {
                // Session key is invalid
                responseDebitTotal.ResponseCode = (int) EResponseDebitTotalCode.SessionKeyInvalid;
            }
            else
            {
                // Session key is valid
                var responseDebitTotalBUS = new ResponseDebitTotalBUS();
                responseDebitTotal = responseDebitTotalBUS.GetDebitTotal(dFrom, dTo, cUnit, cCust);
            }
            return responseDebitTotal;
        }

        /// <summary>
        /// Get stock total
        /// </summary>
        /// <param name="dFrom">From date format dd/MM/yyyy</param>
        /// <param name="dTo">To date format dd/MM/yyyy</param>
        /// <param name="stite">Warehouse Code</param>
        /// <param name="cUnit">Branch code</param>
        /// <param name="sessionKey">Session key</param>
        /// <returns>ResponseStockTotal</returns>
        public ResponseStockTotal GetStockTotal(string dFrom, string dTo, string stite, string cUnit, string sessionKey)
        {
            var responseStockTotal = new ResponseStockTotal();
            // Check session key
            if (!VerifySessionKey(sessionKey))
            {
                // Session key is invalid
                responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.SessionKeyInvalid;
            }
            else
            {
                // Session key is valid
                var responseStockTotalBUS = new ResponseStockTotalBUS();
                responseStockTotal = responseStockTotalBUS.GetStockTotal(dFrom, dTo, stite, cUnit);
            }
            return responseStockTotal;
        }

        /// <summary>
        /// For test API
        /// </summary>
        /// <param name="yourName">Input your name</param>
        /// <returns>Print out your name</returns>
        public string TestAPIForDummy(string yourName)
        {
            return "Xin chào " + yourName;
        }
    }
}
