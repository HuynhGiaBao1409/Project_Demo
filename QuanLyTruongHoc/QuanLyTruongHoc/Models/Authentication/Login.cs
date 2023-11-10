using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Authentication
{
    public class Login
    {
        [Display(Name = "Tên đăng nhập ")]
        public string username { get; set; }
        [Display(Name = "Mật khẩu ")]
        public string password { get; set; }
    }
}