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
    public class CartController : Controller
    {
        // GET: Cart
        Store_apiEntities db = new Store_apiEntities();
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
                if ( list!= null)
                {
                    
                    return View(list);
                }
            }
            else
            {
                ViewBag.TotalQuantity = 0;
            }
            return RedirectToAction("Index", "DangnhapTK"); 
        }

        public async Task<List<Shopping_Cart>> Getcart(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/apiStore/GetCart?userId="+id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shopping_Cart> listcart = new List<Shopping_Cart>();
                    listcart = res.Content.ReadAsAsync<List<Shopping_Cart>>().Result;
                    return listcart;
                }
                return null;
            }

        }

        public async Task<ActionResult> Addtocart(string id_product, int soluong)   
        {
            var user = (User)Session["user"];
            if (user != null)
            {       
                var add = new AddToCartRequest
                {
                    UserId = user.user_id,
                    ProductId = id_product,
                    Quantity = soluong
                };
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/apiStore/AddToCart", add);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng!";
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["ErrorMessage"] = "Sản phẩm đã hết hàng!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {  
                return RedirectToAction("Index", "DangnhapTK");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCart(string userId, string productId, int quantity)
        {
            var updateRequest = new AddToCartRequest
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/apiStore/UpdateCart", updateRequest);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã cập nhật giỏ hàng!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật giỏ hàng!";
                }

                return RedirectToAction("Index", "Cart");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCart(string cartid)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/apiStore/deletecart?cartid=" + cartid);
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