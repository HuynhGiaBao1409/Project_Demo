using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTruongHoc.Config;
using QuanLyTruongHoc.Models.Admin;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyTruongHoc.Controllers.Admin
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index(int PageNumber = 1)
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
            ViewBag.TotalPages = Math.Ceiling(staff.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            staff = staff.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(staff);
        }

        [HttpPost]
        public ActionResult Index(string searchtxt)
        {
            List<Staff> staff = new List<Staff>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SearchStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Staffname", searchtxt);
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
            ViewBag.TotalPages = Math.Ceiling(staff.Count() / 10.0);
            return View(staff);
        }
    }
}