using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class ChiTietDonHangMetadata
    {
        [Required(ErrorMessage = "Số lượng không được để trống.")]
        public Nullable<int> SoLuong { get; set; }

        [Required(ErrorMessage = "Tổng tiền không được để trống.")]
        public Nullable<double> TongTien { get; set; }
    }
}