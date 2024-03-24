using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    public class HomeController : Controller

    {
        Database1Entities db = new Database1Entities();
        public async Task<ActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
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

            List<Shoes> list = await GetProduct();
            return View(list);
        }
        private async Task<List<Shoes>> GetProduct()   // Hàm Gọi API trả về list user
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproduct");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    return list;
                }
                return null;
            }
        }
    }
}
