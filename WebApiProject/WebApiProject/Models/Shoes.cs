using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class Shoes
    {
        public string product_id { get; set; }
        public Nullable<int> category_id { get; set; }
        public string product_name { get; set; }
        public Nullable<int> brand_id { get; set; }
        public Nullable<decimal> price { get; set; }
        public string productimage_url { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
    }
}