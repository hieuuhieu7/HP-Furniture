using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class DonHangMetadata
    {
        [Required(ErrorMessage = "Ngày đặt không được để trống.")]
        public Nullable<System.DateTime> NgayDatHang { get; set; }

        [Required(ErrorMessage = "Tổng tiền không được để trống.")]
        public Nullable<double> TongTien { get; set; }
    }
}