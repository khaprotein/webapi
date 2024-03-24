using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;


namespace WebApiProject.APIController
{
    public class CartController : ApiController
    {
        Database1Entities db = new Database1Entities();
        [HttpGet]
        public IHttpActionResult Getcartbyiduser(string id_user)
        {
            if (string.IsNullOrEmpty(id_user))
            {
                return BadRequest("Invalid user ID");
            }  
            var list = db.Shopping_Cart.Where(c => c.user_id == id_user).Select(c=>new GioHang {
                cart_id = c.cart_id,
                product_id = c.product_id,
                product_size_quantity_id = c.product_size_quantity_id,
                quantity = c.quantity,
                user_id = c.user_id,
                totalprice = c.Product.price    
            }).ToList();

            return Ok(list);
        }
        [HttpPost]
        public HttpResponseMessage PostCart(GioHang cart)
        {
            Shopping_Cart newcart = new Shopping_Cart
            {
                cart_id = cart.cart_id,
                product_id = cart.product_id,
                product_size_quantity_id = cart.product_size_quantity_id,
                quantity = cart.quantity,
                user_id = cart.user_id
            };
            
            db.Shopping_Cart.Add(newcart);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpResponseMessage PutCart(Shopping_Cart cart)
        {
            Shopping_Cart oldcart = db.Shopping_Cart.Where(c => c.cart_id == cart.cart_id).FirstOrDefault();
            oldcart.product_id = cart.product_id;
            oldcart.product_size_quantity_id = cart.product_size_quantity_id;
            oldcart.quantity = cart.quantity;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage Deletecart(string id_cart)
        {
            Shopping_Cart cart = db.Shopping_Cart.Where(c => c.cart_id == id_cart).FirstOrDefault();
            db.Shopping_Cart.Remove(cart);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}
