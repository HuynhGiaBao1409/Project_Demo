using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class AssignClass
    {
        [Key]
        public int ID { get; set; }


        [Display(Name = "ID Nhân sự")]
        [Required(ErrorMessage = "ID nhân sự không được bỏ trống")]
        public int StaffID { get; set; }

        [Display(Name = "Tên Nhân sự")]
        [Required(ErrorMessage = "Tên nhân sự không được bỏ trống")]
        public string Staffname { get; set; }

        [Display(Name = "ID Cấp")]
        [Required(ErrorMessage = "ID cấp không được bỏ trống")]
        public int ClassLevelID { get; set; }

        [Display(Name = "Cấp")]
        [Required(ErrorMessage = "Cấp không được bỏ trống")]
        public string ClassLevelName { get; set; }

        [Display(Name = "Mã Lớp")]
        [Required(ErrorMessage = "Mã lớp không được bỏ trống")]
        public string Prefix { get; set; }
    }
}