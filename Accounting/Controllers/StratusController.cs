using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StratusAccounting.Controllers
{
    [AllowAnonymous]
    public class StratusController : Controller
    {
        //
        // GET: /Stratus/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TakeTour()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }
    }
}
