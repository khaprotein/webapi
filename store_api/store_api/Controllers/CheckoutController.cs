using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using store_api.Models;


namespace store_api.Controllers
{
    public class CheckoutController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        // GET: Checkout
        
        public async Task<ActionResult> Index()
        {
            var user = (User)Session["user"];
            var list = await Getorder(user.user_id);
            return View(list);
        }

        public async Task< List<Order>> Getorder(string user_id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/apiStore/getOrder?id_user="+user_id);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Order> list = new List<Order>();
                    list = res.Content.ReadAsAsync<List<Order>>().Result;
                    return list;
                }  
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(string shippingAddress, decimal totalAmount)
        {
            try
            {
                if(totalAmount == 40)
                {
                    TempData["ErrorMessage"] = "Giỏ hàng của bạn trống!";
                    return RedirectToAction("Index", "Cart");
                }
                var user = (User)Session["user"];
                Hoadon dl = new Hoadon
                {
                    userID = user.user_id,
                    Diachi = shippingAddress,
                    Tongtien = totalAmount,
                    SDT = user.phonenumber
                };
                // Sử dụng HttpClient để gửi yêu cầu tạo đơn hàng đến API
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/apiStore/createOrderAndDetail", dl);

                    // Kiểm tra mã trạng thái của phản hồi từ API
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Nếu thành công, gửi thông báo thành công và chuyển hướng người dùng đến trang giỏ hàng
                        TempData["SuccessMessage"] = "Đơn hàng đã được đặt!";
                        return RedirectToAction("Index", "Cart");
                    }
                    // Nếu không thành công, gửi thông báo lỗi và chuyển hướng người dùng đến trang giỏ hàng
                    TempData["ErrorMessage"] = "Đơn hàng không đặt được!";
                    return RedirectToAction("Index", "Cart");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và gửi thông báo lỗi nếu có lỗi xảy ra
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi đặt hàng: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

    }
}