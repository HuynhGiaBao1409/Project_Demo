using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class CollectFee
    {
        public int ID { get; set; }

        [Display(Name = "ID")]
        public int AdmissionNo { get; set; }

        [Display(Name = "Tên học sinh")]
        public string StudentName { get; set; }

        [Display(Name = "Mã lớp")]
        public int ClassLevelID { get; set; }

        public string ClassLevelname { get; set; }

        [Display(Name = "Học kì")]
        public string Session { get; set; }

        [Display(Name = "Số tiền")]
        public decimal Amount { get; set; }

        [Display(Name = "Người giao dịch ")]
        public string TellerNo { get; set; }

        [Display(Name = "Ngân hàng")]
        public string Bank { get; set; }

        [Display(Name = "Ngày ")]
        public DateTime Date { get; set; }
    }
}