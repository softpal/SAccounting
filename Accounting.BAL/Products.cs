using System;
using System.Collections.Generic;
using StratusAccounting.Models;
using StratusAccounting.DAL;
using System.Data;
namespace StratusAccounting.BAL
{
    public static class Products
    {
        public static bool SaveProducts(UMst_BusinessItems products)
        {
            using (var db= new AccountingEntities())
            {
                
            }
            //using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            //{
            //    try
            //    {
            //        cmd.AddParameters(products.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
            //        cmd.AddParameters(products.UniqueId, CommonConstants.ProductsUniqueId, SqlDbType.VarChar);
            //        cmd.AddParameters(products.Title, CommonConstants.ProductsTitle, SqlDbType.VarChar);
            //        cmd.AddParameters(products.Description, CommonConstants.ProductsDesc, SqlDbType.VarChar);
            //        cmd.AddParameters(products.CostPrice, CommonConstants.CostPrice, SqlDbType.Money);
            //        cmd.AddParameters(products.DiscountPrice, CommonConstants.DiscountPrice, SqlDbType.Money);
            //        cmd.AddParameters(products.PreferredPrice, CommonConstants.PreferredPrice, SqlDbType.Money);
            //        cmd.AddParameters(products.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.Int);
            //        cmd.AddParameters(products.Taxable, CommonConstants.Taxable, SqlDbType.Bit);
            //        // cmd.AddParameters(products.IsActive, CommonConstants.IsActive, System.Data.SqlDbType.Bit);
            //        if (products.ItemId > 0)
            //        {
            //            cmd.AddParameters(products.ItemId, CommonConstants.ProductsId, SqlDbType.Int);
            //            cmd.AddParameters(products.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
            //        }
            //        else
            //        {
            //            cmd.AddParameters(products.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
            //        }
            //        cmd.ExecuteNonQuery(SqlProcedures.Insert_Prouducts);
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            return true;
        }

        public static IList<UMst_UserAccounts> GetUserAccountTypes(int businessId)
        {
            List<UMst_UserAccounts> userAccounts = new List<UMst_UserAccounts>();
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader("[Security].[Get_ItemsCategory]");
                    while (ireader.Read())
                    {
                        var userAccount = new UMst_UserAccounts
                                 {
                                     AccountName = ireader.GetString(CommonColumns.AccountName),
                                     UserAccountsId = ireader.GetInt32(CommonColumns.UserAccountsId),
                                     //BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                                     //AccountNum = ireader.GetInt32(CommonColumns.AccountNum),
                                     //ParentAccountName = ireader.GetString(CommonColumns.ParentAccountName)
                                 };
                        userAccounts.Add(userAccount);
                    }
                    return userAccounts;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<UMst_UserParentAccounts> GetUserParentAccountTypes(int businessId)
        {
            List<UMst_UserParentAccounts> userParentAccounts = new List<UMst_UserParentAccounts>();
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UserParentAccountTypes);
                    while (ireader.Read())
                    {
                        var userParentAccount = new UMst_UserParentAccounts
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            UserParentAccountsId = ireader.GetInt32(CommonColumns.UserParentAccountsId),
                            AccountName = ireader.GetString(CommonColumns.AccountName)
                        };
                        userParentAccounts.Add(userParentAccount);
                    }
                    return userParentAccounts;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public static IList<UMst_Products> GetProducts(int businessId, int userId)
        //{
        //    List<UMst_Products> products = new List<UMst_Products>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
        //            cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
        //            IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessProducts);
        //            while (ireader.Read())
        //            {
        //                var product = new UMst_Products
        //                         {
        //                             // BusinessID = ireader.GetInt32("BusinessID"),
        //                             ProductsId = ireader.GetInt64(CommonColumns.ProductsId),
        //                             ProductsTitle = ireader.GetString(CommonColumns.ProductsTitle),
        //                             ProductsDesc = ireader.GetString(CommonColumns.ProductsDesc),
        //                             ProductsUniqueId = ireader.GetString(CommonColumns.ProductsUniqueId),
        //                             CostPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.CostPrice)),
        //                             DiscountPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.DiscountPrice)),
        //                             PreferredPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.PreferredPrice)),
        //                             AccountName = ireader.GetString(CommonColumns.AccountName),
        //                             UserAccountsId = ireader.GetInt64(CommonColumns.UserAccountsId),
        //                             Taxable = ireader.GetBoolean(CommonColumns.Taxable),
        //                         };
        //                products.Add(product);
        //            }
        //            return products;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public static bool DeleteProduct(int businessId, int userId, long productId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(productId, CommonColumns.ProductsId, SqlDbType.BigInt);
                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BusinessProducts);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return false;
        }

        //public static IList<UMst_Products> SearchProducts(int BusinessId, int UserId, string keyword)
        //{
        //    List<UMst_Products> products = new List<UMst_Products>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(BusinessId, CommonColumns.BusinessID, SqlDbType.Int);
        //            cmd.AddParameters(UserId, CommonColumns.UserId, SqlDbType.Int);
        //            cmd.AddParameters(keyword, "@keyword", SqlDbType.VarChar);

        //            IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_SearchBusinessProducts);

        //            while (ireader.Read())
        //            {
        //                var product = new UMst_Products
        //                {
        //                    // BusinessID = ireader.GetInt32("BusinessID"),
        //                    ProductsId = ireader.GetInt64(CommonColumns.ProductsId),
        //                    ProductsTitle = ireader.GetString(CommonColumns.ProductsTitle)
        //                };
        //                products.Add(product);
        //            }
        //            return products;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

       
    }
}
