using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_PhongController : Controller
    {
        // GET: Admin/QL_SPphongkhach
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        //Danh sách SP phòng khách
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
                var list = db.Phong.ToList();
                return View(list);
            }
        }

        //Thêm SP phòng khách
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Models.Phong obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Phong.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa SP phòng khách
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.Phong.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.Phong obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.Phong.Find(obj.ID);
                    crrObj.TenPhongKhach = obj.TenPhongKhach;
                    crrObj.HinhAnhPhongKhach = obj.HinhAnhPhongKhach;
                    crrObj.ID_LoaiPhong = obj.ID_LoaiPhong;
                    crrObj.Mt1 = obj.Mt1;
                    crrObj.Mt2 = obj.Mt2;
                    crrObj.Mt3 = obj.Mt3;
                    crrObj.Mt4 = obj.Mt4;
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

        //Xóa SP phòng khách
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.Phong.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.Phong.Find(ID);
            if (obj != null)
            {
                db.Phong.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết SP phòng khách
        public ActionResult Details(int id)
        {
            var obj = db.Phong.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

    }
}