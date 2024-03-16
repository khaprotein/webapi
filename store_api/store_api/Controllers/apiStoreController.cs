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

        [HttpGet]
        [Route("api/apiStore/Product")]
        public Product getproduct(string product_id)
        {
            return db.Products.FirstOrDefault(c=>c.product_id==product_id);
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

        [HttpGet]
        [Route("api/apiStore/userbyid")]
        public User getuser(string iduser)
        {
            return db.Users.SingleOrDefault(c=>c.user_id==iduser);
        }

        [HttpGet]
        [Route("api/apiStore/userbyid")]
        public Shopping_Cart get_cart(string iduser)
        {
            return db.Shopping_Cart.FirstOrDefault(c=>c.user_id==iduser);
        }


        [HttpPut]
        [Route("api/apiStore/updateuser")]
        public IHttpActionResult UpdateUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Find the user in the database by user ID
            var userInDb = db.Users.SingleOrDefault(u => u.user_id == updatedUser.user_id);
            if (userInDb == null)
            {
                return NotFound(); // User not found
            }

            // Update the user information
            userInDb.name = updatedUser.name;
            userInDb.email = updatedUser.email;
            userInDb.phonenumber = updatedUser.phonenumber;
            userInDb.password = updatedUser.password;
            userInDb.address = updatedUser.address;
            // Update other fields as needed

            // Save changes to the database
            db.SaveChanges();

            return Ok("Cập nhật thông tin tài khoản thành công!");
        }
            


    }
}
