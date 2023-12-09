using AfluexHRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                model.Fk_UserId = Session["Pk_userId"].ToString();
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
    }
}









