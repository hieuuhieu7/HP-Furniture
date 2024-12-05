using HP_furniture.Models;
using HP_furniture.Models.Payments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP_furniture.Controllers
{
    public class DatMuaController : Controller
    {
        Models.DB_QuanLyCuaHangDoNoiThatEntities db = new Models.DB_QuanLyCuaHangDoNoiThatEntities();
        // GET: DatMua
        public ActionResult Index()
        {
            // Lấy thông tin sản phẩm từ Session giỏ hàng
            var products = Session["Cart"] as List<HP_furniture.Models.CartModel>;

            // Kiểm tra nếu session "Cart" chưa được khởi tạo, thì khởi tạo nó
            if (products == null)
            {
                // Khởi tạo session "Cart" với một danh sách rỗng
                products = new List<HP_furniture.Models.CartModel>();

                // Gán session "Cart" với danh sách sản phẩm rỗng
                Session["Cart"] = products;
            }

            ViewBag.Products = products;

            // Tính tổng cộng
            decimal totalAmount = 0;
            if (products != null)
            {
                foreach (var product in products)
                {
                    totalAmount = products.Sum(p => p.Quantity * (decimal)(p.ProductDetail.GiaSanPhamSale ?? (double)p.ProductDetail.GiaSanPham));

                }
            }
            ViewBag.TotalAmount = totalAmount.ToString("#,##0"); // Định dạng tiền tệ

            return View();
        }

        [HttpPost]
        public ActionResult DatHang(string hoTen, string soDienThoai, string email, string diaChi, string note, int PTTT)
        {
            var code = new { success = false, quy = -1, Url = "" };
            // Lấy thông tin sản phẩm từ Session giỏ hàng
            if (ModelState.IsValid)
            {
                var products = Session["Cart"] as List<HP_furniture.Models.CartModel>;
                    
                if (products != null && products.Any())
                {
                    // Tạo một đơn hàng mới
                    Models.DonHang newOrder = new Models.DonHang
                    {
                        NgayDatHang = DateTime.Now, // Lấy ngày hiện tại làm ngày đặt hàng
                        TongTien = products.Sum(p => (double?)(p.Quantity * ((decimal)(p.ProductDetail.GiaSanPhamSale ?? p.ProductDetail.GiaSanPham))))
                    };

                    // Thêm đơn hàng vào cơ sở dữ liệu
                    db.DonHang.Add(newOrder);
                    db.SaveChanges();

                    Random rd = new Random();
                    // Lưu chi tiết đơn hàng
                    foreach (var product in products)
                    {
                        HP_furniture.Models.ChiTietDonHang chiTietDonHang = new HP_furniture.Models.ChiTietDonHang
                        {
                            // Gán các giá trị từ form vào từng chi tiết đơn hàng
                            HoTen = hoTen,
                            SDT = soDienThoai,
                            Email = email,
                            DiaChi = diaChi,
                            GhiChu = note,
                            Ngaymua = DateTime.Now,
                            ThanhToan = PTTT,
                            Code = "HD" + rd.Next() + rd.Next(),

                            ID_DonHang = newOrder.ID, // Lấy ID của đơn hàng vừa thêm vào cơ sở dữ liệu
                            ID_SanPham = db.SanPham.FirstOrDefault(sp => sp.TenSanPham == product.ProductDetail.TenSanPham)?.ID,
                            SoLuong = product.Quantity,
                            TongTien = products.Sum(p => (double?)(p.Quantity * ((decimal)(p.ProductDetail.GiaSanPhamSale ?? p.ProductDetail.GiaSanPham))))
                        };

                        // Thêm chi tiết đơn hàng vào cơ sở dữ liệu
                        db.ChiTietDonHang.Add(chiTietDonHang);
                        db.SaveChanges();
                        code = new { success = true, quy = PTTT, Url = "" };
                        if (PTTT == 0)
                        {
                            var url = UrlPayment(PTTT, chiTietDonHang.Code);
                            code = new { success = true, quy = PTTT, Url = url };

                        }
                    }


                    // Lưu thay đổi vào cơ sở dữ liệu
                    
                 

                    // Xóa session giỏ hàng sau khi đã đặt hàng thành công
                    Session.Remove("Cart");

                    // Cập nhật số lượng sản phẩm trong Header cart về 0 sau khi đặt hàng thành công
                    Session["CartQuantity"] = 0;

                    // Redirect hoặc trả về view tùy vào logic của bạn
            
                    

                }
                else
                {
                    // Xử lý trường hợp giỏ hàng trống
                    // Redirect hoặc trả về view tương ứng
                    return RedirectToAction("Index", "GioHang"); // Ví dụ
                }
            }
            return Json(code);

        }

        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = db.ChiTietDonHang.FirstOrDefault(x => x.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.Status = 2;//đã thanh toán
                            db.ChiTietDonHang.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
            }
            //var a = UrlPayment(0, "DH3574");
            return View();
        }
        #region Thanh toán vnpay
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = db.ChiTietDonHang.FirstOrDefault(x => x.Code == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)order.TongTien * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.Ngaymua.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.ID);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion
    }
}