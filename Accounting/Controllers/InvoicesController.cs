using StratusAccounting.BAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StratusAccounting.Controllers
{
    public class InvoicesController : BasicController
    {
        //
        // GET: /Invoices/


        #region "Invocies"

        #region "Invoice List"
        int pageNo = 1;
        int pageSize = 5;// 10;
        string sortColumn = "DueDate";
        string sortDirection = "Desc";

        public ActionResult Index()
        {
            // ViewBag.InvoiceStatus = new SelectList(BAL.Invoices.GetInvoiceStatuses(), "InvoicestatusId", "InvoicestatusDesc", "--Select--");
            var invoice = BAL.Invoices.GetInvoices(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            Session["PoInvoiceId"] = null;
            ViewBag.GetInvoices = invoice;
            return View(invoice);
        }

        #endregion

        #region Add Invoices

        [HttpGet]
        public ActionResult AddPurchaseInvoice(PurchaseInvoice invoice)
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);

            ViewBag.PurchaseProduct = new SelectList(BAL.PurchaseOrders.GetBusinessItems(BusinessId, UserId), "ItemId", "Title", "--Select--");
            ViewBag.POServiceNum = new SelectList(BAL.PurchaseOrders.GetPODetailsByVendorId(0, 0, 0), "", "", "--Select--");
            ViewBag.InvoiceDueTerms = new SelectList(BAL.Invoices.GetInvoiceDueTerms(), "DueTermId", "Term", "--Select--");
            PurchaseInvoiceDetail objInvDetails = new PurchaseInvoiceDetail();
            ViewBag.poId = 0;
            invoice = new PurchaseInvoice
            {
                PurchaseOrder = new PurchaseOrder(),
                PurchaseInvoiceDetail = objInvDetails,
                PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
            };
            //  return View(objInvoice);
            if (Session["PoInvoiceId"] != null)
            {
                var polist = BAL.Invoices.GetPurchaseInvoiceByInvID(BusinessId, UserId, Convert.ToInt32(Session["PoInvoiceId"]));
                if (polist != null && polist.Count > 0)
                {
                    invoice = polist[0];
                    ViewBag.poId = invoice.PurchaseOrderId;
                    // objPurchase.PurchaseInvoiceDetails = BAL.PurchaseOrders.GetPoDetailsByPOID(BusinessId, UserId, Convert.ToInt32(objPurchase.PurchaseInvoice.UserVendorId), id);
                    decimal amt = 0;
                    decimal taxamt = 0;
                    //foreach (PurchaseInvoiceDetail item in objPurchase.PurchaseInvoiceDetails)
                    //{
                    //    if (!string.IsNullOrEmpty(item.Quantity.ToString()) && !string.IsNullOrEmpty(item.Amount.ToString()))
                    //        amt += Convert.ToInt32(item.Quantity) * Convert.ToDecimal(item.Amount);
                    //    if (!string.IsNullOrEmpty(item.Tax.ToString()))
                    //        taxamt += Convert.ToDecimal(item.Tax);
                    //}

                    ViewBag.Total = amt + taxamt;
                    ViewBag.SubTotal = amt;
                    ViewBag.Tax = taxamt;

                }
            }
            PurchaseOrder objPO = new PurchaseOrder();
            return View(invoice);
        }

        [HttpPost]
        public ActionResult AddPurchaseInvoice(PurchaseInvoice invoice, FormCollection fc)
        {
            if (fc["PurchaseOrderId"] != null)
            {
                invoice.PurchaseOrderId = Convert.ToInt32(fc["PurchaseOrderId"].ToString().Replace(",", "").Trim());
            }
            //if (ModelState.IsValid)
            //{
                invoice.BusinessID = BusinessId;
                invoice.CreatedByUserId = UserId;
                string index = "";
                decimal dAmount = 0;
                List<PurchaseInvoiceDetail> lstPODetails = new List<PurchaseInvoiceDetail>();
                if (fc["PurchaseInvoiceDetail.index"] != null)
                {
                    index = fc["PurchaseInvoiceDetail.index"];
                    string[] indexKeys = index.Split(',');
                    for (int i = 0; i < indexKeys.Length; i++)
                    {
                        string ItemId = "PurchaseInvoiceDetail[" + indexKeys[i] + "].ItemId";
                        string Description = "PurchaseInvoiceDetail[" + indexKeys[i] + "].Description";
                        string Quantity = "PurchaseInvoiceDetail[" + indexKeys[i] + "].Quantity";
                        string Amount = "PurchaseInvoiceDetail[" + indexKeys[i] + "].Amount";
                        string Tax = "PurchaseInvoiceDetail[" + indexKeys[i] + "].Tax";
                        if (!string.IsNullOrEmpty(fc[ItemId]) && !string.IsNullOrEmpty(fc[Quantity]) && !string.IsNullOrEmpty(fc[Amount]))
                        {
                            if (int.Parse(fc[Quantity]) > 0)
                            {
                                PurchaseInvoiceDetail objPODetails = new PurchaseInvoiceDetail();
                                objPODetails.BusinessId = BusinessId;
                                objPODetails.CreatedByUserId = UserId;
                                objPODetails.ItemId = long.Parse(fc[ItemId]);
                                objPODetails.Description = fc[Description];
                                objPODetails.Quantity = int.Parse(fc[Quantity]);
                                objPODetails.Amount = decimal.Parse(fc[Amount]);
                                if (!string.IsNullOrEmpty(fc[Tax]))
                                    objPODetails.Tax = decimal.Parse(fc[Tax]);
                                else
                                    objPODetails.Tax = 0;

                                lstPODetails.Add(objPODetails);
                                if (!string.IsNullOrEmpty(fc[Tax]))
                                    dAmount += (int.Parse(fc[Quantity]) * decimal.Parse(fc[Amount])) + decimal.Parse(fc[Tax]);
                                else
                                    dAmount += (int.Parse(fc[Quantity]) * decimal.Parse(fc[Amount]));
                            }
                        }
                    }
                }
                invoice.PurchaseInvoiceDetails = lstPODetails;
                invoice.Amount = dAmount;
                if (fc["optionsRadios1"].ToString() == "option2")
                {
                    BAL.Invoices.SaveSalesInvoice(invoice);
                    return RedirectToAction("Index");
                }
                else
                {
                    BAL.Invoices.SavePurchaseInvoice(invoice);
                    return RedirectToAction("Index");
                }

                // PurchaseInvoice invoiceNew = new PurchaseInvoice();
                // invoiceNew.BusinessID = BusinessId;
                // invoiceNew.CreatedByUserId = UserId;
                //return RedirectToAction("AddPurchaseInvoice", invoiceNew);
                return RedirectToAction("Index");
        }

        public JsonResult GetVendorsList(string keyword, string val)
        {
            if (val == "option1")
            {
                return Json(BAL.Vendors.GetVendorsList(BusinessId, keyword), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(BAL.Invoices.GetSalesReceiptsCustomers(BusinessId, UserId, keyword), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPODetails(string vendorId, string type)
        {
            if (type == "option1")
            {
                var pos = BAL.PurchaseOrders.GetPODetailsByVendorId(BusinessId, UserId, Convert.ToInt32(vendorId));
                ViewBag.POServiceNum = new SelectList(pos, "", "", "--Select--");
                return Json(pos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pos = BAL.Invoices.GetEstimatesByCustomer(BusinessId, UserId, Convert.ToInt32(vendorId));
                ViewBag.POServiceNum = new SelectList(pos, "", "", "--Select--");
                return Json(pos, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BlankEditorRow()
        {
            ViewBag.PurchaseProduct = new SelectList(BAL.PurchaseOrders.GetBusinessItems(BusinessId, UserId), "ItemId", "Title", "--select--");
            // return PartialView("Transactions/PurchaseOrderDetails");EditorTemplates
            return PartialView("EditorTemplates/PurchaseInvoiceDetails");

        }

        public ActionResult BindEditorRow(string itemId, string desc, string qty, string amt, string tax)
        {
            ViewBag.PurchaseProduct = new SelectList(BAL.PurchaseOrders.GetBusinessItems(BusinessId, UserId), "ItemId", "Title", "--select--");
            PurchaseInvoiceDetail objPoInvDetail = new PurchaseInvoiceDetail();
            if (!string.IsNullOrEmpty(itemId))
                objPoInvDetail.ItemId = Convert.ToInt64(itemId);
            else
                objPoInvDetail.ItemId = 0;
            objPoInvDetail.Description = desc;
            if (!string.IsNullOrEmpty(qty))
                objPoInvDetail.Quantity = Convert.ToInt32(qty);
            if (!string.IsNullOrEmpty(amt))
                objPoInvDetail.Amount = Convert.ToDecimal(amt);
            if (!string.IsNullOrEmpty(tax))
            {
                try
                {
                    if (Convert.ToDecimal(tax) > 0)
                        objPoInvDetail.Tax = Convert.ToDecimal(tax);
                }
                catch (Exception ex)
                {
                }
            }

            return PartialView("EditorTemplates/PurchaseInvoiceDetails", objPoInvDetail);
        }

        public JsonResult GetPurchaseOrderDetailsByPOID(string vendorId, string poId, string type)
        {

            if (type == "1")
            {
                return Json(BAL.PurchaseOrders.GetPurchaseOrderDetailsByPOID(BusinessId, UserId, Convert.ToInt32(vendorId), Convert.ToInt32(poId)), JsonRequestBehavior.AllowGet);
            }
            else if (type == "2")
            {
                return Json(BAL.Invoices.GetEstimateDetailsByID(BusinessId, UserId, Convert.ToInt32(vendorId), Convert.ToInt32(poId)), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(BAL.PurchaseOrders.GetPurchaseOrderDetailsByPOID(BusinessId, UserId, Convert.ToInt32(vendorId), Convert.ToInt32(poId)), JsonRequestBehavior.AllowGet);
            }
        }



        //public ActionResult AddMore()
        //{
        //    return View();
        //}

        //public ActionResult GetInvoices()
        //{
        //    //Invoices objInvoice = new Invoices();
        //    Session["pageNo"] = 1;
        //    Session["pageSize"] = 5;// 10;
        //    Session["sortColumn"] = "Description";
        //    Session["sortDirection"] = "Desc";
        //    ViewBag.sortDirection = "Desc";
        //    int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
        //    int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
        //    string sortColumn = Session["sortColumn"].ToString();
        //    string sortDirection = Session["sortDirection"].ToString();
        //    var items = Invoices.GetInvoices(this.BusinessId, this.UserId, pageNo, pageSize, sortDirection, sortColumn);
        //    return View(items);
        //}

        //public ActionResult GetPayments()
        //{
        //    var items = Invoices.GetPayments(this.BusinessId, this.UserId);
        //    return View(items);
        //}

        //[HttpGet]
        //public ActionResult EditInvoices(int purchaseInvoiceId)
        //{
        //    //var items = Invoices.UpdateInvoices(purchaseInvoiceId, this.BusinessId, this.UserId);
        //    var items = Invoices.GetInvoices(this.BusinessId, this.UserId).ToList();
        //    var result = items.FirstOrDefault(u => u.PurchaseInvoiceId == purchaseInvoiceId);
        //    return View(result);
        //}

        //[HttpPost]
        //public ActionResult EditInvoices(PurchaseInvoicePayment invoicePayments)
        //{
        //    return View();
        //}

        ////
        //// GET: /Invoice/Create

        //public ActionResult CreateInvoice()
        //{
        //    //ViewBag.BusinessID = new SelectList(_db.BusinessRegistrations, "BusinessID", "BusinessName");
        //    //var items = new 
        //    return View();
        //}


        #endregion

        #region " View Invoices"

        public ActionResult UpdateInvoiceStatus(string id)
        {
            //var objPurchase = new PurchaseInvoiceDetail
            //{
            //    PurchaseInvoice = new PurchaseInvoice(),
            //    PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
            //};
            var objPurchase = new PurchaseInvoice
            {
                //PurchaseInvoice = new PurchaseInvoice(),
                PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
            };
            objPurchase.BusinessID = BusinessId;
            objPurchase.CreatedByUserId = UserId;
            if (id.Contains("-"))
            {
                string[] array = id.Split('-');
                if (array != null && array.Length == 2)
                {
                    objPurchase.StatusId = Convert.ToInt16(array[0].ToString());
                    objPurchase.PurchaseInvoiceId = Convert.ToInt64(array[1].ToString());
                    BAL.Invoices.UpdateInvoicestatus(objPurchase);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewPurchaseInvoice(int id)
        {
            ViewBag.SubTotal = 0;
            ViewBag.Tax = 0;
            ViewBag.Total = 0;
            var objPurchase = new PurchaseInvoice
            {
                PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
            };



            var polist = BAL.Invoices.GetPurchaseInvoiceByInvID(BusinessId, UserId, id);
            if (polist != null && polist.Count > 0)
            {
                objPurchase = polist[0];


                // objPurchase.PurchaseInvoiceDetails = BAL.PurchaseOrders.GetPoDetailsByPOID(BusinessId, UserId, Convert.ToInt32(objPurchase.PurchaseInvoice.UserVendorId), id);
                decimal amt = 0;
                decimal taxamt = 0;
                //foreach (PurchaseInvoiceDetail item in objPurchase.PurchaseInvoiceDetails)
                //{
                //    if (!string.IsNullOrEmpty(item.Quantity.ToString()) && !string.IsNullOrEmpty(item.Amount.ToString()))
                //        amt += Convert.ToInt32(item.Quantity) * Convert.ToDecimal(item.Amount);
                //    if (!string.IsNullOrEmpty(item.Tax.ToString()))
                //        taxamt += Convert.ToDecimal(item.Tax);
                //}


                ViewBag.Total = amt + taxamt;
                ViewBag.SubTotal = amt;
                ViewBag.Tax = taxamt;

            }

            return View(objPurchase);
        }

        [HttpPost]
        public ActionResult ViewPurchaseInvoice(PurchaseInvoice obj, FormCollection fc)
        {
            if (!string.IsNullOrEmpty(fc["PurchaseInvoiceId"]))
            {
                Session["PoInvoiceId"] = fc["PurchaseInvoiceId"].ToString();
                return RedirectToAction("AddPurchaseInvoice");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #endregion


        #region "Payments or Add Payments"

        public ActionResult Payments(PurchaseInvoicePayment objPayment)
        {

            //int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            //int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            //string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);
            ViewBag.paidTo = 0;
            ViewBag.Tags = "";
            // ViewBag.GetBankAccounts = new SelectList(BAL.Invoices.GetBankAccountNames(BusinessId, UserId), "BankAccountId", "BankName", "--Select--");
            ViewBag.GetInstrumentTypes = new SelectList(BAL.Invoices.GetInstruments(), "PaymentTypesId", "PaymentDesc", "--Select--");
            ViewBag.GetPaymentStatusTypes = new SelectList(BAL.Invoices.GetStatuses(), "PaymentStatusId", "PaymentDesc", "--Select--");
            if (Session["POInvoivePaymentID"] != null)
            {
                var payment = BAL.Invoices.GetInvoicePaymentInvoiceByInvoiceId(Convert.ToInt32(Session["POInvoivePaymentID"].ToString()), BusinessId, UserId);
                objPayment = payment;
                ViewBag.paidTo = objPayment.PaidToTypeId.ToString();
                string strTags = "";
                strTags = BAL.Invoices.GetInvoiceTages(BusinessId, UserId, Convert.ToInt64(objPayment.PurchaseInvoiceId));
                // ViewBag.Tags = strTags.Replace(' ', '|');
                objPayment.Tags = strTags;
            }
            else
            {
                objPayment.PurchaseInvoicePaymentId = 0;
                objPayment.CurrentPaymentStatusId = 0;
                objPayment.NextPaymentStatusId = 0;
                objPayment.NextPaymentStatus = "";
                objPayment.CurrentPaymentStatus = "";
            }
            return View(objPayment);
        }

        [HttpPost]
        public ActionResult Payments(PurchaseInvoicePayment objPayment, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                objPayment.Date = Convert.ToDateTime(fc["txtPaymentDate"]);
                objPayment.ItemId = 1;//need to modify
                objPayment.PaidToTypeId = 3;//for vendors // need to modify
                objPayment.BusinessID = BusinessId;
                objPayment.CreatedByUserId = UserId;
                if (Session["POInvoivePaymentID"] != null)
                {

                }
                BAL.Invoices.SaveInvoicePayment(objPayment);
            }
            PurchaseInvoicePayment objPaynt = new PurchaseInvoicePayment();
            Session["POInvoivePaymentID"] = null;
            return RedirectToAction("GetPayments", objPaynt);
        }


        public JsonResult GetPaymentBankAccounts(string keyword)
        {
            return Json(BAL.Invoices.GetBankAccountNames(BusinessId, UserId, keyword), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvociesList(string keyword)
        {
            return Json(BAL.Invoices.GetInvoiceNumbers(BusinessId, UserId, keyword), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvociesTages(string InvoiceID)
        {
            return Json(BAL.Invoices.GetInvoiceTages(BusinessId, UserId, Convert.ToInt64(InvoiceID)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoicePaymentsByID(string id)
        {
            Session["POInvoivePaymentID"] = id;


            PurchaseInvoicePayment objPayment = new PurchaseInvoicePayment();
            try
            {
                var InvList = BAL.Invoices.GetInvoicePaymentInvoiceByInvoiceId(Convert.ToInt32(id), BusinessId, UserId);
                objPayment = InvList;
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Payments", objPayment);
        }

        public ActionResult SalesRevenues()
        {
            return View();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            // _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult GetPayments()
        {
            Session["POInvoivePaymentID"] = null;
            var payments = BAL.Invoices.GetPayments(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.getpayments = payments;
            return View(payments);
        }

        public ActionResult InvoiceSalesReceipts()
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);

            var salesReceipts = BAL.Invoices.GetInvoiceReceipts(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            return View(salesReceipts);
        }

        public ActionResult AddInvoiceSalesReceipts(SalesReceipt objSalesReceipt)
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            ViewBag.GetInstrumentTypes = new SelectList(BAL.Invoices.GetInstruments(), "PaymentTypesId", "PaymentDesc", "--Select--");
            objSalesReceipt.BusinessId = BusinessId;
            objSalesReceipt.CreatedByUserId = UserId;
            return View(objSalesReceipt);
        }

        public ActionResult GetSalesReceipts()
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            var payments = BAL.Invoices.GetInvoiceReceipts(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.getpayments = payments;
            return View(payments);
        }

        public JsonResult GetReceipts(string keyword)
        {
            return Json(BAL.Invoices.GetInvoiceReceipts(BusinessId, UserId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddInvoiceSalesReceipts(SalesReceipt objSalesReceipt, FormCollection fc)
        {

            int hdnBtns = 0;
            if (!string.IsNullOrEmpty(fc["hdnBtns"]))
            {
                hdnBtns = Convert.ToInt32(fc["hdnBtns"]);
            }
            if (hdnBtns > 0)
            {
                objSalesReceipt.Date = Convert.ToDateTime(fc["txtSaleRecDate"]);
                objSalesReceipt.PaymentStatusId = 4;

                BAL.Invoices.SaveInvoiceReceipts(objSalesReceipt);
                return RedirectToAction("GetSalesReceipts");
            }
            else
            {
                return RedirectToAction("GetSalesReceipts");

            }
        }

        public JsonResult GetSalesReceiptsCustomers(string keyword)
        {
            return Json(BAL.Invoices.GetSalesReceiptsCustomers(BusinessId, UserId, keyword), JsonRequestBehavior.AllowGet);

        }


        //public ActionResult CancelInvoiceReceipt()
        //{
        //    return RedirectToAction("GetSalesReceipts");
        //}



        //[HttpGet]
        //public ActionResult AddPurchaseInvoice(SalesReceipt Slrcpt)
        //{
        //    int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
        //    int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
        //    string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);


        //    SalesReceipt objSlrcptDetails = new SalesReceipt();
        //    //Slrcpt = new PurchaseInvoice
        //    //{
        //    //    PurchaseOrder = new PurchaseOrder(),
        //    //    PurchaseInvoiceDetail = objInvDetails,
        //    //    PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
        //    //};
        //    //  return View(objInvoice);
        //    PurchaseOrder objPO = new PurchaseOrder();
        //    return View(Slrcpt);
        //}
        //[HttpPost]
        //public ActionResult AddSalesReceipts(SalesReceipt SalRcpt, FormCollection fc)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SalRcpt.BusinessId = BusinessId;
        //        SalRcpt.CreatedByUserId = UserId;
        //        string index = "";
        //        decimal dAmount = 0;
        //        List<SalesReceipt> lstSalRcptDetails = new List<SalesReceipt>();
        //        if (fc["SalesReceipt.index"] != null)
        //        {
        //            index = fc["SalesReceipt.index"];
        //            string[] indexKeys = index.Split(',');
        //            for (int i = 0; i < indexKeys.Length; i++)
        //            {
        //                string Receipt = "1";
        //                string PaymentMethod = "1";

        //                string Date = "SalesReceipt[" + indexKeys[i] + "].txtSaleRecDate";
        //                string Customers = "1";
        //                string Amount = "SalesReceipt[" + indexKeys[i] + "].txtAmount";
        //                string Description = "SalesReceipt[" + indexKeys[i] + "].txtDescription";


        //                if (!string.IsNullOrEmpty(fc[Receipt]) && !string.IsNullOrEmpty(fc[PaymentMethod]) && !string.IsNullOrEmpty(fc[Amount])
        //                     && !string.IsNullOrEmpty(fc[Date]) && !string.IsNullOrEmpty(fc[Customers]))
        //                {
        //                    SalesReceipt objSalRcptDetails = new SalesReceipt();
        //                    objSalRcptDetails.BusinessId = BusinessId;
        //                    objSalRcptDetails.CreatedByUserId = UserId;

        //                    objSalRcptDetails.ReceiptNum = fc[Receipt];
        //                    objSalRcptDetails.PaymentMethod = fc[PaymentMethod];
        //                    objSalRcptDetails.Date = DateTime.Parse(fc[Date]);
        //                    objSalRcptDetails.Customer = fc[Customers];
        //                    objSalRcptDetails.Amount = decimal.Parse(fc[Amount]);
        //                    objSalRcptDetails.Description = fc[Description];

        //                    // objSalRcptDetails.Add(objSalRcptDetails);
        //                }
        //            }
        //        }
        //        // SalRcpt.SalesReceipt = lstSalRcptDetails;
        //        BAL.Invoices.SaveInvoiceReceipts(SalRcpt);
        //        // PurchaseInvoice invoiceNew = new PurchaseInvoice();
        //        // invoiceNew.BusinessID = BusinessId;
        //        // invoiceNew.CreatedByUserId = UserId;
        //        //return RedirectToAction("AddPurchaseInvoice", invoiceNew);
        //        return RedirectToAction("Index");
        //    }
        //    return View(SalRcpt);
        //}
    }
}
