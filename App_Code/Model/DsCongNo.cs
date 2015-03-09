namespace Model
{
    /// <summary>
    /// DsCongNo object
    /// </summary>
    public class DsCongNo
    {
        public DsCongNo()
        {
            DK_PSCo = 0;
            DK_PSNo = 0;
            TK_PSCo = 0;
            TK_PSNo = 0;
            BaoLanh = 0;
            CK_PSCo = 0;
            CK_PSNo = 0;
        }
        public DsCongNo(int stt, string maKH, string tenKH, string taiKhoan
            , int dk_PSCo, int dk_PSNo, int tk_PSCo, int tk_PSNo
            , int baoLanh, int ck_PSCo, int ck_PSNo)
        {
            STT = stt;
            MaKH = maKH;
            TenKH = tenKH;
            TaiKhoan = taiKhoan;
            DK_PSCo = dk_PSCo;
            DK_PSNo = dk_PSNo;
            TK_PSCo = tk_PSCo;
            TK_PSNo = tk_PSNo;
            BaoLanh = baoLanh;
            CK_PSCo = ck_PSCo;
            CK_PSNo = ck_PSNo;
        }

        public int STT { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string TaiKhoan { get; set; }
        public int DK_PSCo { get; set; }
        public int DK_PSNo { get; set; }
        public int TK_PSCo { get; set; }
        public int TK_PSNo { get; set; }
        public int BaoLanh { get; set; }
        public int CK_PSCo { get; set; }
        public int CK_PSNo { get; set; }
    }
}
