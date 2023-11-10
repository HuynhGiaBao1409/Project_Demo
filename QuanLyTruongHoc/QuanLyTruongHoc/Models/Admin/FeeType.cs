using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class FeeType
    {
        public int FeeTypeID { get; set; }

        [Display(Name = "Loại phí")]
        public string FeeName { get; set;}
    }
}