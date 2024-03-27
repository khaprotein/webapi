using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class DonHang
    {
        public string orders_id { get; set; }
        public string user_id { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public Nullable<System.DateTime> orders_date { get; set; }
        public string shipping_address { get; set; }
        public string user_phone { get; set; }
        public Nullable<int> oderstatus_id { get; set; }
        public string status { get; set; }
    }
}