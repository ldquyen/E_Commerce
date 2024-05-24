using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private static List<HangHoaVM> NowList;
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context)
        {
            db = context;
        }

        public IActionResult Index(int? loai, int sortOrder)
        {
            if(sortOrder == 0)
            {
                var hangHoas = db.HangHoas.AsQueryable();
                if (loai.HasValue)
                {
                    hangHoas = hangHoas.Where(hh => hh.MaLoai == loai.Value);
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
                }).ToList();

                NowList = result;
            }
            else if (sortOrder == 1)
            {
                NowList = NowList.OrderBy(x => x.Dongia).ToList();
            }
            else if (sortOrder == 2)
            {
                NowList = NowList.OrderByDescending(x => x.Dongia).ToList();
            }

            return View(NowList);
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
            }).ToList();
            NowList = result;
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
        //private void EnsureNowListIsPopulated()
        //{
        //    if (NowList == null)
        //    {
        //        var hangHoas = db.HangHoas.Select(res => new HangHoaVM
        //        {
        //            Mahh = res.MaHh,
        //            Tenhh = res.TenHh,
        //            Dongia = res.DonGia ?? 0,
        //            Anh = res.Hinh ?? "",
        //            Mota = res.MoTaDonVi ?? "",
        //            TenLoai = res.MaLoaiNavigation.TenLoai
        //        }).ToList();

        //        NowList = hangHoas;
        //    }
        //}
        //public IActionResult SortList(int sortOrder)
        //{
        //    var result = NowList;
        //    if (sortOrder == 0)
        //    {
        //        result = NowList;
        //    }
        //    else if (sortOrder == 1)
        //    {
        //        result = NowList.OrderBy(x => x.Dongia).ToList();
        //    }
        //    else if (sortOrder == 2)
        //    {
        //        result = NowList.OrderByDescending(x => x.Dongia).ToList();
        //    }
        //    return View(result);
        //}
    }
}
