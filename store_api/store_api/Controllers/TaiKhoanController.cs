using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using store_api.Models;

namespace store_api.Controllers
{
    public class TaiKhoanController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        // GET: TaiKhoan
        public ActionResult ThongTinTaiKhoan()
        {   
            var Khachhang = (User)Session["user"];

            return View(Khachhang);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(User upuser)   // Hàm Gọi API trả về list 
        {   
                 using (var httpClient = new HttpClient())
                 {
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";         
                        var res = await httpClient.PutAsJsonAsync(baseUrl + "api/apiStore/updateuser", upuser);

                        if (res.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Lưu thành công";
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lỗi không lưu được");
                        }
                 }
                
            return View("ThongTinTaiKhoan");
        }



    }
}