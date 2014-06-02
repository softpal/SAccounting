using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intuit.Ipp.Data;
using StratusAccounting.DAL;
using StratusAccounting.Models;

namespace StratusAccounting.BAL
{
    public class BusinessItemTypes
    {
        public static IList<UMst_BusinessItems> GetBusinessItems(int businessId, int userId, BusinessItemsTypes businessItemsTypes)
        {
            try
            {
                //using (var dbEntities = new AccountingEntities())
                //{
                //    IList<UMst_BusinessItems> listItem = dbEntities.Get_BusinessItems(businessId, userId, Convert.ToInt16(businessItemsTypes)).Select(
                //        businessItem => new UMst_BusinessItems
                //        {
                //            BusinessID = businessId,
                //            CostPrice = businessItem.CostPrice,
                //            Description = businessItem.Description,
                //            ItemId = businessItem.ItemId,
                //            DiscountPrice = businessItem.DiscountPrice,
                //            ItemTypeId = businessItem.ItemTypeId,
                //            Taxable = businessItem.Taxable,
                //            Title = businessItem.Title,
                //            PreferredPrice = businessItem.PreferredPrice,
                //            UserAccountsId = businessItem.UserAccountsId,
                //            UniqueId = businessItem.UniqueId,
                //        }).ToList();
                //    return listItem;
                //}
                IList<UMst_BusinessItems> businessItemses = new List<UMst_BusinessItems>();
                using (var cmd = new DBSqlCommand())
                {
                    cmd.AddParameters(businessId, "@BusinessID", SqlDbType.Int);
                    cmd.AddParameters(userId, "@UserId", SqlDbType.Int);
                    cmd.AddParameters(Convert.ToInt16(businessItemsTypes), "@ItemTypeId", SqlDbType.Int);
                    IDataReader reader = cmd.ExecuteDataReader("[Security].[Get_BusinessItems]");
                    while (reader.Read())
                    {
                        var businessItem = new UMst_BusinessItems()
                        {
                            BusinessID = businessId,
                            CostPrice = reader.GetNullableDecimal(reader.GetOrdinal("CostPrice")),
                            Description = reader.GetString("Description"),
                            ItemId = reader.GetInt32("ItemId"),
                            DiscountPrice = reader.GetNullableDecimal(reader.GetOrdinal("DiscountPrice")),
                            ItemTypeId = reader.GetInt16("ItemTypeId"),
                            Taxable = reader.GetBoolean("Taxable"),
                            Title = reader.GetString("Title"),
                            PreferredPrice = reader.GetNullableDecimal(reader.GetOrdinal("PreferredPrice")),
                            UserAccountsId = reader.GetInt32("UserAccountsId"),
                            UniqueId = reader.GetString("UniqueId")
                        };
                        businessItemses.Add(businessItem);
                    }
                }
                return businessItemses;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Exception in GetBusinessItems: " + ex.Message);
            }
        }

        public static bool SaveorEditBusinessItems(UMst_BusinessItems businessItems)
        {
            try
            {
                //using (var dbEntities = new AccountingEntities())
                //{
                //    if (businessItems.ItemId == 0)
                //    {
                //        int returnValue = dbEntities.Insert_BusinessItems(businessItems.CreatedByUserId, businessItems.BusinessID,
                //        businessItems.Title, businessItems.UniqueId, businessItems.Description, businessItems.CostPrice,
                //        businessItems.DiscountPrice, businessItems.PreferredPrice, businessItems.UserAccountsId, businessItems.Taxable,
                //        businessItems.ItemTypeId);
                //        return returnValue > 0 ? true : false;
                //    }
                //    else
                //    {
                //        dbEntities.Update_BusinessItems(businessItems.CreatedByUserId, businessItems.BusinessID, businessItems.ItemId,
                //        businessItems.Title, businessItems.UniqueId, businessItems.Description, businessItems.CostPrice,
                //        businessItems.DiscountPrice, businessItems.PreferredPrice, businessItems.UserAccountsId, businessItems.Taxable
                //        );
                //        return true;
                //    }
                //}

                using (var cmd = new DBSqlCommand())
                {

                    cmd.AddParameters(businessItems.BusinessID, "@BusinessID", System.Data.SqlDbType.Int);
                    cmd.AddParameters(businessItems.Title, "@Title",
                        System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(businessItems.UniqueId, "@UniqueId",
                        System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(businessItems.Description, "@Desc", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(businessItems.CostPrice, "@CostPrice", System.Data.SqlDbType.Money);
                    cmd.AddParameters(businessItems.DiscountPrice, "@DiscountPrice", System.Data.SqlDbType.Money);
                    cmd.AddParameters(businessItems.PreferredPrice, "@PreferredPrice",
                        System.Data.SqlDbType.Money);
                    cmd.AddParameters(businessItems.UserAccountsId, "@UserAccountsId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(businessItems.Taxable, "@Taxable", System.Data.SqlDbType.Bit);


                    if (businessItems.ItemId == 0)
                    {
                        cmd.AddParameters(businessItems.ItemTypeId, "@ItemTypeId", SqlDbType.Int);
                        cmd.AddParameters(businessItems.CreatedByUserId, "@UserId", System.Data.SqlDbType.Int);
                        cmd.ExecuteNonQuery("[Security].[Insert_BusinessItems]");
                    }
                    else
                    {
                        cmd.AddParameters(businessItems.ModifiedByUserId, "@UserId", System.Data.SqlDbType.Int);
                        cmd.AddParameters(businessItems.ItemId, "@ItemId", SqlDbType.Int);
                        cmd.ExecuteNonQuery("[Security].[Update_BusinessItems]");
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Exception in SaveBusinessItems: " + ex.Message);
            }
        }

        public static bool DeleteBusinessItems(int itemId, int businessId, int userId)
        {
            try
            {
                using (var cmd = new DBSqlCommand())
                {
                    cmd.AddParameters(businessId, "@BusinessID", System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, "@UserId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(itemId, "@ItemId", SqlDbType.Int);
                    return cmd.ExecuteNonQuery("[Security].[Delete_BusinessItems]");
                }
                //using (var dbEntities = new AccountingEntities())
                //{
                //    return dbEntities.Delete_BusinessItems(userId, businessId, itemId) > 0 ? true : false;
                //}

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Exception in DeleteBusinessItems: " + ex.Message);
            }
        }
    }
}
