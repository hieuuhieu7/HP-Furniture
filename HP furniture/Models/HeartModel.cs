using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP_furniture.Models
{
    public class HeartModel
    {
        public int YeuThichId { get; set;}
        public Models.SanPham YeuThichDetail { get; set;}
        public int YeuThichQuantity { get; set;}
        public double YeuThichAmount { get; set;}
        public string YeuThichNote { get; set;}
    }
}