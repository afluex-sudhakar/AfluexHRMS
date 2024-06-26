﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Models
{
    public class DBHelper
    {
        public static string connectionString = string.Empty;

        static DBHelper()
        {
            try
            {
                connectionString = "Data Source=101.53.150.222,1440;Initial Catalog=HRMSDemoDB; User Id=sa; Password=Fx1479LVAPbF; Integrated Security=false;";

                //connectionString = "Data Source=23.111.171.42;Initial Catalog= HRMSDemoDB; User Id= hrmsuser; Password=HRMS123!@#; Integrated Security=false;";
                //connectionString = "Data Source=DESKTOP-OEAINLG\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }
        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Msg");
                dt.Columns.Add("ErrorMessage");

                DataRow dr = dt.NewRow();
                dr["Msg"] = "0";
                dr["ErrorMessage"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}



