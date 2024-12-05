using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class GocCamHungController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: GocCamHung
        public ActionResult Index()
        {
            List<Models.GocCamHung> data = db.GocCamHung.ToList();
            ViewBag.Product = data;
            return View();
        }
    }
}