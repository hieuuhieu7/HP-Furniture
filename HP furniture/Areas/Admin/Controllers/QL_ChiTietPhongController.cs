using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_ChiTietPhongController : Controller
    {
        // GET: Admin/QL_ChiTietPhong
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

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
                var list = db.LoaiPhong.ToList();
                return View(list);
            }
        }

        //Thêm mới CTSP
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.LoaiPhong obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.LoaiPhong.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa CTSP
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.LoaiPhong.Find(ID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Models.LoaiPhong obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.LoaiPhong.Find(obj.ID);
                    crrObj.TenLoaiPhong = obj.TenLoaiPhong;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa CTSP
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.LoaiPhong.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.LoaiPhong.Find(ID);
            if (obj != null)
            {
                db.LoaiPhong.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //CTSP
        public ActionResult Details(int id)
        {
            var obj = db.LoaiPhong.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}