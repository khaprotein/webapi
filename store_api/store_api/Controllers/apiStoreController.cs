using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using store_api.Models;

namespace store_api.Controllers
{
    public class apiStoreController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();

        [HttpGet]
        [Route("api/apiStore/listProduct")]
        public List<Product> getallproduct()
        {
            return db.Products.ToList();
        }

        [HttpPost]
        [Route("api/apiStore/register")]
        public IHttpActionResult Register(User newuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }          
            db.Users.Add(newuser);
            db.SaveChanges();

            return Ok("Đăng ký tài khoản thành công!");
        }

        [HttpPost]
        [Route("api/apiStore/login")]
        public IHttpActionResult Login(Dangnhap tk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xác thực người dùng dựa trên thông tin đăng nhập
            var user = db.Users.SingleOrDefault(u => u.email == tk.email && u.password == tk.pass);
            if (user == null)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác.");
                return BadRequest(ModelState);
            }

            // Nếu thông tin đăng nhập chính xác, bạn có thể thực hiện các thao tác khác tại đây, chẳng hạn như tạo token, lưu trạng thái đăng nhập, vv.

            return Ok("Đăng nhập thành công!");
        }




    }
}
