using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class GioHang
    {
        public string cart_id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public Nullable<int> product_size_quantity_id { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> totalprice { get; set; }

    }
}