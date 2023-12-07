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
    public class EmployeeLoginController : UserBaseController
    {
        // GET: EmployeeLogin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeDashboard()
        {
            return View();
        }

        [HttpPost]
        [ActionName("EmployeeDashboard")]
        [OnAction(ButtonName = "sendEmail")]
        public ActionResult SendEmail(EmployeeLogin model)
        {
            try
            {
                model.AddedBy = Session["PK_EmployeeID"].ToString();
                DataSet ds = model.SendEmail();
                if(ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SendEmailSuccess"] = "Your Data Saved Successfully.";
                    }
                    else
                    {
                        TempData["SendEmailError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SendEmailError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch(Exception ex)
            {
                TempData["SendEmailError"] = ex.Message;
            }
            return View(model);
        }



        #region LeaveApplication
        public ActionResult LeaveApplication(EmployeeLogin model)
        {
            #region ddlLeave
            EmployeeLogin obj = new EmployeeLogin();
            int count = 0;
            List<SelectListItem> ddlLeave = new List<SelectListItem>();
            obj.EmployeeID = Session["PK_EmployeeID"].ToString();
            DataSet ds = obj.LeaveTypeListForEmp();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlLeave.Add(new SelectListItem { Text = "Select Leave", Value = "0" });
                    }
                    ddlLeave.Add(new SelectListItem { Text = r["LeaveType"].ToString(), Value = r["PK_EmpLeaveID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlLeave = ddlLeave;

            #endregion

            List<EmployeeLogin> lst = new List<EmployeeLogin>();
            model.EmployeeID = Session["PK_EmployeeID"].ToString();
            DataSet ds1 = model.LeaveTypeListOfEmployee();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeLogin objLv = new EmployeeLogin();
                    objLv.LeaveID = r["PK_EmpLeaveID"].ToString();
                    objLv.LeaveName = r["LeaveType"].ToString();
                    objLv.UsedLeave = r["UsedLeave"].ToString();
                    objLv.LeaveLimit = r["LeaveLimit"].ToString();
                    objLv.RemainingLeave = r["RemainingLeave"].ToString();
                    lst.Add(objLv);
                }
                model.lstList = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("LeaveApplication")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveLeaveApplication(EmployeeLogin obj)
        {
            try
            {
                obj.EmployeeID = Session["PK_EmployeeID"].ToString();
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEmployeeLeave();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmployeeLeaveApp"] = "Employee Leave Request saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["EmployeeLeaveApp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["EmployeeLeaveApp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["EmployeeLeaveApp"] = ex.Message;
            }
            return RedirectToAction("LeaveApplication", "EmployeeLogin");
        }

        public ActionResult LeaveReportForEmployee(EmployeeLogin model)
        {
            #region ddlLeave
            EmployeeLogin obj = new EmployeeLogin();
            int count = 0;
            List<SelectListItem> ddlLeave = new List<SelectListItem>();
            obj.EmployeeID = Session["PK_EmployeeID"].ToString();
            DataSet ds = obj.LeaveTypeListForEmp();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlLeave.Add(new SelectListItem { Text = "Select Leave", Value = "0" });
                    }
                    ddlLeave.Add(new SelectListItem { Text = r["LeaveType"].ToString(), Value = r["PK_EmpLeaveID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlLeave = ddlLeave;

            #endregion
            #region RequestStatus
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion RequestStatus

            return View(model);
        }
        [HttpPost]
        [ActionName("LeaveReportForEmployee")]
        [OnAction(ButtonName = "Search")]
        public ActionResult LeaveReportForEmployeeBy(EmployeeLogin model)
        {
            List<EmployeeLogin> lst = new List<EmployeeLogin>();
            model.LeaveID = model.LeaveID == "0" ? null : model.LeaveID;
            model.EmployeeID = Session["PK_EmployeeID"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.LeaveList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeLogin obj = new EmployeeLogin();
                    obj.LeaveAppID = r["PK_LeaveAppID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeLeaveID = r["FK_EmpLeaveId"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.Remark = r["ApproverRemark"].ToString();
                    obj.Status = r["LeaveStatus"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.ApprovedBy = r["ApprovedByName"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    obj.Name = r["ApprovedByName"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            #region RequestStatus
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion RequestStatus

            #region ddlLeave
            EmployeeLogin objLv = new EmployeeLogin();
            int count = 0;
            List<SelectListItem> ddlLeave = new List<SelectListItem>();
            objLv.EmployeeID = Session["PK_EmployeeID"].ToString();
            DataSet ds = objLv.LeaveTypeListForEmp();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlLeave.Add(new SelectListItem { Text = "Select Leave", Value = "0" });
                    }
                    ddlLeave.Add(new SelectListItem { Text = r["LeaveType"].ToString(), Value = r["PK_EmpLeaveID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlLeave = ddlLeave;

            #endregion
            return View(model);
        }


        public ActionResult LeaveCount(EmployeeLogin model)
        {
            List<EmployeeLogin> lst = new List<EmployeeLogin>();
            model.EmployeeID = Session["PK_EmployeeID"].ToString();
            DataSet ds1 = model.LeaveCount();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    EmployeeLogin obj = new EmployeeLogin();
                    obj.LeaveAppID = r["PK_EmpLeaveID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    obj.LeaveLimit = r["LeaveLimit"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.RemainingLeave = r["RemainingLeave"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }


        #endregion

        #region SalaryReport

        public ActionResult SalaryPaymentReport(Master model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("SalaryPaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SalaryPaymentReportBy(Master model)
        {
            List<Master> lst = new List<Master>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EmployeeCode = Session["LoginID"].ToString();
            DataSet ds1 = model.SalaryPaymentReportBy();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeCode = r["LoginID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.NetSalary = r["TotalSalary"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Balance = r["BalanceAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        #endregion

        #region Attendance

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
            model.EmployeeLoginId = Session["LoginID"].ToString();
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

        #endregion

        #region Message
        public ActionResult Message()
        {
            EmployeeLogin model = new EmployeeLogin();
            try
            {
                model.EmployeeID = Session["PK_EmployeeID"].ToString();
                DataSet ds = model.GetAllMessages();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<EmployeeLogin> lstComplains = new List<EmployeeLogin>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        EmployeeLogin obj = new EmployeeLogin();
                        obj.MessageID = r["PK_MessageID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.Message = r["Message"].ToString();
                        obj.MessageDate = r["MessageDate"].ToString();
                        obj.MessageStatus = r["Status"].ToString();
                        lstComplains.Add(obj);
                    }
                    model.lstList = lstComplains;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Message")]
        [OnAction(ButtonName = "Save")]
        public ActionResult InsertMessage(EmployeeLogin model)
        {
            try
            {
                model.EmployeeID = Session["PK_EmployeeID"].ToString();
                model.LoginID = Session["LoginId"].ToString();
                DataSet ds = model.InsertMessage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Message"] = "Your Message has been submitted successfully. ";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Message"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Message");
        }

        public ActionResult MessageReply(string id)
        {
            EmployeeLogin model = new EmployeeLogin();
            try
            {
                List<EmployeeLogin> lst = new List<EmployeeLogin>();
                model.MessageID = Crypto.Decrypt(id);
                DataSet ds = model.GetMessageResponseAdmin();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.MessageID = ds.Tables[0].Rows[0]["PK_MessageID"].ToString();
                    model.RequestCode = ds.Tables[0].Rows[0]["RequestCode"].ToString();
                    model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    model.MessageDate = ds.Tables[0].Rows[0]["MessageDate"].ToString();
                    model.MessageStatus = ds.Tables[0].Rows[0]["Status"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["EmployeeName"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            EmployeeLogin obj = new EmployeeLogin();
                            obj.Reply = r["Reply"].ToString();
                            obj.ReplyPerson = r["ReplyPerson"].ToString();
                            obj.ReplyDate = r["ReplyDate"].ToString();
                            lst.Add(obj);
                        }
                        model.lstList = lst;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
       
        #endregion
    }
}