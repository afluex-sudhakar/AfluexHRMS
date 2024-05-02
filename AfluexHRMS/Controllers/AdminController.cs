using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
       

    }
}