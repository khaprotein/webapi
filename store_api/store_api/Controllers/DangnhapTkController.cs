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
        public ActionResult dangnhapp(string email, string pass)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.SingleOrDefault(u => u.email == email && u.password == pass);
                if (user != null)
                {
                    if (user.role_id == "R2")
                    {
                        TempData["SuccessMessage"] = "Chào mừng " + user.name + " "+user.user_id;
                        // Lưu ID người dùng vào Session
                        Session["user"] = user;
                        

                        // Redirect đến trang chính sau khi đăng nhập thành công
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("email", "Email hoặc mật khẩu không chính xác.");
                return View("Index");
            }
            return View("Index");
        }
       
            // Action đăng xuất
            public ActionResult Dangxuat()
            {
                // Xóa Session user
                Session["user"] = null;

                // Chuyển hướng đến trang chính sau khi đăng xuất
                return RedirectToAction("Index", "Home");
            }
     


    }
}