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
    public class EmployeeAttendanceController : AdminBaseController
    {
        // GET: EmployeeAttendance
        public ActionResult Index()
        {
            return View();
        }

        #region QuickAttendancePosting
        public ActionResult QuickAttendancePosting()
        {
            #region ddlDepartment
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                DataSet ds1 = obj.BindDepartmentDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                        }
                        ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDepartment = ddlDepartment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            #region ddlDesignation
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet ds1 = obj.BindDesignationDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlEmployee
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlEmployee = new List<SelectListItem>();
                DataSet ds1 = obj.BindEmployeeDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlEmployee.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
                        }
                        ddlEmployee.Add(new SelectListItem { Text = r["EmployeeName"].ToString(), Value = r["PK_EmployeeID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlEmployee = ddlEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            return View();
        }


        [HttpPost]
        [ActionName("QuickAttendancePosting")]
        [OnAction(ButtonName = "Getdetails")]
        public ActionResult GetAttendancelist(EmployeeAttendance model)
        {
            model.DepartmentName = model.DepartmentName == "0" ? null : model.DepartmentName;
            model.DesignationtName = model.DesignationtName == "0" ? null : model.DesignationtName;
            model.EmployeeName = model.EmployeeName == "0" ? null : model.EmployeeName;
            List<EmployeeAttendance> lst = new List<EmployeeAttendance>();
            DataSet ds = model.GetEmployeeForAttendance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.Pk_EmployeeID = r["PK_EmployeeID"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    lst.Add(obj);
                }
                model.listAttendance = lst;
            }

            #region ddlDepartment
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                DataSet ds1 = obj.BindDepartmentDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                        }
                        ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDepartment = ddlDepartment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlDesignation
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet ds1 = obj.BindDesignationDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlEmployee
            try
            {
                EmployeeAttendance obj = new EmployeeAttendance();
                int count = 0;
                List<SelectListItem> ddlEmployee = new List<SelectListItem>();
                DataSet ds1 = obj.BindEmployeeDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlEmployee.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
                        }
                        ddlEmployee.Add(new SelectListItem { Text = r["EmployeeName"].ToString(), Value = r["PK_EmployeeID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlEmployee = ddlEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("QuickAttendancePosting")]
        [OnAction(ButtonName = "btnAttendance")]
        public ActionResult SaveEmployeeAttendance(EmployeeAttendance obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string employeeid = "";
                string chk = "";
                DataTable dtemployee = new DataTable();
                dtemployee.Columns.Add("Fk_EmployeeId", typeof(string));
                dtemployee.Columns.Add("Status", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["checkBoxId_ " + i];
                    if (chk == "on")
                    {
                        obj.Status = "P";
                    }
                    else
                    {
                        obj.Status = "A";
                    }
                    employeeid = Request["Pk_EmployeeID_ " + i].ToString();
                    dtemployee.Rows.Add(employeeid, obj.Status);
                }
                obj.dsEmployeeAttendance = dtemployee;
                obj.AttendanceDate = string.IsNullOrEmpty(obj.AttendanceDate) ? null : Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveStudentAttendance();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Attendancemsg"] = "Attendance saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrAttendancemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrAttendancemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrAttendancemsg"] = ex.Message;
            }
            return RedirectToAction("QuickAttendancePosting", "EmployeeAttendance");
        }

        #endregion

        #region DailyAttendancePosting
        public ActionResult DailyAttendance()
        {
            return View();
        }

        public ActionResult AllPresent(EmployeeAttendance model)
        {
            List<EmployeeAttendance> lst = new List<EmployeeAttendance>();
            model.IsPresent = "1";
            DataSet ds1 = model.EmployeeListForAttendance();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.EmployeeID = r["PK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.WHLimit = r["Hours"].ToString();
                    obj.Attendance = r["Status"].ToString();
                    obj.InTime = r["InTime"].ToString();
                    obj.OutTime = r["OutTime"].ToString();
                    obj.ISHalfDay = r["IshalfDay"].ToString();
                    obj.OverTime = r["Overtime"].ToString();
                    obj.TotalHRWork = r["Hours"].ToString();
                    lst.Add(obj);
                }
            }
            model.lstList = lst;
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ActionName("DailyAttendance")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DailyAttendanceBy(EmployeeAttendance model)
        {
            List<EmployeeAttendance> lst = new List<EmployeeAttendance>();
            DataSet ds1 = model.EmployeeListForAttendance();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.EmployeeID = r["PK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.WHLimit = r["Hours"].ToString();
                    obj.TotalHRWork = r["Hours"].ToString();
                    obj.InTime = r["InTime"].ToString();
                    obj.OutTime = r["OutTime"].ToString();
                    obj.ISHalfDay = r["IshalfDay"].ToString();
                    obj.OverTime = r["Overtime"].ToString();
                    obj.TotalHRWork = r["Hours"].ToString();
                    obj.Attendance = r["Status"].ToString();
                    lst.Add(obj);
                }
            }
            model.lstList = lst;
            return View(model);
        }
        [HttpPost]
        [ActionName("DailyAttendance")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDailyAttendance(EmployeeAttendance obj)
        {
            string FormName = "";
            string Controller = "";
            obj.AttendanceDate = string.IsNullOrEmpty(obj.AttendanceDate) ? null : Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");

            try
            {

                string noofrows = Request["hdrows"].ToString();
                string Empid = "";
                string attend = "";
                string intime = "";
                string outtime = "";
                string totalhr = "";
                string overtime = "";
                string ishalfdy = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_EmployeeID ", typeof(string));
                dtst.Columns.Add("AttendanceStatus", typeof(string));
                dtst.Columns.Add("InTime", typeof(string));
                dtst.Columns.Add("OutTime ", typeof(string));
                dtst.Columns.Add("TotalHoursWork", typeof(string));
                dtst.Columns.Add("OverTime", typeof(string));
                dtst.Columns.Add("IsHalfDay", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    if (Request["txtattend " + i].ToString() == "A")
                    {
                        intime = "";
                        outtime = "";
                        totalhr = "";
                        overtime = "";
                        ishalfdy = "";
                        attend = Request["txtattend " + i].ToString();
                    }
                    else if (Request["txtattend " + i].ToString() == "")
                    {
                        intime = "";
                        outtime = "";
                        totalhr = "";
                        overtime = "";
                        ishalfdy = "";
                        attend = "A";
                    }
                    else
                    {
                        intime = Request["txtintime " + i].ToString();
                        outtime = Request["txtouttime " + i].ToString();
                        totalhr = Request["txttotalhrs " + i].ToString();
                        overtime = Request["txtovertime " + i].ToString();
                        ishalfdy = Request["txthd " + i].ToString();
                        attend = Request["txtattend " + i].ToString();
                    }

                    Empid = Request["empid " + i].ToString();



                    dtst.Rows.Add(Empid, attend, intime, outtime, totalhr, overtime, ishalfdy);

                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtTable = dtst;

                DataSet ds = obj.SaveEmployeeDailyAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DailyAttendance"] = " Daily Attendance Saved successfully !";
                    }
                    else
                    {
                        TempData["ErrDailyAttendance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrDailyAttendance"] = ex.Message;
            }
            FormName = "DailyAttendance";
            Controller = "EmployeeAttendance";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region Reports
        public ActionResult DateWiseAttendanceReport(EmployeeAttendance model)
        {
            #region HAlfFullDay
            List<SelectListItem> AttendType = Common.AttendanceStatus();
            ViewBag.AttendType = AttendType;
            #endregion HAlfFullDay

            #region IsFullHalfDay
            List<SelectListItem> ISHalfFullDay = Common.IsFullHalfDay();
            ViewBag.ISHalfFullDay = ISHalfFullDay;
            #endregion IsFullHalfDay

            return View(model);
        }
        [HttpPost]
        [ActionName("DateWiseAttendanceReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DateWiseAttendanceReportBy(EmployeeAttendance model)
        {

            List<EmployeeAttendance> lst = new List<EmployeeAttendance>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.DateWiseAttendanceReportBy();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.EmployeeID = r["FK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.ISHalfDay = r["IsHalfDay"].ToString();
                    obj.Attendance = r["Status"].ToString();
                    obj.AttendanceDate = r["AttendanceDate"].ToString();
                    lst.Add(obj);
                }
            }
            model.lstList = lst;

            #region HAlfFullDay
            List<SelectListItem> AttendType = Common.AttendanceStatus();
            ViewBag.AttendType = AttendType;
            #endregion HAlfFullDay

            #region IsFullHalfDay
            List<SelectListItem> ISHalfFullDay = Common.IsFullHalfDay();
            ViewBag.ISHalfFullDay = ISHalfFullDay;
            #endregion IsFullHalfDay

            return View(model);
        }


        public ActionResult AttendanceSummaryReport(EmployeeAttendance model)
        {
            return View();
        }
        [HttpPost]
        [ActionName("AttendanceSummaryReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AttendanceSummaryReportBy(EmployeeAttendance model)
        {

            List<EmployeeAttendance> lst = new List<EmployeeAttendance>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.AttendanceSummaryReportBy();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.EmployeeID = r["FK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.PresentDay = r["Presentdays"].ToString();
                    obj.AbsentDay = r["Absentdays"].ToString();
                    obj.OverTime = r["TotalOverTime"].ToString();
                    obj.LeaveDays = r["TotalLeave"].ToString();
                    obj.HalfDays = r["TotalHalfDay"].ToString();
                    obj.FullDays = r["FullDay"].ToString();

                    lst.Add(obj);
                }
            }
            model.lstList = lst;

            return View(model);
        }
        #endregion
    }
}