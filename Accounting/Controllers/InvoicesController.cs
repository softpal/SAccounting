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

        #region "Invoice List"
        int pageNo = 1;
        int pageSize = 5;// 10;
        string sortColumn = "DueDate";
        string sortDirection = "Desc";
        public ActionResult Index()
        {

            var invoice = BAL.Invoices.GetInvoices(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
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

            //ViewBag.PurchaseProduct = new SelectList(_db.PurchaseOrders, "BusinessID", "BusinessName", BusinessId);
            //ViewBag.InvoiceTerm = new SelectList(_db.Mst_InvoiceDueTerm, "Term", "TermName", BusinessId);
            //ViewBag.PurchaseDetails = new SelectList(_db.PurchaseInvoiceDetails, BusinessId);

            ViewBag.PurchaseProduct = new SelectList(BAL.PurchaseOrders.GetBusinessItems(BusinessId, UserId), "ItemId", "Title", "--select--");
            //  ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            ViewBag.POServiceNum = new SelectList(BAL.PurchaseOrders.GetPODetailsByVendorId(0, 0, 0), "", "", "--Select--");
            ViewBag.InvoiceDueTerms = new SelectList(BAL.Invoices.GetInvoiceDueTerms(), "DueTermId", "Term", "--Select--");
            PurchaseInvoiceDetail objInvDetails = new PurchaseInvoiceDetail();
            //objInvDetails.ItemId = 0;
            //objInvDetails.Description = "";
            //objInvDetails.Quantity = "";
            invoice = new PurchaseInvoice
            {
                PurchaseOrder = new PurchaseOrder(),
                PurchaseInvoiceDetail = objInvDetails,
                PurchaseInvoiceDetails = new List<PurchaseInvoiceDetail> { new PurchaseInvoiceDetail() }
            };
            //  return View(objInvoice);
            PurchaseOrder objPO = new PurchaseOrder();
            return View(invoice);
        }

        [HttpPost]
        public ActionResult AddPurchaseInvoice(PurchaseInvoice invoice, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
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
                                objPODetails.Tax = decimal.Parse(fc[Tax]);
                                lstPODetails.Add(objPODetails);
                                dAmount += (int.Parse(fc[Quantity]) * decimal.Parse(fc[Amount])) + decimal.Parse(fc[Tax]);
                            }
                        }
                    }
                }
                invoice.PurchaseInvoiceDetails = lstPODetails;
                invoice.Amount = dAmount;
                BAL.Invoices.SavePurchaseInvoice(invoice);
                // PurchaseInvoice invoiceNew = new PurchaseInvoice();
                // invoiceNew.BusinessID = BusinessId;
                // invoiceNew.CreatedByUserId = UserId;
                //return RedirectToAction("AddPurchaseInvoice", invoiceNew);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        public JsonResult GetVendorsList(string keyword, string val)
        {
            if (val == "option1")
            {
                return Json(BAL.Vendors.GetVendorsList(BusinessId, keyword), JsonRequestBehavior.AllowGet);
            }
            else
            { return Json("", JsonRequestBehavior.AllowGet); }
        }

        public JsonResult GetPODetails(string vendorId, string type)
        {
            if (type == "option1")
            {
                return Json(BAL.PurchaseOrders.GetPODetailsByVendorId(BusinessId, UserId, Convert.ToInt32(vendorId)), JsonRequestBehavior.AllowGet);
            }
            else
            { return Json("", JsonRequestBehavior.AllowGet); }
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
                objPoInvDetail.Tax = Convert.ToDecimal(tax);

            return PartialView("EditorTemplates/PurchaseInvoiceDetails", objPoInvDetail);
        }

        public JsonResult GetPurchaseOrderDetailsByPOID(string vendorId, string poId)
        {
            return Json(BAL.PurchaseOrders.GetPurchaseOrderDetailsByPOID(BusinessId, UserId, Convert.ToInt32(vendorId), Convert.ToInt32(poId)), JsonRequestBehavior.AllowGet);
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

        #region "Payments or Add Payments"

        public ActionResult Payments(PurchaseInvoicePayment objPayment)
        {

            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);

            ViewBag.GetInstrumentTypes = new SelectList(BAL.Invoices.GetInstruments(), "PaymentTypesId", "PaymentDesc", "--Select--");
            ViewBag.GetPaymentStatusTypes = new SelectList(BAL.Invoices.GetStatuses(), "PaymentStatusId", "PaymentDesc", "--Select--");
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
                BAL.Invoices.SaveInvoicePayment(objPayment);
            }
            PurchaseInvoicePayment objPaynt = new PurchaseInvoicePayment();
            return RedirectToAction("Payments", objPaynt);
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



    }
}
