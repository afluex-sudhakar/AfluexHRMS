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
    public class EmployeeLeaveController : AdminBaseController
    {
        // GET: EmployeeLeave
        public ActionResult Index()
        {
            return View();
        }

        #region ListForLeaveApproval

        public ActionResult ListForLeaveApproval(Master model)
        {
            return View();
        }

        [HttpPost]
        [ActionName("ListForLeaveApproval")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ListForLeaveApprovalBy(Master model)
        {

            List<Master> lst = new List<Master>();

            DataSet ds1 = model.ListForLeaveApprovalBy();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LeaveApplicationID = r["PK_LeaveAppID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    //obj.LeaveLimit = r["LeaveLimit"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.Status = r["LeaveStatus"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }
        public ActionResult ApproveLeave(string LeaveApplicationID, string Remark, string UsedLeave)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master model = new Master();
                model.LeaveApplicationID = LeaveApplicationID;
                model.Remark = Remark;
                model.UsedLeave = UsedLeave;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                //  model.Result = "yes";
                DataSet ds = model.ApproveLeave();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ApproveLeave"] = "Leave Approved successfully !";
                    }
                    else
                    {
                        TempData["ErrApproveLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrApproveLeave"] = ex.Message;
            }
            FormName = "ListForLeaveApproval";
            Controller = "EmployeeLeave";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult RejectLeave(string LeaveApplicationID, string Remark, string UsedLeave)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master model = new Master();
                model.LeaveApplicationID = LeaveApplicationID;
                model.Remark = Remark;
                model.UsedLeave = UsedLeave;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                //  model.Result = "yes";
                DataSet ds = model.RejectLeave();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ApproveLeave"] = "Leave rejected  !";
                    }
                    else
                    {
                        TempData["ErrApproveLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrApproveLeave"] = ex.Message;
            }
            FormName = "ListForLeaveApproval";
            Controller = "EmployeeLeave";

            return RedirectToAction(FormName, Controller);

        }
        #endregion

        #region LeaveApprovalReport
        public ActionResult LeaveApprovalReport(Master model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("LeaveApprovalReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult LeaveApprovalReportBy(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.LeaveApprovalReportBy();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LeaveApplicationID = r["PK_LeaveAppID"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    //obj.LeaveLimit = r["LeaveLimit"].ToString();
                    obj.UsedLeave = r["UsedLeave"].ToString();
                    obj.Status = r["LeaveStatus"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }
        #endregion
    }
}