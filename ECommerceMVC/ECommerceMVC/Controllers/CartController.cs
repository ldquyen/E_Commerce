using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using ECommerceMVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;

        public CartController(Hshop2023Context context) {
            db = context;
        }
        
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.FirstOrDefault(p => p.Mahh == id);
            if (item == null)
            {
                var hangmoi = db.HangHoas.SingleOrDefault(x => x.MaHh == id);
                if(hangmoi == null)
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm để thêm vào giỏ hàng";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Mahh = hangmoi.MaHh,
                    Ten = hangmoi.TenHh,
                    Anh = hangmoi.Hinh ?? string.Empty,
                    DonGia = hangmoi.DonGia ?? 0,
                    SoLuong = quantity,
                    
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(x => x.Mahh == id);
            if(item == null)
            {
                TempData["Message"] = "Không xóa được";
                return Redirect("/404");
            }
            gioHang.Remove(item);

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            return RedirectToAction("Index");
        }
    }
}
