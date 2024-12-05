using HP_furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HP_furniture.Controllers
{
    public class SapXepSanPhamController : Controller
    {
        // GET: SapXepSanPham
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        public ActionResult Index(int? gia, int? loaiSanPham)
        {
            ViewBag.LoaiSanPham = loaiSanPham;

            var data = db.SanPham.Where(sp => sp.ID_LoaiSanPham == loaiSanPham);

            // Xử lý sắp xếp sản phẩm theo giá nếu có thông tin về cách sắp xếp giá
            if (gia.HasValue)
            {
                switch (gia)
                {
                    case 3: // Giá từ thấp đến cao
                        data = data.OrderBy(sp => sp.GiaSanPham);
                        break;
                    case 4: // Giá từ cao đến thấp
                        data = data.OrderByDescending(sp => sp.GiaSanPham);
                        break;
                    default:
                        break;
                }
            }

            // Truy vấn toàn bộ dữ liệu sản phẩm và sắp xếp (nếu có)
            var productList = data.ToList();

            // Truyền dữ liệu sản phẩm và các thông tin khác sang view
            ViewBag.ProductList = productList;
            ViewBag.Gia = gia;
            ViewBag.LoaiSanPham = loaiSanPham;

            return View();
        }
    }
}