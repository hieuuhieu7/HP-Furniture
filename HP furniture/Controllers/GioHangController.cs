using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class GioHangController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        // Phương thức này được gọi khi người dùng nhấn nút "Đặt hàng" ở trang giỏ hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            // Lấy thông tin từ session giỏ hàng và tổng số tiền đơn hàng
            if (Session["Cart"] != null && Session["OrderTotal"] != null)
            {
                List<Models.CartModel> lstCart = (List<Models.CartModel>)Session["Cart"];
                decimal orderTotal = (decimal)Session["OrderTotal"];

                // Kiểm tra nếu giỏ hàng không có sản phẩm
                if (lstCart.Count == 0)
                {
                    // Redirect hoặc trả về view tương ứng
                    // Ví dụ:
                    return RedirectToAction("Index", "GioHang");
                }

                //// Tạo một đơn hàng mới
                //Models.DonHang newOrder = new Models.DonHang
                //{
                //    NgayDatHang = DateTime.Now, // Lấy ngày hiện tại làm ngày đặt hàng
                //    TongTien = (float)orderTotal // Ép kiểu orderTotal thành float trước khi gán
                //};

                //// Thêm đơn hàng vào cơ sở dữ liệu
                //db.DonHang.Add(newOrder);
                //db.SaveChanges();

                // Chuyển hướng người dùng đến trang đặt mua
                return RedirectToAction("Index", "DatMua");
            }
            else
            {
                // Xử lý trường hợp session giỏ hàng không tồn tại
                // Hoặc thông tin đơn hàng không hợp lệ
                // Redirect hoặc trả về view tương ứng
                // Ví dụ:
                return RedirectToAction("Index", "GioHang");
            }
        }

        // Phương thức này để hiển thị trang đặt mua với thông tin của đơn hàng
        public ActionResult DatMua(int donHangId)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu
            Models.DonHang order = db.DonHang.Find(donHangId);
            if (order == null)
            {
                // Xử lý trường hợp không tìm thấy đơn hàng
                // Redirect hoặc trả về view tương ứng
                return RedirectToAction("Index", "Home"); // Ví dụ
            }

            // Truyền thông tin đơn hàng vào view và hiển thị
            return View(order);
        }

        public ActionResult ThemGioHang(int id)
        {
            List<Models.CartModel> lstCart = null;
            if (Session["Cart"] == null)
            {
                lstCart = new List<Models.CartModel>();
            }
            else
            {
                lstCart = (List<Models.CartModel>)Session["Cart"];
            }

            Models.CartModel obj = lstCart.FirstOrDefault(m => m.ProductID == id);
            if (obj == null)
            {
                Models.SanPham crrProduct = db.SanPham.First(m => m.ID == id);
                obj = new Models.CartModel
                {
                    ProductID = id,
                    ProductDetail = crrProduct,
                    Quantity = 1,
                    Amount = (double)(1 * (crrProduct.GiaSanPhamSale ?? crrProduct.GiaSanPham)), // Sử dụng giá sale nếu có, nếu không thì sử dụng giá gốc
                    Note = ""
                };
                lstCart.Add(obj);
            }
            else
            {
                obj.Quantity = obj.Quantity + 1;
                obj.Amount = (double)(obj.Quantity * (obj.ProductDetail.GiaSanPhamSale ?? obj.ProductDetail.GiaSanPham)); // Sử dụng giá sale nếu có, nếu không thì sử dụng giá gốc
            }
            Session["Cart"] = lstCart;

            UpdateCartTotal();

            UpdateHeaderCartQuantity();

            return RedirectToAction("Index");
        }

        // Phương thức cập nhật tổng giá trị đơn hàng
        private void UpdateCartTotal()
        {
            if (Session["Cart"] != null)
            {
                // Chuyển đổi giá trị của biến Session có tên là "Cart" thành một danh sách (List)
                List<HP_furniture.Models.CartModel> lstCart = (List<HP_furniture.Models.CartModel>)Session["Cart"];

                // Tính toán tổng giá trị đơn hàng
                decimal orderTotal = 0;
                foreach (var item in lstCart)
                {
                    // Nếu sản phẩm có giảm giá, tính tổng số tiền dựa trên giá giảm giá
                    if (item.ProductDetail.GiaSanPhamSale.HasValue)
                    {
                        orderTotal += (decimal)(item.Quantity * item.ProductDetail.GiaSanPhamSale);
                    }
                    else
                    {
                        orderTotal += (decimal)(item.Quantity * item.ProductDetail.GiaSanPham);
                    }
                }

                // Cập nhật tổng giá trị đơn hàng trong session
                Session["OrderTotal"] = orderTotal;
            }
        }

        public ActionResult RemoveFromCart(int productId)
        {
            if (Session["Cart"] != null)
            {
                //chuyển đổi giá trị của biến Session có tên là "Cart" thành một danh sách (List)
                List<HP_furniture.Models.CartModel> lstCart = (List<HP_furniture.Models.CartModel>)Session["Cart"];

                // Tìm sản phẩm trong giỏ hàng
                var productToRemove = lstCart.FirstOrDefault(item => item.ProductDetail.ID == productId);

                if (productToRemove != null)
                {
                    // Xóa sản phẩm từ giỏ hàng
                    lstCart.Remove(productToRemove);
                }

                // Cập nhật giỏ hàng trong Session
                Session["Cart"] = lstCart;
            }
            UpdateHeaderCartQuantity();
            // Chuyển hướng về trang giỏ hàng hoặc trang sản phẩm
            return RedirectToAction("Index"); // Điều hướng đến trang giỏ hàng hoặc trang sản phẩm
        }

        private void UpdateHeaderCartQuantity()
        {
            // Lấy danh sách sản phẩm từ Session giỏ hàng
            List<HP_furniture.Models.CartModel> lstCart = (List<HP_furniture.Models.CartModel>)Session["Cart"];

            // Tính tổng số lượng sản phẩm trong giỏ hàng
            int totalQuantity = lstCart.Sum(item => item.Quantity);

            // Lưu tổng số lượng vào Session
            Session["CartQuantity"] = totalQuantity;
        }

        // Phương thức này được gọi khi người dùng cập nhật số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            if (Session["Cart"] != null)
            {
                // Chuyển đổi giá trị của biến Session có tên là "Cart" thành một danh sách (List)
                List<HP_furniture.Models.CartModel> lstCart = (List<HP_furniture.Models.CartModel>)Session["Cart"];

                // Tìm sản phẩm trong giỏ hàng để cập nhật số lượng
                var productToUpdate = lstCart.FirstOrDefault(item => item.ProductID == productId);

                if (productToUpdate != null)
                {
                    // Cập nhật số lượng sản phẩm trong giỏ hàng
                    productToUpdate.Quantity = quantity;
                    // Sử dụng giá sale nếu có, nếu không sử dụng giá gốc
                    double price = (double)(productToUpdate.ProductDetail.GiaSanPhamSale ?? productToUpdate.ProductDetail.GiaSanPham);
                    productToUpdate.Amount = quantity * price;
                }

                // Cập nhật giỏ hàng trong Session
                Session["Cart"] = lstCart;

                // Cập nhật tổng giá trị đơn hàng sau khi cập nhật số lượng sản phẩm
                UpdateCartTotal();

                // Cập nhật số lượng sản phẩm trong Header cart
                UpdateCartQuantityInHeaderCart(lstCart);
            }

            // Chuyển hướng về trang giỏ hàng
            return RedirectToAction("Index");
        }

        // Phương thức cập nhật số lượng sản phẩm trong Header cart
        private void UpdateCartQuantityInHeaderCart(List<HP_furniture.Models.CartModel> cartItems)
        {
            if (cartItems != null)
            {
                // Tính tổng số lượng sản phẩm trong giỏ hàng
                int totalQuantity = cartItems.Sum(item => item.Quantity);

                // Cập nhật số lượng sản phẩm trong Header cart
                Session["CartQuantity"] = totalQuantity;
            }
        }
    }
}