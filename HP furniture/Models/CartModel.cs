using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP_furniture.Models
{
    public class CartModel
    {
        public int ProductID { get; set; }

        public Models.SanPham ProductDetail { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public string Note { get; set; }
    }
}