using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_SanPhamMoiController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: Admin/QL_SanPhamMoi
        //public ActionResult Index()
        //{
        //    if (Session["User"] == null)
        //    {
        //        return RedirectToAction("DangNhap", "HomeAdmin");
        //    }
        //    else
        //    {
        //        NhanVien nvSession = (NhanVien)Session["User"];
        //        var count = db.PhanQuyen.Count(m => m.ID_NhanVien == nvSession.ID & m.ID_ChucNang == 4);
        //        if (count == 0)
        //        {
        //            //Báo lỗi không có quyền truy cập
        //            return Redirect("/Admin/BaoLoiPhanQuyen/Index");
        //        }
        //        var list = db.SanPhamMoi.ToList();
        //        return View(list);
        //    }
        //}

        ////Thêm mới SPM
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(Models.SanPhamMoi obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.SanPhamMoi.Add(obj);
        //            db.SaveChanges();
        //        }
        //        catch
        //        {

        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        ////Sửa SPM
        //[HttpGet]
        //public ActionResult Edit(int ID)
        //{
        //    var obj = db.SanPhamMoi.Find(ID);
        //    return View(obj);
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Edit(Models.SanPhamMoi obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var crrObj = db.SanPhamMoi.Find(obj.ID);
        //            crrObj.TenSanPhamMoi = obj.TenSanPhamMoi;
        //            crrObj.GiaSanPhamMoi = obj.GiaSanPhamMoi;
        //            crrObj.SoLuongTonKho = obj.SoLuongTonKho;
        //            crrObj.HinhAnh1 = obj.HinhAnh1;
        //            crrObj.HinhAnh2 = obj.HinhAnh2;
        //            crrObj.MoTaSanPhamMoi = obj.MoTaSanPhamMoi;
        //            db.SaveChanges();
        //        }
        //        catch
        //        {

        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        ////Xóa SPM
        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    var obj = db.SanPhamMoi.Find(ID);
        //    return View(obj);
        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirm(int ID)
        //{
        //    var obj = db.SanPhamMoi.Find(ID);
        //    if (obj != null)
        //    {
        //        db.SanPhamMoi.Remove(obj);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        ////Chi tiết SPM
        //public ActionResult Details(int id)
        //{
        //    var obj = db.SanPhamMoi.Find(id);
        //    if (obj == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(obj);
        //}
    }
}