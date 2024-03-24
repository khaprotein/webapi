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
    public class productController : Controller
    {
        // GET: product
        Database1Entities db = new Database1Entities();
        public async Task<ActionResult> Index()
        {
            List<Shoes> list = await GetProduct();
            return View(list);
        }
        public async Task<List<Shoes>> GetProduct()   // Hàm Gọi API trả về list 
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproduct");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                     List < Shoes > list = new  List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    return list;
                }
                return null;
            }
        }


        public async Task<ActionResult> ProductDetail(string id_product)
        {
            Shoes sp = await GetProductbyID(id_product);

            return View(sp);
        }
        public async Task<Shoes> GetProductbyID(string id_product)   // Hàm Gọi API trả về list 
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid?id_product=" + id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Shoes sp = new Shoes();
                    sp = res.Content.ReadAsAsync<Shoes>().Result;
                    return sp;
                }
                return null;
            }
        }

    }
}