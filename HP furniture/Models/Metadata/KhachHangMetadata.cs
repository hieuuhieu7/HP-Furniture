using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class KhachHangMetadata
    {
        [Required(ErrorMessage = "*")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "*")]
        public string SDT_KH { get; set; }

        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
    }
}