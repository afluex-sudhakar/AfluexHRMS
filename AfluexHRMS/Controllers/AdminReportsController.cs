using AfluexHRMS.Filter;
using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: AdminReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDesignation(string DepartmentID)
        {
            try
            {
                Master model = new Master();
                model.DepartmentID = DepartmentID;

                #region GetDesignation
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsDes = model.DesignationList();

                if (dsDes != null && dsDes.Tables.Count > 0)
                {
                    model.Result = "yes";
                    foreach (DataRow r in dsDes.Tables[0].Rows)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["Pk_DesignationID"].ToString() });
                    }
                }
                model.ddlDesignation = ddlDesignation;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult EmployeeReport(Master model)
        {
            #region ddlCompany
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlCompany = new List<SelectListItem>();
            DataSet ds1 = obj.CompanyList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCompany.Add(new SelectListItem { Text = "Select Company", Value = "0" });
                    }
                    ddlCompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlCompany = ddlCompany;

            #endregion

            #region ddlDepartment
            Master objDep = new Master();
            int count1 = 0;
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            DataSet ds2 = objDep.DepartmentList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                    }
                    ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlDepartment = ddlDepartment;

            #endregion

            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
            ViewBag.ddlDesignation = ddlDesignation;

            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeReportBy(Master model)
        {
            List<Master> lst = new List<Master>();

            model.CompanyID = string.IsNullOrEmpty(model.CompanyID) ? "0" : null;
            model.DepartmentID = string.IsNullOrEmpty(model.DepartmentID) ? "0" : null;
            model.DesignationID = string.IsNullOrEmpty(model.DesignationID) ? "0" : null;
            DataSet ds1 = model.EmployeeReportBy();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master objM = new Master();
                    objM.Password = r["Password"].ToString();
                    objM.EmployeeID = r["PK_EmployeeID"].ToString();
                    objM.EmployeeCode = r["LoginID"].ToString();
                    objM.EmployeeName = r["EmployeeName"].ToString();
                    objM.CompanyName = r["CompanyName"].ToString();
                    objM.DepartmentName = r["DepartmentName"].ToString();
                    objM.DesignationName = r["DesignationName"].ToString();
                    objM.Gender = r["Gender"].ToString();
                    objM.DateOfJoining = r["DateOfJoining"].ToString();
                    objM.Contact = r["MobileNo"].ToString();
                    lst.Add(objM);
                }
                model.lstList = lst;
            }
            #region ddlCompany
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlCompany = new List<SelectListItem>();
            DataSet ds1Company = obj.CompanyList();
            if (ds1Company != null && ds1Company.Tables.Count > 0 && ds1Company.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1Company.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCompany.Add(new SelectListItem { Text = "Select Company", Value = "0" });
                    }
                    ddlCompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlCompany = ddlCompany;

            #endregion

            #region ddlDepartment
            Master objDep = new Master();
            int count1 = 0;
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            DataSet ds2 = objDep.DepartmentList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                    }
                    ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlDepartment = ddlDepartment;

            #endregion

            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
            ViewBag.ddlDesignation = ddlDesignation;

            return View(model);
        }

        public ActionResult SalaryPaymentReport(Master model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("SalaryPaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SalaryPaymentReportBy(Master model)
        {
            List<Master> lst = new List<Master>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.SalaryPaymentReportBy();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeCode = r["LoginID"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.NetSalary = r["TotalSalary"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Balance = r["BalanceAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }


        public ActionResult Details(string id)
        {
            Master model = new Master();
            model.EmployeeID = id;
            DataSet dsblock = model.DetailedEmployeeInfo();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                //if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                if (dsblock != null && dsblock.Tables.Count > 0)
                {
                    model.EmployeeID = dsblock.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                    model.EmployeeCode = dsblock.Tables[0].Rows[0]["LoginID"].ToString();
                    model.EmployeeName = dsblock.Tables[0].Rows[0]["EmployeeName"].ToString();
                    model.CompanyName = dsblock.Tables[0].Rows[0]["CompanyName"].ToString();
                    model.DepartmentName = dsblock.Tables[0].Rows[0]["DepartmentName"].ToString();
                    model.DesignationName = dsblock.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.Gender = dsblock.Tables[0].Rows[0]["Gender"].ToString();
                    model.DateOfJoining = dsblock.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    model.Contact = dsblock.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.ProfilePic = dsblock.Tables[0].Rows[0]["ProfilePic"].ToString();
                    model.Address = dsblock.Tables[0].Rows[0]["PerAddress"].ToString();
                    model.PinCode = dsblock.Tables[0].Rows[0]["PerPinCode"].ToString();
                    model.State = dsblock.Tables[0].Rows[0]["PerState"].ToString();
                    model.City = dsblock.Tables[0].Rows[0]["PerCity"].ToString();
                    model.Email = dsblock.Tables[0].Rows[0]["Email"].ToString();
                    model.DOB = dsblock.Tables[0].Rows[0]["DOB"].ToString();
                    ViewBag.ProfilePic = dsblock.Tables[0].Rows[0]["ProfilePic"].ToString();
                    {
                        List<Master> lst = new List<Master>();
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            Master obj = new Master();
                            obj.Board = r["Degree"].ToString();
                            obj.DegreeCourse = r["Course"].ToString();
                            obj.YearOfPassing = r["YearOfPassing"].ToString();

                            lst.Add(obj);
                        }
                        model.lstQualification = lst;
                    }
                    {
                        List<Master> lst1 = new List<Master>();
                        foreach (DataRow r in dsblock.Tables[2].Rows)
                        {
                            Master obj = new Master();
                            obj.PreviousCompany = r["PreviousCompany"].ToString();
                            obj.FromDate = r["FromDate"].ToString();
                            obj.ToDate = r["ToDate"].ToString();
                            obj.Post = r["Post"].ToString();
                            obj.CertificateSubmitted = r["CertificateSubmitted"].ToString();
                            lst1.Add(obj);
                        }
                        model.lstWorkExperience = lst1;
                    }
                    {
                        List<Master> lst2 = new List<Master>();
                        foreach (DataRow r in dsblock.Tables[3].Rows)
                        {
                            Master obj = new Master();
                            //obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                            //obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                            obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                            //obj.HeadNature = r["HeadNature"].ToString();
                            obj.SalaryAmount = r["Amount"].ToString();
                            lst2.Add(obj);
                        }
                        model.lstEarning = lst2;
                    }
                    {
                        List<Master> lst3 = new List<Master>();
                        foreach (DataRow r in dsblock.Tables[4].Rows)
                        {
                            Master obj = new Master();
                            //obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                            //obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                            obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                            //obj.HeadNature = r["HeadNature"].ToString();
                            obj.SalaryAmount = r["Amount"].ToString();
                            lst3.Add(obj);
                        }
                        model.lstDeduction = lst3;
                    }
                    {
                        List<Master> lst4 = new List<Master>();
                        foreach (DataRow r in dsblock.Tables[5].Rows)
                        {
                            Master obj = new Master();
                            //obj.LeaveID = r["PK_LeaveTypeID"].ToString();
                            obj.LeaveName = r["LeaveType"].ToString();
                            obj.LeaveLimit = r["LeaveLimit"].ToString();
                            lst4.Add(obj);
                        }
                        model.lstLeave = lst4;
                    }
                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }
    }
}