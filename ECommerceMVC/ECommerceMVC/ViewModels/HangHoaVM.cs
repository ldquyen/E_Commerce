namespace ECommerceMVC.ViewModels
{
    public class HangHoaVM
    {
        public int Mahh { get; set; }
        public string Tenhh { get; set; }
        public string Anh {  get; set; }
        public double Dongia { get; set; }
        public string Mota { get; set; }
        public string TenLoai {  get; set; }
    }

    public class ChiTietHangHoaVM
    {
        public int Mahh { get; set; }
        public string Tenhh { get; set; }
        public string Anh { get; set; }
        public double Dongia { get; set; }
        public string Mota { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public int Star { get; set; }
        public int TonKho { get; set; }


    }
}
