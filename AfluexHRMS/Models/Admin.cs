using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class Admin
    {
        public string Pk_AdminID { get; set; }
        public string ProfilePicture { get; set; }


        public DataSet UpdateProfilePic()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_AdminID",Pk_AdminID ) ,
                                      new SqlParameter("@ProfilePic", ProfilePicture)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfilePic", para);
            return ds;
        }
    }
}