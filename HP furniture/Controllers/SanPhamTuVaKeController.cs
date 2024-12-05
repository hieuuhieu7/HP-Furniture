using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class SanPhamTuVaKeController : Controller
    {
        // GET: SanPhamTuVaKe
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        public ActionResult Index()
        {
            List<Models.SanPham> data = db.SanPham
            .Where(sp => sp.ID_LoaiSanPham == 13)
            .ToList();
            ViewBag.Product = data;
            return View();
        }
    }
}