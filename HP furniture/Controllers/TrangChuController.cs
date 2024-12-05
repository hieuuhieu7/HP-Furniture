using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class TrangChuController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        // GET: TrangChu
        [HttpGet]
        public ActionResult Index()
        {
            // Bước 1: Lấy dữ liệu
            List<Models.SanPham> data = db.SanPham
                    .Where(sp => sp.ID_LoaiSanPham == 11)
                    .ToList();

            // Bước 2: Gán vào ViewBag
            ViewBag.Product = data;

            // Bước 3: Trả về View
            var guiYeuCauHoTroModel = new GuiYeuCauHoTro();
            return View(guiYeuCauHoTroModel);
        }

        [HttpPost]
        public ActionResult GuiYeuCauHoTro(Models.GuiYeuCauHoTro obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.GuiYeuCauHoTro.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index", "GuiYeuCauThanhCong");
            }
            return View(obj);
        }

        // POST: /TrangChu/TimKiem
        [HttpPost]
        public ActionResult TimKiem(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return RedirectToAction("Index", "TimKiem", new { key = key });
            }
            else
            {
                // Xử lý trường hợp khi không có từ khóa tìm kiếm được nhập vào
                return RedirectToAction("Index");
            }
        }
    }
}