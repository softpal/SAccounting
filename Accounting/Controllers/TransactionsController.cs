using System.Linq;
using System.Web.Mvc;
using StratusAccounting.Models;
using System.Collections.Generic;
using System;

namespace StratusAccounting.Controllers
{
    [Authorize]
    public class TransactionsController : BasicController
    {
        //
        // GET: /Transactions/

        int pageNo = 1;
        int pageSize = 5;// 10;
        string sortColumn = "DueDate";
        string sortDirection = "Desc";

        public ActionResult ChartAccounts()
        {
            ViewBag.UserAccounts = BAL.ChartAccounts.GetUserAccounts(BusinessId,1,5).ToList();
            ViewBag.ParentAccounts = new SelectList(BAL.Products.GetUserParentAccountTypes(BusinessId), "UserParentAccountsId", "AccountName", "--select--");
            return View();
        }

        public JsonResult GetChartAccounts(int pageNum)
        {
            var accounts = BAL.ChartAccounts.GetUserAccounts(BusinessId, pageNum, 5).ToList();
            ViewBag.UserAccounts = accounts;
            ViewBag.ParentAccounts = new  SelectList(BAL.Products.GetUserParentAccountTypes(BusinessId).ToList(), "UserParentAccountsId", "AccountName", "--select--");
            return Json(accounts.ToList());
        }

        [HttpPost]
        public ActionResult InsertAccount(string accountName, string accountNumber, int accountType)
        {
            var chartofAccountsDto = new ChartofAccounts_DTO
            {
                AccountName = accountName,
                AccountNo = accountNumber,
                AccountTypeId = accountType,
                BusinessId = BusinessId,
                UserId = UserId
            };
            return Json(BAL.ChartAccounts.InsertAccount(chartofAccountsDto));
        }

        public ActionResult Payments()
        {
            var payments = BAL.Invoices.GetPayments(BusinessId, UserId);
            ViewBag.PurchaseOrders = payments;            
            return View(payments);
        }

        [HttpPost]
        public ActionResult AddPayment(Models.PurchaseInvoicePayment objPayment)
        {
            //objPayment.UserId = this.UserId;
            //objPayment.BusinessID = this.BusinessId;
          //  BAL.Invoices.SavePaymentInvoice(objPayment);

            ViewBag.CompanyNames = new SelectList(BAL.Invoices.GetCompanyNames(), "UserTypeId", "UserName", "--select--");
            ViewBag.Statuses = new SelectList(BAL.Invoices.GetStatuses(), "PaymentStatusId", "PaymentDesc", "--select--");
            ViewBag.Instruments = new SelectList(BAL.Invoices.GetInstruments(), "PaymentTypesId", "PaymentDesc", "--select--");
            
            return View(objPayment);
        }

        public ActionResult AddPayment()
        {
            ViewBag.CompanyNames = new SelectList(BAL.Invoices.GetCompanyNames(), "UserTypeId", "UserName", "--select--");
            ViewBag.Statuses = new SelectList(BAL.Invoices.GetStatuses(), "PaymentStatusId", "PaymentDesc", "--select--");
            ViewBag.Instruments = new SelectList(BAL.Invoices.GetInstruments(), "PaymentTypesId", "PaymentDesc", "--select--");
            return View();
        }

        public ActionResult Journal()
        {
            var Journal = BAL.Transactions.GetJournals(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.GetInvoices = Journal;
            return View(Journal);
        }
        [HttpPost]
        public ActionResult Journal(Transaction objJournal, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (fc.Count > 0)
                {
                    objJournal.BusinessID = BusinessId;
                    objJournal.CreatedByUserId = UserId;
                    objJournal.JournalNumber = fc["txtJournalNo"];
                    objJournal.Date = Convert.ToDateTime(fc["txtDate"]);
                    objJournal.Debit = decimal.Parse(fc["txtDebits"]);
                    objJournal.Credit = decimal.Parse(fc["txtCredits"]);
                    objJournal.Description = fc["txtDescription"];
                    objJournal.UserAccountsId = 1;

                    BAL.Transactions.SaveJournals(objJournal);
                }
            }
            var Journal = BAL.Transactions.GetJournals(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.GetInvoices = Journal;
            return View(Journal);
        }

        public ActionResult Revenues()
        {
            var Revenues = BAL.Transactions.GetRevenues(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.GetInvoices = Revenues;
            return View(Revenues);
        }

        [HttpPost]
        public ActionResult Revenues(Transaction objRevenues,FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if(fc.Count>0)
                {
                objRevenues.BusinessID = BusinessId;
                objRevenues.CreatedByUserId = UserId;
                objRevenues.VoucherNumber = fc["txtVoucherNo"];
                objRevenues.Date = Convert.ToDateTime(fc["txtDate"]);
                objRevenues.Account = fc["txtAccount"];
                objRevenues.BankAccountId = 1;// int.Parse(fc[""]);
                objRevenues.InstrumentNum = "1";// fc[""];
                objRevenues.DepositById = 1;// int.Parse(fc[""]);
                objRevenues.ItemId = 1;//int.Parse(fc[""]);
                objRevenues.DepositByTypeId = 1;// int.Parse(fc[""]);
                BAL.Transactions.SaveRevenues(objRevenues);
                }
            }
            var Revenues = BAL.Transactions.GetRevenues(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.GetInvoices = Revenues;
            return View(Revenues);
        }

        public ActionResult Payment()
        {
            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn,1);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }
        [HttpPost]
        public ActionResult Payment(Transaction objPayment,FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (fc.Count > 0)
                {
                    objPayment.BusinessID = BusinessId;
                    objPayment.CreatedByUserId = UserId;
                    objPayment.PaymentTypesId = 1;//fc["txtJournalNo"];
                    objPayment.UserAccountsId = 1;// Convert.ToDateTime(fc["txtDate"]);
                    objPayment.VoucherNumber = fc["txtVoucherNo"];
                    objPayment.Date = Convert.ToDateTime(fc["txtTodate"]);
                    objPayment.Account = fc["txtAccount"];
                    objPayment.PaidTo = 1;// fc[""];
                    objPayment.PayeeType = 1;// fc[""];
                    objPayment.PaidByEmployeeId = 1;// fc[""];
                    objPayment.CardTitle = "";// fc[""];
                    objPayment.BankAccountId = 1;//int.Parse(fc[""]);
                    objPayment.InstrumentNum = "";//fc[""];

                    BAL.Transactions.SavePayment(objPayment);
                }
            }
            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn, 1);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }

        public ActionResult Card_Payment()
        {

            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn, 2);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }

        [HttpPost]
        public ActionResult Card_Payment(Transaction objCardPayment,FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (fc.Count > 0)
                {
                    objCardPayment.BusinessID = BusinessId;
                    objCardPayment.CreatedByUserId = UserId;
                    objCardPayment.PaymentTypesId = 1;//fc["txtJournalNo"];
                    objCardPayment.UserAccountsId = 1;// Convert.ToDateTime(fc["txtDate"]);
                    objCardPayment.VoucherNumber = fc["txtVoucherNo"];
                    objCardPayment.Date = Convert.ToDateTime(fc["txtDate"]);
                    objCardPayment.Account = fc["txtAccount"];
                    objCardPayment.PaidTo = 1;// fc[""];
                    objCardPayment.PayeeType = 1;// fc[""];
                    objCardPayment.PaidByEmployeeId = 1;// fc[""];
                    objCardPayment.CardTitle = fc["txtCardTitle"];
                    objCardPayment.BankAccountId = 1;//int.Parse(fc["txtPaidFor"]);
                    objCardPayment.InstrumentNum = fc[""];
                    BAL.Transactions.SavePayment(objCardPayment);
                }
            }
            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn, 2);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }

        public ActionResult Cheque_Payment()
        {
            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn, 3);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }

        [HttpPost]
        public ActionResult Cheque_Payment(Transaction objChequePayment,FormCollection fc)
        {
            // if (ModelState.IsValid)
            //{
            //    objChequePayment.BusinessID = BusinessId;
            //    objChequePayment.CreatedByUserId = UserId;
            //    objChequePayment.PaymentTypesId = 1;//fc["txtJournalNo"];
            //    objChequePayment.UserAccountsId = 1;// Convert.ToDateTime(fc["txtDate"]);
            //    objChequePayment.VoucherNumber = fc[""];
            //    objChequePayment.Date = Convert.ToDateTime(fc[""]);
            //    objChequePayment.Amount = decimal.Parse(fc[""]);
            //    objChequePayment.PaidTo = fc[""];
            //    objChequePayment.PayeeType = fc[""];
            //    objChequePayment.PaidByEmployeeId = fc[""];
            //    objChequePayment.CardTitle = fc[""];
            //    objChequePayment.BankAccountId = int.Parse(fc[""]);
            //    objChequePayment.InstrumentNum = fc[""];

            //    BAL.Transactions.SavePayment(objChequePayment);
            //}
            var Payment = BAL.Transactions.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn, 3);
            ViewBag.GetInvoices = Payment;
            return View(Payment);
        }

        public JsonResult GetPaymentBankAccounts(string keyword)
        {
            return Json(BAL.Invoices.GetBankAccountNames(BusinessId, UserId, keyword), JsonRequestBehavior.AllowGet);
        }
    }
}
