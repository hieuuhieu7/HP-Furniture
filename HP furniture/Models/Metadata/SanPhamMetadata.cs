using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class SanPhamMetadata
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        public string TenSanPham { get; set; }

        [MinLength(5, ErrorMessage = "Mô tả sản phẩm không được ngắn hơn 5 ký tự")]
        public string MoTaSanPham { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống.")]
        public Nullable<double> GiaSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho.")]
        public Nullable<int> SoLuongTonKho { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đường dẫn hình ảnh.")]
        public string HinhAnhSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đường dẫn hình ảnh.")]
        public string HinhAnhSanPham2 { get; set; }
    }
}