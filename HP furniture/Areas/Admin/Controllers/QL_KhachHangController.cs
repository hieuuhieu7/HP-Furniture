using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_KhachHangController : Controller
    {
        // GET: Admin/QL_KhachHang
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        //Danh sách khách hàng
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin");
            }
            else
            {
                NhanVien nvSession = (NhanVien)Session["User"];
                var count = db.PhanQuyen.Count(m => m.ID_NhanVien == nvSession.ID & m.ID_ChucNang == 4);
                if (count == 0)
                {
                    //Báo lỗi không có quyền truy cập
                    return Redirect("/Admin/BaoLoiPhanQuyen/Index");
                }
                var list = db.KhachHang.ToList();
                return View(list);
            }
        }

        //Thêm khách hàng
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.KhachHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.KhachHang.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa khách hàng
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.KhachHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Models.KhachHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.KhachHang.Find(obj.ID);
                    crrObj.TenKhachHang = obj.TenKhachHang;
                    crrObj.SDT_KH = obj.SDT_KH;
                    crrObj.DiaChi_KH = obj.DiaChi_KH;
                    crrObj.Username = obj.Username;
                    crrObj.Password = obj.Password;
                    crrObj.Avt = obj.Avt;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa khách hàng
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.KhachHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.KhachHang.Find(ID);
            if (obj != null)
            {
                db.KhachHang.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết khách hàng
        public ActionResult Details(int id)
        {
            var obj = db.KhachHang.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}