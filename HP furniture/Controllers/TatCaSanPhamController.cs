using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace HP_furniture.Controllers
{
    public class TatCaSanPhamController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: TatCaSanPham
        public ActionResult Index(int? page)
        {
            int pageSize = 20; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, mặc định là trang 1

            // Sử dụng ToPagedList để phân trang dữ liệu
            // Sử dụng OrderBy để đảm bảo sắp xếp dữ liệu trước khi phân trang
            var data = db.SanPham.OrderBy(p => p.ID).ToPagedList(pageNumber, pageSize);

            return View(data);
        }
    }
}