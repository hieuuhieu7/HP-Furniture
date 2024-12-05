using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class YeuThichController : Controller
    {
        // GET: YeuThich
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        public ActionResult Index()
        {
            // Bước 1: Lấy dữ liệu
            List<Models.SanPham> data = db.SanPham
                    .Where(sp => sp.ID_LoaiSanPham == 11)
                    .ToList();

            // Bước 2: Gán vào ViewBag
            ViewBag.Product = data;

            return View();
        }

        //Thêm sản phẩm yêu thích
        public ActionResult ThemYeuThich(int id)
        {
            List<Models.HeartModel> lstHeart = null;
            if (Session["Heart"] == null)
            {
                lstHeart = new List<Models.HeartModel>();
            }
            else
            {
                lstHeart = (List<Models.HeartModel>)Session["Heart"];
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong danh sách yêu thích chưa
            Models.HeartModel existingItem = lstHeart.FirstOrDefault(m => m.YeuThichId == id);
            if (existingItem == null)
            {
                // Nếu sản phẩm chưa tồn tại trong danh sách, thêm mới
                Models.SanPham crrHeart = db.SanPham.First(m => m.ID == id);
                Models.HeartModel obj = new Models.HeartModel
                {
                    YeuThichId = id,
                    YeuThichDetail = crrHeart,
                    YeuThichQuantity = 1,
                    YeuThichNote = "",
                };
                lstHeart.Add(obj);

                // Cập nhật số lượng sản phẩm yêu thích trong Session
                UpdateHeartQuantityInHeaderCart(lstHeart);
            }
            // Nếu sản phẩm đã tồn tại, bạn có thể thông báo cho người dùng rằng sản phẩm đã được yêu thích trước đó

            Session["Heart"] = lstHeart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromHeart(int heartId)
        {
            if (Session["Heart"] != null)
            {
                //chuyển đổi giá trị của biến Session có tên là "Heart" thành một danh sách (List)
                List<HP_furniture.Models.HeartModel> lstHeart = (List<HP_furniture.Models.HeartModel>)Session["Heart"];

                // Tìm sản phẩm trong yêu thích
                var heartToRemove = lstHeart.FirstOrDefault(item => item.YeuThichDetail.ID == heartId);

                if (heartToRemove != null)
                {
                    // Xóa sản phẩm từ giỏ hàng
                    lstHeart.Remove(heartToRemove);
                }

                // Cập nhật yêu thích trong Session
                Session["Heart"] = lstHeart;
                UpdateHeartQuantityInHeaderCart(lstHeart);
            }
            return RedirectToAction("Index"); // Điều hướng đến trang yêu thích hoặc trang sản phẩm
        }

        private void UpdateHeartQuantityInHeaderCart(List<HP_furniture.Models.HeartModel> cartItems)
        {
            if (cartItems != null)
            {
                // Tính tổng số lượng sản phẩm trong giỏ hàng
                int totalQuantity = cartItems.Sum(item => item.YeuThichQuantity);

                // Cập nhật số lượng sản phẩm trong Header cart
                Session["HeartQuantity"] = totalQuantity;
            }
        }
    }
}