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
    }
}