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
    public class cartController : Controller
    {
        // GET: cart
        Database1Entities1 db = new Database1Entities1();
        private static int _latestCartId = 0;
        public async Task<ActionResult> Index()
        {
            var user = (User)Session["user"];
            if (user != null)
            {
                string id_user = user.user_id;
                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                }
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                }
                var list = await Getcart(id_user);
                if (list != null)
                {
                    return View(list);
                }
            }
            else
            {
                ViewBag.TotalQuantity = 0;
            }
            return RedirectToAction("Index", "Login");    
        }

        public async Task<List<GioHang>> Getcart(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Cart/Getcartbyiduser?id_user=" + id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<GioHang> listcart = new List<GioHang>();
                    listcart = res.Content.ReadAsAsync<List<GioHang>>().Result;
                    return listcart;
                }
                return null;
            }
        }

        public class CartIdGenerator
        {      
            public static string GenerateCartId()
            {
                _latestCartId++;
                return "C" + _latestCartId.ToString("0000");
            }
        }
       
        [HttpPost]
        public async Task<ActionResult> Addtocart(string id_product, int id_size, int soluong, decimal price)
        { 
            var user = (User)Session["user"];
            if (user != null)
            {
                var sotonkho = db.Product_Size_Quantity.Where(c => c.product_size_quantity_id == id_size).FirstOrDefault();
                
                if(sotonkho.quantity < soluong)
                {
                    TempData["ErrorMessage"] = "Sản phẩm đã hết hàng!";
                    return RedirectToAction("Index", "Home");
                }

               GioHang cart = new GioHang
               {
                    cart_id = CartIdGenerator.GenerateCartId(),
                    product_id = id_product,
                    product_size_quantity_id = id_size,
                    quantity = soluong,
                    user_id = user.user_id,
                    totalprice = price
                    
                };
                
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Cart/AddToCart",cart );
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng!";
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["ErrorMessage"] = "Không thêm sản phẩm được!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCart(string cartId, string productId, int sizequantityId, int quantity)
        {
            var user = (User)Session["user"];

            // Tạo đối tượng GioHang để cập nhật giỏ hàng
            GioHang updateRequest = new GioHang
            {
                cart_id = cartId,
                product_id = productId, // Đã chỉnh sửa từ user.user_id thành productId
                product_size_quantity_id = sizequantityId,
                quantity = quantity,
                totalprice = 0 // Có thể cần cập nhật lại giá trị này tùy thuộc vào logic của ứng dụng
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                             Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                // Gửi yêu cầu PUT đến API để cập nhật giỏ hàng
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Cart/PutCart", updateRequest);

                // Kiểm tra kết quả trả về từ yêu cầu API
                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Đã cập nhật giỏ hàng!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật giỏ hàng!";
                }

                // Chuyển hướng người dùng đến trang Index của Cart sau khi cập nhật
                return RedirectToAction("Index", "Cart");
            }
        }



        public async Task<ActionResult> DeleteCart(string cartid)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Cart/Deletecart?id_cart=" + cartid);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã bỏ sản phẩm khỏi giỏ hàng!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra!";
                }

                return RedirectToAction("Index", "Cart");
            }
        }

    }


}