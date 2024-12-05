using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_GocCamHungController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: Admin/QL_GocCamHung
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
                var list = db.GocCamHung.ToList();
                return View(list);
            }
        }

        //Thêm BST
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Models.GocCamHung obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.GocCamHung.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa BST
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.GocCamHung.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.GocCamHung obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.GocCamHung.Find(obj.ID);
                    crrObj.TenCamHung1 = obj.TenCamHung1;
                    crrObj.HinhAnhCamHung1 = obj.HinhAnhCamHung1;
                    crrObj.MoTaCamHung1 = obj.MoTaCamHung1;

                    crrObj.TenCamHung2 = obj.TenCamHung2;
                    crrObj.HinhAnhCamHung2 = obj.HinhAnhCamHung2;
                    crrObj.MoTaCamHung2 = obj.MoTaCamHung2;

                    crrObj.TenCamHung3 = obj.TenCamHung3;
                    crrObj.HinhAnhCamHung3 = obj.HinhAnhCamHung3;
                    crrObj.MoTaCamHung3 = obj.MoTaCamHung3;

                    crrObj.HinhAnhEnd = obj.HinhAnhEnd;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa BST
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.GocCamHung.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.GocCamHung.Find(ID);
            if (obj != null)
            {
                db.GocCamHung.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết bộ sưu tập
        public ActionResult Details(int id)
        {
            var obj = db.GocCamHung.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}