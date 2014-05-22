using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using StratusAccounting.Models;
using StratusAccounting.BAL;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;


namespace StratusAccounting.Controllers
{
    public class ChartofAccountController : BasicController
    {
        private readonly AccountingEntities _db = new AccountingEntities();

        //
        // GET: /ChartOfAccount/

        //public ActionResult Index()
        //{
        //    ViewBag.ParentAccounts = new SelectList(_db.UMst_UserParentAccounts.Where(item => item.BusinessID.Equals(this.BusinessId)), "UserParentAccountsId", "AccountName", "--select--");
        //    return View();
        //}

        public ActionResult ChartofAccounts()
        {
            ViewBag.ParentAccounts = new SelectList(_db.UMst_UserParentAccounts.Where(item => item.BusinessID.Equals(this.BusinessId)), "UserParentAccountsId", "AccountName", "--select--");
            return View("ChartofAccounts");
        }

        public JsonResult GetCharOfAccounts()
        {
            var listAccounts = BAL.ChartofAccounts.GetChartofAccounts(BusinessId, UserId);
            return Json(listAccounts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveChartofAccounts(string accountName, string accountNumber, int accountType)
        {
            var chartofAccountsDto = new ChartofAccounts_DTO
            {
                AccountName = accountName,
                AccountNo = accountNumber,
                AccountTypeId = accountType,
                BusinessId = BusinessId,
                UserId = UserId
            };
            return Json(BAL.ChartofAccounts.SaveChartofAccounts(chartofAccountsDto));
        }

        //
        // GET: /ChartOfAccount/Details/5

        public ActionResult Details(long id = 0)
        {
            UMst_UserAccounts umstUseraccounts = _db.UMst_UserAccounts.Find(id);
            if (umstUseraccounts == null)
            {
                return HttpNotFound();
            }
            return View(umstUseraccounts);
        }

        //
        // GET: /ChartOfAccount/Create

        public ActionResult Create()
        {
            ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName");
            ViewBag.UserParentAccountsId = new SelectList(_db.UMst_UserParentAccounts, "UserParentAccountsId", "AccountName");
            return View();
        }

        //
        // POST: /ChartOfAccount/Create

        [HttpPost]
        public ActionResult Index(UMst_UserAccounts umst_useraccounts)
        {
            //if (ModelState.IsValid)
            //{
            //    umst_useraccounts.BusinessID = this.BusinessId;
            //    umst_useraccounts.CreatedDate = DateTime.Now;
            //    umst_useraccounts.CreatedByUserId = this.UserId;
            //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead }))
            //    {
            //        try
            //        {
            //            umst_useraccounts.UserAccountsId = _db.GetObjectMaxId("UserAccountsId");
            //            _db.UMst_UserAccounts.Add(umst_useraccounts);
            //            _db.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
            //        scope.Complete();
            //    }
            //    //return RedirectToAction("Index");
            //    ViewBag.UserParentAccountsId = new SelectList(_db.UMst_UserParentAccounts, "UserParentAccountsId", "AccountName", umst_useraccounts.UserParentAccountsId);
            //    return PartialView();
            //}

            ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName", umst_useraccounts.BusinessID);
            ViewBag.UserParentAccountsId = new SelectList(_db.UMst_UserParentAccounts, "UserParentAccountsId", "AccountName", umst_useraccounts.UserParentAccountsId);
            return View("Index", umst_useraccounts);
        }

        //
        // GET: /ChartOfAccount/Edit/5

        public ActionResult Edit(long id = 0)
        {
            UMst_UserAccounts umst_useraccounts = _db.UMst_UserAccounts.Find(id);
            if (umst_useraccounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName", umst_useraccounts.BusinessID);
            ViewBag.UserParentAccountsId = new SelectList(_db.UMst_UserParentAccounts, "UserParentAccountsId", "AccountName", umst_useraccounts.UserParentAccountsId);
            return View(umst_useraccounts);
        }

        //
        // POST: /ChartOfAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(UMst_UserAccounts umst_useraccounts)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(umst_useraccounts);//.State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName", umst_useraccounts.BusinessID);
            ViewBag.UserParentAccountsId = new SelectList(_db.UMst_UserParentAccounts, "UserParentAccountsId", "AccountName", umst_useraccounts.UserParentAccountsId);
            return View(umst_useraccounts);
        }

        //
        // GET: /ChartOfAccount/Delete/5

        public ActionResult Delete(long id = 0)
        {
            UMst_UserAccounts umst_useraccounts = _db.UMst_UserAccounts.Find(id);
            if (umst_useraccounts == null)
            {
                return HttpNotFound();
            }
            return View(umst_useraccounts);
        }

        //
        // POST: /ChartOfAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            UMst_UserAccounts umst_useraccounts = _db.UMst_UserAccounts.Find(id);
            _db.UMst_UserAccounts.Remove(umst_useraccounts);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Invoices

        [HttpGet]
        public ActionResult AddPurchaseInvoice()
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);

            var objInvoice = new Inovices_DTO
            {
                PurchaseOrder = new PurchaseOrder(),
                PurchaseOrderDetail = new List<PurchaseOrderDetail> { new PurchaseOrderDetail() }
            };
            ViewBag.POServiceNum = new SelectList(BAL.Invoices.GetPOInvoiceDetails(BusinessId), "PurchaseOrderId", "POTitle","--Select--");
            ViewBag.InvoiceDueTerms = new SelectList(BAL.Invoices.GetInvoiceDueTerms(), "DueTermId", "Term", "--Select--");

            ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            //ViewBag.BusinessItems = new SelectList(BAL.Invoices.GetPOBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            return View(objInvoice);
        }

        //[HttpPost]
        //public ActionResult AddPurchaseInvoice(PurchaseInvoice invoice,FormCollection collection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var items = Invoices.SavePurchaseInvoices(invoice);
        //        //_db.Entry(invoice);
        //        //_db.SaveChanges();
        //        return RedirectToAction("AddPurchaseInvoice");
        //    }
        //    return View(invoice);
        //}

        [HttpPost]
        public ActionResult AddPurchaseInvoice(Inovices_DTO invoice, string[] aryFilePath)
        {
            try
            {
                invoice.PInvoice.ModifiedByUserId = this.UserId;
                invoice.PInvoice.BusinessID = this.BusinessId;
                invoice.PInvoice.UserVendorId = this.UserId;
                //invoice.PInvoice.CreatedByUserId = this.UserId;
                //invoice.PInvoice.CreatedDate = DateTime.Now;
                invoice.PInvoice.ModifiedDate = DateTime.Now;
                
              //  BAL.Invoices.SavePurchaseInvoice(invoice);
                //BAL.Invoices.SavePurchaseInvoices(invoice);

                ViewBag.POServiceNum = new SelectList(BAL.Invoices.GetPOInvoiceDetails(BusinessId), "PurchaseOrderId", "POTitle", "--Select--");
                ViewBag.InvoiceDueTerms = new SelectList(BAL.Invoices.GetInvoiceDueTerms(), "DueTermId", "Term", "--Select--");

                return View("AddPurchaseInvoice");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return View(invoice);
            }
        }

        //[HttpPost]
        //public ActionResult AddPurchaseInvoice(Inovices_DTO invoice)
        //{
        //    try
        //    {
        //        ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
        //        //purchaseOrder.PurchaseOrderDetail.Add(new PurchaseOrderDetail());
        //        return View(invoice);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return null;
        //    }
        //}

        [HttpPost]
        public ActionResult SavePurchaseOrders(string[] pOrder, string[][] pDetails, string[] aryFilePath)
        {
            try
            {
                bool result = false;
                var objPurchase = new PurchaseOrder
                {
                    BusinessID = BusinessId,
                    CreatedByUserId = UserId,
                    POTitle = pOrder[0],
                    UserVendorId = Convert.ToInt64(pOrder[1]),
                    PONumber = pOrder[2]
                };
                DateTime d;
                if (DateTime.TryParseExact(pOrder[3], "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
                {
                    objPurchase.ExpiryDate = d;
                }

                decimal amount = 0;
                //objPurchase.ExpiryDate = Convert.ToDateTime(pOrder[3]);
                objPurchase.StatusId = 1; // need to know which field is status

                var pList = new List<PurchaseOrderDetail>();
                foreach (string[] item in pDetails)
                {
                    var objPurchaseOrder = new PurchaseOrderDetail();
                    objPurchaseOrder.BusinessId = BusinessId;
                    objPurchaseOrder.CreatedByUserId = UserId;
                    objPurchaseOrder.ItemId = Convert.ToInt64(item[0]);// need to know which field is status
                    objPurchaseOrder.Description = item[1];
                    objPurchaseOrder.Quantity = Convert.ToInt32(item[2]);
                    objPurchaseOrder.Amount = Convert.ToDecimal(item[3]);
                    objPurchaseOrder.Tax = Convert.ToDecimal(item[4]);
                    objPurchaseOrder.TaxPercent = ((objPurchaseOrder.Tax * 100) / objPurchaseOrder.Amount);
                    amount += Convert.ToDecimal(item[3]);
                    pList.Add(objPurchaseOrder);
                }

                objPurchase.Amount = amount;
                var files = new List<string>();
                if (aryFilePath != null)
                {
                    files.AddRange(aryFilePath);
                    Guid fileGuid = Guid.NewGuid();
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    if (!Directory.Exists(path + "Documents"))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path + "Documents");
                    }
                    path += "Documents\\" + fileGuid;
                    try
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(path);
                        foreach (string file in files)
                        {
                            string fileName = Path.GetFileName(file);
                            string location = Path.Combine(path, fileName);
                            byte[] input = null;
                            var newFile = new FileStream(location, FileMode.Create);
                            // Write data to the file
                            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                            {
                                input = new Byte[fs.Length];
                                int length = Convert.ToInt32(fs.Length);
                                fs.Read(input, 0, length);
                            }
                            newFile.Write(input, 0, input.Length);
                            // Close file
                            newFile.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
                }

                //int purchaseId = BAL.PurchaseOrders.SavePurchaseOrder(objPurchase);

                foreach (PurchaseOrderDetail item in pList)
                {
                    result = false; //BAL.PurchaseOrders.SavePurchaseOrderDetails(purchaseID, item);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(false);
            }
        }

        public ActionResult AddMore()
        {
            return View();
        }

        public ActionResult GetInvoices()
        {
            //Invoices objInvoice = new Invoices();
            Session["pageNo"] = 1;
            Session["pageSize"] = 5;// 10;
            Session["sortColumn"] = "Description";
            Session["sortDirection"] = "Desc";
            ViewBag.sortDirection = "Desc";
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            var items = Invoices.GetInvoices(this.BusinessId, this.UserId, pageNo, pageSize, sortDirection, sortColumn);
            return View(items);
        }

        public ActionResult GetPayments()
        {
            var items = Invoices.GetPayments(this.BusinessId, this.UserId);
            return View(items);
        }

        [HttpGet]
        public ActionResult EditInvoices(int purchaseInvoiceId)
        {
            //var items = Invoices.UpdateInvoices(purchaseInvoiceId, this.BusinessId, this.UserId);
            var items = Invoices.GetInvoices(this.BusinessId,this.UserId).ToList();
            var result = items.FirstOrDefault(u => u.PurchaseInvoiceId == purchaseInvoiceId);
            return View(result);
        }

        [HttpPost]
        public ActionResult EditInvoices(PurchaseInvoicePayment invoicePayments)
        {
            return View();
        }

        //
        // GET: /Invoice/Create

        public ActionResult CreateInvoice()
        {
            ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName");
            //var items = new 
            return View();
        }
        

        #endregion

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}