using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Areas.Admin.Controllers
{
    public class BaoCaoDoanhThuController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();

        // GET: Admin/BaoCaoDoanhThu
        public ActionResult Index()
        {
            // Khởi tạo danh sách để lưu tổng doanh thu của từng tháng
            List<decimal> monthlyRevenues = new List<decimal>();

            // Khởi tạo biến để tính tổng doanh thu từ trước đến nay
            decimal totalRevenueFromBeginning = 0;

            // Lặp qua từng tháng trong năm
            for (int month = 1; month <= DateTime.Now.Month; month++)
            {
                // Tính ngày đầu tiên và cuối cùng của tháng
                DateTime startDateOfMonth = new DateTime(DateTime.Now.Year, month, 1);
                DateTime endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1);

                // Tính tổng doanh thu của tháng hiện tại
                decimal totalRevenueInMonth = CalculateTotalRevenue(startDateOfMonth, endDateOfMonth);

                // Thêm tổng doanh thu của tháng vào danh sách
                monthlyRevenues.Add(totalRevenueInMonth);

                // Cập nhật tổng doanh thu từ trước đến nay
                totalRevenueFromBeginning += totalRevenueInMonth;
            }

            // Truyền danh sách tổng doanh thu của từng tháng vào ViewBag để sử dụng trong view
            ViewBag.MonthlyRevenues = monthlyRevenues;

            // Truyền tổng doanh thu từ trước đến nay vào ViewBag để sử dụng trong view
            ViewBag.TotalRevenueFromBeginning = totalRevenueFromBeginning;

            // Tính sản phẩm được mua nhiều nhất
            DateTime startDate = db.DonHang.Min(dh => dh.NgayDatHang).GetValueOrDefault();
            DateTime endDate = DateTime.Now;
            var mostBoughtProduct = CalculateMostBoughtProduct(startDate, endDate);

            // Truyền thông tin sản phẩm được mua nhiều nhất vào ViewBag để sử dụng trong view
            ViewBag.MostBoughtProduct = mostBoughtProduct;

            // Tính tổng số lượng của mỗi sản phẩm đã bán ra từ trước đến nay
            var totalQuantitySoldFromBeginning = CalculateTotalQuantitySoldFromBeginning();

            // Truyền thông tin tổng số lượng của mỗi sản phẩm đã bán ra từ trước đến nay vào ViewBag để sử dụng trong view
            ViewBag.TotalQuantitySoldFromBeginning = totalQuantitySoldFromBeginning;

            // Tính tổng số lượng đơn hàng từ trước đến nay
            var totalOrdersFromBeginning = CalculateTotalOrdersFromBeginning();

            // Truyền thông tin tổng số lượng đơn hàng từ trước đến nay vào ViewBag để sử dụng trong view
            ViewBag.TotalOrdersFromBeginning = totalOrdersFromBeginning;

            // Đếm số lượng khách hàng đã đăng ký
            int customerCount = CountCustomers();
            ViewBag.CustomerCount = customerCount;

            // Truyền số lượng đơn hàng đang chờ xử lý
            ViewBag.PendingOrdersCount = CountPendingOrders();

            // Truyền số lượng đơn hàng đang giao
            ViewBag.DeliveringOrdersCount = CountDeliveringOrders();

            // Truyền số lượng đơn hàng đã hoàn thành
            ViewBag.CompletedOrdersCount = CountCompletedOrders();

            return View();
        }

        // Hàm tính tổng doanh thu trong một khoảng thời gian cụ thể
        public decimal CalculateTotalRevenue(DateTime startDate, DateTime endDate)
        {
            // Truy vấn cơ sở dữ liệu để lấy danh sách các đơn hàng trong khoảng thời gian đã cho
            var orders = db.DonHang.Where(o => o.NgayDatHang >= startDate && o.NgayDatHang <= endDate).ToList();

            // Tính tổng doanh thu bằng cách cộng tổng số tiền của các đơn hàng
            decimal totalRevenue = orders.Sum(o => (decimal)(o.TongTien ?? 0));

            return totalRevenue;
        }

        // Hàm tính sản phẩm được mua nhiều nhất
        public string CalculateMostBoughtProduct(DateTime startDate, DateTime endDate)
        {
            // Lấy danh sách tất cả các chi tiết đơn hàng trong khoảng thời gian cụ thể
            var orderDetails = db.ChiTietDonHang
                                .Where(od => od.DonHang.NgayDatHang >= startDate && od.DonHang.NgayDatHang <= endDate)
                                .ToList();

            // Tạo một từ điển để lưu trữ số lượng của mỗi sản phẩm
            Dictionary<int, int> productQuantityMap = new Dictionary<int, int>();

            // Tính tổng số lượng của mỗi sản phẩm
            foreach (var orderDetail in orderDetails)
            {
                int productId = orderDetail.ID_SanPham ?? 0;
                int quantity = orderDetail.SoLuong ?? 0;

                if (productQuantityMap.ContainsKey(productId))
                {
                    productQuantityMap[productId] += quantity;
                }
                else
                {
                    productQuantityMap.Add(productId, quantity);
                }
            }

            // Xác định sản phẩm có số lượng lớn nhất
            int maxQuantity = productQuantityMap.Values.Max();
            int mostBoughtProductId = productQuantityMap.FirstOrDefault(x => x.Value == maxQuantity).Key;

            // Lấy thông tin của sản phẩm được mua nhiều nhất
            var mostBoughtProduct = db.SanPham.FirstOrDefault(p => p.ID == mostBoughtProductId);

            return mostBoughtProduct != null ? mostBoughtProduct.TenSanPham : "Không có sản phẩm được mua";
        }

        // Hàm tính tổng số lượng của mỗi sản phẩm đã bán ra từ trước đến nay
        public Dictionary<string, int> CalculateTotalQuantitySoldFromBeginning()
        {
            // Lấy danh sách tất cả các chi tiết đơn hàng
            var orderDetails = db.ChiTietDonHang.ToList();

            // Tạo một từ điển để lưu trữ tổng số lượng của mỗi sản phẩm
            Dictionary<string, int> productQuantityMap = new Dictionary<string, int>();

            // Tính tổng số lượng của mỗi sản phẩm
            foreach (var orderDetail in orderDetails)
            {
                string productName = orderDetail.SanPham.TenSanPham;
                int quantity = orderDetail.SoLuong ?? 0;

                if (productQuantityMap.ContainsKey(productName))
                {
                    productQuantityMap[productName] += quantity;
                }
                else
                {
                    productQuantityMap.Add(productName, quantity);
                }
            }

            return productQuantityMap;
        }

        // Hàm tính tổng số lượng đơn hàng từ trước đến nay
        public int CalculateTotalOrdersFromBeginning()
        {
            // Truy vấn cơ sở dữ liệu để lấy danh sách tất cả các đơn hàng
            var totalOrders = db.DonHang.Count();

            return totalOrders;
        }

        // Hàm tính số lượng khách hàng
        public int CountCustomers()
        {
            // Sử dụng Entity Framework để truy vấn số lượng khách hàng
            int customerCount = db.KhachHang.Count();

            return customerCount;
        }

        // Hàm tính số lượng đơn hàng đang chờ xử lý
        public int CountPendingOrders()
        {
            // Lấy danh sách các đơn hàng có trạng thái là "Đang xử lý"
            var pendingOrders = db.DonHang.Where(dh => dh.ID_TrangThai == 1).ToList();

            // Đếm số lượng đơn hàng
            int pendingOrdersCount = pendingOrders.Count;

            return pendingOrdersCount;
        }

        // Hàm tính số lượng đơn hàng đang giao
        public int CountDeliveringOrders()
        {
            // Lấy danh sách các đơn hàng có trạng thái là "Đang giao" (ID_TrangThai = 2)
            var deliveringOrders = db.DonHang.Where(dh => dh.ID_TrangThai == 2).ToList();

            // Đếm số lượng đơn hàng
            int deliveringOrdersCount = deliveringOrders.Count;

            return deliveringOrdersCount;
        }

        // Hàm tính số lượng đơn hàng đã hoàn thành
        public int CountCompletedOrders()
        {
            // Lấy danh sách các đơn hàng có trạng thái là "Đã hoàn thành" (ID_TrangThai = 3)
            var completedOrders = db.DonHang.Where(dh => dh.ID_TrangThai == 3).ToList();

            // Đếm số lượng đơn hàng
            int completedOrdersCount = completedOrders.Count;

            return completedOrdersCount;
        }
    }
}