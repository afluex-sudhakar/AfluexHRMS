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
    public class PayrollController : AdminBaseController
    {
        // GET: Payroll
        public ActionResult Index()
        {
            return View();
        }

        #region SalaryPosting
        public ActionResult SalaryPosting(Master model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("SalaryPosting")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SalaryPostingBy(Master model)
        {
            List<Master> lst = new List<Master>();
            model.SalaryDate = string.IsNullOrEmpty(model.SalaryDate) ? null : Common.ConvertToSystemDate(model.SalaryDate, "dd/MM/yyyy");
            model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/MM/yyyy");
            DataSet ds1 = model.SalaryPostingList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.Basic = r["Basic"].ToString();
                    obj.HRA = r["HRA"].ToString();
                    obj.Conveyance = r["Conveyance"].ToString();
                    obj.DA = r["DA"].ToString();
                    obj.Bonus = r["Bonus"].ToString();
                    obj.OtherMed = r["Other(Medical)"].ToString();
                    obj.Misce = r["Misce(Comm)"].ToString();
                    obj.EPF = r["EPF"].ToString();
                    obj.ESICE = r["ESIC-E"].ToString();
                    obj.PerDaySalary = r["PerDaySalary"].ToString();
                    obj.PF = r["PF"].ToString();
                    obj.ESIC = r["ESIC"].ToString();
                    obj.EFund = r["E-Fund"].ToString();
                    obj.Medical = r["Medical"].ToString();
                    obj.Loan = r["Loan"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.Noofdays = r["Noofdays"].ToString();
                    obj.TotalEarning = r["TotalEarning"].ToString();
                    obj.NetSalary = r["NetSalary"].ToString();
                    obj.EmployeeCode = r["LoginId"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.GIP = r["GIP"].ToString();
                    obj.OverTime = r["OverTime"].ToString();
                    obj.OverTimeSalary = r["Overtimesalary"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            return View(model);
        }


        [HttpPost]
        [ActionName("SalaryPosting")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEmployeeSalary(Master obj)
        {
            string FormName = "";
            string Controller = "";

            obj.SalaryDate = string.IsNullOrEmpty(obj.SalaryDate) ? null : Common.ConvertToSystemDate(obj.SalaryDate, "dd/MM/yyyy");
            obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");

            try
            {

                string noofrows = Request["hdrows"].ToString();
                string Empid = "";
                string NoOfDay = "";
                string BS = "";
                string HRA = "";
                string CONY = "";
                string DA = "";
                string BN = "";
                string OtherMed = "";
                string Misc = "";
                string Gross = "";
                string EPF = "";
                string ESICE = "";
                string PF = "";
                string ESIC = "";
                string EFund = "";
                string MED = "";
                string GIP = "";
                string Other = "";
                string Loan = "";
                string netsal = "";
                string signature = "";
                string chk = "";
                string overtime = "";
                string overtimesal = "";
                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_EmpID ", typeof(string));

                dtst.Columns.Add("Noofdays", typeof(string));
                dtst.Columns.Add("Basic", typeof(string));
                dtst.Columns.Add("HRA", typeof(string));
                dtst.Columns.Add("Conveyance", typeof(string));
                dtst.Columns.Add("DA  ", typeof(string));
                dtst.Columns.Add("Bonus ", typeof(string));
                dtst.Columns.Add("OtherMed ", typeof(string));
                dtst.Columns.Add("Misce", typeof(string));
                dtst.Columns.Add("TotalEarning ", typeof(string));
                dtst.Columns.Add("EPF ", typeof(string));
                dtst.Columns.Add("ESICE", typeof(string));
                dtst.Columns.Add("PF", typeof(string));
                dtst.Columns.Add("ESIC ", typeof(string));
                dtst.Columns.Add("EFund", typeof(string));
                dtst.Columns.Add("Medical  ", typeof(string));
                dtst.Columns.Add("GIP ", typeof(string));
                dtst.Columns.Add("Other", typeof(string));
                dtst.Columns.Add("Loan ", typeof(string));
                dtst.Columns.Add("NetSalary", typeof(string));
                dtst.Columns.Add("Signature", typeof(string));
                dtst.Columns.Add("Overtime", typeof(string));
                dtst.Columns.Add("Overtimesalary", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        Empid = Request["empid " + i].ToString();
                        NoOfDay = Request["days " + i].ToString();
                        BS = Request["basic " + i].ToString();
                        HRA = Request["hra " + i].ToString();
                        CONY = Request["cony " + i].ToString();
                        DA = Request["da " + i].ToString();
                        BN = Request["bonus " + i].ToString();
                        OtherMed = Request["otherMed " + i].ToString();
                        Misc = Request["misce " + i].ToString();
                        Gross = Request["ttlearn " + i].ToString();
                        EPF = Request["epf " + i].ToString();
                        ESICE = Request["esice " + i].ToString();
                        PF = Request["pf " + i].ToString();
                        ESIC = Request["esic " + i].ToString();
                        EFund = Request["efund " + i].ToString();
                        MED = Request["medical " + i].ToString();
                        GIP = Request["gip " + i].ToString();
                        Other = Request["other " + i].ToString();
                        Loan = Request["loan " + i].ToString();
                        netsal = Request["netsalary " + i].ToString();
                        signature = Request["txtSig_ " + i].ToString();
                        overtime = Request["overtime " + i].ToString();
                        overtimesal = Request["overtimesal " + i].ToString();
                        

                        dtst.Rows.Add(Empid, NoOfDay, BS, HRA, CONY, DA, BN, OtherMed, Misc, Gross, EPF, ESICE, PF, ESIC, EFund, MED, GIP, Other, Loan, netsal, signature, overtime, overtimesal);
                    }
                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtst;

                DataSet ds = obj.SaveEmployeeSalary();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Salary"] = " Salary Saved successfully !";
                    }
                    else
                    {
                        TempData["ErrSalary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrSalary"] = ex.Message;
            }
            FormName = "SalaryPosting";
            Controller = "Payroll";
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region AdvanceSalaryPayment
        public ActionResult AdvanceSalaryPayment(Master model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("AdvanceSalaryPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AdvanceSalaryPaymentBy(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.EmployeeList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.EmployeeCode = r["LoginID"].ToString();
                    obj.EmployeeID = r["PK_EmployeeID"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("AdvanceSalaryPayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveAdvanceSalaryPayment(Master obj)
        {
            string FormName = "";
            string Controller = "";

            obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
            try
            {
                string noofrows = Request["hdrows"].ToString();
                string Empid = "";
                string amt = "";
                string remark = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_EmpID ", typeof(string));
                dtst.Columns.Add("AdvanceAmount", typeof(string));
                dtst.Columns.Add("Remark", typeof(string));
                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    Empid = Request["empid " + i].ToString();
                    amt = string.IsNullOrEmpty(Request["amt " + i].ToString()) ? "0" : Request["amt " + i].ToString();
                    remark = string.IsNullOrEmpty(Request["remark " + i].ToString()) ? null : Request["remark " + i].ToString();
                    
                    dtst.Rows.Add(Empid, amt, remark);
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtst;

                DataSet ds = obj.SaveEmployeeAdvanceSalary();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AdvanceSalary"] = " Advance Salary Saved successfully !";
                    }
                    else
                    {
                        TempData["ErrAdvanceSalary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrAdvanceSalary"] = ex.Message;
            }
            FormName = "AdvanceSalaryPayment";
            Controller = "Payroll";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region SalaryPayment
        public ActionResult SalaryPayment(Master model)
        {
            #region ddlPaymentMode
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet ds1 = obj.PaymentModeList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_PaymentModeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlPaymentMode = ddlPaymentMode;

            #endregion
            return View(model);

        }
        [HttpPost]
        [ActionName("SalaryPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SalaryPaymentBy(Master model)
        {
            List<Master> lst = new List<Master>();
            model.SalaryDate = string.IsNullOrEmpty(model.SalaryDate) ? null : Common.ConvertToSystemDate(model.SalaryDate, "dd/MM/yyyy");
            DataSet ds1 = model.EmployeeListForSalaryPayment();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj1 = new Master();
                    obj1.EmployeeName = r["EmployeeName"].ToString();
                    obj1.EmployeeCode = r["LoginID"].ToString();
                    obj1.EmployeeID = r["PK_EmpSalaryPaidID"].ToString();
                    obj1.SalaryAmount = r["TotalEarning"].ToString();
                    lst.Add(obj1);
                }
                model.lstList = lst;
            }
            #region ddlPaymentMode
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet ds12 = obj.PaymentModeList();
            if (ds12 != null && ds12.Tables.Count > 0 && ds12.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds12.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_PaymentModeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlPaymentMode = ddlPaymentMode;

            #endregion
            return View(model);

        }
        [HttpPost]
        [ActionName("SalaryPayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSalaryPayment(Master obj)
        {
            string FormName = "";
            string Controller = "";


            obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");

            try
            {

                string noofrows = Request["hdrows"].ToString();
                string Empid = "";
                string totalsal = "";
                string paidamt = "";
                string balance = "";
                string paymode = "";
                string chk = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_EmpID ", typeof(string));
                dtst.Columns.Add("TotalSalary", typeof(string));
                dtst.Columns.Add("PaidAmount", typeof(string));
                dtst.Columns.Add("BalanceAmount", typeof(string));
                dtst.Columns.Add("FK_PaymentMode", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        Empid = Request["empid " + i].ToString();
                        totalsal = string.IsNullOrEmpty(Request["salamt " + i].ToString()) ? "0" : Request["salamt " + i].ToString();
                        paidamt = string.IsNullOrEmpty(Request["paidamt " + i].ToString()) ? "0" : Request["paidamt " + i].ToString();
                        balance = string.IsNullOrEmpty(Request["balance " + i].ToString()) ? "0" : Request["balance " + i].ToString();
                        paymode = string.IsNullOrEmpty(Request["PaymentMode " + i].ToString()) ? null : Request["PaymentMode " + i].ToString();

                        dtst.Rows.Add(Empid, totalsal, paidamt, balance, paymode);
                    }
                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtst;

                DataSet ds = obj.SaveEmployeeFinalSalary();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmpSalary"] = "  Salary Saved successfully !";
                    }
                    else
                    {
                        TempData["ErrEmpSalary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrEmpSalary"] = ex.Message;
            }
            FormName = "SalaryPayment";
            Controller = "Payroll";

            return RedirectToAction(FormName, Controller);
        }

        #endregion
    }
}