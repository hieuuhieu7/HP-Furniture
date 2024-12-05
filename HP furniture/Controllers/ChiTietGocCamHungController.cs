using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class ChiTietGocCamHungController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: ChiTietGocCamHung
        public ActionResult Index(int? Id)
        {
            List<Models.GocCamHung> data = db.GocCamHung.ToList();
            ViewBag.Product = data;

            if (Id == null)
            {
                // Xử lý trường hợp 'Id' không được truyền hoặc có giá trị null
                // Ví dụ: Chuyển hướng hoặc xử lý lỗi
                return RedirectToAction("Error");
            }

            var objBST = db.GocCamHung.FirstOrDefault(n => n.ID == Id);
            if (objBST == null)
            {
                // Xử lý trường hợp không tìm thấy sản phẩm với Id được cung cấp
                // Ví dụ: Chuyển hướng hoặc xử lý lỗi
                return RedirectToAction("NotFound");
            }
            return View(objBST);
        }
    }
}