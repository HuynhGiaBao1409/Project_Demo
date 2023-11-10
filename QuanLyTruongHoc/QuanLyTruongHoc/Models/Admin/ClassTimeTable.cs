using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class ClassTimeTable
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Mã Lớp")]
        [Required(ErrorMessage = "Mã cấp không được bỏ trống")]
        public int ClassLevelID { get; set; }
        [Display(Name = "Tên Lơp ")]
        public string ClassLevelName { get; set; }
        [Display(Name = "Ngày ")]
        public string Days { get; set; }

        [Display(Name = "Môn 1 ")]
        public string Period1 { get; set; }
        [Display(Name = "Môn 2 ")]

        public string Period2 { get; set; }
        [Display(Name = "Môn 3 ")]

        public string Period3 { get; set; }
        [Display(Name = "Môn 4 ")]

        public string Period4 { get; set; }
        [Display(Name = "Môn 5 ")]

        public string Period5 { get; set; }
        [Display(Name = "Môn 6 ")]

        public string Period6 { get; set; }
    }
}