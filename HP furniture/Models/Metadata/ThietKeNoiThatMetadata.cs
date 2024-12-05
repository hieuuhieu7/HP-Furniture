using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class ThietKeNoiThatMetadata
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
    }
}