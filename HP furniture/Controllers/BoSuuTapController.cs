using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class BoSuuTapController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: BoSuuTap
        public ActionResult Index()
        {
            List<Models.BoSuuTap> data = db.BoSuuTap.ToList();
            ViewBag.Product = data;
            return View();
        }
    }
}