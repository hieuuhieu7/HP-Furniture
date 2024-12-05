using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        public ActionResult Index(int? Id)
        {
            // Bước 1: Lấy dữ liệu
            List<Models.SanPham> data = db.SanPham
                    .Where(sp => sp.ID_LoaiSanPham == 11)
                    .ToList();

            // Bước 2: Gán vào ViewBag
            ViewBag.Product = data;

            if (Id == null)
            {
                // Xử lý trường hợp 'Id' không được truyền hoặc có giá trị null
                // Ví dụ: Chuyển hướng hoặc xử lý lỗi
                return RedirectToAction("Error");
            }

            var objSanPham = db.SanPham.FirstOrDefault(n => n.ID == Id);
            if (objSanPham == null)
            {
                // Xử lý trường hợp không tìm thấy sản phẩm với Id được cung cấp
                // Ví dụ: Chuyển hướng hoặc xử lý lỗi
                return RedirectToAction("NotFound");
            }

            return View(objSanPham);
        }
    }
}