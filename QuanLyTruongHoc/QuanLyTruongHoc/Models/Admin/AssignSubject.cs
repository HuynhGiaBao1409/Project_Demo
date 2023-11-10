using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class AssignSubject
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "ID Nhân sự ")]
        [Required(ErrorMessage = "ID nhân sự không được bỏ trống")]
        public int StaffID { get; set; }

        [Display(Name = "Tên Nhân sự ")]
        [Required(ErrorMessage = "Tên nhân sự không được bỏ trống")]
        public string staffname { get; set; }

        [Display(Name = "Mã Môn  ")]
        [Required(ErrorMessage = "Mã Môn không được bỏ trống")]
        public int SubjectID { get; set; }

        [Display(Name = "Môn học ")]
        [Required(ErrorMessage = "Môn học không được bỏ trống")]
        public string Subject { get; set; }

        [Display(Name = "Cấp ")]
        [Required(ErrorMessage = "ID cấp không được bỏ trống")]
        public int ClassLevelID { get; set; }

        [Display(Name = "Lớp ")]
        [Required(ErrorMessage = "Cấp không được bỏ trống")]
        public string ClassLevelname { get; set; }

        [Display(Name = "Mã lớp ")]
        [Required(ErrorMessage = "Mã lớp không được bỏ trống")]
        public string Prefix { get; set; }
    }
}