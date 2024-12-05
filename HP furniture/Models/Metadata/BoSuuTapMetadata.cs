using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP_furniture.Models.Metadata
{
    public class BoSuuTapMetadata
    {
        [Required(ErrorMessage = "Vui lòng nhập đường dẫn hình ảnh.")]
        public string HinhAnhBST { get; set; }

        [Required(ErrorMessage = "Tên bộ sưu tập không được để trống.")]
        public string TenBST { get; set; }

        [Required(ErrorMessage = "Mô tả bộ sưu tập không được để trống.")]
        public string MoTaBST { get; set; }
    }
}