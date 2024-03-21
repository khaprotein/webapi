using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace store_api.Models
{
    public class AddToCartRequest
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}