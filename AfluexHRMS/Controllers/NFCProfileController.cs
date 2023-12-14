﻿using AfluexHRMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AfluexHRMS.Models.NFCProfileModel;

namespace AfluexHRMS.Controllers
{
    public class NFCProfileController : UserBaseController
    {
        // GET: NFCProfile
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult NFCDevice()
        {
            Master model = new Master();
            try
            {
                List<Master> lst = new List<Master>();
                model.Fk_MainServiceTypeId = "3";
                model.Fk_UserId = Session["PK_EmployeeID"].ToString();
                DataSet ds = model.GetService();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.Color = r["ColorCode"].ToString();
                        obj.EncCode = r["ColorCode"].ToString();
                        obj.Pk_ServiceId = r["Pk_ServiceId"].ToString();
                        obj.Fk_MainServiceTypeId = r["Fk_MainServiceTypeId"].ToString();
                        obj.ServiceIcon = r["ServiceIcon"].ToString();
                        obj.Service = r["Service"].ToString();
                        obj.ServiceUrl = r["ServiceUrl"].ToString();
                        obj.Category = r["Category"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.IsActiveDeactiveDate = r["IsActiveDeactiveDate"].ToString();
                        lst.Add(obj);
                        i++;
                    }
                    model.lst = lst;
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    Session["NFCCode"] = ds.Tables[1].Rows[0]["EncCode"].ToString();
                    if (ds.Tables[1].Rows[0]["IsActivated"].ToString() == "False")
                    {
                        Session["NFCActivated"] = "false";
                    }
                }
            }
            catch (Exception ex)
            {
            
            }
            return View(model);
        }

        public ActionResult SetActionDashboard()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            model.Fk_UserId = Session["PK_EmployeeID"].ToString();
            DataSet ds = model.GetActivatedNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Service = r["Service"].ToString();
                    obj.Color = r["ColorCode"].ToString();
                    obj.EncCode = r["EncCode"].ToString();
                    obj.ServiceIcon = r["ServiceIcon"].ToString();
                    obj.Pk_ServiceId = r["PK_ServiceId"].ToString();
                    obj.PK_NFcId = Convert.ToInt32(r["PK_NFCId"]);
                    obj.IsActive = r["IsActivated"].ToString();
                    obj.DecCode = r["Code"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }


        public ActionResult ProfileUpdate(string EncCode)
        {
            //string url = "https://dost.click/NFC/Profile?id=H8eoDq2dRNnw2LmzMaZaJQ==";
            //MyURLShortener u = new MyURLShortener();
            //string str = u.MyURLShorten(url);
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
            NFCProfileModel model = new NFCProfileModel();
            //model.Code = desc;
            model.PK_UserId = Session["PK_EmployeeID"].ToString();
            List<NFCProfileModel> lstUerProfile = new List<NFCProfileModel>();
            #region ddlEmailList
            int count = 0;
            List<SelectListItem> ddlEmail = new List<SelectListItem>();
            DataSet ds1 = model.GetmailList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlEmail.Add(new SelectListItem { Text = "Select Email", Value = "0" });
                    }
                    ddlEmail.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlEmail = ddlEmail;
            #endregion
            #region Mobile
            int count1 = 0;
            List<SelectListItem> Mobile = new List<SelectListItem>();
            DataSet ds2 = model.GetContactList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        Mobile.Add(new SelectListItem { Text = "Select Mobile No.", Value = "0" });
                    }
                    Mobile.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.Mobile = Mobile;
            if (EncCode != null && EncCode != "")
            {
                model.Code = EncCode;
            }
            else if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "")
            {
                model.Code = Session["NFCCode"].ToString();
            }
            else if (Session["UserNFCCode"].ToString() != "na")
            {
                model.Code = Session["UserNFCCode"].ToString();
            }
            else
            {
            }
            #endregion
            #region SocialMedia
            List<SelectListItem> SocialMedia = new List<SelectListItem>();
            DataSet ds3 = model.GetSocialMediaList();
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        SocialMedia.Add(new SelectListItem { Text = "Select Social Media", Value = "0" });
                    }
                    SocialMedia.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.SocialMedia = SocialMedia;
            #endregion SocialMedia
            #region WebLink
            List<SelectListItem> WebLink = new List<SelectListItem>();
            DataSet ds4 = model.GetWebLinkList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        WebLink.Add(new SelectListItem { Text = "Select Web Link", Value = "0" });
                    }
                    WebLink.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.WebLink = WebLink;
            #endregion WebLink
            #region ddlWhatsappList
            int count6 = 0;
            List<SelectListItem> ddlWhatsapp = new List<SelectListItem>();
            DataSet dsWh = model.GetWhatsappList();
            if (dsWh != null && dsWh.Tables.Count > 0 && dsWh.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsWh.Tables[0].Rows)
                {
                    if (count6 == 0)
                    {
                        ddlWhatsapp.Add(new SelectListItem { Text = "Select Whatsapp No.", Value = "0" });
                    }
                    ddlWhatsapp.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count6 = count6 + 1;
                }
            }
            ViewBag.ddlWhatsapp = ddlWhatsapp;
            #endregion
            #region ddlLocationList
            int count7 = 0;
            List<SelectListItem> ddlLocation = new List<SelectListItem>();
            DataSet dsL = model.GetLocationList();
            if (dsL != null && dsL.Tables.Count > 0 && dsL.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsL.Tables[0].Rows)
                {
                    if (count7 == 0)
                    {
                        ddlLocation.Add(new SelectListItem { Text = "Select Location", Value = "0" });
                    }
                    ddlLocation.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count7 = count7 + 1;
                }
            }
            ViewBag.ddlLocation = ddlLocation;
            #endregion

            string[] colorContact = { "#FF4C41", "#68CF29", "#51A6F5", "#eb8153", "#FFAB2D" };
            string[] colorRedirection = { "#eb8153", "#6418C3", "#FF4C90", "#68CF90", "#90A6F9", "#FFAB8D" };
            var i = 0;
            model.DecryptedCode = Crypto.Decrypt(EncCode);
            DataSet ds = model.GetNFCProfileData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.QRImage = ds.Tables[1].Rows[0]["QrImage"].ToString();
                model.IsProfileTurnedOff = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProfileTurnedOff"]);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NFCProfileModel obj = new NFCProfileModel();
                    obj.PK_ProfileId = r["PK_ProfileId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.BusinessName = r["BusinessName"].ToString();
                    obj.Designation = r["Designation"].ToString();
                    obj.Description = r["Description"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ProfileType = r["ProfileType"].ToString();

                    if (obj.Status == "Active")
                    {
                        model.PK_ProfileId = obj.PK_ProfileId;
                        ViewBag.img = obj.ProfilePic = r["ProfilePic"].ToString();

                        model.Leg = obj.Leg;
                    }
                    obj.CardImage = r["CardImage"].ToString();
                    obj.ProfileName = r["ProfileName"].ToString();
                    obj.ActiveStatus = r["IsChecked"].ToString();
                    if (obj.ProfileType == "Redirection")
                    {
                        obj.ColorCodeRedirection = colorRedirection[i];
                    }
                    else
                    {
                        obj.ColorCodeContact = colorContact[i];
                    }
                    lstUerProfile.Add(obj);
                }
                model.lst = lstUerProfile;
            }
            NFCModel objn = new NFCModel();

            objn.Code = Crypto.Decrypt(model.Code);
            DataSet dsProfile = objn.GetNFCProfileData();
            //if (dsProfile != null && dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
            //{
            //    if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["IsProfileTurnedOff"]) == true)
            //    {

            //    }
            //    else
            //    {
            //        model.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
            //        model.Email = ds1.Tables[0].Rows[0]["PrimaryEmail"].ToString();
            //    }
            //}
            if (dsProfile != null && dsProfile.Tables.Count > 1)
            {
                if (dsProfile.Tables[0].Rows.Count > 0)
                {
                    model.enc = Common.EnryptString(dsProfile.Tables[0].Rows[0]["PK_NFCId"].ToString());
                }
                //    ViewBag.ProfilePic = dsProfile.Tables[1].Rows[0]["ProfilePic"].ToString();
                if (dsProfile.Tables[1].Rows.Count > 0)
                {
                    List<NFCContent> NfcContentList = new List<NFCContent>();

                    foreach (DataRow row in dsProfile.Tables[1].Rows)
                    {
                        if (row["Type"].ToString() == "Email")
                        {
                            model.Email = row["Content"].ToString();
                        }
                        NfcContentList.Add(new NFCContent()
                        {
                            Content = row["Content"].ToString(),
                            Type = row["Type"].ToString(),
                            IsWhatsApp = row["IsWhatsapp"].ToString()
                        });
                    }
                    model.NfcContentList = NfcContentList;
                }
            }
            //var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", "Mona",  500, 500);
            //WebResponse response = default(WebResponse);
            //Stream remoteStream = default(Stream);
            //StreamReader readStream = default(StreamReader);
            //WebRequest request = WebRequest.Create(url);
            //response = request.GetResponse();
            //remoteStream = response.GetResponseStream();
            //readStream = new StreamReader(remoteStream);
            //Image img = System.Drawing.Image.FromStream(remoteStream);
            //img.Save("/KYCDocuments/" + Guid.NewGuid() +"Mona.png");
            //response.Close();
            //remoteStream.Close();
            //readStream.Close();
            //if (Session["UserNFCCode"] != null)
            //{
            //    if (Session["UserNFCCode"].ToString() != "na")
            //    {
            //        GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter.CreateQrCode("https://dost.click/NFC/Profile?id=" + Session["UserNFCCode"].ToString());
            //        Qrcode.SaveAsPng("QrCode.png");
            //    }
            //}
            //if (Session["NFCCode"] != null)
            //{
            //    GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter.CreateQrCode("https://dost.click/NFC/Profile?id=" + (Session["NFCCode"].ToString()));
            //    Qrcode.SaveAsPng("QrCode.png");
            //}
            //GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode("https://ironsoftware.com/csharp/barcode/docs/", BarcodeEncoding.Code128);
            //var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo("https://ironsoftware.com/csharp/barcode/", "visual-studio-logo.png", 500);
            //MyQRWithLogo.ChangeBarCodeColor(System.Drawing.Color.DarkGreen);
            ////Save as PDF
            //MyQRWithLogo.SaveAsPdf("MyQRWithLogo.pdf");
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfileUpdateAction(NFCProfileModel model)
        {
            try
            {
                #region ddlgender
                List<SelectListItem> ddlgender = Common.BindGender();
                ViewBag.ddlgender = ddlgender;
                #endregion ddlgender
                model.PK_UserId = Session["PK_EmployeeID"].ToString();

                if (model.IsRedirectWeb == true)
                {
                    model.IsRedirect = true;
                }
                if (model.IsRedirectSocial == true)
                {
                    model.IsRedirect = true;
                }
                if (model.IsIncludedContact == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedEmail == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedWeb == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedSocial == true)
                {
                    model.IsIncluded = true;
                }
                if (model.Leg == "undefined")
                {
                    model.Leg = null;
                }
                if (model.PK_ProfileId != null && model.Flag == null)
                {
                    UpdateBusinessProfileForMobile para = new UpdateBusinessProfileForMobile();
                    int FK_NFCProfileId = 0;
                    int IsIncluded = 0;
                    int IsPrimary = 0;
                    DataTable dtcontact = new DataTable();
                    dtcontact.Columns.Add("FK_NFCProfileId");
                    dtcontact.Columns.Add("IsIncluded");
                    dtcontact.Columns.Add("IsPrimary");
                    if (model.ContactList != null)
                    {
                        model.ContactList = model.ContactList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.ContactList.Length; i++)
                        {
                            if (model.ContactList[i] != "")
                            {
                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.ContactList[i]);
                                IsIncluded = 1;
                                dtcontact.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }
                        }
                    }
                    DataTable dtEmail = new DataTable();
                    dtEmail.Columns.Add("FK_NFCProfileId");
                    dtEmail.Columns.Add("IsIncluded");
                    dtEmail.Columns.Add("IsPrimary");
                    if (model.EmailList != null)
                    {
                        model.EmailList = model.EmailList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.EmailList.Length; i++)
                        {
                            if (model.EmailList[i] != "")
                            {
                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.EmailList[i]);
                                IsIncluded = 1;
                                dtEmail.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }
                        }
                    }
                    DataTable dtWebLink = new DataTable();
                    dtWebLink.Columns.Add("FK_NFCProfileId");
                    dtWebLink.Columns.Add("IsIncluded");
                    dtWebLink.Columns.Add("IsPrimary");
                    if (model.WebLinkList != null)
                    {
                        model.WebLinkList = model.WebLinkList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.WebLinkList.Length; i++)
                        {
                            if (model.WebLinkList[i] != "")
                            {
                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.WebLinkList[i]);
                                IsIncluded = 1;
                                dtWebLink.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }
                        }
                    }
                    DataTable dtSocialMedia = new DataTable();
                    dtSocialMedia.Columns.Add("FK_NFCProfileId");
                    dtSocialMedia.Columns.Add("IsIncluded");
                    dtSocialMedia.Columns.Add("IsPrimary");
                    if (model.SocialLink != null)
                    {
                        model.SocialLink = model.SocialLink[0].Split(',');
                        int i = 0;

                        for (i = 0; i < model.SocialLink.Length; i++)
                        {
                            if (model.SocialLink[i] != "")
                            {
                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.SocialLink[i]);
                                IsIncluded = 1;
                                dtSocialMedia.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }

                        }
                    }
                    DataTable dtWhatsapp = new DataTable();
                    dtWhatsapp.Columns.Add("FK_NFCProfileId");
                    dtWhatsapp.Columns.Add("IsIncluded");
                    dtWhatsapp.Columns.Add("IsPrimary");
                    if (model.WhatsappList != null)
                    {
                        model.WhatsappList = model.WhatsappList[0].Split(',');
                        int i = 0;

                        for (i = 0; i < model.WhatsappList.Length; i++)
                        {
                            if (model.WhatsappList[i] != "")
                            {

                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.WhatsappList[i]);
                                IsIncluded = 1;
                                dtWhatsapp.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }

                        }
                    }
                    DataTable dtLocation = new DataTable();
                    dtLocation.Columns.Add("FK_NFCProfileId");
                    dtLocation.Columns.Add("IsIncluded");
                    dtLocation.Columns.Add("IsPrimary");
                    if (model.LocationList != null)
                    {
                        model.LocationList = model.LocationList[0].Split(',');
                        int i = 0;

                        for (i = 0; i < model.LocationList.Length; i++)
                        {
                            if (model.LocationList[i] != "")
                            {
                                IsPrimary = 0;
                                FK_NFCProfileId = Convert.ToInt32(model.LocationList[i]);
                                IsIncluded = 1;
                                dtLocation.Rows.Add(FK_NFCProfileId, IsIncluded, IsPrimary);
                            }
                        }
                    }
                    model.dtcontact = dtcontact;
                    model.dtemail = dtEmail;
                    model.dtweblink = dtWebLink;
                    model.dtsocial = dtSocialMedia;
                    model.dtwhatsapp = dtWhatsapp;
                    model.dtlocation = dtLocation;
                    model.FK_UserId = Session["Pk_userId"].ToString();
                    DataSet ds = model.UpdateProfileInfo();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            model.Result = "Success";
                            model.Message = "Profile Updated Successfully";
                        }
                        else
                        {
                            model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                if (model.Flag != null && model.Flag != "")
                {
                    if (model.Flag == "Contact")
                    {
                        TempData["FlagResponse"] = "Contact";
                        model.Type = "ContactNo";
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Contact No updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Result = "Success";
                                model.Message = "Profile updated successfully";
                            }
                        }
                        else
                        {

                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Contact No saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Contact No not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Email")
                    {
                        TempData["FlagResponse"] = "Email";
                        model.Type = "Email";
                        model.Content = model.ContentEmail;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Email updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Email not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.ContentEmail;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Email saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Profile not updated successfully";
                            }
                        }
                    }
                    if (model.Flag == "WebLink")
                    {
                        TempData["FlagResponse"] = "WebLink";
                        model.Type = "WebLink";
                        model.Content = model.WebLink + model.ContentWebLinks;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "WebLink updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "WebLink not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.WebLink + model.ContentWebLinks;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "WebLink saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "WebLink not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "SocialMedia")
                    {
                        TempData["FlagResponse"] = "SocialMedia";
                        model.Type = "SocialMedia";
                        model.Content = model.SocialMedia + model.ContentSocialMedia;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Social Media Link updated Successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Social Media Link not updated Successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.SocialMedia + model.ContentSocialMedia;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Social Media Link saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {

                                model.Message = "Social Media Link not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Location")
                    {
                        TempData["FlagResponse"] = "Location";
                        model.Type = "Location";
                        model.Content = model.ContentLocation;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Location updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Location not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.ContentLocation;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Location saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Location not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Whatsapp")
                    {
                        TempData["FlagResponse"] = "Whatsapp";
                        model.Type = "Whatsapp";
                        model.Content = model.ContentWhatsapp;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Whatsapp no. updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Whatsapp no. not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.ContentWhatsapp;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Whatsapp no. saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Whatsapp no. not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Profile")
                    {
                        if (model.PK_ProfileId == null || model.PK_ProfileId == "")
                        {
                                
                            DataSet ds = model.InsertProfileName();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "New Contact card save Successfully";
                                    model.PK_ProfileId = ds.Tables[0].Rows[0]["PK_ProfileId"].ToString();
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                        }
                    }
                    if (model.Flag == "Business")
                    {
                        TempData["FlagResponse"] = "Business";
                        if (model.PK_ProfileId == null || model.PK_ProfileId == "")
                        {
                            DataSet ds = model.InsertBusinessInfo();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Profile updated Successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (Request.Files.Count > 0)
                            {
                                HttpFileCollectionBase files = Request.Files;
                                HttpPostedFileBase fileUploaderControl = null;
                                for (int i = 0; i < files.Count; i++)
                                {
                                    fileUploaderControl = files[i];
                                }
                                if (fileUploaderControl != null)
                                {
                                    string filename = System.IO.Path.GetFileName(fileUploaderControl.FileName);
                                    fileUploaderControl.SaveAs(Server.MapPath("~/images/ProfilePicture/" + filename));
                                    string filepathtosave = "/images/ProfilePicture/" + filename;
                                    model.Name = model.Name;
                                    model.ProfilePic = filepathtosave;
                                    DataSet ds = model.UpdateBusinessInfo();
                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                        {
                                            model.Result = "Success";
                                            model.Message = "Profile Updated Successfully";
                                        }
                                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                        {
                                            model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        model.Message = "Profile Updated Successfully";
                                    }
                                }
                            }
                            else
                            {
                                DataSet ds = model.UpdateBusinessInfo();
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                    {
                                        model.Result = "Success";
                                        model.Message = "Profile Updated Successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                    {
                                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                                else
                                {
                                    model.Message = "Profile Updated Successfully";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return Json(model.Result,model.Message,JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateProfileStatus(string ProfileId, bool IsChecked)
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["PK_EmployeeID"].ToString();
            model.PK_ProfileId = ProfileId;
            DataSet ds = model.UpdateProfileStatus(IsChecked);
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                model.Result = "Yes";
            }
            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
            {
                model.Result = "No";
            }
            else
            {
                model.Result = "No";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProfilePicForContactCard(string PK_ProfileId)
        {
            NFCProfileModel obj = new NFCProfileModel();
            bool msg = false;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                string fileName = file.FileName;
                obj.PK_ProfileId = PK_ProfileId;
                obj.ProfilePic = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                DataSet ds = obj.UpdateProfilePic();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "true";
                        obj.Message = "Picture Uploaded succesfully ! ";
                    }
                    else
                    {
                        obj.Result = "false";
                        obj.Message = "Not Uploaded. Kindly try after some times. !! ";
                    }
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUrlForRedirection(string Type)
        {
            NFCProfileModel obj = new NFCProfileModel();
            obj.Type = Type;
            obj.FK_UserId = Session["PK_EmployeeID"].ToString();
            DataSet ds = obj.GetUrlForRedirection();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var JsonList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(JsonList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateRedirectionUrl(string PK_ProfileId, string PK_NFCProfileId, string func)
        {
            NFCProfileModel obj = new NFCProfileModel();
            obj.PK_ProfileId = PK_ProfileId;
            obj.Pk_NfcProfileId = Convert.ToInt32(PK_NFCProfileId);
            obj.FK_UserId = Session["PK_EmployeeID"].ToString();
            if (func == "Update")
            {
                obj.IsRedirect = true;
                obj.IsIncluded = true;
            }
            else if (func == "Remove")
            {
                obj.IsRedirect = false;
                obj.IsIncluded = false;
            }
            else
            {

            }
            DataSet ds = obj.UpdateRedirectionUrl();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    obj.Result = "Success";
                    if (func == "Remove")
                    {
                        obj.Message = "Removed successfully";
                    }
                    else
                    {
                        obj.Message = "Profile updated successfully";
                    }

                }
                else
                {
                    obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProfileData(string Type, string PK_ProfileId)
        {
            NFCProfileModel model = new NFCProfileModel();

            model.PK_UserId = Session["PK_EmployeeID"].ToString();
            model.Type = Type;
            if (PK_ProfileId == null)
            {
                model.PK_ProfileId = "0";
            }
            else
            {
                model.PK_ProfileId = PK_ProfileId;
            }
            TempData["ReDesc"] = model.Code;
            DataSet ds = model.GetProfileDataForNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteNFCProfileData(string Pk_NfcProfileId)
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["PK_EmployeeID"].ToString();
            model.Pk_NfcProfileId = Convert.ToInt32(Pk_NfcProfileId);
            DataSet ds = model.DeleteNFCProfileData();
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                model.Result = "Yes";
                TempData["Update"] = "Success";
            }
            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
            {
                model.Result = "No";
                TempData["Update"] = "Error";
            }
            else
            {
                model.Result = "No";
                TempData["Update"] = "Error";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProfilePersonalData()
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["PK_EmployeeID"].ToString();
            TempData["ReDesc"] = model.Code;
            DataSet ds = model.GetNFCProfileData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditProfileSetAction(string id)
        {
            NFCProfileModel objProfile = new NFCProfileModel();
            try
            {
                if (id == null || id == "")
                {
                    return RedirectToAction("Login", "Home");
                }
                NFCModel obj = new NFCModel();
                id = id.Replace(" ", "+");
                var desc = Crypto.DecryptNFC(id);
                obj.Code = desc;
                objProfile.Code = id;
                DataSet ds = obj.CheckNFCCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsActivated"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"].ToString()))
                    {
                        DataSet ds1 = obj.GetNFCProfileData();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            objProfile.PK_ProfileId = ds1.Tables[0].Rows[0]["PK_ProfileId"].ToString();
                            objProfile.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                            objProfile.Email = ds1.Tables[0].Rows[0]["PrimaryEmail"].ToString();
                            //objProfile.DOB = ds1.Tables[0].Rows[0]["DOB"].ToString();
                            //objProfile.Mobile = ds1.Tables[0].Rows[0]["Mobile"].ToString();
                            //objProfile.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();
                            //objProfile.Gender = ds1.Tables[0].Rows[0]["Sex"].ToString();
                            //objProfile.PK_UserId = ds1.Tables[0].Rows[0]["PK_UserId"].ToString();
                            objProfile.Summary = ds1.Tables[0].Rows[0]["Summary"].ToString();
                            //objProfile.BusinessName = ds1.Tables[0].Rows[0]["BusinessName"].ToString();
                            objProfile.EmailBodyHTML = ds1.Tables[0].Rows[0]["Designation"].ToString();
                            //objProfile.UserCode = ds1.Tables[0].Rows[0]["UserCode"].ToString();
                            //objProfile.Leg = ds1.Tables[0].Rows[0]["NFCProfileLeg"].ToString();
                            Session["SponsorId"] = ds1.Tables[0].Rows[0]["LoginId"].ToString();
                            objProfile.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                            {
                                List<NFCContent> NfcContentList = new List<NFCContent>();

                                foreach (DataRow row in ds1.Tables[1].Rows)
                                {
                                    if (row["Type"].ToString() == "Email")
                                    {
                                        objProfile.Email = row["Content"].ToString();
                                    }
                                    NfcContentList.Add(new NFCContent()
                                    {
                                        Content = row["Content"].ToString(),
                                        Type = row["Type"].ToString(),
                                        IsWhatsApp = row["IsWhatsapp"].ToString(),
                                        IsPrimary = row["IsPrimary"].ToString()
                                    });
                                }
                                objProfile.NfcContentList = NfcContentList;
                            }
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                            {
                                List<NFCProfileModel> lst = new List<NFCProfileModel>();

                                foreach (DataRow row in ds1.Tables[1].Rows)
                                {
                                    if (row["Type"].ToString() == "ContactNo")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            Mobile = row["Content"].ToString(),
                                            IsPrimaryNumber = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                    if (row["Type"].ToString() == "Email")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            Email = row["Content"].ToString(),
                                            IsPrimaryEmail = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                    if (row["Type"].ToString() == "SocialMedia")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            SocialMedia = row["Content"].ToString(),
                                            IsPrimarySocial = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                    if (row["Type"].ToString() == "WebLink")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            WebLink = row["Content"].ToString(),
                                            IsPrimaryWebLink = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                    if (row["Type"].ToString() == "Whatsapp")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            Mobile = row["Content"].ToString(),
                                            IsPrimaryNumber = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                    if (row["Type"].ToString() == "Location")
                                    {
                                        lst.Add(new NFCProfileModel()
                                        {
                                            Pk_NfcProfileId = Convert.ToInt32(row["PK_NFcProfileId"]),
                                            WebLink = row["Content"].ToString(),
                                            IsPrimaryNumber = Convert.ToBoolean(row["IsPrimary"]),
                                            IsDisplay = row["IsDisplay"].ToString(),
                                            Type = row["Type"].ToString()
                                        });
                                    }
                                }
                                objProfile.lst = lst;
                            }
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[3].Rows.Count > 0)
                            {
                                List<ListSkill> lstTo = new List<ListSkill>();
                                List<UserSkill> lstSkill = new List<UserSkill>();

                                foreach (DataRow row in ds1.Tables[3].Rows)
                                {
                                    UserSkill objSkill = new UserSkill();
                                    objSkill.Pk_SkillId = row["PK_SkillId"].ToString();
                                    objSkill.Skill = row["Skill"].ToString();
                                    lstSkill.Add(objSkill);
                                }
                                objProfile.lstSkill = lstSkill;
                            }
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[4].Rows.Count > 0)
                            {
                                List<UserLanguage> lstLanguage = new List<UserLanguage>();

                                foreach (DataRow row in ds1.Tables[4].Rows)
                                {
                                    UserLanguage objLanguage = new UserLanguage();
                                    objLanguage.Pk_LanguageId = row["PK_LanguageId"].ToString();
                                    objLanguage.Language = row["Language"].ToString();
                                    lstLanguage.Add(objLanguage);
                                }
                                objProfile.lstLanguage = lstLanguage;
                            }
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[5].Rows.Count > 0)
                            {
                                List<UserAchievement> lstAchievement = new List<UserAchievement>();

                                foreach (DataRow row in ds1.Tables[5].Rows)
                                {
                                    UserAchievement objA = new UserAchievement();
                                    objA.Pk_AchievementId = row["PK_AchievementId"].ToString();
                                    objA.Achievement = row["Achievement"].ToString();
                                    lstAchievement.Add(objA);
                                }
                                objProfile.lstAchievement = lstAchievement;
                            }
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[6].Rows.Count > 0)
                            {
                                objProfile.Description = ds1.Tables[6].Rows[0]["Description"].ToString();
                                objProfile.BannerImage = ds1.Tables[6].Rows[0]["BannerImage"].ToString();
                            }
                        }
                        else
                        {
                            return RedirectToAction("ProfileUpdate", "NFCProfile");
                        }
                        ViewBag.ISActivated = true;
                        Session["NFCCode"] = id;
                        Session["NFCActivated"] = "true";
                    }
                    else
                    {
                        //Not Activated login or activation required
                        ViewBag.ISActivated = false;
                        Session["NFCCode"] = id;
                        Session["NFCActivated"] = "false";
                        Session["SponsorId"] = null;
                        Session["Leg"] = null;
                    }
                }
                else
                {
                    //ViewBag.NFCResopnse = "Wrong NFC Code";
                }
                //}
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(objProfile);
        }
    }
}









