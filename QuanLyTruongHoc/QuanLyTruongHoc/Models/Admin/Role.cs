using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Role
    {
        [Display(Name = "ID Quyền ")]
        public int RoleID { get; set; }
        [Display(Name = "Quyền")]
        public string Rolename { get; set; }

        [Display(Name = "ID nhân sự ")]
        public int StaffID { get; set; }

        [Display(Name = "Tên nhân sự ")]
        public string Staffname { get; set; }

        [Display(Name = "Tên đăng nhập ")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu ")]
        public string Password { get; set; }
    }
}