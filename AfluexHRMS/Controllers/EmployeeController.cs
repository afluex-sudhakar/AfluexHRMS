using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class EmployeeController : UserBaseController
    {
        // GET: Employee
        public ActionResult EmployeeWork()
        {
            return View();
        }
    }
}