using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;

        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(
                lo => new MenuLoaiVM
                {
                    Id = lo.MaLoai,
                    Name = lo.TenLoai,
                    Amount = lo.HangHoas.Count,
                }
                ).OrderBy(p => p.Amount);
            return View( data );
        }
    }
}
