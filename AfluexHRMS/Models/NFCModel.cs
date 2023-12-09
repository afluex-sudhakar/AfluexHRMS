using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexHRMS.Models
{
    public class NFCModel
    {
        public string Code { get; set; }
        public string LogId { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }


        public DataSet GetNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code),
                  new SqlParameter ("@LogId",LogId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCProfileData", para);
            return ds;
        }

        public DataSet CheckNFCCode()
        {
            SqlParameter[] para ={new SqlParameter ("@NFCCode",Code)};
            DataSet ds = DBHelper.ExecuteQuery("CheckNFCCode", para);
            return ds;
        }

        public DataSet ActivateNFCCode(string ActivationCode, string DistributorCode)
        {
            SqlParameter[] para ={
                new SqlParameter ("@LoginId",LoginId),
                new SqlParameter ("@NFCCode",Code),
                new SqlParameter ("@DistributorCode",DistributorCode),
                new SqlParameter ("@ActivationCode",ActivationCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateNFC", para);
            return ds;
        }
    }
}