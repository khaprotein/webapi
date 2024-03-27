using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class NguoiDung
    {
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
    }
}