using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class EmployeeAttendance : Common
    {
        public List<EmployeeAttendance> listAttendance { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationtName { get; set; }
        public string EmployeeName { get; set; }
        public string Pk_EmployeeID { get; set; }
        public string EmployeeLoginId { get; set; }
        public string EmployeeCode { get; set; }
        public string Status { get; set; }
        public DataTable dsEmployeeAttendance { get; set; }
        public string AttendanceDate { get; set; }

        public string IsPresent { get; set; }
        public string EmployeeID { get; set; }

        public string WHLimit { get; set; }
        public string Attendance { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string ISHalfDay { get; set; }
        public string OverTime { get; set; }
        public string TotalHRWork { get; set; }
        public List<EmployeeAttendance> lstList { get; set; }
        public DataTable dtTable { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string PresentDay { get; set; }
        public string AbsentDay { get; set; }
        public string LeaveDays { get; set; }
        public string HalfDays { get; set; }
        public string FullDays { get; set; }


        public DataSet BindDepartmentDetails()
        {
            SqlParameter[] para ={
                                new SqlParameter("@Pk_DepartmentId",DepartmentName),};
            DataSet ds = DBHelper.ExecuteQuery("GetDepartment", para);
            return ds;

        }

        public DataSet BindDesignationDetails()
        {
            SqlParameter[] para ={
                                new SqlParameter("@Pk_DesignationID",DesignationtName), };
            DataSet ds = DBHelper.ExecuteQuery("GetDesignation", para);
            return ds;
        }

        public DataSet BindEmployeeDetails()
        {
            SqlParameter[] para ={
                                new SqlParameter("@PK_EmployeeID",EmployeeName),};
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }

        public DataSet GetEmployeeForAttendance()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginID",EmployeeLoginId),
                                        new SqlParameter("@Fk_DepartmentId",DepartmentName),
                                        new SqlParameter("@Fk_DesignationId",DesignationtName),
                                        new SqlParameter("@PK_EmployeeID",EmployeeName),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }

        public DataSet SaveStudentAttendance()
        {
            SqlParameter[] para = {
                 new SqlParameter("@EmployeeAttendance", dsEmployeeAttendance),
                new SqlParameter("@AttendanceDate", AttendanceDate),
                 new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AddEmployeeAttendance", para);
            return ds;
        }

        public DataSet EmployeeListForAttendance()
        {
            SqlParameter[] para ={
                                new SqlParameter("@EmployeeName",EmployeeName),
                                new SqlParameter("@EmployeeCode",EmployeeLoginId),
                                new SqlParameter("@IsPresent",IsPresent)
                            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeForAttendance", para);
            return ds;
        }

        public DataSet SaveEmployeeDailyAttendance()
        {
            SqlParameter[] para ={
                                new SqlParameter("@EmployeeAttendance",dtTable),
                                new SqlParameter("@AttendanceDate",AttendanceDate),
                                  new SqlParameter("@AddedBy",AddedBy),
                            };
            DataSet ds = DBHelper.ExecuteQuery("DailyAttendancePosting", para);
            return ds;
        }

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

        public DataSet AttendanceSummaryReportBy()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@EmployeeCode",EmployeeLoginId),
                                new SqlParameter("@EmployeeName",EmployeeName),
                                new SqlParameter("@FromDate",FromDate),
                                new SqlParameter("@ToDate",ToDate),
                            };
            DataSet ds = DBHelper.ExecuteQuery("AttendanceSummaryReport", para);
            return ds;

        }
    }
}