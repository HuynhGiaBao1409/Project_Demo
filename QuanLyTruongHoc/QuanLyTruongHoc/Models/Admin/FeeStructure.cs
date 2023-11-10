using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class FeeStructure
    {
        public int FeeStructureID { get; set; }

        [Display(Name = "ID khoản phí")]
        public int FeeTypeID { get; set; }

        [Display(Name = "Khoản phí")]
        public string FeeTypename { get; set;}
        [Display(Name = "ID học kì ")]
        public int SectionID { get; set; }
        [Display(Name = "Học kì ")]
        public string SectionName { get; set; }
        [Display(Name = "Số tiền")]
        public decimal Amount { get; set; }

    }
}