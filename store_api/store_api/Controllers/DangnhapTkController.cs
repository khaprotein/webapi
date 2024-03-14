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
    public class DangnhapTkController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        // GET: DangnhapTk
        // GET: DangnhapTK
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string pass)
        {
            if (!ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.email == email && u.password == pass);
                if (user != null)
                {
                    if (user.Role.role_name == "R2")
                    {
                        TempData["SuccessMessage"] = "Chào mừng " + user.name;

                        // Lưu ID người dùng vào Session
                        Session["UserID"] = user.user_id;

                        // Redirect đến trang chính sau khi đăng nhập thành công
                        return RedirectToAction("Index", "Home");
                    }

                } 
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác.");
                return View();

            }
            return View();
        }




    }
}