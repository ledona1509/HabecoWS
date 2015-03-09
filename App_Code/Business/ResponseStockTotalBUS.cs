using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Helper;
using Model;

namespace Business
{
    /// <summary>
    /// ResponseStockTotalBUS is reponsibility for ResponseStockTotal object
    /// </summary>
    public class ResponseStockTotalBUS
    {
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

        private const string SqlSelectFromKho = "select * from dmkho where ma_kho = @stite";
        private const string SqlSelectFromDonVi = "select * from dmdvcs where ma_dvcs = @cUnit";

        public ResponseStockTotalBUS(){}

        /// <summary>
        /// Get stock total
        /// </summary>
        /// <param name="dFrom">From date format dd/MM/yyyy</param>
        /// <param name="dTo">To date format dd/MM/yyyy</param>
        /// <param name="stite">Warehouse Code</param>
        /// <param name="cUnit">Branch code</param>
        /// <returns>ResponseStockTotal</returns>
        public ResponseStockTotal GetStockTotal(string dFrom, string dTo, string stite, string cUnit)
        {
            var responseStockTotal = new ResponseStockTotal();

            // Check dFrom
            try
            {
                var fromDate = DateTime.ParseExact(dFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dFrom = fromDate.ToString("yyyyMMdd");
            }
            catch (FormatException)
            {
                responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.DFromInvalid;
                return responseStockTotal;
            }
            // Check dTo
            try
            {
                var toDate = DateTime.ParseExact(dTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dTo = toDate.ToString("yyyyMMdd");
            }
            catch (FormatException)
            {
                responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.DToInvalid;
                return responseStockTotal;
            }

            // Check cUnit
            if (cUnit != "" && !DatabaseHelper.ExistDataFromDb(DatabaseHelper.Connect(), SqlSelectFromDonVi, new Dictionary<string, string> { { "@cUnit", cUnit } }))
            {
                responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.CUnitInvalid;
                return responseStockTotal;
            }

            // Check stite
            if (stite != "" && !DatabaseHelper.ExistDataFromDb(DatabaseHelper.Connect(), SqlSelectFromKho, new Dictionary<string, string> { { "@stite", stite } }))
            {
                responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.STiteInvalid;
                return responseStockTotal;
            }

            using (var conn = DatabaseHelper.Connect())
            {
                // Check connection success or not
                if (conn == null)
                {
                    responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.ConnectionFail;
                    return responseStockTotal;
                }
                using (var command = new SqlCommand("zcthanh_phamx", conn) { CommandType = CommandType.StoredProcedure })
                {
                    //Pass the parameter to procedure
                    command.Parameters.AddWithValue("dFrom", dFrom);
                    command.Parameters.AddWithValue("dTo", dTo);
                    command.Parameters.AddWithValue("Site", stite);
                    command.Parameters.AddWithValue("dPFrom", "");
                    command.Parameters.AddWithValue("dPTo ", "");
                    command.Parameters.AddWithValue("cUnit", cUnit);
                    command.Parameters.AddWithValue("cForm", "zcthanh_pham");

                    try
                    {
                        // Check ExecuteReader success or not
                        using (var reader = command.ExecuteReader())
                        {
                            //read the data
                            while (reader.Read())
                            {
                                var dsTonKho = new DsTonKho
                                {
                                    STT = Convert.ToInt32(reader["stt"].ToString()),
                                    TonDau = Convert.ToInt32(reader["sl_ton_dau_tt"].ToString()),
                                    NhapNB = Convert.ToInt32(reader["sl_n_noi_bo"].ToString()),
                                    NhapTCTY = Convert.ToInt32(reader["sl_n_tcty"].ToString()),
                                    NhapKhac = Convert.ToInt32(reader["sl_n_khac"].ToString()),
                                    XuatBan = Convert.ToInt32(reader["sl_x_ban_tt"].ToString()),
                                    XuatKM = Convert.ToInt32(reader["sl_km"].ToString()),
                                    XuatHT = Convert.ToInt32(reader["sl_ht"].ToString()),
                                    XuatNB = Convert.ToInt32(reader["sl_x_noi_bo"].ToString()),
                                    XuatKhac = Convert.ToInt32(reader["sl_x_khac"].ToString()),
                                    TonCuoi = Convert.ToInt32(reader["Ton_cuoi_tt"].ToString()),
                                    TenVattu = reader["chi_tieu"].ToString()
                                };
                                responseStockTotal.StockTotal.Add(dsTonKho);
                            }
                            responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.Success;
                            return responseStockTotal;
                        }
                    }
                    catch (Exception)
                    {
                        // ExecuteReader fail to execute
                        responseStockTotal.ResponseCode = (int)EResponseStockTotalCode.Fail;
                        return responseStockTotal;
                    }
                }
            }
        }

        /// <summary>
        /// Get fake List DsTonKho for test
        /// </summary>
        /// <returns>List DsTonKho</returns>
        private static List<DsTonKho> GetMockListDsTonKho()
        {
            var dsTonKho = new List<DsTonKho>
                               {
                                   new DsTonKho(1, "Vật Tư 1", 123, 321, 123, 321, 123, 321, 123, 321, 123, 321),
                                   new DsTonKho(2, "Vật Tư 11", 123, 321, 123, 321, 123, 321, 123, 321, 123, 321)
                               };
            return dsTonKho;
        }
    }
}
