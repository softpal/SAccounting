using StratusAccounting.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StratusAccounting.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using StratusAccounting.BAL;

namespace StratusAccounting.Controllers
{
    public class SuperAdminController : BasicController
    {
        private Models.AccountingEntities db = new Models.AccountingEntities();
        [HttpPost]
        public ActionResult ChangePassword(string emailid, string existingpwd, string newpwd)
        {
            using (var db = new Models.AccountingEntities())
            {
                try
                {
                    newpwd = newpwd.GetSHA1();
                    existingpwd = existingpwd.GetSHA1();

                    User user = db.Users.Where(item => (item.Email.Equals(emailid) && item.UserPassword.Equals(existingpwd))).FirstOrDefault();
                    if (user != null)
                    {
                        user.UserPassword = newpwd;
                        StratusAccounting.Models.BusinessRegistration reg = db.BusinessRegistrations.Where(item => item.BusinessID.Equals(this.BusinessId)).First();
                        reg.Email = emailid;
                        reg.ModifiedByUserId = user.UserId;
                        reg.ModifiedDate = DateTime.Now;
                        db.SaveChanges();
                        return View();
                    }
                    ModelState.AddModelError("", "Existing password is not matching");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // GET: /ClientsList/

        public ActionResult ClientsList()
        {
            try
            {
                //var BUList = GetBU();
                //AccountingEntities db = new Models.AccountingEntities();
                var ulist = db.Users.ToList();
                //var userlist = (from p in db.BusinessRegistrations select p).ToList();
                return View(ulist);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Clients List", ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ClientsList(int[] cid)
        {
            var grid = new GridView();
            Response.ClearContent();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=candidateRecord.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //var candidate = GetBU();

            var db = new Models.AccountingEntities();
            grid.DataSource = (from p in db.BusinessRegistrations where cid.Contains(p.BusinessID) select p).ToList();//{ name = p.FirstName, companyName = p.Company.CompanyName };
            grid.DataBind();
            grid.RenderControl(hw);

            Response.Write(sw);
            Response.End();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult UpdateStatus(string clientId, string UpdatableStatus)
        {
            var result = string.Empty;
            try
            {
                using (var AccDBContext = new AccountingEntities())
                {
                    var query = AccDBContext.BusinessRegistrations.Find(Convert.ToInt32(clientId));
                    if (query != null)
                    {
                        //result = query.IsActive == false ? "Activate" : "DeActivate";
                        query.IsActive = query.IsActive == false ? true : false;
                        query.ModifiedDate = DateTime.Now;
                        query.ModifiedByUserId = this.UserId;
                        AccDBContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error:" + ex.Message);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        //public List<BusinessUsersBasicView> GetBU()
        //{
        //    List<BusinessUsersBasicView> BUList = null;
        //    try
        //    {
        //        BUList = new List<BusinessUsersBasicView>();
        //        using (var db = new StratusAccounting.Models.AccountingEntities())
        //        {
        //            BUList = (from BusinessRegistrations in db.BusinessRegistrations
        //                      select new BusinessUsersBasicView
        //                     {
        //                         BusinessId = BusinessRegistrations.BusinessID,
        //                         Business = BusinessRegistrations.BusinessName,
        //                         RegisterdOn = BusinessRegistrations.CreatedDate,
        //                         ExpiresOn = DateTime.Now,
        //                         AdminEmail = BusinessRegistrations.Email,
        //                         Licenses = BusinessRegistrations.Licences,
        //                         Status = BusinessRegistrations.IsActive// == true ? "Active" : "Deactivate"
        //                     }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error:" + ex.Message);
        //    }
        //    return BUList;
        //}
    }
}
