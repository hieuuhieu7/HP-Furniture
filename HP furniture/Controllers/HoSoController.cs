using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class HoSoController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        public ActionResult Index()
        {
            List<Models.KhachHang> data = db.KhachHang.ToList();
            ViewBag.Product = data;
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase avt, KhachHang model)
        {
            var user = (KhachHang)Session["khach"];
            if (user == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            if (avt != null && avt.ContentLength > 0)
            {
                var fileName = Path.GetFileName(avt.FileName);
                var uploadsDir = Server.MapPath("~/Uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }
                var path = Path.Combine(uploadsDir, fileName);
                avt.SaveAs(path);

                // Lưu ảnh mới
                user.Avt = "/Uploads/" + fileName;
            }

            // Cập nhật thông tin cá nhân chỉ khi có sự thay đổi
            if (!string.IsNullOrEmpty(model.TenKhachHang))
            {
                user.TenKhachHang = model.TenKhachHang;
            }
            if (!string.IsNullOrEmpty(model.SDT_KH))
            {
                user.SDT_KH = model.SDT_KH;
            }
            if (!string.IsNullOrEmpty(model.DiaChi_KH))
            {
                user.DiaChi_KH = model.DiaChi_KH;
            }
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.Username = model.Username;
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }

            // Lưu lại thông tin người dùng vào session hoặc database tùy theo yêu cầu của bạn
            Session["khach"] = user;

            // Cập nhật thông tin trong database nếu cần
            var dbUser = db.KhachHang.Find(user.ID);
            if (dbUser != null)
            {
                dbUser.TenKhachHang = user.TenKhachHang;
                dbUser.SDT_KH = user.SDT_KH;
                dbUser.DiaChi_KH = user.DiaChi_KH;
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.Avt = user.Avt;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "HoSo"); // Điều hướng về trang hồ sơ hoặc trang chủ
        }
    }
}