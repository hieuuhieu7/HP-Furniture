using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;


namespace HP_furniture.Controllers
{
    public class SanPhamBanController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: SanPhamBan
        public ActionResult Index(int? page)
        {
            int pageSize = 12; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, mặc định là trang 1

            var data = db.SanPham
                         .Where(sp => sp.ID_LoaiSanPham == 9)
                         .OrderBy(sp => sp.ID) // Sắp xếp sản phẩm theo ID hoặc thuộc tính khác
                         .ToPagedList(pageNumber, pageSize);

            return View(data);
        }
    }
}