using QuanLyTruongHoc.Config;
using QuanLyTruongHoc.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace QuanLyTruongHoc.Controllers.Admin
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Staff()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new Staff
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                Designation = row["Designation"].ToString(),
                                Gender = row["Gender"].ToString()


                            }

                            );
                    }
                }
            }
            return View(staff);
        }

        public ActionResult CreateStaff()
        {
            ViewBag.Designation = PopulateDesignation();
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff(Staff staff)
        {
            string filename = Path.GetFileNameWithoutExtension(staff.ResumeImageFile.FileName);
            string extension = Path.GetExtension(staff.ResumeImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmsssff") + extension;
            staff.Resume = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/imgs/"), filename);
            staff.ResumeImageFile.SaveAs(filename);
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CreateStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fullname", staff.Staffname);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@Designation", staff.DesignationID);
                    cmd.Parameters.AddWithValue("@Phone1", staff.Phone1);
                    cmd.Parameters.AddWithValue("@Phone2", staff.phone2);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@StaffAddress", staff.StaffAddress);
                    cmd.Parameters.AddWithValue("@dateOfAppointment", staff.DateOfAppointment);
                    cmd.Parameters.AddWithValue("@Nationality", staff.Nationality);
                    cmd.Parameters.AddWithValue("@YOE", staff.YOE);
                    cmd.Parameters.AddWithValue("@PreviousOrg", staff.previouseOrg);
                    cmd.Parameters.AddWithValue("@Resume", staff.Resume);
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Staff");
        }

        private static List<Designation> PopulateDesignation()
        {
            List<Designation> designation = new List<Designation>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("Select  * from Designation", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            designation.Add(
                                new Designation
                                {
                                    DesignationID = Convert.ToInt32(sdr["DesignationID"]),
                                    DesignationName = sdr["Designation"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return designation;
            }
        }

        public ActionResult StaffAccountStatus()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new Staff
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                Designation = row["Designation"].ToString(),
                                Gender = row["Gender"].ToString(),
                                AccountStatus = row["AccountStatus"].ToString()


                            }

                            );
                    }
                }
            }
            return View(staff);

        }


        public ActionResult ActivateStaffAccount(int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Update StaffTable set AccountStatus='Active' where StaffID ='" + id + "'", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                    TempData["SuccessMessage"] = "Record Updated Successfully";
                    return RedirectToAction("StaffAccountStatus");
                }
            }
        }

        public ActionResult DeactivateStaffAccount(int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Update StaffTable set AccountStatus='In-Active' where StaffID ='" + id + "'", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                    TempData["SuccessMessage"] = "Record Updated Successfully";
                    return RedirectToAction("StaffAccountStatus");
                }
            }
        }

        public ActionResult Role()
        {
            List<Role> role = new List<Role>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        role.Add(
                            new Role
                            {
                                RoleID = Convert.ToInt32(row["RoleID"].ToString()),
                                Rolename = row["Role"].ToString(),
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["Fullname"].ToString(),
                                Username = row["Username"].ToString(),


                            }

                            );
                    }
                }
            }
            return View(role);
        }

        public ActionResult CreateRole()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getActiveStaffs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new Staff
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                Designation = row["Designation"].ToString(),
                                Gender = row["Gender"].ToString(),
                                AccountStatus = row["AccountStatus"].ToString()


                            }

                            );
                    }
                }
            }
            return View(staff);

        }

        public ActionResult AddRole(int id)
        {
            Role role = new Role();

            DataTable dtsession = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from StaffTable where StaffID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);

                sqlDA.Fill(dtsession);

            }
            if (dtsession.Rows.Count == 1)
            {
                role.StaffID = Convert.ToInt32(dtsession.Rows[0][0].ToString());
                role.Staffname = dtsession.Rows[0][1].ToString();
                return View(role);
            }

            else


                return View("CreateRole");


        }

        [HttpPost]
        public ActionResult Addrole(Role role)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", role.Rolename);
                    cmd.Parameters.AddWithValue("@StaffID", role.StaffID);
                    cmd.Parameters.AddWithValue("@Username", role.Username);
                    cmd.Parameters.AddWithValue("@Password", role.Password);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("Role");
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

        private static List<Staff> PopulateStaff()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select  * from StaffTable s inner join Designation d  on s.DesignationID = d.DesignationID where d.Designation = 'Giao Vien' and AccountStatus = 'Active' Order by FullName ASC ", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            staff.Add(
                                new Staff
                                {
                                    StaffID = Convert.ToInt32(sdr["StaffID"]),
                                    Staffname = sdr["FullName"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return staff;

            }
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
                                    SubjectID = Convert.ToInt32(sdr["ID"]),
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


        public ActionResult AssignedClass()
        {
            List<AssignClass> assign = new List<AssignClass>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getAssignClass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        assign.Add(
                            new AssignClass
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                ClassLevelName = row["ClassLevel"].ToString(),
                                Prefix = row["Prefix"].ToString()

                            }

                            );
                    }
                }
            }
            return View(assign);
        }

        public ActionResult AssignClass()
        {
            ViewBag.Staff = PopulateStaff();
            ViewBag.ClassLevel = PopulateClassLevel();
            return View();
        }

        [HttpPost]
        public ActionResult AssignClass(AssignClass assign)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AssignClasses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", assign.StaffID);
                    cmd.Parameters.AddWithValue("@ClassLevelID", assign.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Prefix", assign.Prefix);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("AssignedClass");
        }


        public ActionResult AssignedSubject()
        {

            List<AssignSubject> assign = new List<AssignSubject>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getAssignedSubject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        assign.Add(
                            new AssignSubject
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                staffname = row["FullName"].ToString(),
                                Subject = row["Subject"].ToString(),
                                ClassLevelname = row["ClassLevel"].ToString(),
                                Prefix = row["Prefix"].ToString(),


                            }

                            );
                    }
                }
            }
            return View(assign);
        }

        public ActionResult AssignSubject()
        {
            ViewBag.Subject = PopulateSubject();
            ViewBag.Staff = PopulateStaff();
            ViewBag.ClassLevel = PopulateClassLevel();
            return View();
        }

        [HttpPost]
        public ActionResult AssignSubject(AssignSubject assign)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AssignSubject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", assign.StaffID);
                    cmd.Parameters.AddWithValue("@SubjectID", assign.SubjectID);
                    cmd.Parameters.AddWithValue("@LevelID", assign.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Prefix", assign.Prefix);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("AssignedSubject");
        }

        public ActionResult StaffFinance()
        {
            List<StaffFinance> staff = new List<StaffFinance>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStaffFinance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new StaffFinance
                            {
                                staffID = Convert.ToInt32(row["StaffID"].ToString()),
                                StaffName = row["FullName"].ToString(),
                                TotalPay = Convert.ToInt32(row["TotalPay"].ToString())


                            }

                            );
                    }
                }
            }
            return View(staff);
        }

        public ActionResult StaffListForSalary()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new Staff
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                Designation = row["Designation"].ToString(),
                                Gender = row["Gender"].ToString()


                            }

                            );
                    }
                }
            }
            return View(staff);
        }

        public ActionResult CreateStaffFinance(int id)
        {
            StaffFinance finance = new StaffFinance();

            DataTable dtTable = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from StaffTable where StaffID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);

                sqlDA.Fill(dtTable);

            }
            if (dtTable.Rows.Count == 1)
            {
                finance.staffID = Convert.ToInt32(dtTable.Rows[0][0].ToString());
                finance.StaffName = dtTable.Rows[0][1].ToString();
                return View(finance);
            }

            else


                return View("StaffListForSalary");



        }

        [HttpPost]
        public ActionResult CreateStaffFinance(StaffFinance finance)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertStaffFinance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", finance.staffID);
                    cmd.Parameters.AddWithValue("@BasicSalary", finance.BasicSalary);
                    cmd.Parameters.AddWithValue("@HouseAllowance", finance.HouseAllowance);
                    cmd.Parameters.AddWithValue("@TransportAllowance", finance.TransportAllowance);
                    cmd.Parameters.AddWithValue("@LateComingFee", finance.LateComingFee);
                    cmd.Parameters.AddWithValue("@Tax", finance.TransportAllowance);
                    cmd.Parameters.AddWithValue("@Vat", finance.TransportAllowance);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("StaffFinance");
        }


        public ActionResult GeneratePayrollSLip(int id)
        {

            StaffFinance finance = new StaffFinance();

            DataTable dtsession = new DataTable();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "select * from StaffFinance sf inner join StaffTable s on sf.StaffID = s.StaffID  where sf.StaffID = @ID";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, con);
                sqlDA.SelectCommand.Parameters.AddWithValue("@ID", id);

                sqlDA.Fill(dtsession);

            }
            if (dtsession.Rows.Count == 1)
            {
                finance.staffID = Convert.ToInt32(dtsession.Rows[0][1].ToString());
                finance.StaffName = dtsession.Rows[0][10].ToString();
                finance.BasicSalary = Convert.ToDecimal(dtsession.Rows[0][2].ToString());
                finance.HouseAllowance = Convert.ToDecimal(dtsession.Rows[0][3].ToString());
                finance.TransportAllowance = Convert.ToDecimal(dtsession.Rows[0][4].ToString());
                finance.TransportAllowance = Convert.ToDecimal(dtsession.Rows[0][4].ToString());
                finance.Tax = Convert.ToDecimal(dtsession.Rows[0][6].ToString());
                finance.Vat = Convert.ToDecimal(dtsession.Rows[0][7].ToString());
                finance.TotalPay = Convert.ToDecimal(dtsession.Rows[0][8].ToString());
                return View(finance);
            }

            else


                return View("StaffFinance");

        }


        public ActionResult StaffAttendance()
        {
            List<StaffAttendance> staff = new List<StaffAttendance>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("getStaffAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtTable = new DataTable();

                    dtTable.Load(sdr);

                    foreach (DataRow row in dtTable.Rows)
                    {
                        staff.Add(
                            new StaffAttendance
                            {
                                StaffID = Convert.ToInt32(row["StaffID"].ToString()),
                                Staffname = row["FullName"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Remark = row["Remarks"].ToString()



                            }

                            );
                    }
                }
            }


            return View(staff);
        }

        private static List<Staff> PopulateAllStaff()
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("select  * from StaffTable  Order by FullName ASC ", con))
                {
                    cmd.Connection = con;

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            staff.Add(
                                new Staff
                                {
                                    StaffID = Convert.ToInt32(sdr["StaffID"]),
                                    Staffname = sdr["FullName"].ToString()
                                }

                                );
                        }
                        con.Close();
                    }


                }

                return staff;

            }
        }

        public ActionResult CreateStaffAttendance()
        {
            ViewBag.Staff = PopulateAllStaff();

            return View();
        }

        [HttpPost]
        public ActionResult CreateStaffAttendance(StaffAttendance staff)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertStaffAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", staff.StaffID);
                    cmd.Parameters.AddWithValue("@Date", staff.Date);
                    cmd.Parameters.AddWithValue("@Remark", staff.Remark);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            TempData["SuccessMessage"] = "Record Saved Successfully";
            return RedirectToAction("StaffAttendance");
        }
    }

}