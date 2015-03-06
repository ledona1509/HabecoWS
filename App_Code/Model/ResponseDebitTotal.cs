using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Summary description for ResponseDebitTotal
    /// </summary>
    public class ResponseDebitTotal
    {
        public string ResponseDate { get; set; }
        public List<DsCongNo> DebitTotal { get; set; }
        public int ResponseCode { get; set; }

        public ResponseDebitTotal()
        {
            ResponseDate = DateTime.Now.ToString("dd-MM-yyyy");
            DebitTotal = new List<DsCongNo>();
        }

        public ResponseDebitTotal(string responseDate, List<DsCongNo> debitTotal, int responseCode )
        {
            ResponseDate = responseDate;
            DebitTotal = debitTotal;
            ResponseCode = responseCode;
        }
    }
}
