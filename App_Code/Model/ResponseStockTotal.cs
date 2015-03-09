using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// ResponseStockTotal object
    /// </summary>
    public class ResponseStockTotal
    {
        public string ResponseDate { get; set; }
        public List<DsTonKho> StockTotal { get; set; }
        public int ResponseCode { get; set; }

        public ResponseStockTotal()
        {
            ResponseDate = DateTime.Now.ToString("dd-MM-yyyy");
            StockTotal = new List<DsTonKho>();
        }

        public ResponseStockTotal(string responseDate, List<DsTonKho> stockTotal, int responseCode)
        {
            ResponseDate = responseDate;
            StockTotal = stockTotal;
            ResponseCode = responseCode;
        }
    }
}
