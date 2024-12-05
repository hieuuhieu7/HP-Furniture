using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_BoSuuTapController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: Admin/QL_BoSuuTap
        //Danh sách BST
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
                var list = db.BoSuuTap.ToList();
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
        public ActionResult Create(Models.BoSuuTap obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.BoSuuTap.Add(obj);
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
            var obj = db.BoSuuTap.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.BoSuuTap obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.BoSuuTap.Find(obj.ID);
                    crrObj.TenBST = obj.TenBST;
                    crrObj.HinhAnhBST = obj.HinhAnhBST;
                    crrObj.MoTaBST = obj.MoTaBST;
                    crrObj.MoTaDai = obj.MoTaDai;
                    crrObj.HinhAnh1 = obj.HinhAnh1;
                    crrObj.HinhAnh2 = obj.HinhAnh2;
                    crrObj.HinhAnh3 = obj.HinhAnh3;
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
            var obj = db.BoSuuTap.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.BoSuuTap.Find(ID);
            if (obj != null)
            {
                db.BoSuuTap.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết bộ sưu tập
        public ActionResult Details(int id)
        {
            var obj = db.BoSuuTap.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
    }
}