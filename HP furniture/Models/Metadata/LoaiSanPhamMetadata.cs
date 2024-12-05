using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class LoaiSanPhamMetadata
    {
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống.")]
        public string TenLoaiSanPham { get; set; }
    }
}