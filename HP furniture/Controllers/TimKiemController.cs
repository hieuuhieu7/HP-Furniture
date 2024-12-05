using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class TimKiemController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        public ActionResult Index()
        {
            return View();
        }
        public List<SanPham> SearchByKey(string key)
        {
            return db.SanPham.SqlQuery("SELECT * FROM SanPham WHERE TenSanPham LIKE @p0", "%" + key + "%").ToList();
        }
    }
}