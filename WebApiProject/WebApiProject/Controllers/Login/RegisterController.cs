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
    public class RegisterController : Controller
    {
        Database1Entities db = new Database1Entities();
        
        // GET: Register
        public ActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            return View();
        }
        public class UserIdGenerator
        {
            private static int _latestUserId = 0;

            public static string GenerateUserId()
            {
                _latestUserId++;
                return "U" + _latestUserId.ToString("000");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Dangky(DangKy accountInfo)   // Hàm Gọi API trả về list 
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.email == accountInfo.email))
                {
                    ModelState.AddModelError("email", "Email đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                if (db.Users.Any(u => u.phonenumber == accountInfo.soDienThoai))
                {
                    ModelState.AddModelError("email", "Số điện thoại đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                  
                string newUserId = UserIdGenerator.GenerateUserId();
                // Tạo một đối tượng User mới và lưu vào cơ sở dữ liệu
                User newUser = new User
                {
                    user_id = newUserId,
                    role_id = "R2",
                    name = accountInfo.hoten,
                    email = accountInfo.email,
                    phonenumber = accountInfo.soDienThoai,
                    password = accountInfo.password,
                    address = accountInfo.diachi
                };

                using (var httpClient = new HttpClient())
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                     Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
                    var res = await httpClient.PostAsJsonAsync(baseUrl + "api/Users/Postuser", newUser);
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Đăng ký thành công hãy tiếp tục thực hiện đăng nhập";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi khi tạo tài khoản");
                    }
                }
            }
            return View("Index", accountInfo);
        }


    }
}