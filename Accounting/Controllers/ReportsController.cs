using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StratusAccounting.BAL;
using StratusAccounting.Models;

namespace StratusAccounting.Controllers
{
    public class ReportsController : BasicController
    {
        private LedgerTypes ledgerType;
        private string sortColumns = string.Empty;
        private string sortDirection = string.Empty;
        private DateTime _fromDate = DateTime.Today;
        private DateTime _toDate = DateTime.Today;
        //
        // GET: /Reports/

        #region private methods
        IList<Ledger_DTO> GetLedgers(int? accountId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            //Get User Accounts
            ViewBag.UserAccounts = ledgerType == LedgerTypes.ScheduleWise ? new SelectList(new List<SelectListItem>() {
                new SelectListItem(){ Text="Asset",Value="128" } ,
                new SelectListItem(){ Text="Income",Value="130" } ,
                new SelectListItem(){ Text="Equity",Value="167" } ,
                new SelectListItem(){ Text="Liability",Value="129" } ,
                new SelectListItem(){ Text="Expense",Value="131" } 
            }, "Value", "Text", accountId) : new SelectList(Products.GetUserAccountTypes(BusinessId), "UserAccountsId", "AccountName", accountId);

            _fromDate = fromDate ?? DateTime.Today;// : fromDate;
            _toDate = toDate ?? DateTime.Today;
            pageNo = pageNo ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.FromDate = _fromDate.ToPreferenceDateTime(HttpRuntime.Cache["DateFormat"].ToString());
            ViewBag.ToDate = _toDate.ToPreferenceDateTime(HttpRuntime.Cache["DateFormat"].ToString());
            //sortColumns = sortColumns ?? string.Empty;
            //sortDirection = sortDirection ?? string.Empty;
            IList<Ledger_DTO> listLedgers = Ledgers.GetLedgers(BusinessId, UserId, ledgerType, fromDate, toDate, accountId, pageNo, pageSize, sortDirection, sortColumns);
            return listLedgers;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Ledger(int? accountId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            ledgerType = LedgerTypes.Ledger;
            Session["LedgerType"] = ledgerType;
            ViewBag.UserAccounts = new SelectList(Products.GetUserAccountTypes(BusinessId), "UserAccountsId", "AccountName", accountId);
            return View(); // (GetLedgers(accountId, fromDate, toDate, pageNo, pageSize));
        }

        //[HttpPost]
        public ActionResult FilterLedgers(FormCollection collection)
        {
            string accountId = collection[0] ?? "0";
            string fromDate = collection[1] ?? DateTime.Today.ToString();
            string toDate = collection[2] ?? DateTime.Today.ToString();
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            int pageNo = 1, pageSize = 20;
            ledgerType = (LedgerTypes)Session["LedgerType"];
            switch (ledgerType)
            {
                case LedgerTypes.DayWise:
                    return View("DayWise", GetLedgers(Convert.ToInt32(accountId), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), pageNo, pageSize));
                case LedgerTypes.MonthWise:
                    return View("MonthWise", GetLedgers(Convert.ToInt32(accountId), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), pageNo, pageSize));
                case LedgerTypes.ScheduleWise:
                    return View("ScheduleWise", GetLedgers(Convert.ToInt32(accountId), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), pageNo, pageSize));
                case LedgerTypes.Ledger:
                    return View("Ledger", GetLedgers(Convert.ToInt32(accountId), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), pageNo, pageSize));
            }
            return View();
        }

        public ActionResult DayWise(int? accountId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            ledgerType = LedgerTypes.DayWise;
            Session["LedgerType"] = ledgerType;
            return View(GetLedgers(accountId, fromDate, toDate, pageNo, pageSize));
        }

        public ActionResult MonthWise(int? accountId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            ledgerType = LedgerTypes.MonthWise;
            Session["LedgerType"] = ledgerType;
            return View(GetLedgers(accountId, fromDate, toDate, pageNo, pageSize));
        }

        public ActionResult ScheduleWise(int? accountId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            ledgerType = LedgerTypes.ScheduleWise;
            Session["LedgerType"] = ledgerType;
            return View(GetLedgers(accountId, fromDate, toDate, pageNo, pageSize));
        }

        public ActionResult TrailBalance(DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize)
        {
            fromDate = fromDate ?? DateTime.Today;// : fromDate;
            toDate = toDate ?? DateTime.Today;
            pageNo = pageNo ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            IList<Ledger_DTO> listTrailBalance = Ledgers.GetTrailBalance(BusinessId, UserId, fromDate, toDate, pageNo, pageSize, sortColumns, sortDirection);
            return View(listTrailBalance);
        }

    }
}
