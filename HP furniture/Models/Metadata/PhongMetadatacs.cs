using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class PhongMetada
    {
        [Required(ErrorMessage = "Vui lòng nhập đường dẫn hình ảnh.")]
        public string HinhAnhPhongKhach { get; set; }

        [Required(ErrorMessage = "Tên phòng khách không được để trống.")]
        public string TenPhongKhach { get; set; }
    }
}