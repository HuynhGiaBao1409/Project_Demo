using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using QuanLyTruongHoc.Config;
using QuanLyTruongHoc.Models.Admin;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace QuanLyTruongHoc.Controllers.Admin
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Student()
        {
            List<Student> student = new List<Student>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtSessions = new DataTable();

                    dtSessions.Load(sdr);

                    foreach (DataRow row in dtSessions.Rows)
                    {
                        student.Add(
                            new Student
                            {
                                AdmissionNo = Convert.ToInt32(row["AdmissionNo"].ToString()),
                                StudentName = row["FullName"].ToString(),
                                SessionName = row["Session"].ToString(),
                                ClassLevel = row["ClassLevel"].ToString(),
                                ClassAphabet = row["ClassAlphabet"].ToString(),
                                Gender = row["Gender"].ToString()


                            }

                            );
                    }
                }
            }
            return View(student);
        }

        public ActionResult CreateStudent()
        {
            ViewBag.Session = PopulateSession();
            ViewBag.ClassLevel = PopulateClassLevel();

            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fullname", student.StudentName);
                    cmd.Parameters.AddWithValue("@DOB", student.DOB);
                    cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender);
                    cmd.Parameters.AddWithValue("@Religion", student.Religion);
                    cmd.Parameters.AddWithValue("@Session", student.SessionID);
                    cmd.Parameters.AddWithValue("@Term", student.Term);
                    cmd.Parameters.AddWithValue("@ClassLevelID", student.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Alphabet", student.ClassAphabet);
                    cmd.Parameters.AddWithValue("@AdmissionDate", student.AdmissionDate);
                    cmd.Parameters.AddWithValue("@Fathername", student.Fathername);
                    cmd.Parameters.AddWithValue("@Mothername", student.Mothername);
                    cmd.Parameters.AddWithValue("@FatherContact", student.FatherPhone);
                    cmd.Parameters.AddWithValue("@MotherContact", student.MotherPhone);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Student");
        }

        private static List<Session> PopulateSession()
        {
            List<Session> session = new List<Session>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select * from  SessionTable", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            session.Add(
                                new Session
                                {
                                    SessionID = Convert.ToInt32(sdr["ID"]),
                                    SessionName = sdr["Session"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return session;

            }
        }

        private static List<ClassLevel> PopulateClassLevel()
        {
            List<ClassLevel> level = new List<ClassLevel>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("Select  * from ClassLevel ", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            level.Add(
                                new ClassLevel
                                {
                                    LevelID = Convert.ToInt32(sdr["ID"]),
                                    Levelname = sdr["ClassLevel"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return level;

            }
        }

        public ActionResult StudentDetails(int id)
        {

            Student student = new Student();

            DataTable dtTable = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "select * from Student s inner join ClassLevel cl on s.ClassLevelID = cl.ID inner join SessionTable se on  s.SessionID = se.ID  where s.AdmissionNo = @Id";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDA.Fill(dtTable);

            }
            if (dtTable.Rows.Count == 1)
            {
                student.AdmissionNo = Convert.ToInt32(dtTable.Rows[0][0].ToString());
                student.StudentName = dtTable.Rows[0][1].ToString();
                student.DOB = Convert.ToDateTime(dtTable.Rows[0][2].ToString());
                student.Nationality = dtTable.Rows[0][3].ToString();
                student.Gender = dtTable.Rows[0][4].ToString();
                student.Religion = dtTable.Rows[0][5].ToString();
                student.SessionName = dtTable.Rows[0][19].ToString();
                student.Term = dtTable.Rows[0][7].ToString();
                student.ClassLevel = dtTable.Rows[0][16].ToString();
                student.ClassAphabet = dtTable.Rows[0][9].ToString();
                student.AdmissionDate = Convert.ToDateTime(dtTable.Rows[0][10].ToString());
                student.Fathername = dtTable.Rows[0][11].ToString();
                student.Mothername = dtTable.Rows[0][12].ToString();
                student.FatherPhone = dtTable.Rows[0][13].ToString();
                student.MotherPhone = dtTable.Rows[0][14].ToString();
                return View(student);
            }

            else


                return View("Student");
        }

        public ActionResult EditStudent(int id)
        {
            ViewBag.Session = PopulateSession();
            ViewBag.ClassLevel = PopulateClassLevel();
            Student student = new Student();

            DataTable dtTable = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "select * from Student s inner join ClassLevel cl on s.ClassLevelID = cl.ID inner join SessionTable se on  s.SessionID = se.ID  where s.AdmissionNo = @Id";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDA.Fill(dtTable);

            }
            if (dtTable.Rows.Count == 1)
            {
                student.AdmissionNo = Convert.ToInt32(dtTable.Rows[0][0].ToString());
                student.StudentName = dtTable.Rows[0][1].ToString();
                student.DOB = Convert.ToDateTime(dtTable.Rows[0][2].ToString());
                student.Nationality = dtTable.Rows[0][3].ToString();
                student.Gender = dtTable.Rows[0][4].ToString();
                student.Religion = dtTable.Rows[0][5].ToString();
                student.SessionName = dtTable.Rows[0][6].ToString();
                student.Term = dtTable.Rows[0][7].ToString();
                student.ClassLevelID = Convert.ToInt32(dtTable.Rows[0][8].ToString());
                student.ClassAphabet = dtTable.Rows[0][9].ToString();
                student.AdmissionDate = Convert.ToDateTime(dtTable.Rows[0][10].ToString());
                student.Fathername = dtTable.Rows[0][11].ToString();
                student.Mothername = dtTable.Rows[0][12].ToString();
                student.FatherPhone = dtTable.Rows[0][13].ToString();
                student.MotherPhone = dtTable.Rows[0][14].ToString();
                return View(student);
            }

            else


                return View("Student");
        }

        [HttpPost]
        public ActionResult EditStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdmissionNo", student.AdmissionNo);
                    cmd.Parameters.AddWithValue("@Fullname", student.StudentName);
                    cmd.Parameters.AddWithValue("@DOB", student.DOB);
                    cmd.Parameters.AddWithValue("@Nationality", student.Nationality);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender);
                    cmd.Parameters.AddWithValue("@Religion", student.Religion);
                    cmd.Parameters.AddWithValue("@Session", student.SessionID);
                    cmd.Parameters.AddWithValue("@Term", student.Term);
                    cmd.Parameters.AddWithValue("@ClassLevelID", student.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Alphabet", student.ClassAphabet);
                    cmd.Parameters.AddWithValue("@AdmissionDate", student.AdmissionDate);
                    cmd.Parameters.AddWithValue("@Fathername", student.Fathername);
                    cmd.Parameters.AddWithValue("@Mothername", student.Mothername);
                    cmd.Parameters.AddWithValue("@FatherContact", student.FatherPhone);
                    cmd.Parameters.AddWithValue("@MotherContact", student.MotherPhone);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            return RedirectToAction("Student");
        }
    }
}