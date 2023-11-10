using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTruongHoc.Models.Admin
{
    public class Subject
    {
        [Display(Name = "ID môn học ")]
        public int SubjectID { get; set; }

        [Display(Name = "Môn học")]
        public string Subjectname { get; set; }

        [Display(Name = "ID cấp chung ")]
        public int SectionID { get; set; }

        [Display(Name = "Cáp chung")]
        public string SectionName { get; set; }
    }
}