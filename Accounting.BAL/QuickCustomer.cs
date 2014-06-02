using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    using Intuit.Ipp.Core;
    using Intuit.Ipp.DataService;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Security;
    using System.Collections.Generic;

    public class QuickCustomer
    {
        //QuickCustomer customer = new QuickCustomer();
        //public static bool AddCustomers(Customer customer)
        //{
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(customer.AcctNum, CommonConstants.AcctNum, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.ContactName, CommonConstants.ContactName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.CompanyName, CommonConstants.CompanyName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.AltContactName, CommonConstants.AltContactName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.AlternatePhone, CommonConstants.AlternatePhone, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.AnyIntuitObject, CommonConstants.AnyIntuitObject, System.Data.SqlDbType.r);
        //            cmd.AddParameters(customer.AttachableRef, CommonConstants.AttachableRef, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.Balance, CommonConstants.Balance, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.BalanceSpecified, CommonConstants.BalanceSpecified, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.BalanceWithJobs, CommonConstants.BalanceWithJobs, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.BalanceWithJobsSpecified, CommonConstants.BalanceWithJobsSpecified, System.Data.SqlDbType.Bit);
        //            cmd.AddParameters(customer.BillAddr, CommonConstants.BillAddr, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.BillWithParent, CommonConstants.BillWithParent, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.BillWithParentSpecified, CommonConstants.BillWithParentSpecified, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.CCDetail, CommonConstants.CCDetail, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.CreditLimit, CommonConstants.CreditLimit, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.CreditLimitSpecified, CommonConstants.CreditLimitSpecified, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.CurrencyRef, CommonConstants.CurrencyRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.CustomerEx, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.CustomerTypeRef, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.CreditLimit, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.DefaultTaxCodeRef, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.DisplayName, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.domain, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.FamilyName, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Fax, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.FullyQualifiedName, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.GivenName, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.HeaderFull, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.HeaderLite, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Id, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.IntuitId, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.ItemElementName, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Job, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.JobInfo, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.JobSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Level, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.LevelSpecified, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.MetaData, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.MiddleName, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Mobile, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.NameAndId, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Notes, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.OpenBalanceDate, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.OpenBalanceDateSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Organization, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.OrganizationSpecified, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.OtherAddr, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.OtherContactInfo, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.OverDueBalance, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.OverDueBalanceSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Overview, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.ParentRef, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.PaymentMethodRef, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.PreferredDeliveryMethod, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.PriceLevelRef, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.PrimaryEmailAddr, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.PrimaryPhone, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.PrintOnCheckName, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.ResaleNum, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.RootCustomerRef, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.SalesRepRef, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.SalesTermRef, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.ShipAddr, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.sparse, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.sparseSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.status, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.statusSpecified, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.Suffix, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.SyncToken, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Taxable, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.TaxableSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Title, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.TotalExpense, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.TotalExpenseSpecified, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.TotalRevenue, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.TotalRevenueSpecified, CommonConstants.CustomerTypeRef, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.UserId, CommonConstants.CreditLimit, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.WebAddr, CommonConstants.CustomerEx, System.Data.SqlDbType.Int);


        //            // cmd.AddParameters(customer.IsActive, CommonConstants.IsActive, System.Data.SqlDbType.Bit);
        //            if (customer.UserCustomerId > 0)
        //            {
        //                cmd.AddParameters(customer.UserCustomerId, CommonConstants.UserCustomerId, System.Data.SqlDbType.Int);
        //                cmd.AddParameters(customer.ModifiedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            }
        //            else
        //            {
        //                cmd.AddParameters(customer.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            }
        //            cmd.ExecuteNonQuery(SqlProcedures.Insert_QuickCustomer);
        //        }
        //        catch (Exception ex)
        //        { }
        //    }
        //    return true;
        //}



        //public static bool AddCustomers(UserCustomer customer)
        //{
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(customer.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.UserCustomerId, CommonConstants.UserCustomerId, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.CompanyName, CommonConstants.CompanyName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.CustomerFirstName, CommonConstants.CustomerFirstName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.CustomerLastName, CommonConstants.CustomerLastName, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.BusinessTypeId, CommonConstants.BusinessTypeId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.Website, CommonConstants.Website, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.PaymentTypesId, CommonConstants.PaymentTypesId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.CreditCardToken, CommonConstants.CreditCardToken, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(customer.CreatedDate, CommonConstants.CreatedDate, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.CreatedByUserId, CommonConstants.CreatedByUserId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(customer.ModifiedDate, CommonConstants.ModifiedDate, System.Data.SqlDbType.DateTime);
        //            cmd.AddParameters(customer.ModifiedByUserId, CommonConstants.ModifiedByUserId, System.Data.SqlDbType.Int);
        //            // cmd.AddParameters(customer.IsActive, CommonConstants.IsActive, System.Data.SqlDbType.Bit);
        //            if (customer.UserCustomerId > 0)
        //            {
        //                cmd.AddParameters(customer.UserCustomerId, CommonConstants.UserCustomerId, System.Data.SqlDbType.Int);
        //                cmd.AddParameters(customer.ModifiedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            }
        //            else
        //            {
        //                cmd.AddParameters(customer.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            }
        //            cmd.ExecuteNonQuery(SqlProcedures.Insert_QuickCustomer);
        //        }
        //        catch (Exception ex)
        //        { }
        //    }
        //    return true;
        //}

        //public static IList<UserCustomer> GetAllQuickCustomers(int businessId)
        //{
        //    List<UserCustomer> customerAccounts = new List<UserCustomer>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.GetAll_UserCustomers);
        //            while (ireader.Read())
        //            {
        //                var userAccount = new UserCustomer
        //                {
        //                    CompanyName = ireader.GetString(CommonColumns.CompanyName),
        //                    UserCustomerId = ireader.GetInt32(CommonColumns.UserCustomerId)
        //                };
        //                customerAccounts.Add(userAccount);
        //            }
        //            return customerAccounts;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public static UserCustomer GetQuickCustomer(int userCustomerId)
        //{
        //    UserCustomer userAccounts = new UserCustomer();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(userCustomerId, CommonColumns.UserCustomerId, System.Data.SqlDbType.Int);
        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UserCustomer);
        //            while (ireader.Read())
        //            {
        //                //var userAccount = new UserCustomer
        //                //{
        //                //    CompanyName = ireader.GetString(CommonColumns.CompanyName),
        //                //    UserCustomerId = ireader.GetInt32(CommonColumns.UserCustomerId)
        //                //};
        //                //userAccounts.Add(userAccount);
        //            }
        //            return userAccounts;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }

    //public class AsyncCallCompletedEventArgs : AsyncCompletedEventArgs
    //{
    //}

}
