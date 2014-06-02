using System;
using System.Linq;
using System.Web.Mvc;
using StratusAccounting.Models;
using StratusAccounting.BAL;
using System.Web;
using System.IO;
using System.Text;

namespace StratusAccounting.Controllers
{
    using Intuit.Ipp.Core;
    using Intuit.Ipp.DataService;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Security;
    using System.Collections.Generic;

    [Authorize]
    public class OrganizationController : BasicController
    {
        // private readonly AccountingEntities _db = new AccountingEntities();

        #region QuickBooks

        public ActionResult QBTestDemo()
        {
            string accessToken = "";
            string accessTokenSecret = "";
            string consumerKey = "qyprdqAXeMk94NeZb2dO6Yp2xS2KTh";  //OAuth consumerKey
            string consumerSecret = "55DUoMnMk8MU7FLuc82dfulqkLw72zSRqAzAJaDj"; //OAuth consumerSecret
            //string appID = "b7psikxw9r";
            OAuthRequestValidator oauthValidator = new OAuthRequestValidator(
            accessToken, accessTokenSecret, consumerKey, consumerSecret);

            string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
            string companyID = "1204390255"; //string companyName = "dotsoft";
            ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oauthValidator);
            DataService service = new DataService(context);
            //Create the Data Object
            Customer customer = new Customer();
            //Mandatory Fields
            customer.GivenName = "Bhaskar";
            customer.Title = "Mr.";
            customer.MiddleName = "Jayne";
            customer.FamilyName = "Cooper";

            //Call the Service  .....Add()
            Customer resultCustomer = service.Add(customer) as Customer;

            //Call the Service........... Update()
            //Customer resultCustomer = service.Update(customer) as Customer;

            return View();
        }



        public ActionResult DisplayCustomers()
        {
            string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
            string companyID = "1204390255";
            OAuthRequestValidator oAuthReq = new OAuthRequestValidator(companyID);
            ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
            DataService service = new DataService(context);
            Customer customer = new Customer();
            Customer resultCustomer = service.FindById(customer) as Customer;
            return PartialView(resultCustomer);
        }

        public ActionResult DisplayVendors()
        {
            string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
            string companyID = "1204390255";
            ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD);
            DataService service = new DataService(context);
            Vendor vendor = new Vendor();
            Vendor resultCustomer = service.FindById(vendor) as Vendor;
            return PartialView(resultCustomer);
        }

        //public ActionResult GetQuickCustomer(OAuthRequestValidator oAuthReq)
        //{
        //    //ViewBag.CustomerList = 
        //    string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
        //    string companyID = "1204390255";
        //    ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
        //    DataService service = new DataService(context);
        //    Customer customer = new Customer();
        //    Customer resultCustomer = service.FindById(customer) as Customer;
        //    return PartialView(resultCustomer);
        //}

        //public ActionResult AddQuickBooks(OAuthRequestValidator oAuthReq)
        //{
        //    //int appToken;   //int companyID;
        //    string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
        //    string companyID = "1204390255";
        //    ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
        //    DataService service = new DataService(context);
        //    Customer customer = new Customer();
        //    //customer.
        //    Vendor vendor = new Vendor();
        //    Customer resultCustomer = service.Add(customer) as Customer;
        //    return View(resultCustomer);
        //}

        //public ActionResult UpdateQuickBooks(OAuthRequestValidator oAuthReq)
        //{
        //    //int appToken;   //int companyID;
        //    string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
        //    string companyID = "1204390255";
        //    ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
        //    DataService service = new DataService(context);
        //    Customer customer = new Customer();
        //    Customer resultCustomer = service.Update(customer) as Customer;
        //    return View(resultCustomer);
        //}

        //public ActionResult FindByIdQuickBooks(OAuthRequestValidator oAuthReq)
        //{
        //    //int appToken;   //int companyID;
        //    string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
        //    string companyID = "1204390255";
        //    ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
        //    DataService service = new DataService(context);
        //    Customer customer = new Customer();
        //    Customer resultCustomer = service.FindById(customer) as Customer;
        //    return View(resultCustomer);
        //}

        //public ActionResult FindAllQuickBooks(OAuthRequestValidator oAuthReq)
        //{
        //    //int appToken;   //int companyID;
        //    string appToken = "26af6e9bb2908b4960bbb02bd281e40fcb64";
        //    string companyID = "1204390255";
        //    ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oAuthReq);
        //    DataService service = new DataService(context);
        //    Customer customer = new Customer();

        //    int startPosition = 1;
        //    int maxResult = 10;

        //    List<Customer> customers = service.FindAll(customer, startPosition, maxResult).ToList<Customer>();
        //    //service.Delete(customer);
        //    //service.Void(customer);

        //    return View(customers);
        //}

        #endregion

        #region BankAccounts

        //
        // GET: /Organization/BankAccounts

        public ActionResult BankAccounts()
        {
            ViewBag.BanksList = BAL.BankAccounts.GetBankAccount(BusinessId, UserId);
            ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
            return View();
        }

        [HttpPost]
        public ActionResult BankAccounts(UserBankAccount bankAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bankAccount.BusinessID = BusinessId;
                    bankAccount.UserId = UserId;
                    bankAccount.CreatedByUserId = UserId;
                    bankAccount.UserTypeId = (short)UserType.Business;
                    //
                    BAL.BankAccounts.SaveBankAccounts(bankAccount);
                    ViewBag.BanksList = BAL.BankAccounts.GetBankAccount(BusinessId, UserId);
                    ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
                    return RedirectToAction("BankAccounts");
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View("BankAccounts", bankAccount);
        }

        [HttpPost]
        public ActionResult EditBankAccounts(UserBankAccount bankAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bankAccount.UserId = UserId;
                    bankAccount.BusinessID = BusinessId;
                    bankAccount.UserTypeId = (short)UserType.Business;
                    bankAccount.ModifiedByUserId = UserId;
                    bankAccount.ModifiedDate = DateTime.Now;
                    BAL.BankAccounts.SaveBankAccounts(bankAccount);

                    ViewBag.BanksList = BAL.BankAccounts.GetBankAccount(BusinessId, UserId);
                    ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
                    return RedirectToAction("BankAccounts");//, bankAccount);
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            ViewBag.BanksList = BAL.BankAccounts.GetBankAccount(BusinessId, UserId);
            ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
            return View("BankAccounts", bankAccount);
        }

        public ActionResult EditBankAccounts(int bankAccountId = 0)
        {
            UserBankAccount bankAccount = BAL.BankAccounts.GetBankAccount(BusinessId, UserId).FirstOrDefault(b => b.BankAccountsId == bankAccountId);
            if (bankAccount == null)
                return HttpNotFound();
            ViewBag.BanksList = BAL.BankAccounts.GetBankAccount(BusinessId, UserId);
            ViewBag.AccountType = new SelectList(BAL.BankAccounts.GetBankAccountTypes(), "BankAccountTypeId", "AccountTypeDesc", "--select--");
            return View("BankAccounts", bankAccount);
        }

        [HttpPost]
        public ActionResult DeleteBankAccount(long bankAccountId)
        {
            BAL.BankAccounts.DeleteBankAccount(BusinessId, UserId, bankAccountId);
            return Json("success");
        }

        #endregion

        #region common methods

        private UMst_BusinessItems BindData(BusinessItemsTypes businessItemsTypes, int itemTypeId = 0)
        {
            ViewBag.AccountTypeList = new SelectList(BAL.Products.GetUserAccountTypes(BusinessId), "UserAccountsId", "AccountName", "--select--");
            var list = BAL.BusinessItemTypes.GetBusinessItems(BusinessId, UserId,
                businessItemsTypes);
            ViewBag.BusinessItems = list;
            ViewBag.ParentAccounts = new SelectList(BAL.Products.GetUserParentAccountTypes(BusinessId), "UserParentAccountsId", "AccountName", "--select--");
            if (itemTypeId > 0)
            {
                return list.FirstOrDefault(bItem => bItem.ItemId == itemTypeId);
            }
            return null;
        }

        #endregion

        #region Services
        //
        // GET: /Organization/Services

        public ActionResult Services()
        {
            BindData(BusinessItemsTypes.Services);
            return View();
        }

        [HttpPost]
        public ActionResult Services(UMst_BusinessItems service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.BusinessID = BusinessId;
                    service.CreatedByUserId = UserId;
                    service.CreatedDate = DateTime.Now;
                    service.ItemTypeId = Convert.ToInt16(BusinessItemsTypes.Services);
                    try
                    {
                        BAL.BusinessItemTypes.SaveorEditBusinessItems(service);
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            BindData(BusinessItemsTypes.Services);
            return RedirectToAction("Services");
        }

        //Get Service
        // Organization/EditService
        public ActionResult EditService(int serviceId)
        {
            UMst_BusinessItems service = BindData(BusinessItemsTypes.Services, serviceId);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View("Services", service);
        }

        //Get Service
        // Organization/EditService
        [HttpPost]
        public ActionResult EditService(UMst_BusinessItems service)
        {
            try
            {
                service.ModifiedByUserId = UserId;
                service.BusinessID = BusinessId;
                service.ModifiedDate = DateTime.Now;

                bool chkStatus = BusinessItemTypes.SaveorEditBusinessItems(service);
                if (!chkStatus)
                {
                    ModelState.AddModelError("", "Update was failed");
                }
                BindData(BusinessItemsTypes.Services);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Update was failed");
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return RedirectToAction("Services");//, service);
        }

        [HttpPost]
        public ActionResult Delete(long serviceId)
        {
            bool chkStatus = BusinessItemTypes.DeleteBusinessItems(Convert.ToInt32(serviceId), BusinessId, UserId);
            string result = chkStatus == true ? "sucess" : "failed";
            return Json(result);
        }

        public JsonResult BindSearchServices(string keyword)
        {
            //var services = BAL.Services.SearchServices(BusinessId, UserId, keyword).ToList();
            return Json("");
        }

        #endregion

        #region Project

        //
        // GET: /Organization/BankAccounts

        public ActionResult Projects()
        {
            BindData(BusinessItemsTypes.Projects);
            return View();
        }

        [HttpPost]
        public ActionResult Projects(UMst_BusinessItems project)
        {
            try
            {
                project.BusinessID = BusinessId;
                project.CreatedByUserId = UserId;
                project.CreatedDate = DateTime.Now;
                project.ItemTypeId = Convert.ToInt16(BusinessItemsTypes.Projects);
                try
                {
                    bool chkStatue = BusinessItemTypes.SaveorEditBusinessItems(project);
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    ModelState.AddModelError("", ex.Message);
                }
                BindData(BusinessItemsTypes.Projects);
                return RedirectToAction("Projects");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View("Projects", project);
        }

        [HttpPost]
        public ActionResult EditProject(UMst_BusinessItems project)
        {
            try
            {
                project.ModifiedByUserId = UserId;
                project.BusinessID = BusinessId;
                bool chkStatus = BusinessItemTypes.SaveorEditBusinessItems(project);
                BindData(BusinessItemsTypes.Projects);
                return RedirectToAction("Projects");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View("Projects", project);
        }

        public ActionResult EditProject(int projectId)
        {

            var project = BindData(BusinessItemsTypes.Projects, projectId);
            if (project == null)
                return HttpNotFound();
            return View("Projects", project);
        }

        [HttpPost]
        public ActionResult DeleteProject(long projectId)
        {
            bool chkStatus = BusinessItemTypes.DeleteBusinessItems(Convert.ToInt32(projectId), BusinessId, UserId);
            string result = chkStatus == true ? "sucess" : "failed";
            return Json(result);
        }

        public JsonResult BindSearchProjects(string prefix)
        {
            //var projects = BAL.Projects.SearchProjects(BusinessId, UserId, prefix).ToList();
            return Json("");
        }

        #endregion

        #region Products

        //
        // GET: /Organization/BankAccounts

        public ActionResult Products()
        {
            BindData(BusinessItemsTypes.Products);
            return View();
        }

        [HttpPost]
        public ActionResult Products(UMst_BusinessItems product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.BusinessID = BusinessId;
                    product.CreatedByUserId = UserId;
                    product.CreatedDate = DateTime.Now;
                    product.ItemTypeId = Convert.ToInt16(BusinessItemsTypes.Products);
                    product.IsActive = true;
                    bool chkstatus = BAL.BusinessItemTypes.SaveorEditBusinessItems(product);
                    if (chkstatus)
                        return RedirectToAction("Products");//, product);
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while saving data");
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(UMst_BusinessItems product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.BusinessID = BusinessId;
                    product.ModifiedByUserId = UserId;
                    product.ModifiedDate = DateTime.Now;
                    bool chkstatus = BAL.BusinessItemTypes.SaveorEditBusinessItems(product);
                    if (chkstatus)
                        return RedirectToAction("Products");//, product);
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while saving data");
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            BindData(BusinessItemsTypes.Products);
            return View("Products", product);
        }

        public ActionResult EditProduct(int productId)
        {
            var product = BindData(BusinessItemsTypes.Products, productId);
            if (product == null)
                return HttpNotFound();
            return View("Products", product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(long productId)
        {
            BusinessItemTypes.DeleteBusinessItems(Convert.ToInt32(productId), BusinessId, UserId);
            return Json("success");
        }

        public JsonResult BindSearchProducts(string keyword)
        {
            //var products = BAL.Products.SearchProducts(BusinessId, UserId, keyword).ToList();
            return Json("");
        }

        #endregion

        #region Assets

        //
        // GET: /Organization/Assets

        int pageNo = 1;
        int pageSize = 5;
        string sortColumn = "CreatedDate";
        string sortDirection = "Ascending";
        public ActionResult Assets(int? assetId)
        {

            // AssetsBinding();
            ViewBag.AssetTypes = BAL.Assets.GetAssetsTypeList(BusinessId);
            ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(BusinessId), "AssetTypeId", "AssetName", "--select");
            ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(), "AssetDispositionId", "AssetDispositionName", "--select--");
            ViewBag.Vendors = new SelectList(BAL.Assets.GetVendorsList(BusinessId), "UserVendorId", "VendorFirstName", "--select--");
            //ViewBag.AssetsToEmployee = BAL.Assets.GetBusinessAssetsToEmployee(this.BusinessId, assetId.Value, this.UserId);            
            var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            var asset = assets.FirstOrDefault(a => a.AssetsId == assetId);
            ViewBag.Files = GetAssetFilesById(asset);
            return View(asset);
        }

        private static List<string> GetAssetFilesById(Asset asset)
        {
            List<string> files = new List<string>();
            if (asset != null)
            {
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", asset.AssetsId.ToString());


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

        public ActionResult AssetsList()
        {
            Session["pageNo"] = 1;
            Session["pageSize"] = 5;// 10;
            Session["sortColumn"] = "Description";
            Session["sortDirection"] = "Desc";
            ViewBag.sortDirection = "Desc";
            ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(BusinessId), "AssetTypeId", "AssetName", "--select");
            ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(), "AssetDispositionId", "AssetDispositionName", "--select--");
            //ViewBag.Vendors = new SelectList(BAL.Assets.GetVendorsList(BusinessId), "UserVendorId", "VendorFirstName", "--select--");
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            ViewBag.Assets = assets;
            return View(assets);//db.Assets.Where(service => service.BusinessID.Equals(this.BusinessId) && service.IsActive.Equals(true)));
            //return View();
        }

        [HttpPost]
        public ActionResult Assets(Asset asset)
        {
            //if (ModelState.IsValid)
            //{
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = string.Empty;
                var directory = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory, "Assets", asset.AssetsId.ToString());
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                savedFileName = Path.Combine(directory,
                      Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

            }
            asset.BusinessID = BusinessId;
            asset.CreatedByUserId = UserId;
            asset.CreatedDate = DateTime.Now;
            try
            {
                BAL.Assets.SaveAssets(asset);
            }

            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            //return View("AssetsList",assets);
            return View("AssetsList");
        }

        [HttpPost]
        public ActionResult EditAsset(Asset asset, HttpPostedFileBase postedFile)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory,
                   Path.GetFileName(hpf.FileName));

            }
            asset.BusinessID = BusinessId;
            asset.ModifiedByUserId = UserId;
            asset.ModifiedDate = DateTime.Now;
            try
            {
                BAL.Assets.SaveAssets(asset);

                ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(BusinessId), "AssetTypeId", "AssetName", "--select");
                ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(), "AssetDispositionId", "AssetDispositionName", "--select--");
                ViewBag.Vendors = new SelectList(BAL.Assets.GetVendorsList(BusinessId), "UserVendorId", "VendorFirstName", "--select--");
            }


            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            return View("AssetsList");
        }

        public ActionResult EditAsset(int? assetId)
        {
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            //var asset = assets.FirstOrDefault(a => a.AssetsId == assetId);
            //if (asset == null)
            //    return HttpNotFound();
            ViewBag.AssetTypes = BAL.Assets.GetAssetsTypeList(BusinessId);
            //ViewBag.AssetsToEmployee = BAL.Assets.GetBusinessAssetsToEmployee(this.BusinessId, assetId.Value, this.UserId);
            ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(BusinessId), "AssetTypeId", "AssetName", "--select");
            ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(), "AssetDispositionId", "AssetDispositionName", "--select--");
            ViewBag.Vendors = new SelectList(BAL.Assets.GetVendorsList(BusinessId), "UserVendorId", "VendorFirstName", "--select--");
            //AssetsBinding();
            //ViewBag.Files = GetAssetFilesById(asset);
            //return View("Assets", asset);
            return View();
        }

        public ActionResult DeleteAsset(int? assetId)
        {
            //BAL.Assets.DeleteAsset(this.BusinessId, this.UserId, assetId.Value);
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            //return View("AssetsList",assets);
            return View("AssetsList");
        }

        public JsonResult GetAssets(int pageNum)
        {
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            ViewBag.AssestsTypeList = new SelectList(BAL.Assets.GetAssetsTypeList(BusinessId), "AssetTypeId", "AssetName", "--select");
            ViewBag.FDisposition = new SelectList(BAL.Assets.GetAssetsDispositionList(), "AssetDispositionId", "AssetDispositionName", "--select--");
            var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNum, 5, sortDirection, sortColumn).ToList();
            return Json(assets);
        }

        [HttpPost]
        public ActionResult DeleteAssetType(int? assetTypeId)
        {
            //BAL.Assets.DeleteAssetType(BusinessId, UserId,assetTypeId.Value);
            return Json("success");
        }

        [HttpPost]
        public ActionResult DeleteAssetsToEmployee(int? employeeId, int? assetId)
        {
            //BAL.Assets.DeleteBusinessAssetsToEmployee(assetId.Value,BusinessId, UserId,employeeId.Value);
            return Json("success");
        }
        public ActionResult SortAssets(string column, string direction)
        {
            Session["sortColumn"] = column;
            Session["sortDirection"] = direction;
            if (direction == "asc")
            {
                ViewBag.sortDirection = "dsc";
            }
            else
            {
                ViewBag.sortDirection = "asc";
            }
            int pageNo = Convert.ToInt32(Session["pageNo"].ToString());
            int pageSize = Convert.ToInt32(Session["pageSize"].ToString());
            string sortColumn = Session["sortColumn"].ToString();
            string sortDirection = Session["sortDirection"].ToString();
            //var assets = BAL.Assets.GetAssets(BusinessId, UserId, pageNo, pageSize, sortDirection, sortColumn);
            //return View("AssetsList", assets);
            return View("AssetsList");
        }

        [HttpPost]
        public ActionResult SaveAssets(long assetId, string purchaseDt, string description, long vendorId, int initialValue, short assetTypeId, string locatedAt, string poNumber, short assetDispositionId, string glAccountCode, string depreciationGLAccountCode, int currentValue)
        {
            Asset objAsset = new Asset();
            objAsset.BusinessID = BusinessId;
            objAsset.CreatedByUserId = UserId;
            objAsset.AssetsId = assetId;
            DateTime d;
            if (DateTime.TryParseExact(purchaseDt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
            {
                objAsset.PurchasedDate = d;
            }
            objAsset.AssetDescription = description;
            objAsset.UserVendorId = vendorId;
            objAsset.InitialValue = initialValue;
            objAsset.AssetTypeId = assetTypeId;
            objAsset.LocatedAt = locatedAt;
            objAsset.PONumber = poNumber;
            objAsset.AssetDispositionId = assetDispositionId;
            objAsset.GLAccountCode = glAccountCode;
            objAsset.DepreciationGLAccountCode = depreciationGLAccountCode;
            objAsset.CurrentValue = currentValue;
            return Json(BAL.Assets.SaveAssets(objAsset));
        }

        public ActionResult InsertAssetType(string asset)
        {
            List<Mst_AssetType> lstAsset = new List<Mst_AssetType>();
            if (BAL.Assets.InsertAssetType(BusinessId, UserId, asset))
            {
                lstAsset = (BAL.Assets.GetAssetsTypeList(BusinessId)).ToList();
            }     
            return Json(lstAsset);
        }

        #endregion

        #region Preferences

        public ActionResult Preferences()
        {
            int UserId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
            int BusinessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);
            string DateFormat = Convert.ToString(HttpRuntime.Cache["DateFormat"]);
            string NumberFormat = Convert.ToString(HttpRuntime.Cache["NumberFormat"]);
            string FiscalYearFormat = Convert.ToString(HttpRuntime.Cache["FiscalYearFormat"]);
            string PrimaryCurrency = Convert.ToString(HttpRuntime.Cache["PrimaryCurrency"]);
            ViewBag.BusinessType = new SelectList(BAL.Preferences.GetBusinessTypes(), "BusinessTypeId", "BusinessType", "--select--");
            //ViewBag.Countrys = new SelectList(CountryStateCity.GetCountryStateCity(string.Empty, "country", 0)); //new SelectList(BAL.Preferences.GetCountries().Where(item => item.IsActive.Equals(true)), "CountriesId", "Country", "--select--");
            ViewBag.Countrys = new SelectList(CountryStateCity.GetCountryStateCity(string.Empty, "country", 0), "CountryId", "CountryName", "--select--");
            ViewBag.States = new SelectList(CountryStateCity.GetCountryStateCity(string.Empty, "state", 1), "StateId", "StateName", "--select--");
            ViewBag.Cities = new SelectList(CountryStateCity.GetCountryStateCity(string.Empty, "city", 1), "CityId", "CityName", "--select--");
            ViewBag.GreetingEmails = new SelectList(BAL.Preferences.GetGreetingsList(), "GreetingsListId", "GreetingName", "--select--");
            ViewBag.CustomersRename = new SelectList(BAL.Preferences.GetCustomersRename(), "CustomersRenameId", "CustomersRename", "--select--");
            //
            ViewBag.DateFormatPreference = new SelectList(BAL.Preferences.GetPreferenceValues().Where(item => item.PreferenceFieldsId == 1), "PreferenceValuesId", "PreferenceValue", "--select--");// dateformat
            ViewBag.NumberFormat = new SelectList(BAL.Preferences.GetPreferenceValues().Where(item => item.PreferenceFieldsId == 2), "PreferenceValuesId", "PreferenceValue", "--select--");
            ViewBag.FiscalFormat = new SelectList(BAL.Preferences.GetPreferenceValues().Where(item => item.PreferenceFieldsId == 3), "PreferenceValuesId", "PreferenceValue", "--select--");
            ViewBag.Currency = new SelectList(BAL.Preferences.GetPreferenceValues().Where(item => item.PreferenceFieldsId == 4), "PreferenceValuesId", "PreferenceValue", "--select--");

            ////bind data different views
            int businessId = this.BusinessId;
            BusinessPreferences_DTO bpreference = new BusinessPreferences_DTO();
            bpreference.DateFormatPreference = BAL.Preferences.GetBusinessPreferences(this.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(1));
            bpreference.NumberFormatPreference = BAL.Preferences.GetBusinessPreferences(this.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(2));
            bpreference.FiscalYearFormat = BAL.Preferences.GetBusinessPreferences(this.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(3));
            bpreference.PrimaryCurrency = BAL.Preferences.GetBusinessPreferences(this.BusinessId).FirstOrDefault(item => item.PreferenceFieldsId.Equals(4));
            //
            //bpreference.BusiReg = BAL.Preferences.GetBusinessRegistration(this.BusinessId).FirstOrDefault();
            //bpreference.CustomFields = BAL.Preferences.GetCustomizeFields(this.BusinessId).FirstOrDefault();
            bpreference.TaxDetails = BAL.Preferences.GetTaxDetails(this.BusinessId).ToList();
            //bpreference.UserCustFields = BAL.Preferences.GetCustomFields(this.BusinessId).FirstOrDefault();

            return View(bpreference);
        }

        public ActionResult GetGreetingMessages()
        {
            var greetingMsgs = BAL.Preferences.GetGreetingMessages(this.BusinessId);
            return Json(greetingMsgs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Preferences(BusinessPreferences_DTO bPreference)
        {
            try
            {
                //Models.BusinessRegistration reg = bPreference.BusiReg;
                bPreference.BusiReg.ModifiedByUserId = this.UserId;
                bPreference.BusiReg.BusinessID = this.BusinessId;
                bPreference.BusiReg.ModifiedDate = DateTime.Now;
                bPreference.BusiReg.Zip = string.IsNullOrEmpty(bPreference.BusiReg.Zip) ? "001" : bPreference.BusiReg.Zip;
                bPreference.BusiReg.DUNS_ = string.IsNullOrEmpty(bPreference.BusiReg.DUNS_) ? "001" : bPreference.BusiReg.DUNS_;
                bPreference.BusiReg.DBA = string.IsNullOrEmpty(bPreference.BusiReg.DBA) ? "001" : bPreference.BusiReg.DBA;


                foreach (var taxDet in bPreference.TaxDetails)
                {
                    taxDet.ModifiedByUserId = this.UserId;
                    taxDet.BusinessID = this.BusinessId;
                    taxDet.ModifiedDate = DateTime.Now;
                    //db.Entry(taxDet).State = EntityState.Modified;
                }

                #region update preferences
                UMst_BusinessPreferences dateformat = bPreference.DateFormatPreference;
                bPreference.DateFormatPreference.PreferenceFieldsId = 1;
                bPreference.DateFormatPreference.ModifiedByUserId = this.UserId;
                bPreference.DateFormatPreference.BusinessID = this.BusinessId;
                bPreference.DateFormatPreference.ModifiedDate = DateTime.Now;
                //db.Entry(dateformat).State = EntityState.Modified;


                UMst_BusinessPreferences number = bPreference.NumberFormatPreference;
                bPreference.NumberFormatPreference.PreferenceFieldsId = 2;
                bPreference.NumberFormatPreference.ModifiedByUserId = this.UserId;
                bPreference.NumberFormatPreference.BusinessID = this.BusinessId;
                bPreference.NumberFormatPreference.ModifiedDate = DateTime.Now;
                // db.Entry(number).State = EntityState.Modified;

                UMst_BusinessPreferences fiscYear = bPreference.FiscalYearFormat;
                bPreference.FiscalYearFormat.PreferenceFieldsId = 3;
                bPreference.FiscalYearFormat.ModifiedByUserId = this.UserId;
                bPreference.FiscalYearFormat.BusinessID = this.BusinessId;
                bPreference.FiscalYearFormat.ModifiedDate = DateTime.Now;
                // db.Entry(fiscYear).State = EntityState.Modified;

                UMst_BusinessPreferences currency = bPreference.PrimaryCurrency;
                bPreference.PrimaryCurrency.PreferenceFieldsId = 4;
                bPreference.PrimaryCurrency.ModifiedByUserId = this.UserId;
                bPreference.PrimaryCurrency.BusinessID = this.BusinessId;
                bPreference.PrimaryCurrency.ModifiedDate = DateTime.Now;
                //db.Entry(currency).State = EntityState.Modified;
                #endregion

                UMst_UserCustomizeFields cfields = bPreference.CustomFields;
                bPreference.CustomFields.ModifiedByUserId = this.UserId;
                bPreference.CustomFields.ModifiedDate = DateTime.Now;
                bPreference.CustomFields.BusinessID = this.BusinessId;
                // db.Entry(cfields).State = EntityState.Modified;
                BAL.Preferences.SavePreferences(bPreference);

                //db.SaveChanges();
                return View(bPreference);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveBusinessInformation(string companyName, short? businessTypeId, string businessEmail, string businessPhone, string companyAddress, int? cityID, int? stateID, string txtZip, short? countryID, int? county, string txtDUNS, string txtDBA, string license)
        {

            var objRegistration = new Models.BusinessRegistration();
            objRegistration.CountryId = countryID;
            objRegistration.BusinessName = companyName;
            objRegistration.BusinessTypeId = businessTypeId;
            objRegistration.Email = businessEmail;
            objRegistration.Phone = businessPhone;
            objRegistration.BusinessID = BusinessId;
            objRegistration.AddressLine1 = companyAddress;
            objRegistration.CountyId = county;
            objRegistration.CityId = cityID;
            objRegistration.StateId = stateID;
            objRegistration.Zip = txtZip;
            objRegistration.DUNS_ = txtDUNS;
            objRegistration.DBA = txtDBA;
            objRegistration.Licences = Convert.ToInt32(license);
            objRegistration.CreatedByUserId = UserId;
            return Json(BAL.Preferences.SaveBusinessInformation(objRegistration));
        }

        [HttpPost]
        public ActionResult SavePreferenceInformation(short dateFormat, short numberFormat, short fiscalYear, short currency)
        {
            Models.BusinessPreferences_DTO objbusinessPrefDTO = new BusinessPreferences_DTO();
            Models.UMst_BusinessPreferences objbusinessPref = new UMst_BusinessPreferences();
            objbusinessPref.BusinessID = BusinessId;
            objbusinessPref.CreatedByUserId = UserId;
            objbusinessPref.PreferenceFieldsId = 1;
            objbusinessPref.PreferenceValuesId = dateFormat;
            objbusinessPrefDTO.DateFormatPreference = objbusinessPref;
            objbusinessPref = new UMst_BusinessPreferences();
            objbusinessPref.BusinessID = BusinessId;
            objbusinessPref.CreatedByUserId = UserId;
            objbusinessPref.PreferenceFieldsId = 2;
            objbusinessPref.PreferenceValuesId = numberFormat;
            objbusinessPrefDTO.NumberFormatPreference = objbusinessPref;
            objbusinessPref = new UMst_BusinessPreferences();
            objbusinessPref.BusinessID = BusinessId;
            objbusinessPref.CreatedByUserId = UserId;
            objbusinessPref.PreferenceFieldsId = 3;
            objbusinessPref.PreferenceValuesId = fiscalYear;
            objbusinessPrefDTO.FiscalYearFormat = objbusinessPref;
            objbusinessPref = new UMst_BusinessPreferences();
            objbusinessPref.BusinessID = BusinessId;
            objbusinessPref.CreatedByUserId = UserId;
            objbusinessPref.PreferenceFieldsId = 4;
            objbusinessPref.PreferenceValuesId = currency;
            objbusinessPrefDTO.PrimaryCurrency = objbusinessPref;
            return Json(BAL.Preferences.SavePreferenceInformation(objbusinessPrefDTO));
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
                objbusinessTax.TaxTypesId = Convert.ToInt16(item[0]);//federal
                objbusinessTax.TaxNumber = item[1];
                objbusinessTax.TaxValue = string.IsNullOrEmpty(item[2]) ? 0 : Convert.ToDecimal(item[2]);
                result = BAL.Preferences.SaveTaxInformation(objbusinessTax);
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult SaveGreeting(string[][] userGreetings)
        {
            bool result = false;
            List<UMst_UserGreetingEmails> lstGreetingEmails = new List<UMst_UserGreetingEmails>();
            foreach (string[] item in userGreetings)
            {
                UMst_UserGreetingEmails objEmail = new UMst_UserGreetingEmails();
                objEmail.GreetingsListId = Convert.ToInt16(item[0]);
                objEmail.Subject = item[1];
                objEmail.Message = item[2];
                objEmail.BusinessID = BusinessId;
                objEmail.CreatedByUserId = UserId;
                result = BAL.Preferences.SaveGreeting(objEmail);
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult SaveCustomize(short renameCustomer, string txtInvoiceTerms, string txtPOTerms, string customField1,
                    string customField2, string customField3, string txtOrder1, string txtOrder2, string txtOrder3, string logo, string txtMerchantID, string txtgatewayPwd, string[] aryFilePath)
        {
            UMst_UserCustomizeFields objCustomize = new UMst_UserCustomizeFields();
            UMst_UserCustomFields objCustom = new UMst_UserCustomFields();
            objCustomize.CustomersRenameId = renameCustomer;
            objCustomize.InvoiceTerms = txtInvoiceTerms;
            objCustomize.POTerms = txtPOTerms;
            objCustomize.Logo = Encoding.UTF8.GetBytes(logo);
            objCustom.CustomField1Label = customField1;
            objCustom.CustomField2Label = customField2;
            objCustom.CustomField3Label = customField3;
            objCustom.BusinessID = objCustomize.BusinessID = BusinessId;
            objCustom.CreatedByUserId = objCustomize.CreatedByUserId = UserId;


            List<string> files = new List<string>();

            if (aryFilePath != null)
            {
                foreach (string item in aryFilePath)
                {
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
                    }

                }
                catch (Exception e)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }

            return Json(BAL.Preferences.SaveCustomize(objCustomize, objCustom));
        }

        #endregion

        #region Popups

        [HttpGet]
        public ActionResult ServicesPopUp()
        {
            //ViewBag.ParentAccounts = new SelectList(db.UMst_UserParentAccounts.Where(item => item.BusinessID.Equals(this.BusinessId)), "UserParentAccountsId", "AccountName", "--select--");
            return PartialView();
            //return PartialView("~/Views/Shared/ChartOfAccounts");
        }

        [HttpPost]
        public ActionResult ServicesPopUp(UMst_UserAccounts umst_useraccounts)
        {
            //umst_useraccounts.BusinessID = this.BusinessId;
            //umst_useraccounts.CreatedDate = DateTime.Now;
            //umst_useraccounts.CreatedByUserId = this.UserId;
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead }))
            //{
            //    try
            //    {
            //        umst_useraccounts.UserAccountsId = db.GetObjectMaxId("UserAccountsId");
            //        db.UMst_UserAccounts.Add(umst_useraccounts);
            //        db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    scope.Complete();
            //}
            return RedirectToAction("Services");
        }

        [HttpGet]
        public JsonResult GetServicesList()
        {
            var data = "";// _db.UMst_Services.Select(a => a.ServicesId.Equals(this.BusinessId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoComplete(string search)
        {
            //var result = db.UserBankAccounts.ToList();
            var result = "";// _db.UserBankAccounts.Where(u => u.BankName.StartsWith(search.ToLower()));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
