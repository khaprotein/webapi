using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers.Orders
{
    public class ordersController : Controller
    {
        Database1Entities1 db = new Database1Entities1();
        private static int _latestOrderId = 0;
        private static int _latestOrderDetailId = 0;
        // GET: orders
        public async Task<ActionResult> Index(string user_id)
        {
            var list = await Getorder(user_id);

            return View(list);
        }
        public async Task<List<DonHang>> Getorder(string user_id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/Getorders?id_user=" + user_id);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DonHang> list = new List<DonHang>();
                    list = res.Content.ReadAsAsync<List<DonHang>>().Result;
                    return list;
                }
                return null;
            }
        }

        public class OrderIdGenerator
        {
            public static string GenerateOrderId()
            {
                _latestOrderId++;
                return "O" + _latestOrderId.ToString("0000");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Createorder(string shippingAddress, decimal totalAmount)
        {
            if (totalAmount == 40)
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn trống!";
                return RedirectToAction("Index", "Cart");
            }
            var user  = (User)Session["user"]; ;
            DonHang neworder = new DonHang
            {
                orders_id = OrderIdGenerator.GenerateOrderId(),
                user_id = user.user_id,
                total_amount = totalAmount,
                orders_date = DateTime.Now,
                shipping_address = shippingAddress,
                user_phone = user.phonenumber,
                oderstatus_id = 1

            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Orders/PostOrder", neworder);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Createorderdetail", new { id_order = neworder.orders_id, id_user = user.user_id });
                    //TempData["SuccessMessage"] = "Đơn hàng đã được đặt!";
                    //return RedirectToAction("Index", "Cart");
                }
                TempData["ErrorMessage"] = "Đơn hàng không đặt được!";
                return RedirectToAction("Index", "Cart");

            }

        }

        public async Task<ActionResult> detail(string id_order)
        {
           
            var list = await GetDetail(id_order);

            return View(list);
        }
        public async Task<List<ChiTietDH>> GetDetail(string id_order)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Detail/Getdetail?id_order=" +  id_order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<ChiTietDH> list = new List<ChiTietDH>();
                    list = res.Content.ReadAsAsync<List<ChiTietDH>>().Result;
                    return list;
                }
                return null;
            }
        }



        public class OrderDetailIdGenerator
        {  
            public static string GenerateOrderDetailId()
            {
                _latestOrderDetailId++;
                return "OD" + _latestOrderDetailId.ToString("00000");
            }
        }
        public async Task<ActionResult> Createorderdetail(string id_user, string id_order)
        {
            List<Shopping_Cart> cartItems = db.Shopping_Cart.Where(c => c.user_id == id_user).ToList();
            try
            {
                foreach (var item in cartItems)
                {
                    ChiTietDH newOD = new ChiTietDH
                    {
                        order_details_id = OrderDetailIdGenerator.GenerateOrderDetailId(),
                        order_id = id_order,
                        product_size_quantity_id = item.product_size_quantity_id,
                        quantity = item.quantity,
                        price_oder = item.Product.price
                    };

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Detail/PostDetail", newOD);
                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Nếu thành công, gửi thông báo thành công và tiếp tục vòng lặp
                            continue;
                        }
                        else
                        {
                            //TempData["ErrorMessage"] = "Đơn hàng không đặt được!";
                            TempData["ErrorMessage"] = "Loi tao chi  tiet don hang!";
                            return RedirectToAction("Index", "Cart");
                        }
                    }
                }

                // Nếu không có lỗi, thông báo thành công và chuyển hướng người dùng đến trang giỏ hàng
                TempData["SuccessMessage"] = "Đơn hàng đã được đặt!";
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi đặt hàng: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }



    }
}
    