using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AfluexHRMS.Models
{
    public class NFCModel
    {
        public string Code { get; set; }
        public string LogId { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string Medium { get; set; }

        public string WebClient { get; set; }
        public string ZipCode { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public string Location { get; set; }
        public string Device { get; set; }

        public string Body { get; set; }
        public string Result { get; set; }


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
        public DataSet InsertLog()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code),
                new SqlParameter ("@Browser",Browser),
                new SqlParameter ("@IP",IP),
                new SqlParameter ("@Medium",Medium),
                new SqlParameter ("@Lat",Lat),
                new SqlParameter ("@Long",Long),
                new SqlParameter ("@Location",Location),
                new SqlParameter ("@ZipCode",ZipCode),
                new SqlParameter ("@Device",Device)
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertLog", para);
            return ds;
        }
    }
    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }
    
}