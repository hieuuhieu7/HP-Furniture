using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_SanPhamController : Controller
    {
        // GET: Admin/QL_SanPham
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        //Danh sách SP
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
                var list = db.SanPham.ToList();
                return View(list);
            }
        }

        //Thêm mới SP
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Models.SanPham obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SanPham.Add(obj);
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Sửa SP
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var obj = db.SanPham.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Models.SanPham obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crrObj = db.SanPham.Find(obj.ID);
                    crrObj.TenSanPham = obj.TenSanPham;
                    crrObj.GiaSanPham = obj.GiaSanPham;
                    crrObj.Sale = obj.Sale;
                    crrObj.GiaSanPhamSale = obj.GiaSanPhamSale;
                    crrObj.SoLuongTonKho = obj.SoLuongTonKho;
                    crrObj.HinhAnhSanPham = obj.HinhAnhSanPham;
                    crrObj.HinhAnhSanPham2 = obj.HinhAnhSanPham2;
                    crrObj.HinhAnhSanPham3 = obj.HinhAnhSanPham3;
                    crrObj.HinhAnhSanPham4 = obj.HinhAnhSanPham4;
                    crrObj.HinhAnhSanPham5 = obj.HinhAnhSanPham5;
                    crrObj.ID_LoaiSanPham = obj.ID_LoaiSanPham;
                    crrObj.MoTaSanPham = obj.MoTaSanPham;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Xóa SP
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.SanPham.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.SanPham.Find(ID);
            if (obj != null)
            {
                db.SanPham.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Chi tiết SP
        public ActionResult Details(int id)
        {
            var obj = db.SanPham.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

    }
}