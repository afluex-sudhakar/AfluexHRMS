using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexHRMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Employee"))
                        {
                            Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                            Session["PK_EmployeeID"] = ds.Tables[0].Rows[0]["PK_EmployeeID"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            FormName = "EmployeeDashboard";
                            Controller = "EmployeeLogin";

                            if (ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                Session["UserNFCCode"] = Crypto.Encrypt(ds.Tables[1].Rows[0]["Code"].ToString());
                            }
                            else
                            {
                                Session["UserNFCCode"] = "na";
                            }
                            //NFC Activation Method
                            if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
                            {
                                return RedirectToAction("NFCActivation");
                            }

                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            if (ds.Tables[0].Rows[0]["UserTypeName"].ToString() == "Admin")
                            {
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                            else
                            {
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                        }
                        else
                        {
                            TempData["Login"] = "Oops! Something went wrong, please try again later";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Oops! Something went wrong, please try again later";
                        FormName = "Login";
                        Controller = "Home";
                    }
                }

                else
                {
                    TempData["Login"] = "Oops! Something went wrong, please try again later";
                    FormName = "Login";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = "Oops! Something went wrong, please try again later";
                FormName = "Login";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult NFCActivation(string C)
        {
            NFCProfileModel model = new NFCProfileModel();
            Home Modal = new Home();
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("Login", "home");
            }
            if (C != null && C != "")
            {
                Session["NFCCode"] = C;
                Session["NFCActivated"] = "false";
            }
            if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
            {

                Modal.Code = Session["NFCCode"].ToString();
                model.DecryptedCode = Crypto.DecryptNFC(Session["NFCCode"].ToString());
                DataSet ds = Modal.GetNFCAllotmentStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.IsAlloted = "false";
                }
                else
                {
                    //Assign false for remove Coupon Code functionality
                    ViewBag.IsAlloted = "false";
                }
            }
            else if (Session["UserNFCCode"].ToString() != "" && Session["UserNFCCode"].ToString() != "na")
            {
                return RedirectToAction("Profile", "NFC", new { id = Session["UserNFCCode"].ToString() });
            }
            else if (Session["UserNFCCode"].ToString() == "na")
            {

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ActivateNFC(string NFCCode, string ActivationCode, string DistributorCode)
        {
            NFCModel obj1 = new NFCModel();
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["UserNFCCode"].ToString() == "na")
            {
                obj1.Code = NFCCode;
                DataSet ds = obj1.CheckNFCCode();
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["IsActivated"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"].ToString()) == false)
                        {
                            //Session["NFCCode"] = null;
                            if (Session["NFCCode"] == null)
                            {
                                Session["NFCCode"] = Crypto.Encrypt(NFCCode);
                                Session["NFCActivated"] = "false";
                                ActivationCode = "ManualPurchase";
                            }
                        }
                    }
                }
            }
            if (Session["LoginId"] != null)
            {
                //NFC Activation Method
                if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
                {
                    var desc = Crypto.DecryptNFC(Session["NFCCode"].ToString());
                    obj1.Code = desc;
                    if (obj1.Code != NFCCode)
                    {
                        return Json("Invalid NFC Code", JsonRequestBehavior.AllowGet);
                    }

                    obj1.LoginId = Session["LoginId"].ToString();
                    DataSet ds1 = obj1.ActivateNFCCode(ActivationCode, DistributorCode);
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            return Json(ds1.Tables[0].Rows[0]["ErrorMessage"].ToString(), JsonRequestBehavior.AllowGet);
                        }
                        if (ds1.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            obj1.Email = ds1.Tables[0].Rows[0]["Email"].ToString();
                            //NFC Activated
                            Session["NFCActivated"] = "true";
                            Session["UserNFCCode"] = Session["NFCCode"].ToString();
                            if (Session["Name"] != null)
                            {
                                try
                                {
                                    //string msg = "Dear " + Session["FullName"].ToString() + " you have successfully activated your DOST Business Card, You can now start managing your Information on card " + Session["FullName"].ToString() + " Be Limitless. Team -DOST INC";
                                    //BLSMS.sendSMSUpdated(msg, obj1.LoginId);
                                }
                                catch (Exception ex)
                                {

                                }
                                //send mail
                                try
                                {
                                    string Subject = "Afluex HRMS Card Activation";
                                    //string mail = "Welcome to the DOST network, your DOST business card is successfully activated now your business and personal profile is in your full control, share whatever you wish to, whoever you wish to & switch off any time.<br><br>Manage your profile <a href='https://dost.click/'>Click Here</a><br><br>For any issue please reach us through email or through DOST helpline.";
                                    //BLMail.SendNFCActivationMail(Session["FullName"].ToString(), mail, Subject, obj1.Email);
                                }
                                catch (Exception ex)
                                {

                                }
                                return Json("NFC Code Activated successfully", JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("Error occurred", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (ds1.Tables[0].Rows[0][0].ToString() == "5")
                        {
                            return Json("Invalid Activation Code", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //NFC Code Activation Error  
                            return Json("NFC Code Activation Failed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //NFC Code Activation Error
                        return Json("NFC Code Activation Failed", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("NFC Code Activation Failed", JsonRequestBehavior.AllowGet);
                }
                //End
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            //return RedirectToAction(FormName,Controller);
            //return View();
        }
    }
}