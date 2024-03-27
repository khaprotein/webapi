using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers.Login
{
    public class LoginController : Controller
    {
        Database1Entities1 db = new Database1Entities1();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login(string email, string pass)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.SingleOrDefault(u => u.email == email && u.password == pass);
                if (user != null)
                {
                    if (user.role_id == "R2")
                    {
                        TempData["SuccessMessage"] = "Chào mừng " + user.name;
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

        public ActionResult signout()
        {
            // Xóa Session user
            Session["user"] = null;

            // Chuyển hướng đến trang chính sau khi đăng xuất
            return RedirectToAction("Index", "Home");
        }

    }
}