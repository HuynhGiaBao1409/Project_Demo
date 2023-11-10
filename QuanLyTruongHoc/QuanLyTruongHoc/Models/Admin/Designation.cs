using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Designation
    {
        [Display(Name = "ID bộ phận")]
        public int DesignationID { get; set; }
        [Display(Name = "Bộ phận")]
        public string DesignationName { get; set; }
    }
}