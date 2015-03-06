using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Controller;
using Helper;
using Model;

namespace Business
{
    /// <summary>
    /// ResponseDebitTotalBUS is responsibility for ResponseDebitTotal object
    /// </summary>
    public class ResponseDebitTotalBUS
    {
        public ResponseDebitTotalBUS(){}

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
            , TimeOut = 7
        }

        private const string SqlSelectFromKh = "select * from dmkh where ma_kh = @cCust";
        private const string SqlSelectFromDonVi = "select * from dmdvcs where ma_dvcs = @cUnit";

        /// <summary>
        /// Get debit total
        /// </summary>
        /// <param name="dFrom">From date format dd/MM/yyyy</param>
        /// <param name="dTo">To date format dd/MM/yyyy</param>
        /// <param name="cUnit">Branch code</param>
        /// <param name="cCust">Customer code</param>
        /// <returns>ResponseDebitTotal object</returns>
        public ResponseDebitTotal GetDebitTotal(string dFrom, string dTo, string cUnit, string cCust)
        {
            var responseDebitTotal = new ResponseDebitTotal();
            
            // Check dFrom
            try
            {
                var fromDate = DateTime.ParseExact(dFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dFrom = fromDate.ToString("yyyyMMdd");
            }
            catch (FormatException)
            {
                responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.DFromInvalid;
                return responseDebitTotal;
            }
            // Check dTo
            try
            {
                var toDate = DateTime.ParseExact(dTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dTo = toDate.ToString("yyyyMMdd");
            }
            catch (FormatException)
            {
                responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.DToInvalid;
                return responseDebitTotal;
            }

            // Check cUnit
            if (cUnit != "" && !DatabaseHelper.ExistDataFromDb(DatabaseHelper.Connect(), SqlSelectFromDonVi, new Dictionary<string, string> { { "@cUnit", cUnit } }))
            {
                responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.CUnitInvalid;
                return responseDebitTotal;
            }

            // Check cCust
            if (cCust != "" && !DatabaseHelper.ExistDataFromDb(DatabaseHelper.Connect(), SqlSelectFromKh, new Dictionary<string, string> { { "@cCust", cCust } }))
            {
                responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.CCustInvalid;
                return responseDebitTotal;
            }

            using (var conn = DatabaseHelper.Connect())
            {
                // Check connection success or not
                if (conn == null)
                {
                    responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.ConnectionFail;
                    return responseDebitTotal;
                }
                using (var command = new SqlCommand("fs_AcctsCustomers_DMS", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    //Pass the parameter to procedure
                    command.Parameters.AddWithValue("dFrom", dFrom);
                    command.Parameters.AddWithValue("dTo", dTo);
                    command.Parameters.AddWithValue("cUnit", cUnit);
                    command.Parameters.AddWithValue("cAcct", "131");
                    command.Parameters.AddWithValue("cCust ", cCust);
                    command.Parameters.AddWithValue("Group1", "");
                    command.Parameters.AddWithValue("Group2", "");
                    command.Parameters.AddWithValue("Group3", "");
                    command.Parameters.AddWithValue("Groups", "");
                    command.Parameters.AddWithValue("cOrder", "ten_kh");
                    command.Parameters.AddWithValue("strKey0", "1 = 1");
                    command.Parameters.AddWithValue("strKey1", "");
                    command.Parameters.AddWithValue("strKey2", "");
                    command.Parameters.AddWithValue("strKey3", "");
                    command.Parameters.AddWithValue("strKey4", "");
                    command.Parameters.AddWithValue("strKey5", "");
                    command.Parameters.AddWithValue("strKey6", "");
                    command.Parameters.AddWithValue("strKey7", "");
                    command.Parameters.AddWithValue("strKey8", "");
                    command.Parameters.AddWithValue("strKey9", "");

                    try
                    {
                        // Check ExecuteReader success or not
                        using (var reader = command.ExecuteReader())
                        {
                            //read the data
                            while (reader.Read())
                            {
                                // var t = reader["column_name"].ToString();
                                // Console.WriteLine(reader["no_dk"].ToString());
                            }

                            // create fake List DsCongNo for test
                            responseDebitTotal.DebitTotal = GetMockResponseDebitTotal();

                            responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.Success;
                            return responseDebitTotal;
                        }
                    }
                    catch (Exception)
                    {
                        // ExecuteReader fail to execute
                        responseDebitTotal.ResponseCode = (int)EResponseDebitTotalCode.Fail;
                        return responseDebitTotal;
                    }

                }
            }
        }

        /// <summary>
        /// Get fake List DsCongNo for test
        /// </summary>
        /// <returns>List DsCongNo</returns>
        private static List<DsCongNo> GetMockResponseDebitTotal()
        {
            var dsCongNo = new List<DsCongNo>
                               {
                                   new DsCongNo(1, "KH001", "Dona", "0123", 123, 321, 123, 321, 123, 321, 123),
                                   new DsCongNo(2, "KH002", "Dona", "0123", 123, 321, 123, 321, 123, 321, 123)
                               };
            return dsCongNo;
        }
    }
}
