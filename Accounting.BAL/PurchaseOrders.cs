using System;
using System.Collections.Generic;
using StratusAccounting.Models;
using StratusAccounting.DAL;
using System.Data;

namespace StratusAccounting.BAL
{
    public static class PurchaseOrders
    {
        public static IList<PurchaseOrder> GetPurchaseOrders(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            List<PurchaseOrder> pOrders = new List<PurchaseOrder>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);

                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PurchaseOrders);
                    var statuses = new List<Mst_Invoicestatus>();
                    statuses = BAL.Invoices.GetInvoiceStatuses();
                    while (ireader.Read())
                    {
                        var item = new PurchaseOrder
                        {
                            //Amount = ireader.GetDecimal(CommonColumns.Amount),
                            Amount = ireader.GetFormatDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            ExpiryDate = ireader.GetDateTime(CommonColumns.ExpiryDate),
                            PONumber = ireader.GetString(CommonColumns.PONumber),
                            Status = ireader.GetString(CommonColumns.Status),
                            VendorName = ireader.GetString(CommonColumns.Vendor),
                            PurchaseOrderId = ireader.GetInt64(CommonColumns.PurchaseOrderId),
                            StatusList = statuses
                        };
                        pOrders.Add(item);

                    }

                    return pOrders;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool SavePurchaseOrder(PurchaseOrder_DTO objPurchase)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(objPurchase.PurchaseOrder.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.PurchaseOrder.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.PurchaseOrder.POTitle, CommonConstants.POTitle, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(objPurchase.PurchaseOrder.ExpiryDate, CommonConstants.ExpiryDate, System.Data.SqlDbType.DateTime);
                    cmd.AddParameters(objPurchase.PurchaseOrder.Amount, CommonConstants.Amount, System.Data.SqlDbType.Money);

                    if (objPurchase.PurchaseOrder.PurchaseOrderId == 0)
                        cmd.AddParameters(objPurchase.PurchaseOrder.UserVendorId, CommonConstants.VendorId, System.Data.SqlDbType.BigInt);
                    if (objPurchase.PurchaseOrder.PurchaseOrderId == 0)
                        cmd.AddParameters(objPurchase.PurchaseOrder.StatusId, CommonConstants.StatusId, System.Data.SqlDbType.SmallInt);

                    if (objPurchase.PurchaseOrder.PurchaseOrderId > 0)
                        cmd.AddParameters(objPurchase.PurchaseOrder.PurchaseOrderId, CommonConstants.PurchaseOrderId, System.Data.SqlDbType.BigInt);

                    int PurchaseOrderId = 0;
                    if (objPurchase.PurchaseOrder.PurchaseOrderId == 0)
                    {
                        var _PurchaseOrderId = cmd.ExecuteScalar(SqlProcedures.Insert_PurchaseOrders);
                        PurchaseOrderId = Convert.ToInt32(_PurchaseOrderId);
                    }
                    else
                    {
                        var _PurchaseOrderId = cmd.ExecuteScalar(SqlProcedures.Update_PurchaseOrders);
                        PurchaseOrderId = Convert.ToInt32(objPurchase.PurchaseOrder.PurchaseOrderId);
                    }

                    if (PurchaseOrderId > 0)
                    {
                        foreach (PurchaseOrderDetail item in objPurchase.PurchaseOrderDetail)
                        {
                            BAL.PurchaseOrders.SavePurchaseOrderDetails(PurchaseOrderId, item);
                        }
                    }
                    //cmd.ExecuteNonQuery(SqlProcedures.Insert_Invoices);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<UMst_BusinessItems> GetBusinessItems(int businessId, int userId)
        {
            List<UMst_BusinessItems> businessItems = new List<UMst_BusinessItems>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);

                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessItems);

                    while (ireader.Read())
                    {
                        var item = new UMst_BusinessItems
                         {
                             // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                             ItemId = ireader[CommonColumns.ItemId] != System.DBNull.Value ? ireader.GetInt64(CommonColumns.ItemId) : 0,
                             Title = ireader[CommonColumns.Title] != System.DBNull.Value ? ireader.GetString(CommonColumns.Title) : string.Empty,
                             Amount = ireader[CommonColumns.Amount] != System.DBNull.Value ? ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)) : 0,
                             Description = ireader[CommonColumns.Description] != System.DBNull.Value ? ireader.GetString(CommonColumns.Description) : string.Empty
                             //  ItemTypeId = ireader[CommonColumns.ItemTypeId] != System.DBNull.Value ? ireader.GetInt32(CommonColumns.ItemTypeId) : 0
                         };
                        businessItems.Add(item);

                    }
                    return businessItems;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool SavePurchaseOrderDetails(int purchaseID, PurchaseOrderDetail item)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(item.BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(purchaseID, CommonConstants.PurchaseOrderId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.ItemId, CommonConstants.ItemId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.Description, CommonConstants.Description, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(item.Quantity, CommonConstants.Quantity, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.Amount, CommonConstants.Amount, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.Tax, CommonConstants.Tax, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.TaxPercent, CommonConstants.TaxPercent, System.Data.SqlDbType.Decimal);
                    if (item.PODetailsId > 0)
                        cmd.AddParameters(item.PODetailsId, CommonConstants.PODetailsId, System.Data.SqlDbType.BigInt);

                    if (item.PODetailsId == 0)
                        cmd.ExecuteNonQuery(SqlProcedures.Insert_PurchaseOrdersDetails);
                    else if (item.PODetailsId > 0)
                        cmd.ExecuteNonQuery(SqlProcedures.Update_PurchaseOrdersDetails);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SavePurchaseInvoiceDetails(PurchaseOrder_DTO objPurchase)
        {
            foreach (var item in objPurchase.PurchaseOrderDetail)
            {
                using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
                {
                    try
                    {
                        cmd.AddParameters(item.ItemId, CommonConstants.ItemId, SqlDbType.Int);
                        cmd.AddParameters(item.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                        cmd.AddParameters(item.Description, CommonConstants.Description, SqlDbType.VarChar);
                        cmd.AddParameters(item.Tax, CommonConstants.Tax, SqlDbType.Decimal);
                        cmd.AddParameters(item.TaxPercent, CommonConstants.TaxPercent, SqlDbType.Decimal);
                        cmd.AddParameters(item.Quantity, CommonConstants.Quantity, SqlDbType.Int);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    int PurchaseInvoiceId = Convert.ToInt32(cmd.ExecuteNonQuery(SqlProcedures.Insert_Invoices));
                }
            }
            return true;
        }

        public static IList<PurchaseOrder> GetPODetailsByVendorId(int businessId, int userId, int vendorId)
        {
            List<PurchaseOrder> pOInvoiceDetails = new List<PurchaseOrder>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonColumns.VendorId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_POsByVendorId);
                    while (ireader.Read())
                    {
                        var pOInvoiceDetail = new PurchaseOrder
                        {
                            //BusinessID = ireader.GetInt32("BusinessID"),
                            //BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            PurchaseOrderId = ireader.GetInt16(CommonColumns.PurchaseOrderId),
                            POTitle = ireader.GetString(CommonColumns.PurchaseOrder),
                            PONumber = ireader.GetString(CommonColumns.PONumber),
                        };

                        pOInvoiceDetails.Add(pOInvoiceDetail);

                    }
                    return pOInvoiceDetails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<PurchaseOrderDetail> GetPurchaseOrderDetailsByPOID(int businessId, int userId, int vendorId, int poId)
        {
            List<PurchaseOrderDetail> poDetails = new List<PurchaseOrderDetail>();

            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonColumns.VendorId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(poId, CommonColumns.PurchaseOrderId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PODetailsByPoId);
                    while (ireader.Read())
                    {
                        var item = new PurchaseOrderDetail
                        {
                            //Amount = ireader.GetDecimal(CommonColumns.Amount),
                            PurchaseOrderId = ireader.GetInt64(CommonColumns.PurchaseOrderId),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            ItemId = ireader.GetInt64(CommonColumns.ItemId),
                            Description = ireader.GetString(CommonColumns.Description),
                            Quantity = ireader.GetInt32(CommonColumns.Quantity),
                            Tax = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Tax))

                        };
                        poDetails.Add(item);

                    }
                    return poDetails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<PurchaseOrder> GetPurchaseOrderByPoID(int businessId, int userId, int poId)
        {
            var polist = new List<PurchaseOrder>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(poId, CommonConstants.PurchaseOrderId, SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PurchaseOrderFields);
                    while (ireader.Read())
                    {
                        var po = new PurchaseOrder
                        {
                            PONumber = ireader.GetString(CommonColumns.PONumber),
                            ExpiryDate = ireader.GetDateTime(CommonColumns.ExpiryDate),
                            VendorName = ireader.GetString(CommonColumns.Vendor),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            Status = ireader.GetString(CommonColumns.Status),
                            POTitle = ireader.GetString(CommonColumns.POTitle),
                            PurchaseOrderId = ireader.GetInt32(CommonColumns.PurchaseOrderId),
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            // CreatedByUserId = ireader.GetInt32(CommonColumns.UserVendorId),
                            StatusId = ireader.GetInt16(CommonColumns.StatusId),
                            ExpiryDtString = ireader.GetString(CommonColumns.ExpiryDate),
                            UserVendorId = ireader.GetInt32(CommonColumns.UserVendorId),

                        };
                        polist.Add(po);

                    }
                    return polist;
                }
                catch (Exception ex)
                {
                    return null;
                }

                return polist;

            }
        }

        public static List<PurchaseOrderDetail> GetPoDetailsByPOID(int businessId, int userId, int vendorId, int poId)
        {
            List<PurchaseOrderDetail> poDetails = new List<PurchaseOrderDetail>();

            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonColumns.VendorId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(poId, CommonColumns.PurchaseOrderId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PODetailsByPoId);
                    while (ireader.Read())
                    {
                        var item = new PurchaseOrderDetail
                        {
                            //Amount = ireader.GetDecimal(CommonColumns.Amount),
                            PurchaseOrderId = ireader.GetInt64(CommonColumns.PurchaseOrderId),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            ItemId = ireader.GetInt64(CommonColumns.ItemId),
                            Description = ireader.GetString(CommonColumns.Description),
                            Quantity = ireader.GetInt32(CommonColumns.Quantity),
                            Tax = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Tax)),
                            ItemName = ireader.GetString(ireader.GetOrdinal(CommonColumns.Item)),
                            PODetailsId = ireader.GetInt32(CommonColumns.PODetailsId)
                        };
                        poDetails.Add(item);

                    }
                    return poDetails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool UpdatePOstatus(PurchaseOrder_DTO objPurchase)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(objPurchase.PurchaseOrder.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.PurchaseOrder.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.PurchaseOrder.StatusId, CommonConstants.InvoiceStatusId, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(objPurchase.PurchaseOrder.PurchaseOrderId, CommonConstants.PurchaseOrderId, System.Data.SqlDbType.BigInt);

                    var _PurchaseOrderId = cmd.ExecuteScalar(SqlProcedures.Update_PurchaseOrderStatus);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
