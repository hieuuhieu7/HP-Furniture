using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class QL_ThietKeNoiThatController : Controller
    {
        // GET: Admin/QL_ThietKeNoiThat
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
                var list = db.ThietKeNoiThat.ToList();
                return View(list);
            }
        }

        //Xóa yêu cầu
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var obj = db.ThietKeNoiThat.Find(ID);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int ID)
        {
            var obj = db.ThietKeNoiThat.Find(ID);
            if (obj != null)
            {
                db.ThietKeNoiThat.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}