using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Session
    {
        [Display(Name = "ID năm học ")]
        public int SessionID { get; set; }

        [Display(Name = "Năm học ")]
        public string SessionName { get; set; }

        [Display(Name = "Học kì ")]
        public string Term { get; set; }

        [Display(Name = "Hoạt động ")]
        public string IsActive { get; set; }
    }
}