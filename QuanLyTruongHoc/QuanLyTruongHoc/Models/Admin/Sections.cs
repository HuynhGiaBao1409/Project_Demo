using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Sections
    {
        [Display(Name = "ID cấp chung ")]
        public int SectionID { get; set; }
        [Display(Name = "Cấp chung")]
        public string SectionName { get; set; }
    }
}