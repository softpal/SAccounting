using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    public class Services
    {
        //public static bool SaveServices(UMst_Services services)
        //{
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {

               
        //        cmd.AddParameters(services.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
        //        cmd.AddParameters(services.ServicesTitle, CommonConstants.ServicesTitle, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(services.ServicesUniqueId, CommonConstants.ServicesUniqueId, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(services.ServicesDesc, CommonConstants.ServicesDesc, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(services.CostPrice, CommonConstants.CostPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(services.DiscountPrice, CommonConstants.DiscountPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(services.PreferredPrice, CommonConstants.PreferredPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(services.UserAccountsId, CommonConstants.UserAccountsId, System.Data.SqlDbType.Int);
        //        cmd.AddParameters(services.Taxable, CommonConstants.Taxable, System.Data.SqlDbType.Bit);
        //        if (services.ServicesId > 0)
        //        {
        //            cmd.AddParameters(services.ServicesId, CommonConstants.ServicesId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(services.ModifiedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessServices);
        //    }
        //        else
        //        {
        //            cmd.AddParameters(services.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinessServices);
        //        }
               
        //    }
        //    return true;
        //}

        //public static IList<UMst_Services> GetServices(int businessId, int userId)
        //{
        //    List<UMst_Services> services = new List<UMst_Services>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessServices);

        //            while (ireader.Read())
        //            {
        //                var service = new UMst_Services
        //                {

        //                    ServicesId = ireader.GetInt64(CommonColumns.ServicesId),
        //                    ServicesTitle = ireader.GetString(CommonColumns.ServicesTitle),
        //                    ServicesDesc = ireader.GetString(CommonColumns.ServicesDesc),
        //                    ServicesUniqueId = ireader.GetString(CommonColumns.ServicesUniqueId),
        //                    CostPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.CostPrice)),
        //                    DiscountPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.DiscountPrice)),
        //                    PreferredPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.PreferredPrice)),
        //                    AccountName = ireader.GetString(CommonColumns.AccountName),
        //                    UserAccountsId = ireader.GetInt64(CommonColumns.UserAccountsId),
        //                    Taxable = ireader.GetBoolean(CommonColumns.Taxable),
        //                    //CreatedByUserId = ireader.GetInt32("CreatedByUserId"),
        //                    //ModifiedByUserId = ireader.GetInt32("ModifiedByUserId"),
        //                    //CreatedDate = ireader.GetDateTime("CreatedDate"),
        //                    //ModifiedDate = ireader.GetDateTime("ModifiedDate"),
        //                    //IsActive = ireader.GetBoolean("IsActive")

        //                };
        //                services.Add(service);

        //            }
        //            return services;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        //}

        public static bool DeleteService(int businessId, int userId, long serviceId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(serviceId, CommonColumns.ServicesId, System.Data.SqlDbType.BigInt);


                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BusinessServices);
                    return true;

                }
                catch (Exception ex)
                { }
            }
            return false;
        }

        //public static IList<UMst_Services> SearchServices(int BusinessId, int UserId, string keyword)
        //{
        //    List<UMst_Services> services = new List<UMst_Services>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(BusinessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(UserId, CommonColumns.UserId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(keyword, "@keyword", System.Data.SqlDbType.VarChar);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_SearchBusinessServices);

        //            while (ireader.Read())
        //            {
        //                var service = new UMst_Services
        //                {
        //                    ServicesId = ireader.GetInt64(CommonColumns.ServicesId),
        //                    ServicesTitle = ireader.GetString(CommonColumns.ServicesTitle)
        //                };
        //                services.Add(service);
        //            }

        //            return services;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
