using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class OrdersController : ApiController
    {
        Database1Entities1 db = new Database1Entities1();
        [HttpGet]
        public IHttpActionResult Getorders(string id_user)
        {
            var list = db.Orders.Where(c => c.user_id == id_user).
            Select(o=>new DonHang {
                orders_id = o.orders_id,
                user_id = o.user_id,
                total_amount = o.total_amount,
                orders_date = o.orders_date,
                shipping_address = o.shipping_address,
                user_phone = o.user_phone,
                oderstatus_id = o.oderstatus_id,
                status = o.oderStatusCheck.status
            }).ToList();

            return Ok(list);
        }

      

        [HttpPost] 
        public HttpResponseMessage PostOrder(DonHang o)
        { 
                    // Tạo một đối tượng Order mới từ đối tượng DonHang được gửi lên
                    Order newOrder = new Order
                    {
                        orders_id = o.orders_id,
                        user_id = o.user_id,
                        total_amount = o.total_amount,
                        orders_date = DateTime.Now, // Sử dụng thời gian hiện tại cho ngày đặt hàng
                        shipping_address = o.shipping_address,
                        user_phone = o.user_phone,
                        oderstatus_id = o.oderstatus_id,
                    };

                    // Thêm đối tượng mới vào DbSet của Orders và lưu thay đổi vào cơ sở dữ liệu
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    // Trả về một phản hồi HTTP 201 Created nếu mọi thứ thành công
                    return Request.CreateResponse(HttpStatusCode.OK);
             
            
        }

       

    }
}
