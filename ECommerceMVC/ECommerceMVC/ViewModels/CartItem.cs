namespace ECommerceMVC.ViewModels
{
    public class CartItem
    {
        public int Mahh { get; set; }
        public string Anh {  get; set; }
        public string Ten { get; set; }
        public double DonGia {  get; set; }
        public int SoLuong {  get; set; }
        public double Tong => DonGia * SoLuong;
    }
}
