using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class Home:Common
    {
        public string Password { get; set; }
        public string Loginid { get; set; }
        public string Code { get; set; }


        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",Loginid),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }


        public DataSet GetNFCAllotmentStatus()
        {
            SqlParameter[] para = { new SqlParameter("@Code", Code) };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCAllotmentStatus", para);
            return ds;
        }
    }
}