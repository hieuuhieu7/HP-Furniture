using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap");
            }
            else
            {
                return View();
            }
        }

        //Đăng nhập
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(Models.NhanVien obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.NhanVien check = db.NhanVien.FirstOrDefault(m => m.Username == obj.Username && m.Password == obj.Password);
                    if (check != null)
                    {
                        //Đăng nhập thành công, điều hướng sang trang chủ admin
                        Session["User"] = check;
                        return RedirectToAction("Index", "BaoCaoDoanhThu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                    }
                }
                catch
                {

                }
            }

            return View(obj);
        }

        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session.Remove("User");
            FormsAuthentication.SignOut();

            return RedirectToAction("DangNhap");
        }
    }
}