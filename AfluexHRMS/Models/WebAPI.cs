using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Models
{
    public class WebAPI
    {
    }

    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class LoginRequest
    {
        public string LoginId { get; set; }
        public string Password { get; set; }

        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
    }

    public class LoginResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginID { get; set; }
        public string PK_EmployeeID { get; set; }
        public string UserType { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePic { get; set; }
    }
    
    public class LeaveApplicationRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LeaveID { get; set; }
        public string EmployeeID { get; set; }
        public List<LeaveApplicationResponse> lstleaveapplication { get; set; }
        public List<SelectListItem> ddlLeave { get; set; }

        public DataSet LeaveTypeListForEmp()
        {
            SqlParameter[] para =
                            {
                     new SqlParameter("@Pk_LeaveTypeId",LeaveID),
                      new SqlParameter("@FK_EmpID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveTypeForEmployee", para);
            return ds;
        }

        public DataSet LeaveTypeListOfEmployee()
        {
            SqlParameter[] para =
                            {
                     new SqlParameter("@FK_EmpID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveOfEmployee", para);
            return ds;
        }
    }
    public class LeaveApplicationResponse
    {
        public string LeaveID { get; set; }
        public string LeaveName { get; set; }
        public string UsedLeave { get; set; }
        public string LeaveLimit { get; set; }
        public string RemainingLeave { get; set; }
    }

    public class SaveEmployeeLeaveRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeID { get; set; }
        public string Remark { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LeaveID { get; set; }

        public DataSet SaveEmployeeLeave()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter("@FK_EmpID",EmployeeID),
                           new SqlParameter("@AddedBy",EmployeeID),
                           new SqlParameter("@EmployeeRemark",Remark),
                           new SqlParameter("@FromDate",FromDate),
                           new SqlParameter("@ToDate",ToDate),
                           new SqlParameter("@FK_LeaveID",LeaveID)
                            };
            DataSet ds = DBHelper.ExecuteQuery("LeaveApplicationByEmployee", para);
            return ds;
        }
    }

    public class LeaveReportForEmployeeRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LeaveID { get; set; }
        public string EmployeeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LeaveStatus { get; set; }
        public List<LeaveReportForEmployeeResponse> lstleavereports { get; set; }
        public List<SelectListItem> ddlLeave { get; set; }
        public List<SelectListItem> RequestStatus { get; set; }

        public DataSet LeaveTypeListForEmp()
        {
            SqlParameter[] para =
                            {
                     new SqlParameter("@Pk_LeaveTypeId",LeaveID),
                      new SqlParameter("@FK_EmpID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveTypeForEmployee", para);
            return ds;
        }

        public DataSet LeaveList()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter("@FK_EmpID",EmployeeID),
                           new SqlParameter("@FromDate",FromDate),
                           new SqlParameter("@ToDate",ToDate),
                           new SqlParameter("@LeaveStatus", LeaveStatus),
                           new SqlParameter("@FK_EmpLeaveId", LeaveID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("LeaveReportForEmployee", para);
            return ds;
        }
    }

    public class LeaveReportForEmployeeResponse
    {
        public string LeaveAppID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeLeaveID { get; set; }
        public string UsedLeave { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Remark { get; set; }
        public string LeaveStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveName { get; set; }
        public string Name { get; set; }
    }

    public class LeaveCountRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeID { get; set; }
        public List<LeaveCountResponse> lstLeaveCount { get; set; }

        public DataSet LeaveCount()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter("@FK_EmpID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeLeaveReport", para);
            return ds;
        }
    }

    public class LeaveCountResponse
    {
        public string LeaveAppID { get; set; }
        public string EmployeeID { get; set; }
        public string LeaveName { get; set; }
        public string LeaveLimit { get; set; }
        public string UsedLeave { get; set; }
        public string RemainingLeave { get; set; }
    }

    public class SalaryPaymentReportByRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PaymentMode { get; set; }
        public List<SalaryPaymentReportByResponse> lstSalaryPaymentRepor { get; set; }

        public DataSet SalaryPaymentReportBy()
        {
            SqlParameter[] para =
                            {
             new SqlParameter("@EmployeeName", EmployeeName),
             new SqlParameter("@EmployeeCode", EmployeeCode),
             new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
             new SqlParameter("@PaymentMode", PaymentMode),
                            };
            DataSet ds = DBHelper.ExecuteQuery("SalaryPaymentReport", para);
            return ds;
        }
    }

    public class SalaryPaymentReportByResponse
    {
        public string EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string NetSalary { get; set; }
        public string PaidAmount { get; set; }
        public string Balance { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
    }

    public class DateWiseAttendanceReportRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeLoginId { get; set; }
        public string EmployeeName { get; set; }
        public string Attendance { get; set; }
        public string ISHalfDay { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<DateWiseAttendanceReportResponse> lstDateWiseAttendanceReport { get; set; }
        public List<SelectListItem> AttendType { get; set; }
        public List<SelectListItem> ISHalfFullDay { get; set; }

        public DataSet DateWiseAttendanceReportBy()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@EmployeeCode",EmployeeLoginId),
                                new SqlParameter("@EmployeeName",EmployeeName),
                                new SqlParameter("@Status",Attendance),
                                new SqlParameter("@HDFD ",ISHalfDay),
                                new SqlParameter("@FromDate",FromDate),
                                new SqlParameter("@ToDate",ToDate),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DateWiseAttendanceReport", para);
            return ds;
        }
    }

    public class DateWiseAttendanceReportResponse
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLoginId { get; set; }
        public string ISHalfDay { get; set; }
        public string Attendance { get; set; }
        public string AttendanceDate { get; set; }
    }

    public class MessageRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeID { get; set; }
        public string LoginID { get; set; }
        public string NewMessage { get; set; }

        public DataSet InsertMessage()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter ("@FK_UserID",EmployeeID),
                            new SqlParameter("@LoginID", LoginID ),
                            new SqlParameter("@Message" ,NewMessage)

                            };
            DataSet ds = DBHelper.ExecuteQuery("InsertMessage", para);
            return ds;
        }
    }
 
    public class MessagesListRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string EmployeeID { get; set; }
        public string MessageStatus { get; set; }
        public List<MessagesListResponse> lstList { get; set; }
        
        public DataSet GetAllMessages()
        {
            SqlParameter[] para ={
                           new SqlParameter("@FK_UserID",EmployeeID),
                            new SqlParameter("@Status",MessageStatus),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetAllMessags", para);
            return ds;
        }
    }

    public class MessagesListResponse
    {
        public string MessageID { get; set; }
        public string RequestCode { get; set; }
        public string Message { get; set; }
        public string MessageDate { get; set; }
        public string MessageStatus { get; set; }          
    }



    public class EmployeeDashboardRequest
    {
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string FileUpload { get; set; }
        public string AddedBy { get; set; }

        public DataSet SaveQuickEmail()
        {
            SqlParameter[] para ={
                            new SqlParameter("@Email",Email),
                                new SqlParameter("@Subject",Subject),
                            new SqlParameter("@Message",Message),
                                new SqlParameter("@FileUpload",FileUpload),
                               new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = DBHelper.ExecuteQuery("SaveQuickEmail", para);
            return ds;
        }

    }
    


}