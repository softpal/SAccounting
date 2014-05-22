using System.Linq;
using System.Web.Mvc;
using StratusAccounting.Models;

namespace StratusAccounting.Controllers
{
    [Authorize]
    public class TransactionsController : BasicController
    {
        //
        // GET: /Transactions/

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

    }
}
