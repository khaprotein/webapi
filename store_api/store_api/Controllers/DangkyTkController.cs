using store_api.Models;
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
    public class DangkyTkController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        // GET: DangkyTk
        public async Task<ActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Dangky(DangKyTaiKhoan accountInfo)   // Hàm Gọi API trả về list 
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.email == accountInfo.email))
                {
                    ModelState.AddModelError("email", "Email đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                if (db.Users.Any(u =>  u.phonenumber == accountInfo.soDienThoai))
                {
                    ModelState.AddModelError("email", "Số điện thoại đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                // Tạo một user_id mới tự động
                string latestUserId = db.Users.OrderByDescending(u => u.user_id).Select(u => u.user_id).FirstOrDefault();
            int userIdSuffix = 1;
            if (!string.IsNullOrEmpty(latestUserId))
            {
                userIdSuffix = int.Parse(latestUserId.Substring(1)) + 1;
            }
            string newUserId = "u" + userIdSuffix;

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
                    var res = await httpClient.PostAsJsonAsync(baseUrl + "api/apiStore/register", newUser);
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