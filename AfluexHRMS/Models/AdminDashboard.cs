using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class AdminDashboard : Common
    {
        public List<AdminDashboard> List { get; set; }
        public string Month { get; set; }
        public string City2 { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public string TotalUser { get; set; }

        public DataSet BindDataForAdminDashboard()
        {
            DataSet ds = DBHelper.ExecuteQuery("BindDataForAdminDashboard");
            return ds;
        }
    }
}