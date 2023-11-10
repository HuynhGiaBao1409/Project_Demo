using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Teacher
{
    public class StudentAttendance
    {
        public int AddmissionNo { get; set; }

        public string Studentname { get; set; }

        public DateTime Date { get; set; }

        public string Remark { get; set; }
    }
}