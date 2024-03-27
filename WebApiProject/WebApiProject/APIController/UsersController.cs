using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class UsersController : ApiController
    {
        Database1Entities1 db = new Database1Entities1();
        public IHttpActionResult Getuser()
        {
            var list = db.Users.Select(u=>new NguoiDung{
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address
            }).ToList();

            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Getuserbyid(string id_user)
        {
            var u = db.Users.Where(c => c.user_id == id_user).FirstOrDefault();
            
            NguoiDung user =  new NguoiDung
            {
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address
            };
            return Ok(user);
        }
        [HttpPost]
        public HttpResponseMessage Postuser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK); 
        }
        [HttpPut]
        public HttpResponseMessage Putuser(NguoiDung user)
        {
            User olduser = db.Users.Where(c => c.user_id == user.user_id).FirstOrDefault();
            olduser.name = user.name;
            olduser.email = user.email;
            olduser.password = user.password;
            olduser.role_id = user.role_id;
            olduser.phonenumber = user.phonenumber;
            olduser.address = user.address;

            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
