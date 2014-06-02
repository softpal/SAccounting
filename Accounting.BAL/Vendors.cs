using System.Data;
using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
//using Elmah;

namespace StratusAccounting.BAL
{
    public class Vendors
    {
        public static bool InsertVendors(Add_Vendor vendors)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(vendors.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                cmd.AddParameters(vendors.UserId, CommonConstants.UserId, SqlDbType.Int);

                cmd.AddParameters(vendors.CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.VendorFirstName, CommonConstants.VendorFirstName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.Title, CommonConstants.Title, SqlDbType.VarChar);
                cmd.AddParameters(vendors.VendorLastName, CommonConstants.VendorLastName, SqlDbType.VarChar);
                //cmd.AddParameters(vendors.VendorTypeId, CommonConstants.VendorTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(null, CommonConstants.VendorTypeId, SqlDbType.SmallInt);

                cmd.AddParameters(vendors.BankName, CommonConstants.BankName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.AccountNum, CommonConstants.AccountNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.BankAccountTypeId, CommonConstants.BankAccountTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(vendors.RountingNum, CommonConstants.RountingNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.BankAddress, CommonConstants.BankAddress, SqlDbType.VarChar);
                //cmd.AddParameters(vendors.BankAddress, "@BankAddress", SqlDbType.VarChar);

                //cmd.AddParameters(vendors.UserVendor.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.BigInt);
                cmd.AddParameters(vendors.CreditCardToken, CommonConstants.CreditCardToken, SqlDbType.VarChar);
                cmd.AddParameters(vendors.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);

                //cmd.AddParameters(vendors.IsTaxable, "@IsTaxable", SqlDbType.Bit);
                //cmd.AddParameters(vendors.FederalTaxNum, "@FederalTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.FederalTaxValue, "@FederalTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.StateTaxNum, "@StateTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.StateTaxValue, "@StateTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.CityTaxNum, "@CityTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.CityTaxValue, "@CityTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.CountyTaxNum, "@CountyTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.CountyTaxValue, "@CountyTaxValue", SqlDbType.Decimal);
                cmd.AddParameters(vendors.IsTaxable, CommonConstants.IsTaxable, SqlDbType.Bit);
                cmd.AddParameters(vendors.FederalTaxNum, CommonConstants.FederalTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.FederalTaxValue, CommonConstants.FederalTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(vendors.StateTaxNum, CommonConstants.StateTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.StateTaxValue, CommonConstants.StateTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(vendors.CityTaxNum, CommonConstants.CityTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CityTaxValue, CommonConstants.CityTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(vendors.CountyTaxNum, CommonConstants.CountyTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CountyTaxValue, CommonConstants.CountyTaxValue, SqlDbType.Decimal);

                cmd.AddParameters(vendors.Email, CommonConstants.Email, SqlDbType.VarChar);
                cmd.AddParameters(vendors.Phone, CommonConstants.Phone, SqlDbType.VarChar);
                cmd.AddParameters(vendors.Fax, CommonConstants.Fax, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CommStreet, "@CommStreet", SqlDbType.VarChar);
                cmd.AddParameters(vendors.CommCityId, "@CommCityId", SqlDbType.Int);
                cmd.AddParameters(vendors.CommStateId, "@CommStateId", SqlDbType.Int);
                cmd.AddParameters(vendors.CommCountryId, "@CommCountryId", SqlDbType.Int);
                cmd.AddParameters(vendors.Commzip, "@Commzip", SqlDbType.VarChar);
                cmd.AddParameters(vendors.Website, CommonConstants.Website, SqlDbType.VarChar);

                cmd.AddParameters(vendors.BillingStreet, "@BillingStreet", SqlDbType.VarChar);
                cmd.AddParameters(vendors.BillingCityId, "@BillingCityId", SqlDbType.Int);
                cmd.AddParameters(vendors.BillingStateId, "@BillingStateId", SqlDbType.Int);
                cmd.AddParameters(vendors.BillingCountryId, "@BillingCountryId", SqlDbType.SmallInt);
                cmd.AddParameters(vendors.Billingzip, "@Billingzip", SqlDbType.VarChar);

                cmd.AddParameters(vendors.ShippingStreet, "@ShippingStreet", SqlDbType.VarChar);
                cmd.AddParameters(vendors.ShippingCityId, "@ShippingCityId", SqlDbType.Int);
                cmd.AddParameters(vendors.ShippingStateId, "@ShippingStateId", SqlDbType.Int);
                cmd.AddParameters(vendors.ShippingCountryId, "@ShippingCountryId", SqlDbType.SmallInt);
                cmd.AddParameters(vendors.ShippingZip, "@Shippingzip", SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Insert_Vendors);
            }
            return true;
        }

        public static bool SaveVendors(UserVendor vendors)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(vendors.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                //cmd.AddParameters(vendors.BusinessTypeId, CommonConstants.BusinessTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(vendors.CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.UserVendorId, CommonConstants.UserVendorId, SqlDbType.BigInt);
                cmd.AddParameters(vendors.VendorFirstName, CommonConstants.VendorFirstName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.VendorLastName, CommonConstants.VendorLastName, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CreditCardToken, CommonConstants.CreditCardToken, SqlDbType.VarChar);
                cmd.AddParameters(vendors.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.VarChar);
                cmd.AddParameters(vendors.PurchaseOrders, CommonConstants.PurchaseOrders, SqlDbType.VarChar);
                cmd.AddParameters(vendors.VendorsAddresses, CommonConstants.VendorsAddresses, SqlDbType.VarChar);
                cmd.AddParameters(vendors.VendorTaxDetails, CommonConstants.VendorTaxDetails, SqlDbType.VarChar);
                cmd.AddParameters(vendors.Website, CommonConstants.Website, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.VarChar);
                cmd.AddParameters(vendors.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                cmd.AddParameters(vendors.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                cmd.AddParameters(vendors.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                cmd.AddParameters(vendors.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                if (vendors.UserVendorId > 0)
                {
                    cmd.AddParameters(vendors.UserVendorId, CommonConstants.UserAccountsId, SqlDbType.Int);
                    cmd.AddParameters(vendors.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Update_Businessvendors);
                }
                else
                {
                    cmd.AddParameters(vendors.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_Businessvendors);
                }
            }
            return true;
        }

        public static IList<UserVendor> GetVendors(int businessId, int userId)
        {
            var vendors = new List<UserVendor>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessVendors);

                    while (ireader.Read())
                    {
                        var vendor = new UserVendor
                        {
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            //businessty = ireader.GetInt16(CommonConstants.BusinessTypeId),
                            CompanyName = ireader.GetString(CommonConstants.CompanyName),
                            UserVendorId = ireader.GetInt16(CommonConstants.UserVendorId),
                            VendorFirstName = ireader.GetString(CommonConstants.VendorFirstName),
                            VendorLastName = ireader.GetString(CommonConstants.VendorLastName),
                            CreditCardToken = ireader.GetString(CommonConstants.CreditCardToken),
                            PaymentTypesId = ireader.GetInt16(CommonConstants.PaymentTypesId),
                            //PurchaseOrders= ireader.GetString(CommonConstants.PurchaseOrders),
                            //VendorsAddresses= ireader.GetString(CommonConstants.VendorsAddresses),
                            //VendorTaxDetails= ireader.GetString(CommonConstants.VendorTaxDetails),
                            Website = ireader.GetString(CommonConstants.Website),
                            CreatedByUserId = ireader.GetInt32(CommonColumns.CreatedByUserId),
                            ModifiedByUserId = ireader.GetInt32(CommonColumns.ModifiedByUserId),
                            CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate),
                            ModifiedDate = ireader.GetDateTime(CommonColumns.ModifiedDate),
                            IsActive = ireader.GetBoolean(CommonColumns.IsActive)
                        };
                        vendors.Add(vendor);
                    }
                    return vendors;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<UserVendor> GetVendorsList(int businessId)
        {
            var vendors = new List<UserVendor>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsList);
                    while (ireader.Read())
                    {
                        var vendor = new UserVendor
                        {
                            UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId),
                            VendorFirstName = ireader.GetString(CommonColumns.VendorFirstName),
                        };
                        vendors.Add(vendor);
                    }
                    return vendors;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool DeleteVendor(int businessId, int userId, long userVendorId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(userVendorId, CommonColumns.UserVendorId, SqlDbType.BigInt);


                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BusinessVendors);
                    return true;

                }
                catch (Exception)
                { }
            }
            return false;
        }

        public static IList<UserVendor> SearchVendors(int businessId, int UserId, string keyword)
        {
            var vendors = new List<UserVendor>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(keyword, "@keyword", SqlDbType.VarChar);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_SearchBusinessVendors);

                    while (ireader.Read())
                    {
                        var vendor = new UserVendor
                        {
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId),
                        };
                        vendors.Add(vendor);
                    }
                    return vendors;
                }
                catch (Exception ex)
                {
                    return null;
                    //Elmah
                }
            }
        }

        public static ObservableCollection<UserVendor> GetUserVendors(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            //var vendors = new List<Add_Vendor>();

            ObservableCollection<UserVendor> VendorList = new ObservableCollection<UserVendor>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);


                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsList);

                    while (ireader.Read())
                    {
                        var vendor = new UserVendor
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            //BusinessTypeId = ireader.GetInt16(CommonConstants.BusinessTypeId),
                            Sno = ireader.GetInt32(CommonColumns.SNo),
                            CompanyName = ireader.GetString(CommonColumns.CompanyName),
                            UserVendorId = ireader.GetInt16(CommonColumns.UserVendorId),
                            VendorFirstName = ireader.GetString(CommonColumns.VendorFirstName),
                            VendorLastName = ireader.GetString(CommonColumns.VendorLastName),
                            Title = ireader.GetString(CommonColumns.Title),
                            PhoneNo = ireader.GetString(CommonColumns.Phone),
                            Payable = ireader.GetFormatDecimal(ireader.GetOrdinal(CommonColumns.Payable))
                            //PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            //PurchaseOrders= ireader.GetString(CommonConstants.PurchaseOrders),
                            //VendorsAddresses= ireader.GetString(CommonConstants.VendorsAddresses),
                            //VendorTaxDetails= ireader.GetString(CommonConstants.VendorTaxDetails),
                            //Website = ireader.GetString(CommonConstants.Website),
                            //CreatedByUserId = ireader.GetInt32(CommonColumns.CreatedByUserId),
                            //ModifiedByUserId = ireader.GetInt32(CommonColumns.ModifiedByUserId),
                            //CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate),
                            //ModifiedDate = ireader.GetDateTime(CommonColumns.ModifiedDate),
                            //IsActive = ireader.GetBoolean(CommonColumns.IsActive)

                        };
                        VendorList.Add(vendor);

                    }
                    return VendorList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        //private static List<string> GetVendorsById(UserVendor vendor)
        //{
        //    List<string> files = new List<string>();
        //    if (vendor != null)
        //    {
        //        var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserVendors", vendor.UserVendorId.ToString();

        //        if (Directory.Exists(directory))
        //        {
        //            files = Directory.GetFiles(directory).ToList();
        //        }
        //    }
        //    List<string> fileNames = new List<string>();
        //    foreach (var file in files)
        //    {
        //        var arr = file.Split('\\');
        //        fileNames.Add(arr.GetValue(arr.Length - 1).ToString());
        //    }
        //    return fileNames;
        //}

        public static IList<UserVendor> GetVendorsList(int businessId, string keyword)
        {
            var vendors = new List<UserVendor>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(keyword, "@keyword", SqlDbType.VarChar);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsList);
                    while (ireader.Read())
                    {
                        var vendor = new UserVendor
                        {
                            UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId),
                            VendorFirstName = ireader.GetString(CommonColumns.VendorFirstName),
                        };
                        vendors.Add(vendor);
                    }
                    return vendors;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<VendorsAddress> GetVendorAddress(int businessId, int userId, int vendorId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonConstants.VendorId, SqlDbType.Int);

                    var vendorDetail = new List<VendorsAddress>();

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsAddress);
                    while (ireader.Read())
                    {
                        var vendorDet = new VendorsAddress
                        {
                            Email = ireader.GetString(CommonColumns.VendorEmail),
                            Phone = ireader.GetString(CommonColumns.Phone),
                            Fax = ireader.GetString(CommonColumns.Fax),
                            //Street = ireader.GetString(CommonColumns.Street),
                            City = ireader.GetString(CommonColumns.City),
                            State = ireader.GetString(CommonColumns.State),
                            Country = ireader.GetString(CommonColumns.Country),
                            Zip = ireader.GetString(CommonColumns.Zip),
                            AddressType = ireader.GetString(CommonColumns.AddressType)
                        };

                        vendorDetail.Add(vendorDet);
                    }
                    return vendorDetail;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public static UserVendor GetVendorDetails(int businessId, int userId, int vendorId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonConstants.VendorId, SqlDbType.Int);

                    var vendorDetail = new UserVendor();

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsDetails);
                    while (ireader.Read())
                    {
                        var vendorDet = new UserVendor
                        {
                            UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId),
                            CompanyName = ireader.GetString(CommonColumns.CompanyName),
                            VendorFirstName = ireader.GetString(CommonColumns.VendorFirstName),
                            Title = ireader.GetString(CommonColumns.Title),
                            Website = ireader.GetString(CommonColumns.WebSite),
                            PreferredPaymentMethod = ireader.GetString(CommonColumns.PreferredPaymentMethod),
                            CreditCardToken = ireader.GetString(CommonColumns.CreditCardToken),
                            LifeTimePayment = ireader.GetString(CommonColumns.LifeTimePayment),
                            YearToDatePayments = ireader.GetString(CommonColumns.YearToDatePayments),
                            LastYearPayments = ireader.GetString(CommonColumns.LastYearPayments),
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            BankName = ireader[CommonColumns.BankName] != System.DBNull.Value ? ireader.GetString(CommonColumns.BankName) : string.Empty,
                            AccountNum = ireader.GetString(CommonColumns.AccountNum),
                            AccountTypeDesc = ireader.GetString(CommonColumns.AccountTypeDesc),
                            Address = ireader.GetString(CommonColumns.Address),
                            BankAccountTypeId = ireader.GetInt16(CommonColumns.BankAccountTypeId),
                            RountingNum = ireader.GetString(CommonColumns.RountingNum),
                            BankAccountsId = ireader.GetInt64(CommonColumns.BankAccountsId),
                            IsActive = ireader.GetBoolean(CommonColumns.IsActive)
                        };

                        vendorDetail = vendorDet;
                    }
                    return vendorDetail;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static UserBankAccount GetVendorBankDetails(int UserId, int vendorID)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(vendorID, CommonConstants.VendorId, SqlDbType.Int);
                    var vendorDetail = new UserBankAccount();
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsBank);
                    while (ireader.Read())
                    {
                        var vendorDet = new UserBankAccount
                        {
                            BankName = ireader[CommonColumns.BankName] != System.DBNull.Value ? ireader.GetString(CommonColumns.BankName) : string.Empty,
                            AccountNum = ireader.GetString(CommonColumns.AccountNum),
                            BankAccountTypeId = ireader.GetInt16(CommonColumns.BankAccountTypeId),
                            RountingNum = ireader.GetString(CommonColumns.RountingNum),
                            BankAccountsId = ireader.GetInt64(CommonColumns.BankAccountsId),
                            IsActive = ireader.GetBoolean(CommonColumns.IsActive)
                        };
                        vendorDetail = vendorDet;
                    }
                    return vendorDetail;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //public static Payment GetPaymentsByVendorId()
        //{
        //}

        public static bool SaveVendorDetails(int BusinessId, int UserId, string[] vendorDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt64(vendorDetails[0]), CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(vendorDetails[1], CommonConstants.CompanyName, SqlDbType.VarChar);
                    cmd.AddParameters(vendorDetails[2], CommonConstants.VendorFirstName, SqlDbType.VarChar);
                    cmd.AddParameters(vendorDetails[3], CommonConstants.VendorLastName, SqlDbType.VarChar);
                    cmd.AddParameters(vendorDetails[4], CommonConstants.Title, SqlDbType.VarChar);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsDetails);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static bool SaveCommunicationDetails(int BusinessId, int UserId, string[] vendorAddress, int addressType)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt64(vendorAddress[0]), CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(addressType, CommonConstants.AddressTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(vendorAddress[1], CommonConstants.Email, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[2], CommonConstants.Phone, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[3], CommonConstants.Fax, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[4], CommonConstants.AddressLine1, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[5], CommonConstants.CountryId, SqlDbType.SmallInt);
                    cmd.AddParameters(vendorAddress[6], CommonConstants.StateId, SqlDbType.Int);
                    cmd.AddParameters(vendorAddress[7], CommonConstants.CityId, SqlDbType.Int);
                    cmd.AddParameters(vendorAddress[8], CommonConstants.Zip, SqlDbType.VarChar);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsAddress);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public static bool SaveAddressDetails(int BusinessId, int UserId, string[] vendorAddress, int addressType)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt64(vendorAddress[0]), CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(addressType, CommonConstants.AddressTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(vendorAddress[1], CommonConstants.AddressLine1, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[2], CommonConstants.Country, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[3], CommonConstants.State, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[4], CommonConstants.City, SqlDbType.VarChar);
                    cmd.AddParameters(vendorAddress[5], CommonConstants.Zip, SqlDbType.VarChar);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsAddress);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public static bool SaveBankDetails(int BusinessId, int UserId, string[] bankDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    int vendorId = 0;
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(Convert.ToInt64(bankDetails[0]), CommonConstants.BankAccountsId, SqlDbType.BigInt);
                    cmd.AddParameters(bankDetails[1], CommonConstants.BankName, SqlDbType.VarChar);
                    cmd.AddParameters(bankDetails[2], CommonConstants.AccountNum, SqlDbType.VarChar);
                    cmd.AddParameters(Convert.ToInt64(bankDetails[3]), CommonConstants.BankAccountTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(bankDetails[4], CommonConstants.RountingNum, SqlDbType.VarChar);
                    cmd.AddParameters(bankDetails[5], CommonConstants.Address, SqlDbType.VarChar);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsBank);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static bool SavePaymentDetails(int BusinessId, int UserId, string[] paymentDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt64(paymentDetails[0]), CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(Convert.ToInt64(paymentDetails[1]), CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
                    cmd.AddParameters(paymentDetails[1], CommonConstants.CreditCardToken, SqlDbType.VarChar);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsPaymentDetails);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }


        public static UserVendor GetVendorTaxDetails(int businessId, int userId, int vendorId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonConstants.VendorId, SqlDbType.Int);

                    var vendorDetail = new UserVendor();

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorTaxDetails);
                    while (ireader.Read())
                    {
                        vendorDetail.UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId);
                        var vendorDet = new UMst_TaxDetails
                        {
                            TaxTypesId = ireader.GetInt16(CommonColumns.TaxTypesId),
                            TaxName = ireader.GetString(CommonColumns.TaxName),
                            TaxNumber = ireader.GetString(CommonColumns.TaxNumber),
                            TaxValue = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.TaxValue))
                        };

                        var type = vendorDet.TaxName;
                        switch (type)
                        {
                            case "Federal": vendorDetail.FederalTaxNum = vendorDet.TaxNumber;
                                vendorDetail.FederalTaxValue = vendorDet.TaxValue; break;
                            case "State": vendorDetail.StateTaxNum = vendorDet.TaxNumber;
                                vendorDetail.StateTaxValue = vendorDet.TaxValue; break;
                            case "City": vendorDetail.CityTaxNum = vendorDet.TaxNumber;
                                vendorDetail.CityTaxValue = vendorDet.TaxValue; break;
                            case "County": vendorDetail.CountyTaxNum = vendorDet.TaxNumber;
                                vendorDetail.CountyTaxValue = vendorDet.TaxValue; break;
                        }
                    }
                    return vendorDetail;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public static bool SaveTaxDetails(int BusinessId, int UserId, string[] taxDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt64(taxDetails[0]), CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(taxDetails[1], CommonConstants.FederalTaxNum, SqlDbType.VarChar);
                    cmd.AddParameters(Convert.ToDecimal(taxDetails[2]), CommonConstants.FederalTaxValue, SqlDbType.Decimal);
                    cmd.AddParameters(taxDetails[3], CommonConstants.CityTaxNum, SqlDbType.VarChar);
                    cmd.AddParameters(Convert.ToDecimal(taxDetails[4]), CommonConstants.CityTaxValue, SqlDbType.Decimal);
                    cmd.AddParameters(taxDetails[5], CommonConstants.StateTaxNum, SqlDbType.VarChar);
                    cmd.AddParameters(Convert.ToDecimal(taxDetails[6]), CommonConstants.CityTaxValue, SqlDbType.Decimal);
                    cmd.AddParameters(taxDetails[7], CommonConstants.CountyTaxNum, SqlDbType.VarChar);
                    cmd.AddParameters(Convert.ToDecimal(taxDetails[8]), CommonConstants.CityTaxValue, SqlDbType.Decimal);

                    return cmd.ExecuteNonQuery(SqlProcedures.Update_VendorsTax);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
    }
}
