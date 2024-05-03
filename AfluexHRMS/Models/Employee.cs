using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class Employee
    {
        public string number { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string father { get; set; }
        public string house { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string pollingstation { get; set; }
        public string stationaddress { get; set; }
        public string kinType { get; set; }
        public string year { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string postcode { get; set; }
        
        public List<Employee> lstList { get; set; }
        public List<Employee> lstListenglish { get; set; }

        public DataSet EmployeeReportBy()
        {
            DataSet ds = DBHelper.ExecuteQuery("list");
            return ds;
        }

        public DataSet ReporntInEnglish()
        {
            DataSet ds = DBHelper.ExecuteQuery("list");
            return ds;
        }
    }
}