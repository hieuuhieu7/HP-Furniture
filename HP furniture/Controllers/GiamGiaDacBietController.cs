using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class GiamGiaDacBietController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        // GET: GiamGiaDacBiet
        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm có loại sản phẩm là 'ID của tên loại sản phẩm'
            List<Models.SanPham> data = db.SanPham
                                            .Where(sp => sp.ID_LoaiSanPham == 7)
                                            .ToList();

            // Truyền danh sách sản phẩm tới view
            ViewBag.Product = data;

            return View();
        }
    }
}