using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class SanPhamPhongKhachController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: SanPhamPhongKhach
        public ActionResult Index(int? page)
        {
            int pageSize = 6; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, mặc định là trang 1

            var data = db.Phong
                         .Where(sp => sp.ID_LoaiPhong == 1)
                         .OrderBy(sp => sp.ID) // Sắp xếp sản phẩm theo ID hoặc thuộc tính khác
                         .ToPagedList(pageNumber, pageSize);

            return View(data);
        }
    }
}