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
using static System.Collections.Specialized.BitVector32;

namespace QuanLyTruongHoc.Controllers.Admin
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public new ActionResult Session(int PageNumber = 1)
        {
            List<Session> session = new List<Session>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetSession", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtSessions = new DataTable();

                    dtSessions.Load(sdr);

                    foreach (DataRow row in dtSessions.Rows)
                    {
                        session.Add(
                            new Session
                            {
                                SessionID = Convert.ToInt32(row["ID"].ToString()),
                                SessionName = row["Session"].ToString(),
                                Term = row["Term"].ToString(),
                                IsActive = row["IsActive"].ToString()
                            }

                            );
                    }
                }
            }
            ViewBag.TotalPages = Math.Ceiling(session.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            session = session.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(session);
        }

        [HttpPost]
        public new ActionResult Session(string searchtxt)
        {
            List<Session> session = new List<Session>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SearchSession", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Session", searchtxt);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtSessions = new DataTable();

                    dtSessions.Load(sdr);

                    foreach (DataRow row in dtSessions.Rows)
                    {
                        session.Add(
                            new Session
                            {
                                SessionID = Convert.ToInt32(row["ID"].ToString()),
                                SessionName = row["Session"].ToString(),
                                Term = row["Term"].ToString(),
                                IsActive = row["IsActive"].ToString()

                            }

                            );
                    }
                }
            }
            ViewBag.TotalPages = Math.Ceiling(session.Count() / 10.0);
            return View(session);
        }

        public ActionResult AddSession()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSession(Session session)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("addsession", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SessionName", session.SessionName);
                    cmd.Parameters.AddWithValue("@Term", session.Term);
                    cmd.Parameters.AddWithValue("@Isactive", session.IsActive);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Session");
        }

        public ActionResult EditSession(int id)
        {
            Session session = new Session();

            DataTable dtsession = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from SessionTable where ID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDA.Fill(dtsession);

            }
            if (dtsession.Rows.Count == 1)
            {
                session.SessionID = Convert.ToInt32(dtsession.Rows[0][0].ToString());
                session.SessionName = dtsession.Rows[0][1].ToString();
                session.Term = dtsession.Rows[0][2].ToString();
                session.IsActive = dtsession.Rows[0][3].ToString();
                return View(session);
            }

            else


                return View("Session");


        }

        [HttpPost]
        public ActionResult EditSession(int id, Session session)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("EditSession", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@SessionName", session.SessionName);
                    cmd.Parameters.AddWithValue("@Term", session.Term);
                    cmd.Parameters.AddWithValue("@Isactive", session.IsActive);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Updated Successfully";
            return RedirectToAction("Session");
        }

        public ActionResult DeleteSession(int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Deletesession", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Session");
        }

        public ActionResult FeeType()
        {
            List<FeeType> fee = new List<FeeType>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetFeeType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtFeeType = new DataTable();

                    dtFeeType.Load(sdr);

                    foreach (DataRow row in dtFeeType.Rows)
                    {
                        fee.Add(
                            new FeeType
                            {
                                FeeTypeID = Convert.ToInt32(row["FeeID"].ToString()),
                                FeeName = row["Fee"].ToString(),
                            }

                            );
                    }
                }
            }
            return View(fee);
        }

        public ActionResult CreateFeeType()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateFeeType(FeeType fee)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateFeeType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeeName ", fee.FeeName);


                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("FeeType");


        }

        public ActionResult EditFeeType(int id)
        {

            FeeType fee = new FeeType();

            DataTable dtFeeType = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from FeeType where FeeID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDA.Fill(dtFeeType);

            }
            if (dtFeeType.Rows.Count == 1)
            {
                fee.FeeTypeID = Convert.ToInt32(dtFeeType.Rows[0][0].ToString());
                fee.FeeName = dtFeeType.Rows[0][1].ToString();
                return View(fee);
            }

            else


                return View("FeeType");
        }

        [HttpPost]
        public ActionResult EditFeeType(int id, FeeType fee)
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateFeeType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeeID", id);
                    cmd.Parameters.AddWithValue("@FeeName", fee.FeeName);


                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("FeeType");
        }

        public ActionResult DeleteFeeType(int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteFeeType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeeID", id);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Deleted Successfully";
            return RedirectToAction("FeeType");
        }

        public ActionResult Sections()
        {
            List<Sections> section = new List<Sections>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetSections", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtSections = new DataTable();

                    dtSections.Load(sdr);

                    foreach (DataRow row in dtSections.Rows)
                    {
                        section.Add(
                            new Sections
                            {
                                SectionID = Convert.ToInt32(row["ID"].ToString()),
                                SectionName = row["ClassType"].ToString(),


                            }

                            );
                    }
                }
            }
            return View(section);
        }

        public ActionResult CreateSections()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSections(Sections section)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertSections", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@section", section.SectionName);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Sections");
        }

        public ActionResult ClassLevel()
        {
            List<ClassLevel> level = new List<ClassLevel>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetClassLevel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        level.Add(
                            new ClassLevel
                            {
                                LevelID = Convert.ToInt32(row["ID"].ToString()),
                                Levelname = row["ClassLevel"].ToString(),
                                Section = row["ClassType"].ToString(),


                            }

                            );
                    }
                }
            }
            return View(level);
        }

        public ActionResult CreateClassLevel()
        {
            ViewBag.Section = PopulateSection();

            return View();

        }

        [HttpPost]
        public ActionResult CreateClassLevel(ClassLevel level)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateClassLevel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassLevel", level.Levelname);
                    cmd.Parameters.AddWithValue("@SectionId", level.SectionID);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("ClassLevel");
        }

        private static List<Sections> PopulateSection()
        {
            List<Sections> section = new List<Sections>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("Select  * from Sections", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            section.Add(
                                new Sections
                                {
                                    SectionID = Convert.ToInt32(sdr["ID"]),
                                    SectionName = sdr["ClassType"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return section;

            }
        }


        private static List<FeeType> PopulateFeeType()
        {
            List<FeeType> fee = new List<FeeType>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("Select  * from FeeType", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            fee.Add(
                                new FeeType
                                {
                                    FeeTypeID = Convert.ToInt32(sdr["FeeID"]),
                                    FeeName = sdr["Fee"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return fee;
            }
        }

        public ActionResult FeeStructure()
        {
            List<FeeStructure> fee = new List<FeeStructure>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getFeeStructure", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        fee.Add(
                            new FeeStructure
                            {
                                FeeStructureID = Convert.ToInt32(row["ID"].ToString()),
                                FeeTypename = row["Fee"].ToString(),
                                SectionName = row["ClassType"].ToString(),
                                Amount = Convert.ToDecimal(row["Amount"].ToString())


                            }

                            );
                    }
                }
            }
            return View(fee);
        }

        public ActionResult CreateFeeStructure()
        {
            ViewBag.Section = PopulateSection();

            ViewBag.FeeType = PopulateFeeType();


            return View();
        }

        [HttpPost]
        public ActionResult CreateFeeStructure(FeeStructure fee)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertFeeStructure", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeeTypeID", fee.FeeTypeID);
                    cmd.Parameters.AddWithValue("@SectionID", fee.SectionID);
                    cmd.Parameters.AddWithValue("@Amount", fee.Amount);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("FeeStructure");
        }

        public ActionResult Subject()
        {
            List<Subject> subject = new List<Subject>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetSubject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        subject.Add(
                            new Subject
                            {
                                SubjectID = Convert.ToInt32(row["ID"].ToString()),
                                Subjectname = row["Subject"].ToString(),
                                SectionName = row["ClassType"].ToString()


                            }

                            );
                    }
                }
            }
            return View(subject);
        }


        public ActionResult CreateSubject()
        {
            ViewBag.Section = PopulateSection();

            return View();
        }

        [HttpPost]
        public ActionResult CreateSubject(Subject subject)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertSubject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Subject", subject.Subjectname);
                    cmd.Parameters.AddWithValue("@SectionID", subject.SectionID);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Subject");
        }

        public ActionResult Designation()
        {
            List<Designation> designation = new List<Designation>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetDesignation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        designation.Add(
                            new Designation
                            {
                                DesignationID = Convert.ToInt32(row["DesignationID"].ToString()),
                                DesignationName = row["Designation"].ToString(),


                            }

                            );
                    }
                }
            }
            return View(designation);

        }

        public ActionResult CreateDesignation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDesignation(Designation designation)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateDesignation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Designation", designation.DesignationName);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Designation");
        }

        private static List<Subject> PopulateSubject()
        {
            List<Subject> subject = new List<Subject>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select  * from Subject ", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            subject.Add(
                                new Subject
                                {

                                    SectionID = Convert.ToInt32(sdr["ID"].ToString()),
                                    Subjectname = sdr["Subject"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return subject;

            }
        }

        public ActionResult TimeTableClassLevel()
        {
            List<ClassLevel> level = new List<ClassLevel>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetClassLevel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        level.Add(
                            new ClassLevel
                            {
                                LevelID = Convert.ToInt32(row["ID"].ToString()),
                                Levelname = row["ClassLevel"].ToString(),
                                Section = row["ClassType"].ToString()


                            }

                            );
                    }
                }
            }
            return View(level);
        }


        public ActionResult CreateTimeTable(int id)
        {
            ViewBag.Subject = PopulateSubject();


            ClassTimeTable time = new ClassTimeTable();

            DataTable dtsession = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from ClassLevel where ID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);

                sqlDA.Fill(dtsession);

            }
            if (dtsession.Rows.Count == 1)
            {
                time.ClassLevelID = Convert.ToInt32(dtsession.Rows[0][0].ToString());
                time.ClassLevelName = dtsession.Rows[0][1].ToString();
                return View(time);
            }

            else


                return View("TimeTableClassLevel");
        }

        [HttpPost]
        public ActionResult CreateTimeTable(ClassTimeTable time)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateClassTimeTable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LevelID", time.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Days", time.Days);
                    cmd.Parameters.AddWithValue("@Period1", time.Period1);
                    cmd.Parameters.AddWithValue("@Period2", time.Period2);
                    cmd.Parameters.AddWithValue("@Period3", time.Period3);
                    cmd.Parameters.AddWithValue("@Period4", time.Period4);
                    cmd.Parameters.AddWithValue("@Period5", time.Period5);
                    cmd.Parameters.AddWithValue("@Period6", time.Period6);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("TimeTableClassLevel");

        }

        public ActionResult ViewTimeTable(int id)
        {
            List<ClassTimeTable> time = new List<ClassTimeTable>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetClassTimeTable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        time.Add(
                            new ClassTimeTable
                            {
                                ClassLevelName = row["ClassLevel"].ToString(),
                                Days = row["Days"].ToString(),
                                Period1 = row["Period1"].ToString(),
                                Period2 = row["Period2"].ToString(),
                                Period3 = row["Period3"].ToString(),
                                Period4 = row["Period4"].ToString(),
                                Period5 = row["Period5"].ToString(),
                                Period6 = row["Period6"].ToString()
                            }

                            );
                    }
                }
            }
            return View(time);
        }

    }

}
