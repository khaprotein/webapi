using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;
using PagedList;


namespace WebApiProject.Controllers
{
    public class HomeController : Controller

    {
        Database1Entities1 db = new Database1Entities1();    
        public async Task<ActionResult> Index(int? page)
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

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            List<Shoes> list = await GetProduct();
            return View(list.ToPagedList(pageNumber,pageSize));
        }

        public async Task<ActionResult> Getlistbyid_category(int? page, int id_category)
        { 
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            List<Shoes> list = await GetProductbyidcategory(id_category);
            return View(list.ToPagedList(pageNumber, pageSize));
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

        private async Task<List<Shoes>> GetProductbyidcategory(int id_category)   // Hàm Gọi API trả về list user
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid_category?id_category=" + id_category);
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
