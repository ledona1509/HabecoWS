namespace Model
{
    /// <summary>
    /// Summary description for DsTonKho
    /// </summary>
    public class DsTonKho
    {
        public DsTonKho()
        {
            TonDau = 0;
            NhapTCTY = 0;
            NhapNB = 0;
            NhapKhac = 0;
            XuatBan = 0;
            XuatNB = 0;
            XuatKM = 0;
            XuatHT = 0;
            XuatKhac = 0;
            TonCuoi = 0;
        }
        public DsTonKho(int stt, string tenVattu, int tonDau, int nhapTCTY
            , int nhapNB, int nhapKhac, int xuatBan, int xuatNB
            ,int xuatKM, int xuatHT, int xuatKhac, int tonCuoi)
        {
            STT = stt;
            TenVattu = tenVattu;
            TonDau = tonDau;
            NhapTCTY = nhapTCTY;
            NhapNB = nhapNB;
            NhapKhac = nhapKhac;
            XuatBan = xuatBan;
            XuatNB = xuatNB;
            XuatKM = xuatKM;
            XuatHT = xuatHT;
            XuatKhac = xuatKhac;
            TonCuoi = tonCuoi;
        }

        public int STT { get; set; }
        public string TenVattu { get; set; }
        public int TonDau { get; set; }
        public int NhapTCTY { get; set; }
        public int NhapNB { get; set; }
        public int NhapKhac { get; set; }
        public int XuatBan { get; set; }
        public int XuatNB { get; set; }
        public int XuatKM { get; set; }
        public int XuatHT { get; set; }
        public int XuatKhac { get; set; }
        public int TonCuoi { get; set; }
    }
}
