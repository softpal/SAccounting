using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StratusAccounting.Models;
using StratusAccounting.BAL;
using System.IO;
using Newtonsoft.Json;

namespace StratusAccounting.Controllers
{
    public class CustomerController : BasicController
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            var customers = Customers.GetCustomersList(this.BusinessId);
            return View(customers);
        }

        //
        // GET: /Customer/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Customer/Create

        public ActionResult NewCustomer()
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);
            int businessId = this.BusinessId;

            Models.Customers_DTO customer = new Customers_DTO();
            customer.CustomerCommunication = new CustomersAddress() { AddressType = "Communication Details", AddressTypeId = 1 };
            //customer.UserBankAccount = BAL.Customers.GetBankAccountTypes().ToList();
            ViewBag.BankAccounts = new SelectList(BAL.Customers.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--Select--");
            ViewBag.PaymentMethods = new SelectList(BAL.Customers.GetPaymentMethodTypes(), "PaymentTypesId", "PaymentDesc", "--select--");
            ViewBag.CreditPeriods = new SelectList(BAL.Customers.GetPaymentCreditTypes(), "CreditPeriodTypeId", "Period", "--select--");
            customer.TaxDetails = BAL.Customers.GetTaxDetails(this.BusinessId).ToList();
            return View(customer);
        }

        [HttpPost]
        public ActionResult NewCustomer(Customers_DTO cdto, FormCollection fc)
        {
            //Customers_DTO customer = new Customers_DTO();
            //customer.CustomerCommunication = new CustomersAddress() { AddressType = "Communication Details", AddressTypeId = 1 };
            //var data = Customers.SaveCustomers(cdto, this.BusinessId, this.UserId);
            //customer = Customers.SaveCustomers(cdto, this.BusinessId, this.UserId);
            //customer.TaxDetailses = BAL.Preferences.SaveTaxInformation(cdto.TaxDetailses);
            //return View("NewCustomer");

            try
            {
                cdto.UserCustomer.ModifiedByUserId = this.UserId;
                cdto.UserCustomer.BusinessID = this.BusinessId;
                //cdto.UserCustomer.CreatedByUserId = this.UserId;
                //cdto.UserCustomer.CreatedDate = DateTime.Now;
                cdto.UserCustomer.ModifiedDate = DateTime.Now;
                foreach (var taxDet in cdto.TaxDetails)
                {
                    taxDet.ModifiedByUserId = this.UserId;
                    taxDet.BusinessID = this.BusinessId;
                    taxDet.ModifiedDate = DateTime.Now;
                    //db.Entry(taxDet).State = EntityState.Modified;
                }
                BAL.Customers.SaveCustomers(cdto);
                return View(cdto);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Customer(Customers_DTO cdto)
        {
            try
            {
                cdto.UserCustomer.ModifiedByUserId = this.UserId;
                cdto.UserCustomer.BusinessID = this.BusinessId;
                //cdto.UserCustomer.CreatedByUserId = this.UserId;
                //cdto.UserCustomer.CreatedDate = DateTime.Now;
                cdto.UserCustomer.ModifiedDate = DateTime.Now;
                foreach (var taxDet in cdto.TaxDetails)
                {
                    taxDet.ModifiedByUserId = this.UserId;
                    taxDet.BusinessID = this.BusinessId;
                    taxDet.ModifiedDate = DateTime.Now;
                    //db.Entry(taxDet).State = EntityState.Modified;
                }
                BAL.Customers.SaveCustomers(cdto);
                return View(cdto);
            }
            catch
            {
                throw;
            }
            //return RedirectToAction("NewCustomer");
        }

        [HttpPost]
        public ActionResult SaveTaxInformation(string[][] taxDetails)
        {
            bool result = false;
            int businessId = BusinessId;
            int userId = UserId;
            foreach (string[] item in taxDetails)
            {
                var objbusinessTax = new UMst_TaxDetails();
                objbusinessTax.BusinessID = businessId;
                objbusinessTax.CreatedByUserId = userId;
                objbusinessTax.TaxTypesId = Convert.ToInt16(item[0]);
                objbusinessTax.TaxNumber = item[1];
                objbusinessTax.TaxValue = string.IsNullOrEmpty(item[2]) ? 0 : Convert.ToDecimal(item[2]);
                result = BAL.Preferences.SaveTaxInformation(objbusinessTax);
            }
            return Json(result);
        }

        //
        // POST: /Customer/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /Customer/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Customer/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /Customer/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Customer/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult UserCustomers(int? userCustomerId)
        {
            ViewBag.Customers = new SelectList(BAL.Customers.GetCustomersList(BusinessId), "UserCustomerId", "CustomerFirstName", "--select--");
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            var customers = BAL.Customers.GetUserCustomers(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            var customer = customers.FirstOrDefault(v => v.UserCustomerId == userCustomerId);

            ViewBag.Files = GetCustomersById(customer);
            return View(customer);
        }

        private static List<string> GetCustomersById(UserCustomer customer)
        {
            List<string> files = new List<string>();
            if (customer != null)
            {
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserCustomers", customer.UserCustomerId.ToString());

                if (Directory.Exists(directory))
                {
                    files = Directory.GetFiles(directory).ToList();
                }
            }
            List<string> fileNames = new List<string>();
            foreach (var file in files)
            {
                var arr = file.Split('\\');
                fileNames.Add(arr.GetValue(arr.Length - 1).ToString());
            }
            return fileNames;
        }

        public ActionResult UserCustomerList()
        {
            Session["pageNo"] = 1;
            Session["pageSize"] = 10;
            Session["sortColumn"] = "Description";
            Session["sortDirection"] = "Desc";
            ViewBag.sortDirection = "Desc";
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult UserCustomers(UserCustomer customer)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = string.Empty;
                var directory = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory, "Customers", customer.UserCustomerId.ToString());
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                savedFileName = Path.Combine(directory,
                      Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

            }
            customer.BusinessID = BusinessId;
            customer.CreatedByUserId = UserId;
            customer.CreatedDate = DateTime.Now;
            try
            {
                BAL.Customers.SaveCustomer(customer);
            }

            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            return View("UserCustomersList");
        }

        public ActionResult Estimate()
        {
            ViewBag.BusinessItems = JsonConvert.SerializeObject(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId));
            var estimates = BAL.Estimates.GetEstimates(BusinessId, UserId);
            Estimate_DTO estim = new Estimate_DTO()
            {
                Estimate = new Estimate(),
                EstimatesDetail = new List<EstimatesDetail>()
            };
            return View(estim);
        }

        public PartialViewResult NewEstimate()
        {
            ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            var objEstimate = new Estimate_DTO
            {
                Estimate = new Estimate(),
                EstimatesDetail = new List<EstimatesDetail> { new EstimatesDetail() }
            };
            return PartialView(objEstimate);
        }

        public ActionResult BlankEditorRow()
        {
            ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            return PartialView("Customer/estimateDetails");
        }

        [HttpPost]
        public bool SaveEstimate(int estimateNum, string custName, string expiryDate, string[][] estimDetails)
        {
            try
            {
                DateTime expDate;
                DateTime expiryDt = new DateTime();
                if (DateTime.TryParseExact(expiryDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expDate))
                {
                    expiryDt = expDate;
                }

                return BAL.Estimates.SaveNewEstimate(this.BusinessId, this.UserId, estimateNum, custName, expiryDt, estimDetails);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return false;
            }
        }
    }
}
