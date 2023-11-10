using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class ClassLevel
    {

        public int LevelID { get; set; }

        [Display(Name = "Cấp")]
        [Required(ErrorMessage = "Cấp không được bỏ trống")]
        public string Levelname { get; set; }
        public int SectionID { get; set; }

        [Display(Name = "Cấp chung")]
        [Required(ErrorMessage = "Cấp chung không được bỏ trống")]
        public string Section { get; set; }
    }
}