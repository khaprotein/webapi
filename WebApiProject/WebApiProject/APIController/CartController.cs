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
        Database1Entities1 db = new Database1Entities1();
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
            Shopping_Cart oldcart = db.Shopping_Cart.FirstOrDefault(c => c.user_id == cart.user_id && c.product_size_quantity_id == cart.product_size_quantity_id);
            if (oldcart == null)
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
            }
            else
            {
                oldcart.quantity+= cart.quantity;
            } 
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpResponseMessage PutCart(GioHang cart)
        {
            Shopping_Cart oldcart = db.Shopping_Cart.FirstOrDefault(c => c.cart_id == cart.cart_id);
            if (oldcart == null)
            {
                // Không tìm thấy giỏ hàng, trả về lỗi Not Found
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            // Kiểm tra số lượng tồn kho
            if (cart.quantity > oldcart.Product_Size_Quantity.quantity)
            {
                // Số lượng trong giỏ hàng lớn hơn số lượng tồn kho, trả về lỗi BadRequest
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Số lượng tồn kho không đủ");
            }

            // Cập nhật số lượng trong giỏ hàng
            oldcart.quantity = cart.quantity;

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Trả về mã OK nếu mọi thứ diễn ra thành công
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
