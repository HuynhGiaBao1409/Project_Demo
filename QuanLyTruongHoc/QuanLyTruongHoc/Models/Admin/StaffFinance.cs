using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class StaffFinance
    {
        public int ID { get; set; }

        [Display(Name = "ID nhân sự ")]
        public int staffID { get; set; }

        [Display(Name = "Tên nhân sự ")]
        public string StaffName { get; set; }

        [Display(Name = "Lương căn bản")]
        public decimal BasicSalary { get; set; }

        [Display(Name = "Trợ cấp chỗ ở")]
        public decimal HouseAllowance { get; set; }

        [Display(Name = "Trợ cấp phương tiện ")]
        public decimal TransportAllowance { get; set; }

        [Display(Name = "Phạt đi trễ ")]
        public decimal LateComingFee { get; set; }

        [Display(Name = "Thuế")]
        public decimal Tax { get; set; }

        [Display(Name = "Thuế VAT")]
        public decimal Vat { get; set; }

        [Display(Name = "Tổng lương ")]
        public decimal TotalPay { get; set; }
    }
}