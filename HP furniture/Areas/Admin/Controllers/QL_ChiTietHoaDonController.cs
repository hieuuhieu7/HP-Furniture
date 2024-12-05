using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_ChiTietHoaDonController : Controller
    {
        // GET: Admin/QL_ChiTietHoaDon
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        
        //Danh sách CTHĐ
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
                var list = db.ChiTietDonHang.ToList();
                return View(list);
            }
        }

        //Thêm mới CTHĐ
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.ChiTietDonHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ChiTietDonHang.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa CTHĐ
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.ChiTietDonHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.ChiTietDonHang obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.ChiTietDonHang.Find(obj.ID);
                    crrObj.SoLuong = obj.SoLuong;
                    crrObj.TongTien = obj.TongTien;
                    crrObj.ID_DonHang = obj.ID_DonHang;
                    crrObj.ID_SanPham = obj.ID_SanPham;
                    crrObj.HoTen = obj.HoTen;
                    crrObj.SDT = obj.SDT;
                    crrObj.Email = obj.Email;
                    crrObj.DiaChi = obj.DiaChi;
                    crrObj.GhiChu = obj.GhiChu;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa CTHĐ
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.ChiTietDonHang.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.ChiTietDonHang.Find(ID);
            if (obj != null)
            {
                db.ChiTietDonHang.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //CTĐH
        public ActionResult Details(int id)
        {
            var obj = db.ChiTietDonHang.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}