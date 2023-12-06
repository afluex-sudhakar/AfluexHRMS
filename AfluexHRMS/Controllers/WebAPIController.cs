using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class WebAPIController : Controller
    {
        // GET: WebAPI
        [HttpPost]
        public ActionResult Login(LoginRequest model)
        {
            LoginResponse Response = new LoginResponse();         
            string FormName = "";
            string Controller = "";
            try
            {
                DataSet ds = model.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        Response.Status = "0";
                        Response.Message = "Incorrect Login ID Or Password";
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Employee"))
                    {
                        Response.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        Response.PK_EmployeeID = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                        Response.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Response.EmployeeName = ds.Tables[0].Rows[0]["Name"].ToString();
                        Response.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                        Response.Status = "1";
                        Response.Message = "Login Successfully";
                    }
                }
                else
                {
                    Response.Status = "0";
                    Response.Message = "Incorrect Login ID Or Password";
                }
            }
            catch (Exception ex)
            {
                Response.Status = "0";
                Response.Message = "Incorrect Login ID Or Password";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LeaveApplication(LeaveApplicationRequest model)
        {
            List<LeaveApplicationResponse> lstResponse = new List<LeaveApplicationResponse>();
            #region ddlLeave
            int count = 0;
            List<SelectListItem> ddlLeave = new List<SelectListItem>();

            DataSet ds = model.LeaveTypeListForEmp();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";

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

            model.ddlLeave = ddlLeave;

            #endregion
            DataSet ds1 = model.LeaveTypeListOfEmployee();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    LeaveApplicationResponse objLv = new LeaveApplicationResponse();
                    objLv.LeaveID = r["PK_EmpLeaveID"].ToString();
                    objLv.LeaveName = r["LeaveType"].ToString();
                    objLv.UsedLeave = r["UsedLeave"].ToString();
                    objLv.LeaveLimit = r["LeaveLimit"].ToString();
                    objLv.RemainingLeave = r["RemainingLeave"].ToString();
                    lstResponse.Add(objLv);
                }
                model.lstleaveapplication = lstResponse;

                model.Status = "1";
                model.Message = "Record Found";
            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveLeaveApplication(SaveEmployeeLeaveRequest obj)
        {
            Response Response = new Response();
            try
            {
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                DataSet ds = new DataSet();
                ds = obj.SaveEmployeeLeave();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Response.Status = "1";
                        Response.Message = "Employee Leave Request saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        Response.Status = "0";
                        Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    Response.Status = "0";
                    Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                }

            }
            catch (Exception ex)
            {
                Response.Status = "0";
                Response.Message = ex.Message;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LeaveReportForEmployeeBy(LeaveReportForEmployeeRequest model)
        {
            List<LeaveReportForEmployeeResponse> lst = new List<LeaveReportForEmployeeResponse>();

            model.LeaveID = model.LeaveID == "0" ? null : model.LeaveID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.LeaveList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    LeaveReportForEmployeeResponse obj = new LeaveReportForEmployeeResponse();
                    obj.LeaveAppID = r["PK_LeaveAppID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeLeaveID = r["FK_EmpLeaveId"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.Remark = r["ApproverRemark"].ToString();
                    obj.LeaveStatus = r["LeaveStatus"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.ApprovedBy = r["ApprovedByName"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    obj.Name = r["ApprovedByName"].ToString();

                    lst.Add(obj);
                }
                model.lstleavereports = lst;
            }
            //else
            //{
            //    model.Status = "0";
            //    model.Message = "Record Not Found";
            //}

            #region RequestStatus
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            model.RequestStatus = RequestStatus;
            #endregion RequestStatus

            #region ddlLeave
            int count = 0;
            List<SelectListItem> ddlLeave = new List<SelectListItem>();
            DataSet ds = model.LeaveTypeListForEmp();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
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

            model.ddlLeave = ddlLeave;

            #endregion
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LeaveCount(LeaveCountRequest model)
        {
            List<LeaveCountResponse> lst = new List<LeaveCountResponse>();
            DataSet ds1 = model.LeaveCount();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    LeaveCountResponse obj = new LeaveCountResponse();
                    obj.LeaveAppID = r["PK_EmpLeaveID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    obj.LeaveLimit = r["LeaveLimit"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.RemainingLeave = r["RemainingLeave"].ToString();
                    lst.Add(obj);
                }
                model.lstLeaveCount = lst;
            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalaryPaymentReport(SalaryPaymentReportByRequest model)
        {
            List<SalaryPaymentReportByResponse> lst = new List<SalaryPaymentReportByResponse>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.SalaryPaymentReportBy();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    SalaryPaymentReportByResponse obj = new SalaryPaymentReportByResponse();
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
                model.lstSalaryPaymentRepor = lst;
            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DateWiseAttendanceReport(DateWiseAttendanceReportRequest model)
        {

            List<DateWiseAttendanceReportResponse> lst = new List<DateWiseAttendanceReportResponse>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.DateWiseAttendanceReportBy();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    DateWiseAttendanceReportResponse obj = new DateWiseAttendanceReportResponse();
                    obj.EmployeeID = r["FK_EmployeeID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeLoginId = r["LoginID"].ToString();
                    obj.ISHalfDay = r["IsHalfDay"].ToString();
                    obj.Attendance = r["Status"].ToString();
                    obj.AttendanceDate = r["AttendanceDate"].ToString();
                    lst.Add(obj);
                }
                model.lstDateWiseAttendanceReport = lst;
            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found";
            }

            #region HAlfFullDay
            List<SelectListItem> AttendType = Common.AttendanceStatus();
            model.AttendType = AttendType;
            #endregion HAlfFullDay

            #region IsFullHalfDay
            List<SelectListItem> ISHalfFullDay = Common.IsFullHalfDay();
            model.ISHalfFullDay = ISHalfFullDay;
            #endregion IsFullHalfDay

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Message(MessageRequest model)
        {
            Response Response = new Response();
            try
            {
                DataSet ds = model.InsertMessage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        Response.Status = "1";
                        Response.Message = "Your Message has been submitted successfully. ";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        Response.Status = "0";
                        Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    Response.Status = "0";
                    Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Status = "0";
                Response.Message = ex.Message;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MessagesList(MessagesListRequest model)
        {
            List<MessagesListResponse> lstComplains = new List<MessagesListResponse>();
            try
            {
                DataSet ds = model.GetAllMessages();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Status = "1";
                    model.Message = "Record Found";

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        MessagesListResponse obj = new MessagesListResponse();
                        obj.MessageID = r["PK_MessageID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.Message = r["Message"].ToString();
                        obj.MessageDate = r["MessageDate"].ToString();
                        obj.MessageStatus = r["Status"].ToString();
                        lstComplains.Add(obj);
                    }
                    model.lstList = lstComplains;
                }
                else
                {
                    model.Status = "0";
                    model.Message = "Record Not Found";
                }
            }
            catch (Exception ex)
            {
                model.Status = "0";
                model.Message = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}