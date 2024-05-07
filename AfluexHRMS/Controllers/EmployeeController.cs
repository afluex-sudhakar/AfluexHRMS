using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeWork(Employee model)
        {
            List<Employee> lst = new List<Employee>();
            
            DataSet ds1 = model.EmployeeReportBy();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Employee objM = new Employee();
                    objM.number = r["number"].ToString();
                    objM.id = r["id"].ToString();
                    objM.name = r["name"].ToString();
                    objM.father = r["father"].ToString();
                    objM.house = r["house"].ToString();
                    objM.age = r["age"].ToString();
                    objM.gender = r["gender"].ToString();
                    objM.pollingstation = r["pollingstation"].ToString();
                    objM.stationaddress = r["stationaddress"].ToString();
                    objM.kinType = r["kinType"].ToString();
                    objM.year = r["year"].ToString();
                    objM.date1 = r["date1"].ToString();
                    objM.date2 = r["date2"].ToString();
                    objM.add1 = r["add1"].ToString();
                    objM.add2 = r["add2"].ToString();
                    objM.postcode = r["postcode"].ToString();
                    lst.Add(objM);
                }
                model.lstList = lst;
            }
            return View(model);
        }


        public ActionResult EmployeeEducation()
        {
            return View();
        }

        public ActionResult EmployeeFamily()
        {
            return View();
        }
    }
}