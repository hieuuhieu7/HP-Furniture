using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class UserMetadata
    {
        [Required(ErrorMessage = "Tên người dùng không được để trống!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Tên không được để trống!")]
        //public string TenNhanVien { get; set; }

        //[Required(ErrorMessage = "Địa chỉ không được để trống!")]
        //public string DiaChi { get; set; }

        //[Required(ErrorMessage = "SĐT không được để trống!")]
        //public string SDT { get; set; }

        //[Required(ErrorMessage = "Chức vụ không được để trống!")]
        //public string ChucVu { get; set; }
    }
}