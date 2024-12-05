using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HP_furniture.Controllers
{
    public class DangNhapController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: DangNhap
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var kh =db.KhachHang.SingleOrDefault(m => m.Username.ToLower() == username.ToLower() && m.Password == password);
            if(kh != null)
            {
                Session["khach"] = kh;
                //
                ViewBag.IsLoggedIn = true; // Biến kiểm tra đăng nhập
                ViewBag.UserName = kh.TenKhachHang; // Truyền tên người đăng nhập
                //
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng!";
                return View();
            }
        }

        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session.Remove("Khach");
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "TrangChu");
        }
    }
}