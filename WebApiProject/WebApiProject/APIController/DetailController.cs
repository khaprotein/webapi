using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;


namespace WebApiProject.APIController
{
    public class DetailController : ApiController
    {
        Database1Entities1 db = new Database1Entities1();

        [HttpGet]   
        public IHttpActionResult Getdetail(string id_order)
        {
            var list = db.Order_Details.Where(c => c.order_id == id_order).Select(od => new ChiTietDH
            {
                order_details_id = od.order_details_id,
                product_size_quantity_id = od.product_size_quantity_id,
                order_id = od.order_id,
                price_oder = od.price_oder,
                quantity = od.quantity,
                hinhanh = od.Product_Size_Quantity.Product.Product_image.productimage_url,
                kichthuoc = od.Product_Size_Quantity.size,
                tensanpham = od.Product_Size_Quantity.Product.product_name
            }).ToList();

            return Ok(list);
        }

        [HttpPost]  
        public HttpResponseMessage PostDetail(ChiTietDH od)
        {

            Order_Details newod = new Order_Details
            {
                order_details_id = od.order_details_id,
                order_id = od.order_id,
                product_size_quantity_id = od.product_size_quantity_id,
                quantity = od.quantity,
                price_oder = od.price_oder
            };
            db.Order_Details.Add(newod);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
