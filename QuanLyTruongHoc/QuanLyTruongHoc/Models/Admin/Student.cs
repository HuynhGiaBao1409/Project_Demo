using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Student
    {
        [Display(Name = "STT ")]
        public int AdmissionNo { get; set; }

        [Display(Name = "Tên học sinh")]
        public string StudentName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime DOB { get; set; }

        [Display(Name = "Quốc gia ")]
        public string Nationality { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Khu vực ")]
        public string Religion { get; set; }

        [Display(Name = "ID năm học")]
        public int SessionID { get; set; }

        [Display(Name = "Năm học ")]
        public string SessionName { get; set; }

        [Display(Name = "Học kì")]
        public string Term { get; set; }

        [Display(Name = "Cấp lớp ")]
        public string ClassLevel { get; set; }

        [Display(Name = "ID cấp lớp ")]
        public int ClassLevelID { get; set; }

        [Display(Name = "Mã lớp ")]
        public string ClassAphabet { get; set; }

        [Display(Name = "Ngày nhận")]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Tên cha ")]
        public string Fathername { get; set; }

        [Display(Name = "Tên mẹ ")]
        public string Mothername { get; set; }

        [Display(Name = "SĐT cha ")]
        public string FatherPhone { get; set; }

        [Display(Name = "SĐT mẹ ")]
        public string MotherPhone { get; set; }

    }
}