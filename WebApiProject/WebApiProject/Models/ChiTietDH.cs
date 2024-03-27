using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class ChiTietDH
    {
        public string order_details_id { get; set; }
        public string order_id { get; set; }
        public Nullable<int> product_size_quantity_id { get; set; }
        public Nullable<decimal> price_oder { get; set; }
        public Nullable<int> quantity { get; set; }
        public string hinhanh { get; set; }
        public string tensanpham { get; set; }
        public string kichthuoc { get; set; }
    }
}