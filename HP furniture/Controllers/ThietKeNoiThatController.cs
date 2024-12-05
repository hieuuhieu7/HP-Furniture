using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class ThietKeNoiThatController : Controller
    {
        // GET: ThietKeNoiThat
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GuiYeuCau(Models.ThietKeNoiThat obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ThietKeNoiThat.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index", "GuiYeuCauThanhCong");
            }
            return View(obj);
        }
    }
}