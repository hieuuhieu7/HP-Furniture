using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_HoaDonController : Controller
    {
        // GET: Admin/QL_HoaDon
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        //Danh sách HĐ
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
                var list = db.DonHang.ToList();
                return View(list);
            }
        }

        //Thêm mới HĐ
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.DonHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.DonHang.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa HĐ
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.DonHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.DonHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.DonHang.Find(obj.ID);
                    crrObj.ID_KhachHang = obj.ID_KhachHang;
                    crrObj.NgayDatHang = obj.NgayDatHang;
                    crrObj.TongTien = obj.TongTien;
                    crrObj.ID_TrangThai = obj.ID_TrangThai;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa HĐ
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.DonHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.DonHang.Find(ID);
            if (obj != null)
            {
                db.DonHang.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết HĐ
        public ActionResult Details(int id)
        {
            var obj = db.DonHang.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}