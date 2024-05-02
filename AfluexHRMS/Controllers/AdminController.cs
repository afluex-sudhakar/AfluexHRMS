using AfluexHRMS.Filter;
using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminDashboard()
        {
            AdminDashboard obj = new AdminDashboard();
            try
            {
                DataSet Ds = obj.BindDataForAdminDashboard();
                ViewBag.TotalEmployee = Ds.Tables[0].Rows[0]["TotalEmployee"].ToString();
                ViewBag.TotalHolidays = Ds.Tables[1].Rows[0]["TotalHolidays"].ToString();
                ViewBag.DistributedSalary = Ds.Tables[2].Rows[0]["DistributedSalary"].ToString();
                ViewBag.PendingSalary = Ds.Tables[3].Rows[0]["PendingSalary"].ToString();
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(obj);
        }

        public ActionResult GetJoiningDetails()
        {
            List<AdminDashboard> dataList3 = new List<AdminDashboard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AdminDashboard newdata = new AdminDashboard();
            Ds = newdata.BindDataForAdminDashboard();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[4].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[6].Rows)
                {
                    AdminDashboard details = new AdminDashboard();
                    details.TotalUser = (dr["PCount"].ToString());
                    details.Month = (dr["DayNo"].ToString());
                    details.City2 = (dr["ACount"].ToString());
                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AttendanceStatus()
        //{
        //    List<AdminDashboard> dataList2 = new List<AdminDashboard>();
        //    DataSet Ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    AdminDashboard newdata = new AdminDashboard();
        //    Ds = newdata.BindDataForAdminDashboard();
        //    if (Ds.Tables.Count > 0)
        //    {
        //        int count = 0;
        //        foreach (DataRow dr in Ds.Tables[5].Rows)
        //        {
        //            AdminDashboard details = new AdminDashboard();
        //            details.Total = (dr["Total"].ToString());
        //            details.Status = (dr["Status"].ToString());
        //            dataList2.Add(details);
        //            count++;
        //        }
        //    }
        //    return Json(dataList2, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetIDCard()
        {
            return View();
        }
        [HttpPost]
        [ActionName("GetIDCard")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSearchIDCard(Master model)
        {
            
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.GetIDCard();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master objM = new Master();
                    objM.EmployeeID = r["PK_EmployeeID"].ToString();
                    objM.EmployeeCode = r["LoginID"].ToString();
                    objM.EmployeeName = r["EmployeeName"].ToString();
                    objM.CompanyName = r["CompanyName"].ToString();
                    objM.DepartmentName = r["DepartmentName"].ToString();
                    objM.DesignationName = r["DesignationName"].ToString();
                    objM.Gender = r["Gender"].ToString();
                    objM.Email = r["Email"].ToString();
                    objM.DateOfJoining = r["DateOfJoining"].ToString();
                    objM.Contact = r["MobileNo"].ToString();
                    objM.BloodGroup = r["BloodGroup"].ToString();
                    objM.CompanyAddress = r["CompanyAddress"].ToString();
                    objM.CompanyContact = r["CompanyContact"].ToString();
                    objM.ProfilePic = r["ProfilePic"].ToString();
                    lst.Add(objM);
                }
                model.lstGetIDCard = lst;
            }
            return View(model);
        }

    }
}