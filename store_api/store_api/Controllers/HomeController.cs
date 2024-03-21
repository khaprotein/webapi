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
    public class HomeController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        public async Task<ActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (Session["user"] != null)
            {
                var user = (User)Session["user"];
                var cartItems = db.Shopping_Cart.Where(c => c.user_id == user.user_id);
                int totalQuantity = cartItems.Any() ? (int)cartItems.Sum(c => c.quantity) : 0;
                ViewBag.TotalQuantity = totalQuantity;
            }
            else
            {
                ViewBag.TotalQuantity = 0;
            }
            var list = await GetAllProduct();
            return View(list);
        }

        public async Task<List<Product>> GetAllProduct()   // Hàm Gọi API trả về list 
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/apiStore/listProduct");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Product> list = new List<Product>();
                    list = res.Content.ReadAsAsync<List<Product>>().Result;
                    return list;
                }
                return null;
            }
        }


    }
}
