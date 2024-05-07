using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class EmployeeLogin
    {
        public string postedFile { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string ProfilePic { get; set; }
        public string GenderID { get; set; }
        public List<EmployeeLogin> lstListprofile { get; set; }
        public List<EmployeeLogin> lstGetIDCard { get; set; }
        public string EmployeeCode { get; set; }
        public string CompanyName { get; set; }
        public string DateOfJoining { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string DOJ { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PAN { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string FatherName { get; set; }
        public List<EmployeeLogin> lstList { get; set; }
        public string EmployeeID { get; set; }
        public string LeaveID { get; set; }
        public string LeaveName { get; set; }
        public string LeaveLimit { get; set; }
        public string UsedLeave { get; set; }
        public string RemainingLeave { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string LeaveAppID { get; set; }
        public string EmployeeLeaveID { get; set; }

        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string EmployeeName { get; set; }
        public string Name { get; set; }

        public string Message { get; set; }
        public string MessageDate { get; set; }
        public string MessageStatus { get; set; }

        public string MessageID { get; set; }
        public string RequestCode { get; set; }
        public string LoginID { get; set; }

        public string Reply { get; internal set; }
        public string ReplyPerson { get; internal set; }
        public string ReplyDate { get; internal set; }

        public string AddedBy { get; set; }
        public string emailto { get; set; }
        public string subject { get; set; }



        public string Employeee { get; set; }
        public string EmployeeLoginId { get; set; }
        public string ISHalfDay { get; set; }

        public string Attendance { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }

        public string TotalHRWork { get; internal set; }
        public string AttendanceDate { get; internal set; }

        public DataSet LeaveTypeListForEmp()
        {
            SqlParameter[] para = {
                     new SqlParameter("@Pk_LeaveTypeId",LeaveID),
                      new SqlParameter("@FK_EmpID",EmployeeID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveTypeForEmployee", para);
            return ds;
        }

        public DataSet LeaveTypeListOfEmployee()
        {
            SqlParameter[] para = {
                     new SqlParameter("@FK_EmpID",EmployeeID),};
            DataSet ds = DBHelper.ExecuteQuery("GetLeaveOfEmployee", para);
            return ds;
        }

        public DataSet SaveEmployeeLeave()
        {
            SqlParameter[] para ={
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

        public DataSet LeaveList()
        {
            SqlParameter[] para ={
                           new SqlParameter("@FK_EmpID",EmployeeID),
                           new SqlParameter("@FromDate",FromDate),
                           new SqlParameter("@ToDate",ToDate),
                           new SqlParameter("@LeaveStatus", Status),
                           new SqlParameter("@FK_EmpLeaveId", LeaveID),
                            };
            DataSet ds = DBHelper.ExecuteQuery("LeaveReportForEmployee", para);
            return ds;
        }
        public DataSet MonthlyAttendanceReport()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@EmployeeCode",EmployeeLoginId),
                                new SqlParameter("@Status",Attendance),
                                new SqlParameter("@HDFD ",ISHalfDay),
                                new SqlParameter("@FromDate",FromDate),
                                new SqlParameter("@ToDate",ToDate),
                            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeMonthlyAttendanceReport", para);
            return ds;

        }

        public DataSet LeaveCount()
        {
            SqlParameter[] para ={
                           new SqlParameter("@FK_EmpID",EmployeeID),};
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeLeaveReport", para);
            return ds;
        }

        public DataSet GetAllMessages()
        {
            SqlParameter[] para ={
                           new SqlParameter("@FK_UserID",EmployeeID),
                            new SqlParameter("@Status",MessageStatus),
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetAllMessags", para);
            return ds;
        }

        public DataSet InsertMessage()
        {
            SqlParameter[] para ={
                           new SqlParameter ("@FK_UserID",EmployeeID),
                            new SqlParameter("@LoginID", LoginID ),
                            new SqlParameter("@Message" ,Message)

                            };
            DataSet ds = DBHelper.ExecuteQuery("InsertMessage", para);
            return ds;
        }

        public DataSet GetMessageResponseAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@PK_MessageID", MessageID),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetMessageResponse", para);
            return ds;
        }
        
        public DataSet SendEmail()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@EmailTo",emailto),
                new SqlParameter("@Subject",subject),
                new SqlParameter("@Message",Message)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveEmail", para);
            return ds;
        }

        public DataSet GetProfileDetails()
        {
            SqlParameter[] para = {
                     new SqlParameter("@FK_EmpID",EmployeeID),};
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeProfile", para);
            return ds;
        }
        public DataSet GetIDCard()
        {
            SqlParameter[] para = { new SqlParameter("@EmployeeCode", EmployeeCode) };
            DataSet ds = DBHelper.ExecuteQuery("GetIDCard", para);
            return ds;
        }

        public DataSet UpdateProfileDetails()
        {
            SqlParameter[] para =
            {
                      new SqlParameter("@Fk_EmpID",EmployeeID),
                      new SqlParameter("@Profile",postedFile),
                      new SqlParameter("@Name",Name),
                      new SqlParameter("@FatherName",FatherName),
                      new SqlParameter("@DOB",DOB),
                      new SqlParameter("@Gender",GenderID),
                      new SqlParameter("@BloodGroup",BloodGroup),
                      new SqlParameter("@Email",Email),
                      new SqlParameter("@MobileNo",Contact),
                      new SqlParameter("@Pan",PAN),
                      new SqlParameter("@UpdatedBy",EmployeeID)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEmployeeProfileDetails", para);
            return ds;
        }
    }
}