using AfluexHRMS.Filter;
using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class MasterController : AdminBaseController
    {
        // GET: Master
        DataTable dtQual = new DataTable();
        DataTable dtWorkExp = new DataTable();

        public ActionResult Index()
        {
            return View();
        }

        #region Company

        public ActionResult CompanyMaster(string CompanyID)
        {
            Master model = new Master();
            if (CompanyID != null)
            {
                model.CompanyID = CompanyID;

                DataSet ds = model.CompanyList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.CompanyID = ds.Tables[0].Rows[0]["PK_CompanyID"].ToString();
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.Contact = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.Website = ds.Tables[0].Rows[0]["Website"].ToString();

                }
            }
            return View(model);
        }
        public ActionResult CompanyList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.CompanyList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CompanyID = r["PK_CompanyID"].ToString();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PinCode = r["PinCode"].ToString();
                    obj.State = r["State"].ToString();
                    obj.City = r["City"].ToString();
                    obj.Contact = r["ContactNo"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Website = r["Website"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult AddCompany(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.AddCompany();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Company"] = "Company saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrCompany"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrCompany"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrCompany"] = ex.Message;
            }
            return RedirectToAction("CompanyMaster", "Master");
        }

        [HttpPost]
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateCompany(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateCompany();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Company"] = "Company updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Company"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["Company"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Company"] = ex.Message;
            }
            return RedirectToAction("CompanyMaster", "Master");
        }


       
        public ActionResult DeleteCompany(String companyid)
        {
            Master obj = new Master();
            try
            {
                obj.CompanyID = companyid;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCompany();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Company"] = "Company deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrCompany"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrCompany"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrCompany"] = ex.Message;
            }
            return RedirectToAction("CompanyList", "Master");
        }
        #endregion

        #region StateCity
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.PinCode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetStateCity1(string Pincode2)
        {
            try
            {
                Master model = new Master();
                model.Pincode2 = Pincode2;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity1();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State2 = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City2 = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State2 = model.City2 = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region Department

        public ActionResult DepartmentMaster(string Pk_DepartmentId)
        {
            Master model = new Master();
            if (Pk_DepartmentId != null)
            {
                model.Pk_DepartmentId = Pk_DepartmentId;
                DataSet ds = model.DepartmentDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_DepartmentId = ds.Tables[0].Rows[0]["PK_DepartmentID"].ToString();
                    model.DepartmentName = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("DepartmentMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveDepartment(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.AddDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentMaster", "Master");
        }

        public ActionResult DepartmentList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.DepartmentDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_DepartmentId = r["PK_DepartmentID"].ToString();
                    obj.DepartmentName = r["DepartmentName"].ToString();

                    lst.Add(obj);
                }
                model.listDepartment = lst;
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("DepartmentMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDepartmentMaster(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentList", "Master");
        }

        public ActionResult DeleteDepartment(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteDepartment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Departmentmsg"] = "Department Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDepartmentmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDepartmentmsg"] = ex.Message;
            }
            return RedirectToAction("DepartmentList", "Master");
        }

        #endregion

        #region Designation

        public ActionResult DesignationMaster(string Pk_DesignationID)
        {
            Master model = new Master();

            #region ddlDepartment
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlDepartment = new List<SelectListItem>();
                DataSet ds1 = obj.DepartmentDetails();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDepartment.Add(new SelectListItem { Text = "Select Department", Value = "0" });
                        }
                        ddlDepartment.Add(new SelectListItem { Text = r["DepartmentName"].ToString(), Value = r["PK_DepartmentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDepartment = ddlDepartment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            if (Pk_DesignationID != null)
            {
                model.Pk_DesignationID = Pk_DesignationID;

                DataSet ds = model.DesignationDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_DesignationID = ds.Tables[0].Rows[0]["Pk_DesignationID"].ToString();
                    model.Fk_DepartmentId = ds.Tables[0].Rows[0]["FK_DepartmentID"].ToString();
                    model.DesignationName = ds.Tables[0].Rows[0]["DesignationName"].ToString();

                }
            }
            return View(model);


        }



        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveDesignation(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.InsertDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Designationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Designationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Designationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationMaster", "Master");
        }


        public ActionResult DesignationList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.DesignationDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_DesignationID = r["Pk_DesignationID"].ToString();
                    obj.Fk_DepartmentId = r["DepartmentName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();

                    lst.Add(obj);
                }
                model.listDesignation = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDesignation(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDesignationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationList", "Master");
        }


        public ActionResult DeleteDesignation(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.DeleteDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designationmsg"] = "Designation Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrDesignationmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrDesignationmsg"] = ex.Message;
            }
            return RedirectToAction("DesignationList", "Master");
        }


        #endregion

        #region SkillCategory
        public ActionResult SkillCategory(string PK_SkillCategoryID)
        {
            Master model = new Master();
            if (PK_SkillCategoryID != null)
            {
                model.PK_SkillCategoryID = PK_SkillCategoryID;

                DataSet ds = model.SkillCategoryList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_SkillCategoryID = ds.Tables[0].Rows[0]["PK_SkillCategoryID"].ToString();
                    model.SkillCategoryCode = ds.Tables[0].Rows[0]["SkillCategoryCode"].ToString();
                    model.SkillCategoryName = ds.Tables[0].Rows[0]["SkillCategoryName"].ToString();
                    model.RemarkSkillCategory = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
            }
            return View(model);
        }
        

        [HttpPost]
        [ActionName("SkillCategory")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSkillCategory(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSkillCategory();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SkillCategorymsg"] = "Skill Category saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrSkillCategorymsg"] = ex.Message;
            }
            return RedirectToAction("SkillCategory", "Master");
        }


        public ActionResult SKillCategoryList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.SkillCategoryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_SkillCategoryID = r["PK_SkillCategoryID"].ToString();
                    obj.SkillCategoryCode = r["SkillCategoryCode"].ToString();
                    obj.SkillCategoryName = r["SkillCategoryName"].ToString();
                    obj.RemarkSkillCategory = r["Remark"].ToString();


                    lst.Add(obj);
                }
                model.listSkillCategory = lst;
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("SkillCategory")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSkillCategory(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSkillCategory();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SkillCategorymsg"] = "Skill Category Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrSkillCategorymsg"] = ex.Message;
            }
            return RedirectToAction("SkillCategoryList", "Master");
        }



        public ActionResult DeleteSkillCategory(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.DeleteSkillCategory();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SkillCategorymsg"] = "Skill category Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSkillCategorymsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrSkillCategorymsg"] = ex.Message;
            }
            return RedirectToAction("SkillCategoryList", "Master");
        }

        #endregion

        #region SalaryHead

        public ActionResult SalaryHead(string PK_SalaryHeadID)
        {

            Common obj = new Common();
            Master model = new Master();

            List<SelectListItem> ddlHeadNature = Common.BindHeadNature();
            ViewBag.ddlHeadNature = ddlHeadNature;


            List<SelectListItem> ddlAmtPer = Common.BindAmountPersentType();
            ViewBag.ddlAmtPer = ddlAmtPer;


            if (PK_SalaryHeadID != null)
            {
                model.PK_SalaryHeadID = PK_SalaryHeadID;

                DataSet ds = model.SalaryHeadList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_SalaryHeadID = ds.Tables[0].Rows[0]["PK_SalaryHeadID"].ToString();
                    model.SalaryHeadCode = ds.Tables[0].Rows[0]["SalaryHeadCode"].ToString();
                    model.SalaryHeadName = ds.Tables[0].Rows[0]["SalaryHeadName"].ToString();
                    model.HeadNature = ds.Tables[0].Rows[0]["HeadNature"].ToString();
                    model.AmountPersent = ds.Tables[0].Rows[0]["IsAmtPer"].ToString();
                    model.RemarkSalaryHead = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("SalaryHead")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult SaveSalaryHead(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSalaryHead();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SallaryHeadtmsg"] = "Salary Head  saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrSallaryHeadtmsg"] = ex.Message;
            }
            return RedirectToAction("SalaryHead", "Master");
        }


        public ActionResult SalaryHeadList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.SalaryHeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.AmountPersent = r["IsAmtPer"].ToString();
                    obj.RemarkSalaryHead = r["Remark"].ToString();

                    lst.Add(obj);
                }
                model.listSalaryHead = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SalaryHead")]
        [OnAction(ButtonName = "btnupdate")]
        public ActionResult UpdateSalaryHead(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSalaryHead();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SallaryHeadtmsg"] = "salary Head Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrSallaryHeadtmsg"] = ex.Message;
            }
            return RedirectToAction("SalaryHeadList", "Master");
        }

        public ActionResult DeleteSalaryHead(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.DeleteSalaryHead();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SallaryHeadtmsg"] = "Salary Head Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrSallaryHeadtmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrSallaryHeadtmsg"] = ex.Message;
            }
            return RedirectToAction("SalaryHeadList", "Master");
        }

        #endregion

        #region leavetype

        public ActionResult LeaveType(string Pk_LeaveTypeId)
        {
            Master model = new Master();
            if (Pk_LeaveTypeId != null)
            {
                model.Pk_LeaveTypeId = Pk_LeaveTypeId;

                DataSet ds = model.GetLeaveTypeDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_LeaveTypeId = ds.Tables[0].Rows[0]["PK_LeaveTypeID"].ToString();
                    model.LeaveType = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                    model.LeaveLimit = ds.Tables[0].Rows[0]["LeaveLimit"].ToString();
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("LeaveType")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveLeaveType(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SalveLeaveType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Leavetypemsg"] = "Leave type saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrLeavetypemsg"] = ex.Message;
            }
            return RedirectToAction("LeaveType", "Master");
        }


        public ActionResult LeaveTypeList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetLeaveTypeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_LeaveTypeId = r["PK_LeaveTypeID"].ToString();
                    obj.LeaveType = r["LeaveType"].ToString();
                    obj.LeaveLimit = r["LeaveLimit"].ToString();
                    obj.Remark = r["Remark"].ToString();

                    lst.Add(obj);
                }
                model.listLeaveType = lst;
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("LeaveType")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateLeaveType(Master obj)
        {
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateLeaveType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Leavetypemsg"] = "Leave Type Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrLeavetypemsg"] = ex.Message;
            }
            return RedirectToAction("LeaveTypeList", "Master");
        }


        public ActionResult DeleteLeaveType(Master obj)
        {
            try
            {
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.DeleteLeaveType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["LeaveTypemsg"] = "Leave Type Deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrLeavetypemsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrLeavetypemsg"] = ex.Message;
            }
            return RedirectToAction("LeaveTypeList", "Master");
        }

        #endregion

        #region Employee
        public ActionResult EmployeeBasicDetails(Master model, string EmployeeID, string Ac)
        {
            if (EmployeeID != null)
            {
                model.EmployeeID = EmployeeID;
                DataSet dsblock = model.DetailedEmployeeInfo();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    model.CompanyID = dsblock.Tables[0].Rows[0]["FK_CompanyID"].ToString();
                    model.DepartmentID = dsblock.Tables[0].Rows[0]["FK_DepartmentID"].ToString();

                    #region GetDesignation
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    DataSet ds = model.DesignationList();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["Pk_DesignationID"].ToString() });

                        }
                    }
                    ViewBag.ddlDesignation = ddlDesignation;
                    #endregion

                    model.DesignationID = dsblock.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.FatherName = dsblock.Tables[0].Rows[0]["FatherName"].ToString();

                    #region Gender
                    List<SelectListItem> Gender = Common.GenderList();
                    ViewBag.Gender = Gender;
                    #endregion Gender

                    model.Gender = dsblock.Tables[0].Rows[0]["Gender"].ToString();
                    model.PhoneNo = dsblock.Tables[0].Rows[0]["PhoneNo"].ToString();
                    model.Address2 = dsblock.Tables[0].Rows[0]["LocAddress"].ToString();
                    model.Pincode2 = dsblock.Tables[0].Rows[0]["LocPinCode"].ToString();
                    model.State2 = dsblock.Tables[0].Rows[0]["LocState"].ToString();
                    model.City2 = dsblock.Tables[0].Rows[0]["LocCity"].ToString();
                    model.PFNo = dsblock.Tables[0].Rows[0]["PFNumber"].ToString();
                    model.PAN = dsblock.Tables[0].Rows[0]["PAN"].ToString();
                    model.BankAccountNo = dsblock.Tables[0].Rows[0]["BankAccountNo"].ToString();
                    model.BankName = dsblock.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsblock.Tables[0].Rows[0]["BankBranchName"].ToString();
                    model.IFSCCOde = dsblock.Tables[0].Rows[0]["IFSCCOde"].ToString();
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
                }
            }

            else
            {
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                ViewBag.ddlDesignation = ddlDesignation;

                model.DateOfJoining = DateTime.Now.ToString("dd/MM/yyyy");

                #region Gender
                List<SelectListItem> Gender = Common.GenderList();
                ViewBag.Gender = Gender;
                #endregion Gender
            }
            model.Result = Ac;


            Session["tmpData"] = null;
            Session["tmpData1"] = null;



            #region CertificateSubmitted
            List<SelectListItem> CertificateSubmitted = Common.CertificateSubmittedList();
            ViewBag.CertificateSubmitted = CertificateSubmitted;
            #endregion CertificateSubmitted

            #region VerificationStatus
            List<SelectListItem> VerificationStatus = Common.VerificationStatusList();
            ViewBag.VerificationStatus = VerificationStatus;
            #endregion VerificationStatus

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

            return View(model);
        }

        public ActionResult UpdateEmployee(string EmployeeID)
        {
            Master model = new Master();
            if (EmployeeID != null)
            {
                model.EmployeeID = EmployeeID;
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
                        {
                            List<Master> lst = new List<Master>();
                            foreach (DataRow r in dsblock.Tables[1].Rows)
                            {
                                Master objqual = new Master();
                                objqual.QualificationID = r["PK_EmpQualifID"].ToString();
                                objqual.Board = r["Degree"].ToString();
                                objqual.DegreeCourse = r["Course"].ToString();
                                objqual.YearOfPassing = r["YearOfPassing"].ToString();
                                objqual.Status = "P";
                                lst.Add(objqual);
                            }
                            dsblock.Tables[1].Columns.Remove("PK_EmpQualifID");
                            dsblock.Tables[1].Columns.Remove("FK_EmpID");
                            dsblock.Tables[1].Columns["Degree"].ColumnName = "Board";
                            Session["tmpData"] = dsblock.Tables[1];
                            model.lstQualification = lst;
                        }
                        {
                            List<Master> lst1 = new List<Master>();
                            foreach (DataRow r in dsblock.Tables[2].Rows)
                            {
                                Master objwork = new Master();
                                objwork.WorkID = r["PK_EmpWorkExpID"].ToString();
                                objwork.PreviousCompany = r["PreviousCompany"].ToString();
                                objwork.FromDate = r["FromDate"].ToString();
                                objwork.ToDate = r["ToDate"].ToString();
                                objwork.Post = r["Post"].ToString();
                                objwork.CertificateSubmitted = r["CertificateSubmitted"].ToString();
                                objwork.Status = "P";
                                lst1.Add(objwork);
                            }
                            dsblock.Tables[2].Columns.Remove("PK_EmpWorkExpID");
                            dsblock.Tables[2].Columns.Remove("FK_EmpID");
                            dsblock.Tables[2].Columns["PreviousCompany"].ColumnName = "CompanyName";

                            model.lstWorkExperience = lst1;
                            Session["tmpData1"] = dsblock.Tables[2];
                        }

                    }

                }

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult saveQualification(string degree, string board, string yrofpass)
        {

            Master model = new Master();
            try
            {
                if (Session["tmpData"] != null)
                {
                    dtQual = (DataTable)Session["tmpData"];
                    DataRow dr = null;
                    //if (dtQual.Rows.Count > 0)
                    //{
                    dr = dtQual.NewRow();
                    dr["Course"] = degree;
                    dr["Board"] = board;
                    dr["YearOfPassing"] = yrofpass;
                    dr["Status"] = "T";
                    dtQual.Rows.Add(dr);
                    Session["tmpData"] = dtQual;
                    //}
                }
                else
                {
                    dtQual.Columns.Add("Course", typeof(string));
                    dtQual.Columns.Add("Board", typeof(string));
                    dtQual.Columns.Add("YearOfPassing", typeof(string));
                    dtQual.Columns.Add("Status", typeof(string));
                    DataRow dr = dtQual.NewRow();

                    dr["Course"] = degree;
                    dr["Board"] = board;
                    dr["YearOfPassing"] = yrofpass;
                    dr["Status"] = "T";

                    dtQual.Rows.Add(dr);
                    Session["tmpData"] = dtQual;

                }

                dtQual = (DataTable)Session["tmpData"];
                List<Master> lstTmpData = new List<Master>();
                if (dtQual != null && dtQual.Rows.Count > 0)
                {
                    foreach (DataRow r in dtQual.Rows)
                    {
                        Master obj = new Master();
                        obj.Board = r["Board"].ToString();
                        obj.DegreeCourse = r["Course"].ToString();
                        obj.YearOfPassing = r["YearOfPassing"].ToString();
                        obj.Status = r["Status"].ToString();
                        lstTmpData.Add(obj);
                    }
                    model.lstList = lstTmpData;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteQual(string rowid, string Qualid, string status)
        {
            Master model = new Master();
            try
            {
                DataTable dtQual = Session["tmpData"] as DataTable;
                dtQual.Rows.RemoveAt(int.Parse(rowid) - 1);
                dtQual.AcceptChanges();
                Session["tmpData"] = dtQual;

                if (status == "P")
                {
                    model.QualificationID = Qualid;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.DeleteQualification();

                }

                dtQual = (DataTable)Session["tmpData"];
                List<Master> lstTmpData = new List<Master>();
                if (dtQual != null && dtQual.Rows.Count > 0)
                {
                    foreach (DataRow r in dtQual.Rows)
                    {
                        Master obj = new Master();

                        obj.Board = r["Board"].ToString();
                        obj.DegreeCourse = r["Course"].ToString();
                        obj.YearOfPassing = r["YearOfPassing"].ToString();
                        obj.Status = r["Status"].ToString();
                        lstTmpData.Add(obj);
                    }
                    model.lstList = lstTmpData;
                }
            }
            catch (Exception ex)
            {

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult saveWorkExperience(string prevcomp, string fromdt, string todt, string post, string certsub)
        {
            Master model = new Master();
            try
            {
                if (Session["tmpData1"] != null)
                {
                    dtWorkExp = (DataTable)Session["tmpData1"];
                    DataRow dr = null;
                    //if (dtWorkExp.Rows.Count > 0)
                    //{
                    dr = dtWorkExp.NewRow();
                    dr["CompanyName"] = prevcomp;
                    dr["FromDate"] = fromdt;
                    dr["ToDate"] = todt;
                    dr["Post"] = post;
                    dr["CertificateSubmitted"] = certsub;
                    dr["Status"] = "T";
                    dtWorkExp.Rows.Add(dr);
                    Session["tmpData1"] = dtWorkExp;
                    //}
                }
                else
                {
                    dtWorkExp.Columns.Add("CompanyName", typeof(string));
                    dtWorkExp.Columns.Add("FromDate", typeof(string));
                    dtWorkExp.Columns.Add("ToDate", typeof(string));
                    dtWorkExp.Columns.Add("Post", typeof(string));
                    dtWorkExp.Columns.Add("CertificateSubmitted", typeof(string));
                    dtWorkExp.Columns.Add("Status", typeof(string));
                    DataRow dr = dtWorkExp.NewRow();

                    dr["CompanyName"] = prevcomp;
                    dr["FromDate"] = fromdt;
                    dr["ToDate"] = todt;
                    dr["Post"] = post;
                    dr["CertificateSubmitted"] = certsub;
                    dr["Status"] = "T";

                    dtWorkExp.Rows.Add(dr);
                    Session["tmpData1"] = dtWorkExp;

                }

                dtWorkExp = (DataTable)Session["tmpData1"];
                List<Master> lstTmpData = new List<Master>();
                if (dtWorkExp != null && dtWorkExp.Rows.Count > 0)
                {
                    foreach (DataRow r in dtWorkExp.Rows)
                    {
                        Master obj = new Master();
                        obj.PreviousCompany = r["CompanyName"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.Post = r["Post"].ToString();
                        obj.CertificateSubmitted = r["CertificateSubmitted"].ToString();
                        obj.Status = r["Status"].ToString();
                        lstTmpData.Add(obj);
                    }
                    model.lstList = lstTmpData;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteWork(string rowid, string Workid, string status)
        {
            Master model = new Master();
            try
            {
                DataTable dtWorkExp = Session["tmpData1"] as DataTable;
                dtWorkExp.Rows.RemoveAt(int.Parse(rowid) - 1);
                dtWorkExp.AcceptChanges();
                Session["tmpData1"] = dtWorkExp;

                if (status == "P")
                {
                    model.WorkID = Workid;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.DeleteWorkExperience();

                }
                dtWorkExp = (DataTable)Session["tmpData1"];
                List<Master> lstTmpData = new List<Master>();
                if (dtWorkExp != null && dtWorkExp.Rows.Count > 0)
                {
                    foreach (DataRow r in dtWorkExp.Rows)
                    {
                        Master obj = new Master();
                        obj.PreviousCompany = r["CompanyName"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.Post = r["Post"].ToString();
                        obj.CertificateSubmitted = r["CertificateSubmitted"].ToString();
                        obj.Status = r["Status"].ToString();
                        lstTmpData.Add(obj);
                    }
                    model.lstList = lstTmpData;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("EmployeeBasicDetails")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEmployeeDetails(HttpPostedFileBase postedFile, Master obj)
        {
            string FormName = "";
            string Controller = "";

            obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
            obj.DateOfJoining = string.IsNullOrEmpty(obj.DateOfJoining) ? null : Common.ConvertToSystemDate(obj.DateOfJoining, "dd/MM/yyyy");
            try
            {

                #region For Qualification
                string noofrows = "";
                if (Request["hdrows"] != null)
                { noofrows = Request["hdrows"].ToString(); }
                else
                {
                    noofrows = "0";
                }

                string degree = "";
                string board = "";
                string yrofpass = "";
                string noofrows1 = "";
                string stats = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("Board", typeof(string));
                dtst.Columns.Add("Course", typeof(string));
                dtst.Columns.Add("YearOfPassing", typeof(string));
                dtst.Columns.Add("Status", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    degree = Request["txtdrgree_" + i].ToString();
                    board = Request["txtboard_" + i].ToString();
                    yrofpass = Request["txtyear_" + i].ToString();
                    stats = Request["hdStatusQ_" + i].ToString();
                    dtst.Rows.Add(degree, board, yrofpass, stats);
                }
                #endregion  For Qualification

                #region For Work Experience
                if (Request["hdrows1"] != null)
                {
                    noofrows1 = Request["hdrows1"].ToString();
                }
                else
                {
                    noofrows1 = "0";
                }
                string prevcomp = "";
                string fromdt = "";
                string todt = "";
                string post = "";
                string certsub = "";
                string stats1 = "";
                DataTable dtst1 = new DataTable();
                dtst1.Columns.Add("CompanyName", typeof(string));
                dtst1.Columns.Add("FromDate ", typeof(string));
                dtst1.Columns.Add("ToDate ", typeof(string));
                dtst1.Columns.Add("Post", typeof(string));
                dtst1.Columns.Add("CertificateSubmitted  ", typeof(string));
                dtst1.Columns.Add("Status", typeof(string));

                for (int i = 0; i < int.Parse(noofrows1) - 1; i++)
                {
                    prevcomp = Request["txtprevcomp_ " + i].ToString();
                    fromdt = string.IsNullOrEmpty(Request["fromdt_ " + i].ToString()) ? null : Common.ConvertToSystemDate(Request["fromdt_ " + i].ToString(), "dd/MM/yyyy");
                    todt = string.IsNullOrEmpty(Request["txttodt_ " + i].ToString()) ? null : Common.ConvertToSystemDate(Request["txttodt_ " + i].ToString(), "dd/MM/yyyy");
                    post = Request["txtpost_ " + i].ToString();
                    certsub = Request["txtcertsub_ " + i].ToString();
                    stats1 = Request["hdStatusW_" + i].ToString();
                    dtst1.Rows.Add(prevcomp, fromdt, todt, post, certsub, stats1);
                }

                #endregion For Work Experience

                obj.ProfilePic = "../images/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtst;
                obj.dtWorkExp = dtst1;
                DataSet ds = obj.SaveEmployeeBasicDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        if (postedFile != null)
                        {
                            obj.ProfilePic = "../images/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                            postedFile.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                        }
                        //TempData["Emp"] = "Employee Saved successfully.Employee Code is " + ds.Tables[0].Rows[0]["EmpCode"].ToString() + " ,LoginId is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is " + ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["EmpCode"] = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();

                        FormName = "ConfirmationEmployeeBasicDetails";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Emp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EmployeeBasicDetails";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Emp"] = ex.Message;
                FormName = "EmployeeBasicDetails";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("EmployeeBasicDetails")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateEmployeeDetails(HttpPostedFileBase postedFile, Master obj)
        {
            string FormName = "";
            string Controller = "";

            obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
            obj.DateOfJoining = string.IsNullOrEmpty(obj.DateOfJoining) ? null : Common.ConvertToSystemDate(obj.DateOfJoining, "dd/MM/yyyy");
            // obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            //  obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
            try
            {

                #region For Qualification
                string noofrows = "";
                if (Request["hdrows"] != null)
                { noofrows = Request["hdrows"].ToString(); }
                else
                {
                    noofrows = "0";
                }

                string degree = "";
                string board = "";
                string yrofpass = "";
                string noofrows1 = "";
                string stats = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("Board", typeof(string));
                dtst.Columns.Add("Course", typeof(string));
                dtst.Columns.Add("YearOfPassing", typeof(string));
                dtst.Columns.Add("Status", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    degree = Request["txtdrgree_" + i].ToString();
                    board = Request["txtboard_" + i].ToString();
                    yrofpass = Request["txtyear_" + i].ToString();
                    stats = Request["hdStatusQ_" + i].ToString();
                    dtst.Rows.Add(degree, board, yrofpass, stats);
                }
                Session["tmpData"] = dtst;
                #endregion  For Qualification

                #region For Work Experience
                if (Request["hdrows1"] != null)
                {
                    noofrows1 = Request["hdrows1"].ToString();
                }
                else
                {
                    noofrows1 = "0";
                }
                string prevcomp = "";
                string fromdt = "";
                string todt = "";
                string post = "";
                string certsub = "";
                string stats1 = "";
                DataTable dtst1 = new DataTable();
                dtst1.Columns.Add("CompanyName", typeof(string));
                dtst1.Columns.Add("FromDate ", typeof(string));
                dtst1.Columns.Add("ToDate ", typeof(string));
                dtst1.Columns.Add("Post", typeof(string));
                dtst1.Columns.Add("CertificateSubmitted  ", typeof(string));
                dtst1.Columns.Add("Status", typeof(string));

                for (int i = 0; i < int.Parse(noofrows1) - 1; i++)
                {
                    prevcomp = Request["txtprevcomp_ " + i].ToString();
                    fromdt = string.IsNullOrEmpty(Request["fromdt_ " + i].ToString()) ? null : Common.ConvertToSystemDate(Request["fromdt_ " + i].ToString(), "dd/MM/yyyy");
                    todt = string.IsNullOrEmpty(Request["txttodt_ " + i].ToString()) ? null : Common.ConvertToSystemDate(Request["txttodt_ " + i].ToString(), "dd/MM/yyyy");
                    post = Request["txtpost_ " + i].ToString();
                    certsub = Request["txtcertsub_ " + i].ToString();
                    stats1 = Request["hdStatusW_" + i].ToString();
                    dtst1.Rows.Add(prevcomp, fromdt, todt, post, certsub, stats1);
                }
                Session["tmpData1"] = dtst1;
                #endregion For Work Experience


                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtst;
                obj.dtWorkExp = dtst1;
                DataSet ds = obj.UpdateEmployeeDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        if (postedFile != null)
                        {
                            obj.ProfilePic = "../images/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                            postedFile.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                        }
                        TempData["Emp"] = "Employee Updated successfully ";

                    }
                    else
                    {
                        TempData["ErrEmp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrEmp"] = ex.Message;
            }
            FormName = "EmployeeBasicDetails";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult ConfirmationEmployeeBasicDetails()
        {
            return View();
        }




        public ActionResult EditEmployeeBasicInfo(Master model)
        {
            List<Master> lst = new List<Master>();
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

        [HttpPost]
        [ActionName("EditEmployeeBasicInfo")]
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

        public ActionResult DeleteEmployee(string id)
        {
            try
            {
                Master obj = new Master();
                obj.EmployeeID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["Emp"] = "Employee deleted successfully";
                    }
                    else
                    {
                        TempData["ErrEmp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrEmp"] = ex.Message;
            }
            return RedirectToAction("EditEmployeeBasicInfo", "Master");
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
        #endregion

        #region EmpSalary
        public ActionResult EmployeeSalary(Master model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeSalary")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeSalaryBy(Master model)
        {
            DataSet ds = model.EmployeeList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.EmployeeID = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                model.EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                model.Contact = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                model.EmployeeCode = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                model.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
            }
            List<Master> lst = new List<Master>();
            model.HeadNature = "Earning";
            DataSet ds0 = model.SalaryHeadList();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();

                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            List<Master> lst1 = new List<Master>();
            model.HeadNature = "Deduction";
            DataSet ds1 = model.SalaryHeadList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();

                    lst1.Add(obj);
                }
                model.lstList1 = lst1;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeSalary")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSalary(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdrows"].ToString();
                string headid = "";
                string amt = "";

                DataTable dtEarning = new DataTable();
                dtEarning.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtEarning.Columns.Add("Amount", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    headid = Request["ESalHeadId_ " + i].ToString();
                    amt = Request["txtEarAmt " + i].ToString() == "" ? "0" : Request["txtEarAmt " + i].ToString();

                    dtEarning.Rows.Add(headid, amt);
                }

                //=================
                string noofrows1 = Request["hdrows1"].ToString();
                string headid1 = "";
                string amt1 = "";

                DataTable dtDeduction = new DataTable();
                dtDeduction.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtDeduction.Columns.Add("Amount", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows1) - 1; i++)
                {
                    headid1 = Request["DSalHeadId_ " + i].ToString();
                    amt1 = Request["txtDedAmt " + i].ToString() == "" ? "0" : Request["txtDedAmt " + i].ToString();


                    dtDeduction.Rows.Add(headid1, amt1);
                }

                obj.EmployeeID = Request["EmployeeID1"].ToString();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtEarning;
                obj.dtWorkExp = dtDeduction;
                DataSet ds = obj.SaveEmployeeSalaryDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmpSal"] = " Employee Salary Saved successfully !";

                    }
                    else
                    {
                        TempData["ErrEmpSal"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrEmpSal"] = ex.Message;
            }
            FormName = "EmployeeSalary";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult EmployeeSalaryList(string id)
        {
            Master model = new Master();
            model.EmployeeID = id;
            DataSet ds = model.EmployeeSalary();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.EmployeeID = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                model.EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                model.Contact = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                model.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
            }
            List<Master> lst = new List<Master>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Master obj = new Master();
                    obj.SalaryHeadID = r["FK_HeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();
                    obj.SalaryAmount = r["Amount"].ToString();
                    obj.Value = r["VALUE"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            List<Master> lst1 = new List<Master>();
            //model.HeadNature = "Deduction";
            //  DataSet ds1 = model.SalaryHeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Master obj = new Master();
                    obj.SalaryHeadID = r["FK_HeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();
                    obj.SalaryAmount = r["Amount"].ToString();
                    obj.Value = r["VALUE"].ToString();
                    lst1.Add(obj);
                }
                model.lstList1 = lst1;
            }

            return View(model);
        }


        [HttpPost]
        [ActionName("EmployeeSalaryList")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateSalary(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdrows"].ToString();
                string headid = "";
                string amt = "";

                DataTable dtEarning = new DataTable();
                dtEarning.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtEarning.Columns.Add("Amount", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    headid = Request["ESalHeadId_ " + i].ToString();
                    amt = Request["txtEarAmt " + i].ToString() == "" ? "0" : Request["txtEarAmt " + i].ToString();

                    dtEarning.Rows.Add(headid, amt);
                }

                //=================
                string noofrows1 = Request["hdrows1"].ToString();
                string headid1 = "";
                string amt1 = "";


                DataTable dtDeduction = new DataTable();
                dtDeduction.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtDeduction.Columns.Add("Amount", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows1) - 1; i++)
                {
                    headid1 = Request["DSalHeadId_ " + i].ToString();
                    amt1 = Request["txtDedAmt " + i].ToString() == "" ? "0" : Request["txtDedAmt " + i].ToString();


                    dtDeduction.Rows.Add(headid1, amt1);
                }

                obj.EmployeeID = Request["EmployeeID1"].ToString();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtEarning;
                obj.dtWorkExp = dtDeduction;
                DataSet ds = obj.UpdateSalary();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmpSal"] = " Employee Salary Updated successfully !";

                    }
                    else
                    {
                        TempData["ErrEmpSal"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrEmpSal"] = ex.Message;
            }
            FormName = "EditEmployeeBasicInfo";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region EmployeeLeave
        public ActionResult EmployeeLeave(Master model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeLeave")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeLeaveBy(Master model)
        {
            List<Master> lst1 = new List<Master>();
            DataSet ds = model.EmployeeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj1 = new Master();
                    obj1.EmployeeID = r["PK_EmployeeID"].ToString();
                    obj1.EmployeeName = r["EmployeeName"].ToString();
                    obj1.FatherName = r["FatherName"].ToString();
                    obj1.Contact = r["MobileNo"].ToString();
                    obj1.EmployeeCode = r["EmployeeCode"].ToString();
                    obj1.LoginID = r["LoginID"].ToString();
                    lst1.Add(obj1);
                }
                model.lstListEmployee = lst1;

                model.EmployeeID = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                model.EmployeeName = "";
                //model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                //model.Contact = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                model.EmployeeCode = "";
                //model.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                //    model.EmployeeID = Session["EmpID"].ToString();
            }

            List<Master> lst = new List<Master>();

            DataSet ds1 = model.LeaveTypeList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LeaveID = r["PK_LeaveTypeID"].ToString();
                    obj.LeaveName = r["LeaveType"].ToString();
                    obj.LeaveLimit = r["LeaveLimit"].ToString();

                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeLeave")]
        [OnAction(ButtonName = "Save")]
        public ActionResult AddEmployeeLeave(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.EmployeeID = Request["EmployeeID1"].ToString();
                DataSet ds = obj.AddEmployeeLeave();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmployeeLeave"] = "Employee Leave saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrEmployeeLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrEmployeeLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrEmployeeLeave"] = ex.Message;
            }
            return RedirectToAction("EmployeeLeave", "Master");
        }
        public ActionResult FilterEmployeeCodeLeave(string EmployeeCode)
        {
            try
            {
                Master model = new Master();
                model.EmployeeCode = EmployeeCode == "" ? null : EmployeeCode;
                
                DataSet ds = model.GetFilterEmployeeLeave();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "yes";
                        model.EmployeeCode = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                        model.EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                    }
                    else
                    {
                        model.Result = "no";
                        model.EmployeeCode = model.EmployeeName = "";
                    }  
                    
                }
                else
                {
                    model.EmployeeCode = model.EmployeeName = "";
                    model.Result = "no";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion
        public ActionResult FilterEmployeeNameLeave(string EmployeeName)
        {
            try
            {
                Master model = new Master();
                model.EmployeeName = EmployeeName == "" ? null : EmployeeName;

                DataSet ds = model.GetFilterEmployeeLeave();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "yes";
                        model.EmployeeCode = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                        model.EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                    }
                    else
                    {
                        model.Result = "no";
                        model.EmployeeCode = model.EmployeeName = "";
                    }

                }
                else
                {
                    model.EmployeeCode = model.EmployeeName = "";
                    model.Result = "no";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #region HolidayMaster
        public ActionResult HolidayMaster(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.HolidayList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.HolidayID = r["PK_HolidayID"].ToString();
                    obj.HolidayName = r["HolidayName"].ToString();
                    obj.HolidayDate = r["HolidayDate"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("HolidayMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveHoliday(Master obj)
        {
            try
            {
                obj.HolidayDate = string.IsNullOrEmpty(obj.HolidayDate) ? null : Common.ConvertToSystemDate(obj.HolidayDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveHoliday();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Holiday"] = "Holiday saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrHoliday"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ErrHoliday"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrHoliday"] = ex.Message;
            }
            return RedirectToAction("HolidayMaster", "Master");
        }
        #endregion

    }
}