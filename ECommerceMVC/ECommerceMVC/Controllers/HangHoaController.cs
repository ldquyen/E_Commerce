using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context) 
        {
            db = context;
        }

        public IActionResult Index(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(hh => hh.MaLoai == loai.Value);
            }
            var result = hangHoas.Select(res =>
            new HangHoaVM{
                Mahh = res.MaHh,
                Tenhh = res.TenHh,
                Dongia = res.DonGia ?? 0,
                Anh = res.Hinh ?? "",
                Mota = res.MoTaDonVi ?? "",
                TenLoai = res.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Search(string? searchname)
        {

            var hangHoas = db.HangHoas.AsQueryable();
            if (searchname != null)
            {
                hangHoas = hangHoas.Where(hh => hh.TenHh.Contains(searchname));
            }
            var result = hangHoas.Select(res =>
            new HangHoaVM
            {
                Mahh = res.MaHh,
                Tenhh = res.TenHh,
                Dongia = res.DonGia ?? 0,
                Anh = res.Hinh ?? "",
                Mota = res.MoTaDonVi ?? "",
                TenLoai = res.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Detail(int idDetail)
        {
            var data = db.HangHoas.Include(x => x.MaLoaiNavigation)
                .FirstOrDefault(x => x.MaHh == idDetail);
            if (data == null)
            {
                TempData["Message"] = "Không tìm thấy sản phẩm ";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                Mahh = data.MaHh,
                Tenhh = data.TenHh,
                Dongia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Anh = data.Hinh ?? string.Empty,
                Mota = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                TonKho = 10,
                Star = 5,
            };
            return View(result);
        }
    }
}
