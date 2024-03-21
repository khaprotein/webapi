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
    public class SanphamController : Controller
    {
        // GET: Sanpham
        Store_apiEntities db = new Store_apiEntities();
       
        public async Task<ActionResult> product(string id_product)
        {
            var sp = await GetProduct(id_product);

            return View(sp);
        }

        public async Task<Product> GetProduct(string id_product)   // Hàm Gọi API trả về list 
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/apiStore/Product?product_id="+id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Product sp = new Product();
                    sp = res.Content.ReadAsAsync<Product>().Result;
                    return sp;
                }
                return null;
            }
        }

    }
}