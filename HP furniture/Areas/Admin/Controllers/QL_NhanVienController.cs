using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_NhanVienController : Controller
    {
        // GET: Admin/QL_NhanVien
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        //Danh sách
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
                var list = db.NhanVien.ToList();
                return View(list);
            }
        }

        //Thêm nhân viên
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.NhanVien obj)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(obj.TenNhanVien) == true)
                {
                    ModelState.AddModelError("", "Họ và tên không được để trống!");
                    return View(obj);
                }
                try
                {
                    db.NhanVien.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa nhân viên
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.NhanVien.Find(ID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Models.NhanVien obj)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(obj.TenNhanVien) == true)
                {
                    ModelState.AddModelError("", "Họ và tên không được để trống!");
                    return View(obj);
                }
                try
                {
                    var crrObj = db.NhanVien.Find(obj.ID);
                    crrObj.TenNhanVien = obj.TenNhanVien;
                    crrObj.NgaySinh = obj.NgaySinh;
                    crrObj.DiaChi = obj.DiaChi;
                    crrObj.SDT = obj.SDT;
                    crrObj.ChucVu = obj.ChucVu;
                    crrObj.Username = obj.Username;
                    crrObj.Password = obj.Password;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa nhân viên
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.NhanVien.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.NhanVien.Find(ID);
            if (obj != null)
            {
                db.NhanVien.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết nhân viên
        public ActionResult Details(int id)
        {
            var obj = db.NhanVien.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}