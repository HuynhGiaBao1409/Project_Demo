using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Staff
    {
        [Display(Name = "ID nhân sự ")]
        public int StaffID { get; set; }

        [Display(Name = "Tên nhân sự")]
        public string Staffname { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "ID bộ phận ")]
        public int DesignationID { get; set; }

        [Display(Name = "Bộ phận")]
        public string Designation { get; set; }

        [Display(Name = "Số điện thoại 1")]
        public string Phone1 { get; set; }

        [Display(Name = "Số điện thoại 2 ")]
        public string phone2 { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Đại chỉ nhân sự ")]
        public string StaffAddress { get; set; }

        [Display(Name = "Ngày nhận ")]
        public DateTime DateOfAppointment { get; set; }

        [Display(Name = "Quốc gia")]
        public string Nationality { get; set; }

        [Display(Name = "Kinh nghiệm")]
        public int YOE { get; set; }

        [Display(Name = "Chỗ làm cũ")]
        public string previouseOrg { get; set; }

        [Display(Name = "Hình ảnh ")]
        public string Resume { get; set; }

        public string AccountStatus { get; set; }

        public HttpPostedFileBase ResumeImageFile { get; set; }
    }
}