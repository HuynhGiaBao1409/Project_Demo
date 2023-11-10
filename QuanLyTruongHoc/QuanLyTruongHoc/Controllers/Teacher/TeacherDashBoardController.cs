using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTruongHoc.Config;
using QuanLyTruongHoc.Models.Admin;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyTruongHoc.Controllers.Teacher
{
    public class TeacherDashBoardController : Controller
    {
        // GET: TeacherDashBoard
        public ActionResult Index(int PageNumber = 1)
        {
            string Username = (string)Session["Username"];
            List<Student> student = new List<Student>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetAssignedStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", Username);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        student.Add(
                            new Student
                            {
                                AdmissionNo = Convert.ToInt32(row["AdmissionNo"].ToString()),
                                StudentName = row["FullName"].ToString(),
                                ClassLevel = row["ClassLevel"].ToString(),
                                ClassAphabet = row["ClassAlphabet"].ToString(),
                                Gender = row["Gender"].ToString()

                            }

                            );
                    }
                }
            }
            ViewBag.TotalPages = Math.Ceiling(student.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            student = student.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(student);
        }


        [HttpPost]
        public ActionResult Index(string searchtxt)
        {
            string Username = (string)Session["Username"];
            List<Student> student = new List<Student>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SearchAssignedStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Studentname", searchtxt);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        student.Add(
                            new Student
                            {
                                AdmissionNo = Convert.ToInt32(row["AdmissionNo"].ToString()),
                                StudentName = row["FullName"].ToString(),
                                ClassLevel = row["ClassLevel"].ToString(),
                                ClassAphabet = row["ClassAlphabet"].ToString(),
                                Gender = row["Gender"].ToString()

                            }

                            );
                    }
                }
            }
            ViewBag.TotalPages = Math.Ceiling(student.Count() / 10.0);
            return View(student);

        }

    }
}