using System.Linq;
using System.Web;
using System.Web.Mvc;
using StratusAccounting.BAL;
using System.Web.Security;
using StratusAccounting.Helper;
using System.Web.Caching;

namespace StratusAccounting.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserVerification(string userName, string password, string captchaText, bool rememberToCheck = false)
        {
            #region Captcha code

            if (HttpContext.Session != null && HttpContext.Session["captchastring"] != null)
            {
                if (captchaText == HttpContext.Session["captchastring"].ToString())
                {
                    ViewBag.Message = "CAPTCHA verification successful!";
                    try
                    {
                        var validation = new UserValidation();
                        var loggedUser = validation.IsValidUser(userName, password.GetSHA1());
                        if (loggedUser == null)
                        {
                            ModelState.AddModelError("", StratusResources.LoginFailed);
                            return View("Index");
                        }
                        if (!loggedUser.IsActive)
                        {
                            ModelState.AddModelError("", StratusResources.AccountVerification);
                            return View("Index");
                        }
                        if (loggedUser.BusinessStatus == "Pending")
                        {
                            ModelState.AddModelError("", StratusResources.AccountVerification);
                            return View("Index");
                        }
                        FormsAuthentication.SetAuthCookie(userName, rememberToCheck);
                        if (loggedUser.RoleName.Equals(StratusResources.SuperUserRole))
                        {
                            return Redirect("/SuperAdmin/ClientsList");
                        }
                        Helper.Helper.ClearCache();
                        HttpRuntime.Cache.Insert("UserId", loggedUser.UserId, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        HttpRuntime.Cache.Insert("BusinessId", loggedUser.BusinessId, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        var objpreference = Preferences.GetBusinessPreferences(loggedUser.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(1));
                        if (objpreference != null)
                        {
                            string preferenceValue = objpreference.Mst_PreferenceValues.PreferenceValue;
                            HttpRuntime.Cache.Insert("DateFormat", preferenceValue, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        }
                        objpreference = Preferences.GetBusinessPreferences(loggedUser.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(2));
                        if (objpreference != null)
                        {
                            string preferenceValue = objpreference.Mst_PreferenceValues.PreferenceValue;
                            HttpRuntime.Cache.Insert("NumberFormat", preferenceValue, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        }
                        objpreference = Preferences.GetBusinessPreferences(loggedUser.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(3));
                        if (objpreference != null)
                        {
                            string preferenceValue = objpreference.Mst_PreferenceValues.PreferenceValue;
                            HttpRuntime.Cache.Insert("FiscalYearFormat", preferenceValue, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        }
                        objpreference = Preferences.GetBusinessPreferences(loggedUser.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(4));
                        if (objpreference != null)
                        {
                            string preferenceValue = objpreference.Mst_PreferenceValues.PreferenceValue;
                            HttpRuntime.Cache.Insert("PrimaryCurrency", preferenceValue, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        }                       
                        
                        return RedirectToAction("Dashboard", "Home");
                    }
                    catch (MembershipCreateUserException e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View("Index");
                    }
                }
                ModelState.AddModelError("", StratusResources.CaptchVerification);
                return View("Index");
            }
            return RedirectToAction("Index", "Home");
            #endregion
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("Index");
        }
    }
}
