﻿using System;
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
        public string RequestRemark { get; set; }
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

    public class UpdateBusinessProfileForMobile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string FK_UserId { get; set; }
        public string PK_ProfileId { get; set; }
        public DataTable dtcontact { get; set; }
        public DataTable dtemail { get; set; }
        public DataTable dtweblink { get; set; }
        public DataTable dtsocial { get; set; }



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


    public class SaveEmployeeAttendanceRequest
    {
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string AttendanceDate { get; set; }
        public string EmployeeID { get; set; }
        public string AddedBy { get; set; }
        public string EmployeePhoto { get; set; }
        public string LatiTude { get; set; }
        public string LongiTude { get; set; }
        public string Activity { get; set; }

        public DataSet SaveEmployeeAttendance()
        {
            SqlParameter[] para ={
                                       //new SqlParameter ("@Intime",InTime),
                                       //new SqlParameter ("@OutTime",OutTime),
                                       //new SqlParameter ("@AttendanceDate",AttendanceDate),
                                       new SqlParameter ("@FK_EmpID",EmployeeID),
                                       new SqlParameter ("@AddedBy",AddedBy),
                                       new SqlParameter ("@UploadFile",EmployeePhoto),
                                       new SqlParameter ("@LatiTude",LatiTude),
                                       new SqlParameter ("@LongiTude",LongiTude)
            };
            DataSet ds = DBHelper.ExecuteQuery("PunchingEmployee", para);
            return ds;
        }
    }



    public class SaveEmployeeAttendanceResponse
    {
        public string status { get; set; }
        public string Message { get; set; }
        public string PunchInDate { get; set; }
        public string PunchInTime { get; set; }

    }



    public class SaveEmployeeAttendancePunchoutRequest
    {

        public string OutTime { get; set; }
        public string AttendanceDate { get; set; }
        public string EmployeeID { get; set; }
        public string OutLongitude { get; set; }
        public string OutLatiTude { get; set; }

        public DataSet SaveEmployeePunchoutAttendance()
        {
            SqlParameter[] para ={
                                       //new SqlParameter ("@OutTime",OutTime),
                                       //new SqlParameter ("@AttendanceDate",AttendanceDate),
                                       new SqlParameter ("@FK_EmpID",EmployeeID),
                                       new SqlParameter ("@OutLongitude",OutLongitude),
                                       new SqlParameter ("@OutLatiTude",OutLatiTude)
            };
            DataSet ds = DBHelper.ExecuteQuery("PunchoutEmployee", para);
            return ds;
        }
    }



    public class SaveEmployeeAttendancePunchoutResponse
    {
        public string status { get; set; }
        public string Message { get; set; }
        public string PunchOutDate { get; set; }
        public string PunchOutTime { get; set; }

    }





    public class GetAttenndaceListReqst
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FK_EmpID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public List<GetAttenndaceListRespons> listAttenndace { get; set; }

        public DataSet GetAttenndaceList()
        {
            SqlParameter[] para ={
                new SqlParameter("@FK_EmpID",FK_EmpID),
                  new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAttenndaceList", para);
            return ds;
        }
    }
    public class GetAttenndaceListRespons
    {
        public string LoginID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string PerAddress { get; set; }
        public string LocAddress { get; set; }
        public string AttendanceDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string UploadFile { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string OutLatitude { get; set; }
        public string OutLongitude { get; set; }
    }



    public class GetEmployeeProfileRequest
    {

        public string PK_EmployeeID { get; set; }

        public DataSet GetEmployeeProfile()
        {
            SqlParameter[] para ={
                new SqlParameter("@FK_EmpID",PK_EmployeeID),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeProfile", para);
            return ds;
        }
    }
    public class GetEmployeeProfileResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginID { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public string PhoneNo { get; set; }
        public string DOJ { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PanNo { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSCCOde { get; set; }
        public string ProfilePic { get; set; }
    }

    public class UpdateEmployeeProfileRequest
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string PerAddress { get; set; }
        public string PerState { get; set; }
        public string PerPinCode { get; set; }
        public string PerCity { get; set; }
        public string Email { get; set; }
        public string LocAddress { get; set; }
        public string DateOfJoining { get; set; }
        public string Pan { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string IFSCCOde { get; set; }
        public string Image { get; set; }

        public DataSet UpdateEmployeeProfile()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter ("@FK_EmpID",EmployeeID),
                            new SqlParameter("@EmployeeName", EmployeeName ),
                            new SqlParameter("@DOB" ,DOB),
                              new SqlParameter ("@Gender",Gender),
                            new SqlParameter("@MobileNo", MobileNo ),
                            new SqlParameter("@PerAddress" ,PerAddress),
                              new SqlParameter ("@PerPinCode",PerPinCode),
                            new SqlParameter("@PerState", PerState ),
                            new SqlParameter("@PerCity" ,PerCity),
                              new SqlParameter ("@Email",Email),
                            new SqlParameter("@LocAddress", LocAddress ),
                            new SqlParameter("@DateOfJoining" ,DateOfJoining),
                              new SqlParameter ("@Pan",Pan),
                            new SqlParameter("@BankAccountNo", BankAccountNo ),
                            new SqlParameter("@BankName" ,BankName),
                              new SqlParameter ("@BankBranchName",BankBranchName),
                            new SqlParameter("@IFSCCOde", IFSCCOde ),
                            new SqlParameter("@Profile" ,Image)
                            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEmployeeProfile", para);
            return ds;
        }
    }



}