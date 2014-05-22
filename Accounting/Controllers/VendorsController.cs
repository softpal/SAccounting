using System;
using System.Linq;
using System.Web.Mvc;
using StratusAccounting.Models;
using System.Web;
using System.IO;

namespace StratusAccounting.Controllers
{
    using System.Collections.Generic;

    public class VendorsController : BasicController
    {
        //
        // GET: /Vendors/

        public ActionResult UserVendors()
        {
            //ViewBag.AssetTypes = BAL.Assets.GetAssetsTypeList(true);
            //ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(true), "AssetTypeId", "AssetName", "--select");
            //ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(true), "AssetDispositionId", "AssetDispositionName", "--select--");
            ViewBag.Vendors = new SelectList(BAL.Vendors.GetVendorsList(BusinessId), "UserVendorId", "VendorFirstName", "--select--");
            //ViewBag.AssetsToEmployee = BAL.Assets.GetBusinessAssetsToEmployee(this.BusinessId, assetId.Value, this.UserId);
            int pageNo = 1;// Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = 5;// Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = "";// Session["sortColumn"].ToString();
            string sortDirection = "Desc";// Session["sortDirection"].ToString();
            var vendors = BAL.Vendors.GetUserVendors(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            //var asset = assets.FirstOrDefault(a => a.AssetsId == assetId);
            //var vendor = vendors.FirstOrDefault(v => v.UserVendorId == userVendorId);



            //ViewBag.Files = GetVendorsById(vendors);
            return View(vendors);
        }

        private static List<string> GetVendorsById(UserVendor vendor)
        {
            List<string> files = new List<string>();
            if (vendor != null)
            {
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserVendors", vendor.UserVendorId.ToString());

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

        public ActionResult NewVendor()
        {
            //var acct = BindData(Add_Vendor.Mst_BankAccountTypes, productId);
            //if (acct == null)
            //    return HttpNotFound();

            ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
            ViewBag.PaymentType = new SelectList(BAL.BankAccounts.GetPaymentTypes(), "PaymentTypesId", "PaymentDesc", "--select--");
            return View("NewVendor");
        }
        [HttpPost]
        public ActionResult Add_Vendor(Add_Vendor vendor)
        {
            try
            {
                vendor.UserId = UserId;
                vendor.BusinessID = BusinessId;

                BAL.Vendors.InsertVendors(vendor);
            }

            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            //
            ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
            ViewBag.PaymentType = new SelectList(BAL.BankAccounts.GetPaymentTypes(), "PaymentTypesId", "PaymentDesc", "--select--");
            return View("NewVendor");
        }

        public ActionResult UserVendorList()
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
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId,pageNo,pageSize,sortDirection,sortColumn);
            //ViewBag.Assets = assets;
            //return View(assets);//db.Assets.Where(service => service.BusinessID.Equals(this.BusinessId) && service.IsActive.Equals(true)));
            return View();
        }



        [HttpPost]
        public ActionResult UserVendors(UserVendor vendor)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = string.Empty;
                var directory = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory, "Vendors", vendor.UserVendorId.ToString());
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                savedFileName = Path.Combine(directory,
                      Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

            }
            vendor.BusinessID = BusinessId;
            vendor.CreatedByUserId = UserId;
            vendor.CreatedDate = DateTime.Now;
            try
            {
                BAL.Vendors.SaveVendors(vendor);
            }

            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            return View("UserVendorList");
        }

        #region Vendors

        //
        // GET: /Organization/BankAccounts

        public ActionResult Vendors()
        {
            //ViewBag.VendorsTypeList = new SelectList(BAL.Vendors.GetVendors(BusinessId), "", "","---Select ---");
            ViewBag.VendorsList = BAL.Vendors.GetVendors(BusinessId, UserId).ToList();
            //ViewBag.UserVendors = new SelectList(BAL.Products.GetUserParentAccountTypes(BusinessId), "UserParentAccountsId", "AccountName", "--select--");
            //ViewBag.UserVendors = new SelectList(BAL.Vendors.GetVendors(BusinessId, UserId), "UserParentAccountsId", "AccountName", "--select--");
            return View();
        }

        [HttpPost]
        public ActionResult Vendors(UserVendor vendor)
        {
            try
            {
                vendor.BusinessID = BusinessId;
                vendor.CreatedByUserId = UserId;
                vendor.CreatedDate = DateTime.Now;
                try
                {
                    BAL.Vendors.SaveVendors(vendor);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
                //ViewBag.VendorsTypeList = new SelectList(BAL.Vendors.GetVendors(BusinessId), "", "","---Select ---");
                ViewBag.VendorsList = BAL.Vendors.GetVendors(BusinessId, UserId).ToList();
                return RedirectToAction("Vendors");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View(vendor);
        }

        [HttpPost]
        public ActionResult EditProject(UserVendor vendor)
        {
            try
            {
                vendor.ModifiedByUserId = UserId;
                vendor.BusinessID = BusinessId;
                BAL.Vendors.SaveVendors(vendor);
                //ViewBag.VendorsTypeList = new SelectList(BAL.Vendors.GetVendors(BusinessId), "", "","---Select ---");
                ViewBag.VendorsList = BAL.Vendors.GetVendors(BusinessId, UserId).ToList();
                return RedirectToAction("Vendors");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View(vendor);
        }

        public ActionResult EditProject(int? userVendorId)
        {
            var vendors = BAL.Vendors.GetVendors(BusinessId, UserId);
            var vendor = vendors.FirstOrDefault(v => v.UserVendorId == userVendorId);
            if (vendor == null)
                return HttpNotFound();

            //ViewBag.VendorsTypeList = new SelectList(BAL.Vendors.GetVendors(BusinessId), "", "","---Select ---");
            ViewBag.VendorList = vendors;
            return View("Projects", vendors);
        }

        [HttpPost]
        public ActionResult DeleteVendor(long userVendorId)
        {
            BAL.Vendors.DeleteVendor(BusinessId, UserId, userVendorId);
            return Json("success");
        }

        public JsonResult BindSearchVendors(string prefix)
        {
            var vendors = BAL.Vendors.SearchVendors(BusinessId, UserId, prefix).ToList();
            return Json(vendors);
        }

        #endregion

        #region Purchase Order

        int pageNo = 1;
        int pageSize = 5;// 10;
        string sortColumn = "Description";
        string sortDirection = "Desc";
        public ActionResult PurchaseOrders()
        {
            ViewBag.sortDirection = "Desc";
            var pOrders = BAL.PurchaseOrders.GetPurchaseOrders(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.PurchaseOrders = pOrders;
            Session["dsPoCreate"] = null;
            return View(pOrders);
        }

        public JsonResult GetPurchaseOrders(int pageNum)
        {
            //string sortColumn = Session["sortColumn"].ToString();
            //string sortDirection = Session["sortDirection"].ToString();
            var pOrders = BAL.PurchaseOrders.GetPurchaseOrders(BusinessId, UserId, pageNum, 5, sortDirection, sortColumn);
            return Json(pOrders);
        }

        [HttpGet]
        public ActionResult POCreate()
        {
            Session["UploadFiles"] = null;
            ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");
            var objPurchase = new PurchaseOrder_DTO
            {
                PurchaseOrder = new PurchaseOrder(),
                PurchaseOrderDetail = new List<PurchaseOrderDetail> { new PurchaseOrderDetail() }
            };
            return View(objPurchase);
        }

        public ActionResult BlankEditorRow()
        {
            ViewBag.BusinessItems = new SelectList(BAL.PurchaseOrders.GetBusinessItems(this.BusinessId, this.UserId), "ItemId", "Title", "--select--");

            return PartialView("Transactions/PurchaseOrderDetails");
        }

        [HttpPost]
        public ActionResult POCreate(PurchaseOrder_DTO purchaseOrder, string[] aryFilePath, FormCollection fc)
        {
            try
            {
                string POTitle = fc["PurchaseOrder.POTitle"];
                string UserVendor = fc["txtVendor"];
                string ExpiryDate = fc["txtExpiry"];
                string index = "";
                int UserVendorId = 0;
                if (fc["PurchaseOrder.UserVendorId"] != null)
                    UserVendorId = Convert.ToInt32(fc["PurchaseOrder.UserVendorId"]);
                short StatusId = 1;
                decimal dAmount = 0;
                List<PurchaseOrderDetail> lstPODetails = new List<PurchaseOrderDetail>();
                if (fc["PurchaseOrderDetail.index"] != null)
                {
                    index = fc["PurchaseOrderDetail.index"];
                    string[] indexKeys = index.Split(',');
                    for (int i = 0; i < indexKeys.Length; i++)
                    {
                        string ItemId = "PurchaseOrderDetail[" + indexKeys[i] + "].ItemId";
                        string Description = "PurchaseOrderDetail[" + indexKeys[i] + "].Description";
                        string Quantity = "PurchaseOrderDetail[" + indexKeys[i] + "].Quantity";
                        string Amount = "PurchaseOrderDetail[" + indexKeys[i] + "].Amount";
                        string Tax = "PurchaseOrderDetail[" + indexKeys[i] + "].Tax";
                        if (!string.IsNullOrEmpty(fc[ItemId]) && !string.IsNullOrEmpty(fc[Quantity]) && !string.IsNullOrEmpty(fc[Amount]))
                        {
                            if (int.Parse(fc[Quantity]) > 0)
                            {
                                PurchaseOrderDetail objPODetails = new PurchaseOrderDetail();
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
                purchaseOrder.PurchaseOrderDetail = lstPODetails;
                PurchaseOrder objPO = new PurchaseOrder();
                objPO.BusinessID = BusinessId;
                objPO.CreatedByUserId = UserId;
                objPO.POTitle = POTitle;
                objPO.Amount = dAmount;
                objPO.UserVendorId = UserVendorId;
                objPO.ExpiryDate = Convert.ToDateTime(ExpiryDate);
                objPO.StatusId = StatusId;
                purchaseOrder.PurchaseOrder = objPO;
                if (purchaseOrder != null)
                {
                    BAL.PurchaseOrders.SavePurchaseOrder(purchaseOrder);
                    //if (Session["UploadFiles"] != null)
                    //{
                    //    string strFiles = "";
                    //    strFiles = (string)Session["UploadFiles"];

                    //    aryFilePath = strFiles.Split('|');
                    //    SaveDocuments(aryFilePath);
                    //}
                }
                return RedirectToAction("POCreate");

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        //[HttpPost]
        //public ActionResult SavePurchaseOrders(string[] pOrder, string[][] pDetails, string[] aryFilePath)
        //{
        //    try
        //    {
        //        bool result = false;
        //        var objPurchase = new PurchaseOrder
        //        {
        //            BusinessID = BusinessId,
        //            CreatedByUserId = UserId,
        //            POTitle = pOrder[0],
        //            UserVendorId = Convert.ToInt64(pOrder[1]),
        //            PONumber = pOrder[2]
        //        };
        //        DateTime d;
        //        if (DateTime.TryParseExact(pOrder[3], "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
        //        {
        //            objPurchase.ExpiryDate = d;
        //        }

        //        decimal amount = 0;
        //        //objPurchase.ExpiryDate = Convert.ToDateTime(pOrder[3]);
        //        objPurchase.StatusId = 1; // need to know which field is status

        //        var pList = new List<PurchaseOrderDetail>();
        //        foreach (string[] item in pDetails)
        //        {
        //            var objPurchaseOrder = new PurchaseOrderDetail();
        //            objPurchaseOrder.BusinessId = BusinessId;
        //            objPurchaseOrder.CreatedByUserId = UserId;
        //            objPurchaseOrder.ItemId = Convert.ToInt64(item[0]);// need to know which field is status
        //            objPurchaseOrder.Description = item[1];
        //            objPurchaseOrder.Quantity = Convert.ToInt32(item[2]);
        //            objPurchaseOrder.Amount = Convert.ToDecimal(item[3]);
        //            objPurchaseOrder.Tax = Convert.ToDecimal(item[4]);
        //            objPurchaseOrder.TaxPercent = ((objPurchaseOrder.Tax * 100) / objPurchaseOrder.Amount);
        //            amount += Convert.ToDecimal(item[3]);
        //            pList.Add(objPurchaseOrder);
        //        }

        //        objPurchase.Amount = amount;
        //        var files = new List<string>();
        //        if (aryFilePath != null)
        //            {
        //            files.AddRange(aryFilePath);
        //            Guid fileGuid = Guid.NewGuid();
        //            var path = AppDomain.CurrentDomain.BaseDirectory;
        //            if (!Directory.Exists(path + "Documents"))
        //            {
        //                DirectoryInfo di = Directory.CreateDirectory(path + "Documents");
        //            }
        //            path += "Documents\\" + fileGuid;
        //            try
        //            {
        //                    // Try to create the directory.
        //                    DirectoryInfo di = Directory.CreateDirectory(path);
        //                    foreach (string file in files)
        //                    {
        //                        string fileName = Path.GetFileName(file);
        //                        string location = Path.Combine(path, fileName);
        //                    byte[] input = null;
        //                    var newFile = new FileStream(location, FileMode.Create);
        //                        // Write data to the file
        //                    using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        //                        {
        //                        input = new Byte[fs.Length];
        //                            int length = Convert.ToInt32(fs.Length);
        //                        fs.Read(input, 0, length);
        //                        }
        //                    newFile.Write(input, 0, input.Length);
        //                        // Close file
        //                        newFile.Close();
        //                    }
        //            }
        //            catch (Exception e)
        //            {
        //                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
        //            }
        //        }

        // int purchaseId = BAL.PurchaseOrders.SavePurchaseOrder(objPurchase);

        //        foreach (PurchaseOrderDetail item in pList)
        //        {
        //            result = false; //BAL.PurchaseOrders.SavePurchaseOrderDetails(purchaseID, item);
        //        }

        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(false);
        //    }
        //}

        public JsonResult GetVendorsList(string keyword)
        {
            return Json(BAL.Vendors.GetVendorsList(BusinessId, keyword), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUploadFiles(string aryFilePath)
        {
            Session["UploadFiles"] = aryFilePath;
            return Json("");
        }


        public void SaveDocuments(string[] aryFilePath)
        {
            List<string> files = new List<string>();
            List<Document> lstDocuments = new List<Document>();

            if (aryFilePath != null)
            {
                foreach (string item in aryFilePath)
                {
                    if (item != null && item != "")
                        files.Add(item);
                }

                Guid fileGuid = Guid.NewGuid();

                var path = AppDomain.CurrentDomain.BaseDirectory;

                if (Directory.Exists(path + "Documents"))
                {

                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path + "Documents");
                }

                path += "Documents\\" + fileGuid.ToString();

                try
                {
                    // Determine whether the directory exists. 
                    if (Directory.Exists(path))
                    {

                    }
                    else
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(path);
                        foreach (string file in files)
                        {
                            string fileName = Path.GetFileName(file);
                            string location = Path.Combine(path, fileName);
                            byte[] Input = null;


                            Document objDoc = new Document();
                            objDoc.BusinessID = BusinessId;
                            objDoc.CreatedByUserId = UserId;
                            //objDoc.FileGUID = fileGuid.ToString();
                            //objDoc.FileName = fileName;
                            objDoc.FilePath = location;
                            objDoc.ScreenId = (short)MasterScreens.NewPO;
                            objDoc.Description = null;
                            lstDocuments.Add(objDoc);

                            System.IO.Stream myStream;

                            System.IO.FileStream newFile = new System.IO.FileStream(location, System.IO.FileMode.Create);

                            // Write data to the file
                            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                            {
                                Input = new Byte[fs.Length];
                                int length = Convert.ToInt32(fs.Length);
                                fs.Read(Input, 0, length);
                            }

                            newFile.Write(Input, 0, Input.Length);
                            // Close file
                            newFile.Close();
                        }

                        if (lstDocuments.Count > 0)
                        {
                            //BAL.UploadDocuments.UploadMultipleFiles(lstDocuments);
                        }
                    }
                }
                catch (Exception e)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
        }
        #endregion

    }
}
