using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class DangNhap
    {
        [Required(ErrorMessage = " Bạn chưa nhập Email!")]
        public string email { get; set; }

        [Required(ErrorMessage = " Bạn chưa nhập mật khẩu!")]
        [DataType(DataType.Password)]
        public string pass { get; set; }
    }
}