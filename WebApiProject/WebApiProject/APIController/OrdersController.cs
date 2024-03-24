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
        Database1Entities db = new Database1Entities();
        [HttpGet]
        public List<Order> Getorders(string id_user)
        {
            return db.Orders.Where(c => c.user_id == id_user).ToList();
        }

        [HttpGet]
        public List<Order_Details> Getdetail(string id_order)
        {
            return db.Order_Details.Where(c => c.order_id == id_order).ToList();
        }

        [HttpPost]
        public HttpResponseMessage PostOrder(Order o)
        {
            db.Orders.Add(o);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage PostDetail(Order_Details od)
        {
            db.Order_Details.Add(od);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
