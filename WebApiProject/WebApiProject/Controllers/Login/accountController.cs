using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers.Login
{
    public class accountController : Controller
    {
        // GET: account
        public ActionResult Info()
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
                var res = await httpClient.PutAsJsonAsync(baseUrl + "api/Users/Putuser", upuser);

                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Lưu thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi không lưu được");
                }
            }
            return View("Info");
        }
    }
}