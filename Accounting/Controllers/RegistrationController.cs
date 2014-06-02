using System;
using System.Web.Mvc;
using StratusAccounting.Models;
using StratusAccounting.BAL;

namespace StratusAccounting.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : BasicController
    {
        //
        // GET: /Registration/
        //readonly AccountingEntities _db = new AccountingEntities();

        [HttpGet]
        public JsonResult CheckForEmail(string email)
        {
            bool chkStatus = BAL.BusinessRegistration.IsExistedEmial(email);
            string result = chkStatus == true ? "sucess" : "failed";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignUp(int? licience)
        {
            //ViewBag.Countries = new SelectList(_db.Mst_Countries, "CountryId", "Country", "--select--");
            ViewBag.Liciences = licience;
            if (licience > 0)
            {
                var uReg = new UsersRegistrations();
                uReg.BusinessRegInfo = new StratusAccounting.Models.BusinessRegistration() { Licences = licience };
                return View("SignUp", uReg);
            }
            return Redirect("~/Stratus/Pricing");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UsersRegistrations userReg)
        {
            //ViewBag.Countries = new SelectList(_db.Mst_Countries, "CountryId", "Country", "--select--");
            if (ModelState.IsValid)
            {
                try
                {
                    int userId=16;
                    userReg.UsersInfo.CreatedDate =
                    userReg.BusinessRegInfo.CreatedDate = DateTime.Now;
                    userReg.UsersInfo.UserPassword = userReg.UsersInfo.UserPassword.GetSHA1();
                    userReg.UsersInfo.CreatedByUserId =
                    userReg.BusinessRegInfo.CreatedByUserId = 1;
                    //
                    var registration = new BAL.BusinessRegistration();
                    var chkStatus = registration.SaveBusingessRegistration(userReg, out userId);
                    if (!chkStatus) return RedirectToAction("Index", "Home");
                    var mails = new SendMails
                    {
                        ToMailId = userReg.UsersInfo.Email,
                        Subject = Helper.Helper.GetApplicationParamValue("RegMailSubject")
                    };
                    //
                    if (Request.UrlReferrer != null)
                        mails.Url = "http://" + Request.UrlReferrer.Authority + "/Registration/UserInitialVerification/?userId=" + userId;
                    mails.SendRegistrationMails();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            return View(userReg);
        }

        public ActionResult UserInitialVerification(string userId)
        {
            try
            {
                var userValidation = new UserValidation();
                userValidation.VerifyUser(userId);
                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return Redirect("/Home/Index");
        }
    }
}
