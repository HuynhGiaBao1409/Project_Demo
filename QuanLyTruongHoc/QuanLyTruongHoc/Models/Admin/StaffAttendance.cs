using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class StaffAttendance
    {
        public int ID { get; set; }

        [Display(Name = "ID nhân sự ")]
        public int StaffID { get; set; }

        [Display(Name = "Tên nhân sự ")]
        public string Staffname { get; set; }

        [Display(Name = "Ngày ")]
        public DateTime Date { get; set; }

        [Display(Name = "Điểm danh ")]
        public string Remark { get; set; }
    }
}