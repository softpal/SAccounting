using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    public class Customers
    {
        public static bool SaveCustomer(UserCustomer customers)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(customers.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                cmd.AddParameters(customers.BusinessTypeId, CommonConstants.BusinessTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(customers.CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);
                cmd.AddParameters(customers.UserCustomerId, CommonConstants.UserVendorId, SqlDbType.BigInt);
                cmd.AddParameters(customers.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.VarChar);
                cmd.AddParameters(customers.Title, CommonConstants.Title, SqlDbType.VarChar);
                cmd.AddParameters(customers.CreditCardToken, CommonConstants.CreditCardToken, SqlDbType.VarChar);
                cmd.AddParameters(customers.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.VarChar);
                cmd.AddParameters(customers.CustomerFirstName, CommonConstants.CustomerFirstName, SqlDbType.VarChar);
                cmd.AddParameters(customers.CustomerLastName, CommonConstants.CustomerLastName, SqlDbType.VarChar);
                cmd.AddParameters(customers.CreditPeriodTypeId, CommonConstants.CreditPeriodTypeId, SqlDbType.VarChar);
                cmd.AddParameters(customers.CreditLimit, CommonConstants.CreditLimit, SqlDbType.VarChar);
                cmd.AddParameters(customers.IsTaxable, CommonConstants.IsTaxable, SqlDbType.Bit);
                cmd.AddParameters(customers.IsProfitType, CommonConstants.IsProfitType, SqlDbType.Bit);
                cmd.AddParameters(customers.Website, CommonConstants.Website, SqlDbType.VarChar);
                cmd.AddParameters(customers.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.VarChar);
                cmd.AddParameters(customers.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                cmd.AddParameters(customers.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                cmd.AddParameters(customers.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                cmd.AddParameters(customers.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                if (customers.UserCustomerId > 0)
                {
                    cmd.AddParameters(customers.UserCustomerId, CommonConstants.UserCustomerId, SqlDbType.Int);
                    cmd.AddParameters(customers.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessCustomers);
                }
                else
                {
                    cmd.AddParameters(customers.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinesCustomers);
                }
            }
            return true;
        }

        //public static bool SaveCustomers(Customers_DTO customers, int businessId, int userId)
        public static bool SaveCustomers(Customers_DTO customers)
        {
            Customers_DTO taxDetails = new Customers_DTO();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(customers.UserCustomer.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(customers.UserCustomer.UserAccountsId, CommonConstants.UserId, SqlDbType.Int);
                    //cmd.AddParameters(customers.UserCustomer.BusinessTypeId, CommonConstants.BusinessTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(customers.UserCustomer.CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.UserCustomer.UserCustomerId, CommonConstants.UserCustomerId, SqlDbType.BigInt);
                    //cmd.AddParameters(customers.UserCustomer.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.VarChar);
                    cmd.AddParameters(customers.UserCustomer.CustomerFirstName, CommonConstants.CustomerFirstName, SqlDbType.VarChar);
                    cmd.AddParameters(customers.UserCustomer.Title, CommonConstants.Title, SqlDbType.VarChar);
                    cmd.AddParameters(customers.UserCustomer.CustomerLastName, CommonConstants.CustomerLastName, SqlDbType.VarChar);
                    cmd.AddParameters(customers.UserCustomer.IsProfitType, CommonConstants.IsProfitType, SqlDbType.Bit);


                    //Bank Details
                    cmd.AddParameters(customers.BankDetails.AccountNum, CommonConstants.AccountNum, SqlDbType.VarChar);
                    cmd.AddParameters(customers.BankDetails.BankName, CommonConstants.BankName, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.BankDetails.BankAccountsId, CommonConstants.BankAccountsId, SqlDbType.Bit);
                    // cmd.AddParameters(customers.BankDetails.UserId, CommonConstants.UserId, SqlDbType.Bit);
                    //cmd.AddParameters(customers.BankDetails.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.BankDetails.UserTypeId, CommonConstants.UserTypeId, SqlDbType.VarChar);
                    cmd.AddParameters(customers.BankDetails.BankAccountTypeId, CommonConstants.BankAccountTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(customers.BankDetails.RountingNum, CommonConstants.RountingNum, SqlDbType.VarChar);
                    cmd.AddParameters(customers.BankDetails.Address, CommonConstants.BankAddress, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.BankDetails.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                    //cmd.AddParameters(customers.BankDetails.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    //cmd.AddParameters(customers.BankDetails.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                    //payment details
                    cmd.AddParameters(customers.UserCustomer.CreditPeriodTypeId, CommonConstants.CreditPeriodTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(customers.UserCustomer.CreditLimit, CommonConstants.CreditLimit, SqlDbType.Decimal);
                    cmd.AddParameters(customers.UserCustomer.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
                    cmd.AddParameters(customers.UserCustomer.CreditCardToken, CommonConstants.CreditCardToken, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.UserCustomer.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    //cmd.AddParameters(customers.UserCustomer.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                    //cmd.AddParameters(customers.CustomerCommunication.AddressLine1, CommonConstants.AddressLine1, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.Email, CommonConstants.Email, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.Fax, CommonConstants.Fax, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.Phone, CommonConstants.Phone, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.StateId, CommonConstants.StateId, SqlDbType.Int);

                    //cmd.AddParameters(customers.CustomerCommunication.UserCustomerId, CommonConstants.UserCustomerId, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.StateId, CommonConstants.StateId, SqlDbType.Int);
                    //
                    //cmd.AddParameters(customers.CustomerCommunication.AddressLine2, CommonConstants.AddressLine2, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.AddressType, CommonConstants.AddressType, SqlDbType.VarChar);
                    //cmd.AddParameters(customers.CustomerCommunication.AddressTypeId, CommonConstants.AddressTypeId, SqlDbType.SmallInt);
                    //cmd.AddParameters(customers.CustomerCommunication.CityId, CommonConstants.CityId, SqlDbType.Int);
                    //cmd.AddParameters(customers.CustomerCommunication.CountryId, CommonConstants.CountryId, SqlDbType.Int);
                    //cmd.AddParameters(customers.CustomerCommunication.CountyId, CommonConstants.CountyId, SqlDbType.Int);
                    //cmd.AddParameters(customers.CustomerCommunication.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    //cmd.AddParameters(customers.CustomerCommunication.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                    //cmd.AddParameters(customers.CustomerCommunication.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
                    //cmd.AddParameters(customers.CustomerCommunication.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                    //cmd.AddParameters(customers.CustomerCommunication.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                    //Tax Details 
                    cmd.AddParameters(customers.UserCustomer.IsTaxable, CommonConstants.IsTaxable, SqlDbType.Bit);
                    foreach (var item in customers.TaxDetails)
                    {
                        cmd.AddParameters(item.TaxNumber, "@" + item.TaxName + "TaxNumber", SqlDbType.VarChar);

                        cmd.AddParameters(item.TaxValue, "@" + item.TaxName + "TaxValue", SqlDbType.Decimal);
                    }

                    //Communication details
                    cmd.AddParameters(customers.CustomerCommunication.Email, CommonConstants.Email, SqlDbType.VarChar);
                    cmd.AddParameters(customers.CustomerCommunication.Fax, CommonConstants.Fax, SqlDbType.VarChar);
                    cmd.AddParameters(customers.CustomerCommunication.Phone, CommonConstants.Phone, SqlDbType.VarChar);
                    cmd.AddParameters(customers.CustomerCommunication.StateId, "@CommStateId", SqlDbType.Int);

                    cmd.AddParameters(customers.CustomerCommunication.AddressLine1, "@CommStreet", SqlDbType.VarChar);
                    cmd.AddParameters(customers.CustomerCommunication.CityId, "@CommCityId", SqlDbType.Int);
                    cmd.AddParameters(customers.CustomerCommunication.CountryId, "@CommCountryId", SqlDbType.Int);
                    cmd.AddParameters(customers.CustomerCommunication.Zip, "@CommZip", SqlDbType.VarChar);
                    cmd.AddParameters("", CommonConstants.Website, SqlDbType.VarChar);

                    //billing details

                    cmd.AddParameters(customers.BillingAddress.StateId, "@BillingStateId", SqlDbType.Int);

                    cmd.AddParameters(customers.BillingAddress.AddressLine1, "@BillingStreet", SqlDbType.VarChar);
                    cmd.AddParameters(customers.BillingAddress.CityId, "@BillingCityId", SqlDbType.Int);

                    cmd.AddParameters(customers.BillingAddress.CountryId, "@BillingCountyId", SqlDbType.Int);
                    cmd.AddParameters(customers.BillingAddress.CountryId, "@BillingCountryId", SqlDbType.Int);
                    cmd.AddParameters(customers.BillingAddress.Zip, "@BillingZip", SqlDbType.VarChar);

                    //shipping details

                    cmd.AddParameters(customers.ShippingAddress.StateId, "@ShippingStateId", SqlDbType.Int);

                    cmd.AddParameters(customers.ShippingAddress.AddressLine1, "@ShippingStreet", SqlDbType.VarChar);
                    cmd.AddParameters(customers.ShippingAddress.CityId, "@ShippingCityId", SqlDbType.Int);
                    cmd.AddParameters(customers.ShippingAddress.CountryId, "@ShippingCountryId", SqlDbType.Int);
                    cmd.AddParameters(customers.ShippingAddress.CountryId, "@ShippingCountyId", SqlDbType.Int);
                    cmd.AddParameters(customers.ShippingAddress.Zip, "@ShippingZip", SqlDbType.VarChar);

                    //if (customers.UserBankAccount.UserId > 0)
                    //{
                    //    //cmd.AddParameters(customers.UserCustomer.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    //    cmd.AddParameters(customers.UserCustomer.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    //    cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessCustomers);
                    //}
                    //else
                    //{
                    //cmd.AddParameters(customers.UserCustomer.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinesCustomers);
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return true;
        }
        public static bool EditCustomer(Customers_DTO customers)
        {
            
            using (var cmd = new DBSqlCommand())
            {

                cmd.AddParameters(customers.UserCustomerDetails[0].UserCustomerId, CommonConstants.CustomerId, SqlDbType.BigInt);
                cmd.AddParameters(customers.UserCustomerDetails[0].ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                cmd.AddParameters(customers.UserCustomerDetails[0].BusinessID, CommonConstants.BusinessID, SqlDbType.Int);

                cmd.AddParameters(customers.UserCustomerDetails[0].CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);

                cmd.AddParameters(customers.UserCustomerDetails[0].CustomerFirstName, CommonConstants.CustomerFirstName, SqlDbType.VarChar);
                cmd.AddParameters(customers.UserCustomerDetails[0].Title, CommonConstants.Title, SqlDbType.VarChar);
                cmd.AddParameters(customers.UserCustomerDetails[0].CustomerLastName, CommonConstants.CustomerLastName, SqlDbType.VarChar);
               // cmd.AddParameters(customers.UserCustomer.IsProfitType, CommonConstants.IsProfitType, SqlDbType.Bit);
                cmd.AddParameters(customers.UserCustomerDetails[0].Website, CommonConstants.Website, SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Update_CustomersDetails);
                
            }
            return true;
        }
        public static IList<UserCustomer> GetCustomerDetails(Int64 CustomerId)
        {
            var customers = new List<UserCustomer>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(CustomerId, "@CustomerId", SqlDbType.BigInt);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_CustomersDetails);

                    while (ireader.Read())
                    {
                        var customer = new UserCustomer
                        {
                            CompanyName = ireader.GetString(CommonColumns.CompanyName),
                            CustomerFirstName=ireader.GetString(CommonColumns.CustomerFirstName),
                            CustomerLastName = ireader.GetString(CommonColumns.CustomerLastName),
                            CreditCardToken = ireader.GetString(CommonColumns.CreditCardToken),
                            
                            //PaymentTypesId = ireader.GetInt16(CommonConstants.PaymentTypesId),
                            //UserCustomerId = ireader.GetInt64(CommonConstants.UserCustomerId),
                            //UserAccountsId = ireader.GetInt64(CommonColumns.UserAccountsId),
                            Title = ireader.GetString(CommonColumns.Title),
                            
                            //CreditPeriod = ireader.GetInt16(CommonColumns.CreditPeriodTypeId),
                            CreditLimit = Convert.ToDecimal(ireader.GetString(CommonColumns.CreditLimit)),
                            //IsTaxable = ireader.GetBoolean(CommonColumns.IsTaxable),
                            //CustomerFirstName = ireader.GetString(CommonConstants.CustomerFirstName),
                            //CustomerLastName = ireader.GetString(CommonColumns.CustomerLastName),
                            //IsProfitType = ireader.GetBoolean(CommonColumns.IsProfitType),
                            Website = ireader.GetString(CommonColumns.WebSite),
                            BusinessType=ireader.GetString(CommonColumns.BusinessType),
                          
                            //CreatedByUserId = ireader.GetInt32(CommonColumns.CreatedByUserId),
                            //ModifiedByUserId = ireader.GetInt32(CommonColumns.ModifiedByUserId),
                            //CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate),
                            //ModifiedDate = ireader.GetDateTime(CommonColumns.ModifiedDate),
                            //IsActive = ireader.GetBoolean(CommonColumns.IsActive)
                        };
                       
                        customers.Add(customer);
                    }
                    return customers;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<Mst_PaymentTypes> GetCustomerPaymentMethodTypes(Int64 CustomerId)
        {
            List<Mst_PaymentTypes> paymentMethodTypes = new List<Mst_PaymentTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(CustomerId, "@CustomerId", SqlDbType.BigInt);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_CustomersDetails);
                    while (ireader.Read())
                    {
                        var paymentMethodType = new Mst_PaymentTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            PaymentDesc = ireader.GetString(CommonColumns.PreferredPaymentMethod)
                        };

                        paymentMethodTypes.Add(paymentMethodType);

                    }
                    return paymentMethodTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<Mst_CreditPeriodTypes> GetCustomerPaymentCreditTypes(Int64 CustomerId)
        {
            List<Mst_CreditPeriodTypes> paymentCreditTypes = new List<Mst_CreditPeriodTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(CustomerId, "@CustomerId", SqlDbType.BigInt);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_CustomersDetails);
                    while (ireader.Read())
                    {
                        var paymentCreditType = new Mst_CreditPeriodTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                           // CreditPeriodTypeId = ireader.GetInt16(CommonColumns.CreditPeriodTypeId),
                            Period = ireader.GetString(CommonColumns.CreditPeriod)
                        };

                        paymentCreditTypes.Add(paymentCreditType);

                    }
                    return paymentCreditTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static IList<CustomersAddress> GetCustomerAddress(Int64 CustomerId)
        {
            List<CustomersAddress> custadd = new List<CustomersAddress>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(CustomerId, "@CustomerId", SqlDbType.BigInt);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_CustomersAddress);
                    while (ireader.Read())
                    {
                        var cadd = new CustomersAddress
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            // CreditPeriodTypeId = ireader.GetInt16(CommonColumns.CreditPeriodTypeId),
                            //Period = ireader.GetString(CommonColumns.CreditPeriod)

                        };

                        custadd.Add(cadd);

                    }
                    return custadd;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static IList<UserCustomer> GetCustomers(int businessId, int userId)
        {
            var customers = new List<UserCustomer>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessCustomers);

                    while (ireader.Read())
                    {
                        var customer = new UserCustomer
                        {
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            BusinessTypeId = ireader.GetInt16(CommonConstants.BusinessTypeId),
                            CompanyName = ireader.GetString(CommonConstants.CompanyName),
                            CreditCardToken = ireader.GetString(CommonConstants.CreditCardToken),
                            PaymentTypesId = ireader.GetInt16(CommonConstants.PaymentTypesId),
                            UserCustomerId = ireader.GetInt64(CommonConstants.UserCustomerId),
                            UserAccountsId = ireader.GetInt64(CommonColumns.UserAccountsId),
                            Title = ireader.GetString(CommonColumns.Title),
                            CreditPeriodTypeId = ireader.GetInt16(CommonColumns.CreditPeriodTypeId),
                            CreditLimit = Convert.ToDecimal(ireader.GetString(CommonColumns.CreditLimit)),
                            IsTaxable = ireader.GetBoolean(CommonColumns.IsTaxable),
                            CustomerFirstName = ireader.GetString(CommonConstants.CustomerFirstName),
                            CustomerLastName = ireader.GetString(CommonColumns.CustomerLastName),
                            IsProfitType = ireader.GetBoolean(CommonColumns.IsProfitType),
                            Website = ireader.GetString(CommonConstants.Website),
                            CreatedByUserId = ireader.GetInt32(CommonColumns.CreatedByUserId),
                            ModifiedByUserId = ireader.GetInt32(CommonColumns.ModifiedByUserId),
                            CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate),
                            ModifiedDate = ireader.GetDateTime(CommonColumns.ModifiedDate),
                            IsActive = ireader.GetBoolean(CommonColumns.IsActive)
                        };
                        customers.Add(customer);
                    }
                    return customers;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<UserCustomer> GetCustomersList(int businessId, int userId)
        {
            var customers = new List<UserCustomer>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessCustomers);

                    while (ireader.Read())
                    {
                        var customer = new UserCustomer
                        {
                            CompanyName = ireader.GetString(CommonConstants.CompanyName),
                            UserCustomerId = ireader.GetInt64(CommonConstants.UserCustomerId),
                            Title = ireader.GetString(CommonColumns.Title),
                            CustomerFirstName = ireader.GetString(CommonColumns.CustomerFirstName),
                            CustomerLastName = ireader.GetString(CommonColumns.CustomerLastName),
                            PhoneNo = ireader.GetString(CommonColumns.Phone),
                            //Payable = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.)
                            
                        };
                        customers.Add(customer);
                    }
                    return customers;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool DeleteCustomer(int businessId, int userId, long userCustomerId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(userCustomerId, CommonColumns.UserCustomerId, SqlDbType.BigInt);

                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BusinessCustomers);
                    return true;
                }
                catch (Exception)
                { }
            }
            return false;
        }

        public static IList<UserCustomer> GetUserCustomers(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            var customers = new List<UserCustomer>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessCustomers);

                    while (ireader.Read())
                    {
                        var customer = new UserCustomer
                        {
                            UserCustomerId = ireader.GetInt64(CommonColumns.UserCustomerId),
                            CompanyName = ireader.GetString(CommonColumns.CompanyName),
                            CustomerFirstName = ireader.GetString(CommonColumns.CustomerFirstName),
                            CustomerLastName = ireader.GetString(CommonColumns.CustomerLastName),
                            Title = ireader.GetString(CommonColumns.Title)
                           
                        };
                        customers.Add(customer);
                    }
                    return customers;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<UMst_TaxDetails> GetTaxDetails(int businessId)
        {
            var taxDetails = new List<UMst_TaxDetails>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UMst_TaxDetails);
                    while (ireader.Read())
                    {
                        var tax = new UMst_TaxDetails
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                            BusinessID = businessId,
                            TaxNumber = ireader[CommonColumns.TaxNumber] != System.DBNull.Value ? ireader.GetString(CommonColumns.TaxNumber) : string.Empty,
                            TaxTypesId = ireader.GetInt16(CommonColumns.TaxTypesId),
                            TaxValue = ireader[CommonColumns.TaxValue] != System.DBNull.Value ? ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.TaxValue)) : 0,
                            TaxName = ireader.GetString(CommonColumns.TaxName)

                        };
                        taxDetails.Add(tax);
                    }
                    return taxDetails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<Mst_BankAccountTypes> GetBankAccountTypes()
        {
            List<Mst_BankAccountTypes> bankAccountTypes = new List<Mst_BankAccountTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BankAccountTypes);
                    while (ireader.Read())
                    {
                        var bankAccountType = new Mst_BankAccountTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            BankAccountTypeId = ireader.GetInt16(CommonColumns.BankAccountTypeId),
                            AccountTypeDesc = ireader.GetString(CommonColumns.AccountTypeDesc)
                        };

                        bankAccountTypes.Add(bankAccountType);

                    }
                    return bankAccountTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<Mst_CreditPeriodTypes> GetPaymentCreditTypes()
        {
            List<Mst_CreditPeriodTypes> paymentCreditTypes = new List<Mst_CreditPeriodTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentCreditTypes);
                    while (ireader.Read())
                    {
                        var paymentCreditType = new Mst_CreditPeriodTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            CreditPeriodTypeId = ireader.GetInt16(CommonColumns.CreditPeriodTypeId),
                            Period = ireader.GetString(CommonColumns.CreditPeriod)
                        };

                        paymentCreditTypes.Add(paymentCreditType);

                    }
                    return paymentCreditTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<Mst_PaymentTypes> GetPaymentMethodTypes()
        {
            List<Mst_PaymentTypes> paymentMethodTypes = new List<Mst_PaymentTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentMethodTypes);
                    while (ireader.Read())
                    {
                        var paymentMethodType = new Mst_PaymentTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            PaymentDesc = ireader.GetString(CommonColumns.PaymentDesc)
                        };

                        paymentMethodTypes.Add(paymentMethodType);

                    }
                    return paymentMethodTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



        //public static List<Mst_BankAccountTypes> GetBankAccountType(int businessId)
        //{
        //    var accountTypes = new List<Mst_BankAccountTypes>();
        //    using (var cmd = new DBSqlCommand())
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BankAccountTypes);
        //            while (ireader.Read())
        //            {
        //                var accountType = new Mst_BankAccountTypes
        //                {
        //                    // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
        //                    //BusinessID = businessId,
        //                    //TaxNumber = ireader[CommonColumns.TaxNumber] != System.DBNull.Value ? ireader.GetString(CommonColumns.TaxNumber) : string.Empty,
        //                    //TaxTypesId = ireader.GetInt16(CommonColumns.TaxTypesId),
        //                    //TaxValue = ireader[CommonColumns.TaxValue] != System.DBNull.Value ? ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.TaxValue)) : 0,
        //                    //TaxName = ireader.GetString(CommonColumns.TaxName)

        //                };
        //                accountTypes.Add(accountType);
        //            }
        //            return accountTypes;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public static bool InsertCustomers(Add_Customer customer)
        {            
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(customer.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                cmd.AddParameters(customer.UserId, CommonConstants.UserId, SqlDbType.Int);

                cmd.AddParameters(customer.CompanyName, CommonConstants.CompanyName, SqlDbType.VarChar);
                cmd.AddParameters(customer.CustomerFirstName, CommonConstants.CustomerFirstName, SqlDbType.VarChar);
                cmd.AddParameters(customer.Title, CommonConstants.Title, SqlDbType.VarChar);
                cmd.AddParameters(customer.CustomerLastName, CommonConstants.CustomerLastName, SqlDbType.VarChar);
                //cmd.AddParameters(vendors.VendorTypeId, CommonConstants.VendorTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(null, CommonConstants.VendorTypeId, SqlDbType.SmallInt);

                cmd.AddParameters(customer.BankName, CommonConstants.BankName, SqlDbType.VarChar);
                cmd.AddParameters(customer.AccountNum, CommonConstants.AccountNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.BankAccountTypeId, CommonConstants.BankAccountTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(customer.RountingNum, CommonConstants.RountingNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.BankAddress, CommonConstants.BankAddress, SqlDbType.VarChar);
                //cmd.AddParameters(vendors.BankAddress, "@BankAddress", SqlDbType.VarChar);

                //cmd.AddParameters(vendors.UserVendor.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.BigInt);
                cmd.AddParameters(customer.CreditCardToken, CommonConstants.CreditCardToken, SqlDbType.VarChar);
                cmd.AddParameters(customer.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);

                //cmd.AddParameters(vendors.IsTaxable, "@IsTaxable", SqlDbType.Bit);
                //cmd.AddParameters(vendors.FederalTaxNum, "@FederalTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.FederalTaxValue, "@FederalTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.StateTaxNum, "@StateTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.StateTaxValue, "@StateTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.CityTaxNum, "@CityTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.CityTaxValue, "@CityTaxValue", SqlDbType.Decimal);
                //cmd.AddParameters(vendors.CountyTaxNum, "@CountyTaxNum", SqlDbType.VarChar);
                //cmd.AddParameters(vendors.CountyTaxValue, "@CountyTaxValue", SqlDbType.Decimal);
                cmd.AddParameters(customer.IsTaxable, CommonConstants.IsTaxable, SqlDbType.Bit);
                cmd.AddParameters(customer.FederalTaxNum, CommonConstants.FederalTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.FederalTaxValue, CommonConstants.FederalTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(customer.StateTaxNum, CommonConstants.StateTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.StateTaxValue, CommonConstants.StateTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(customer.CityTaxNum, CommonConstants.CityTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.CityTaxValue, CommonConstants.CityTaxValue, SqlDbType.Decimal);
                cmd.AddParameters(customer.CountyTaxNum, CommonConstants.CountyTaxNum, SqlDbType.VarChar);
                cmd.AddParameters(customer.CountyTaxValue, CommonConstants.CountyTaxValue, SqlDbType.Decimal);

                cmd.AddParameters(customer.Email, CommonConstants.Email, SqlDbType.VarChar);
                cmd.AddParameters(customer.Phone, CommonConstants.Phone, SqlDbType.VarChar);
                cmd.AddParameters(customer.Fax, CommonConstants.Fax, SqlDbType.VarChar);
                cmd.AddParameters(customer.CommStreet, "@CommStreet", SqlDbType.VarChar);
                cmd.AddParameters(customer.CommCityId, "@CommCityId", SqlDbType.Int);
                cmd.AddParameters(customer.CommStateId, "@CommStateId", SqlDbType.Int);
                cmd.AddParameters(customer.CommCountryId, "@CommCountryId", SqlDbType.Int);
                cmd.AddParameters(customer.Commzip, "@Commzip", SqlDbType.VarChar);
                cmd.AddParameters(customer.Website, CommonConstants.Website, SqlDbType.VarChar);

                cmd.AddParameters(customer.BillingStreet, "@BillingStreet", SqlDbType.VarChar);
                cmd.AddParameters(customer.BillingCityId, "@BillingCityId", SqlDbType.Int);
                cmd.AddParameters(customer.BillingStateId, "@BillingStateId", SqlDbType.Int);
                cmd.AddParameters(customer.BillingCountryId, "@BillingCountryId", SqlDbType.SmallInt);
                cmd.AddParameters(customer.Billingzip, "@Billingzip", SqlDbType.VarChar);

                cmd.AddParameters(customer.ShippingStreet, "@ShippingStreet", SqlDbType.VarChar);
                cmd.AddParameters(customer.ShippingCityId, "@ShippingCityId", SqlDbType.Int);
                cmd.AddParameters(customer.ShippingStateId, "@ShippingStateId", SqlDbType.Int);
                cmd.AddParameters(customer.ShippingCountryId, "@ShippingCountryId", SqlDbType.SmallInt);
                cmd.AddParameters(customer.ShippingZip, "@Shippingzip", SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinesCustomers);
            }
            return true;        
        }
    }
}
