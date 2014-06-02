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
    public class Invoices
    {
        public static bool SavePurchaseInvoice(PurchaseInvoice invoice)
        {
            //List<PurchaseInvoiceDetail> plist = new List<PurchaseInvoiceDetail>();
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(invoice.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(invoice.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(invoice.UserVendorId, CommonConstants.VendorId, SqlDbType.BigInt);
                    cmd.AddParameters(invoice.InvoiceTitle, CommonConstants.InvoiceTitle, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.PurchaseOrderId, CommonConstants.PurchaseOrderId, SqlDbType.BigInt);
                    cmd.AddParameters(invoice.DueTermId, CommonConstants.DueTermId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoice.DueDate, CommonConstants.DueDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoice.Amount, CommonConstants.Amount, SqlDbType.Decimal);

                    var _PurchaseInvoiceId = cmd.ExecuteScalar(SqlProcedures.Insert_Invoices);

                    int PurchaseInvoiceId = Convert.ToInt32(_PurchaseInvoiceId);

                    foreach (PurchaseInvoiceDetail item in invoice.PurchaseInvoiceDetails)
                    {
                        BAL.Invoices.SavePurchaseInvoiceDetails(PurchaseInvoiceId, item);
                    }

                    return true;

                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static bool SavePurchaseInvoiceDetails(int purchaseInvoiceId, PurchaseInvoiceDetail item)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(item.BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(purchaseInvoiceId, CommonConstants.PurchaseInvoiceId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.ItemId, CommonConstants.ItemId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.Description, CommonConstants.Description, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(item.Quantity, CommonConstants.Quantity, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.Amount, CommonConstants.Amount, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.Tax, CommonConstants.Tax, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.TaxPercent, CommonConstants.TaxPercent, System.Data.SqlDbType.Decimal);

                    cmd.ExecuteNonQuery(SqlProcedures.Insert_PurchaseInvoiceDetails);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SaveInvoicePayment(PurchaseInvoice invoice, PurchaseInvoicePayment invoicepayments)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(invoice.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(invoice.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                    cmd.AddParameters(invoice.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                    cmd.AddParameters(invoice.Balance, CommonConstants.Balance, SqlDbType.Decimal);
                    cmd.AddParameters(invoice.PurchaseOrderId, CommonConstants.PurchaseOrderId, SqlDbType.Int);
                    cmd.AddParameters(invoice.CustomField1, CommonConstants.CustomField1, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.CustomField2, CommonConstants.CustomField2, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.CustomField3, CommonConstants.CustomField3, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.DueDate, CommonConstants.DueDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoice.InvoiceNumber, CommonConstants.InvoiceNumber, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.InvoiceStatusId, CommonConstants.InvoiceStatusId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoice.InvoiceTitle, CommonConstants.InvoiceTitle, SqlDbType.VarChar);
                    //cmd.AddParameters(invoice.SalesTermId, CommonConstants.SalesTermId, SqlDbType.SmallInt);
                    //cmd.AddParameters(invoice.ServiceNumber, CommonConstants.ServiceNumber, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoice.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoice.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoice.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoice.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                    cmd.AddParameters(invoicepayments.ItemId, CommonConstants.ItemId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                    //cmd.AddParameters(invoicepayments.PurchaseInvoice, CommonConstants.PurchaseInvoice, SqlDbType.Decimal);
                    cmd.AddParameters(invoicepayments.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
                    cmd.AddParameters(invoicepayments.PaidToTypeId, CommonConstants.PaidToTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PaymentStatusId, CommonConstants.PaymentStatusId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PurchaseInvoicePaymentId, CommonConstants.PurchaseInvoicePaymentId, SqlDbType.BigInt);
                    cmd.AddParameters(invoicepayments.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoicepayments.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoicepayments.IsActive, CommonConstants.IsActive, SqlDbType.Bit);


                    if (invoice.PurchaseInvoiceId > 0 && invoicepayments.PurchaseInvoiceId > 0)
                    {
                        cmd.AddParameters(invoice.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                        cmd.AddParameters(invoicepayments.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                        cmd.AddParameters(invoice.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                        cmd.AddParameters(invoicepayments.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    }
                    else
                    {
                        cmd.AddParameters(invoice.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                        cmd.AddParameters(invoicepayments.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    }
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_PaymentInvoices);
                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static IList<PurchaseInvoice> GetInvoices(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null, string invoiceType = null)
        {
            List<PurchaseInvoice> purchaseInvoice = new List<PurchaseInvoice>();
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
                    cmd.AddParameters(invoiceType, CommonColumns.InvoiceType, System.Data.SqlDbType.VarChar);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Invoices);

                    var statuses = new List<Mst_Invoicestatus>();
                    statuses = BAL.Invoices.GetInvoiceStatuses();

                    while (ireader.Read())
                    {
                        var invoice = new PurchaseInvoice
                        {
                            SNo = ireader.GetInt32(CommonColumns.SNo),
                            PurchaseInvoiceId = ireader.GetInt64(CommonColumns.InvoiceId),
                            InvoiceType = ireader.GetString(CommonColumns.Type),
                            DueDate = ireader.GetDateTime(CommonColumns.DueDate),
                            Customer = ireader.GetString(CommonColumns.InvoiceAgainst),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            InvoiceStatus = ireader.GetString(CommonColumns.Status),
                            Balance = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Balance)),
                            InvoiceNumber = ireader.GetString(CommonColumns.InvoiceId),
                            InvoiceStatusList = statuses
                        };
                        purchaseInvoice.Add(invoice);
                    }
                    return purchaseInvoice;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<PurchaseInvoicePayment> GetPayments(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null, string invoiceType = null)
        {
            List<PurchaseInvoicePayment> purchaseInvoice = new List<PurchaseInvoicePayment>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(invoiceType, CommonColumns.InvoiceType, System.Data.SqlDbType.VarChar);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentInvoices);
                    while (ireader.Read())
                    {
                        var payment = new PurchaseInvoicePayment
                        {
                            SNo = ireader.GetInt32(CommonColumns.SNo),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            // Account = ireader.GetString(CommonColumns.Account),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            PaidTo = ireader.GetString(CommonColumns.PaidTo),
                            Instrument = ireader.GetString(CommonColumns.Instrument),
                            InstrumentNum = ireader.GetString(CommonColumns.InstrumentNum),
                            Status = ireader.GetString(CommonColumns.Status),
                            ReceivedFor = ireader.GetString(CommonColumns.RecievedFor),
                            Tags = ireader.GetString(CommonColumns.Tags),
                            PaymentStatusId = ireader.GetInt16(CommonColumns.PaymentStatusId),
                            //PurchaseInvoicePaymentId = ireader.GetInt64(CommonColumns.PurchaseInvoicePaymentId)
                        };
                        purchaseInvoice.Add(payment);
                    }
                    return purchaseInvoice;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static PurchaseInvoicePayment UpdateInvoices(int purchaseInvoiceid, int businessId, int userId)
        {
            PurchaseInvoicePayment invoicepayments = new PurchaseInvoicePayment();

            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                cmd.AddParameters(purchaseInvoiceid, CommonColumns.PurchaseInvoiceId, System.Data.SqlDbType.Int);
                cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                //cmd.AddParameters(invoiceType, CommonColumns.InvoiceType, System.Data.SqlDbType.VarChar);
                System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentInvoices);

                try
                {
                    cmd.AddParameters(invoicepayments.ItemId, CommonConstants.ItemId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                    //cmd.AddParameters(invoicepayments.PurchaseInvoice, CommonConstants.PurchaseInvoice, SqlDbType.Decimal);
                    cmd.AddParameters(invoicepayments.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
                    cmd.AddParameters(invoicepayments.PaidToTypeId, CommonConstants.PaidToTypeId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PaymentStatusId, CommonConstants.PaymentStatusId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoicepayments.PurchaseInvoicePaymentId, CommonConstants.PurchaseInvoicePaymentId, SqlDbType.BigInt);
                    cmd.AddParameters(invoicepayments.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoicepayments.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                    cmd.AddParameters(invoicepayments.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoicepayments.IsActive, CommonConstants.IsActive, SqlDbType.Bit);
                    if (invoicepayments.PurchaseInvoiceId > 0)
                    {
                        cmd.AddParameters(invoicepayments.PurchaseInvoiceId, CommonConstants.SalesInvoiceId, SqlDbType.Int);
                        cmd.AddParameters(invoicepayments.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    }
                    else
                    {
                        cmd.AddParameters(invoicepayments.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    }
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_Invoices);
                }
                catch (Exception ex)
                { }
            }
            return invoicepayments;
        }

        public static List<Mst_PaymentTypes> GetInstruments()
        {
            var instruments = new List<Mst_PaymentTypes>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InstrumentTypes);
                    while (ireader.Read())
                    {
                        var inst = new Mst_PaymentTypes
                        {
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            PaymentDesc = ireader.GetString(CommonColumns.PaymentType)
                        };
                        instruments.Add(inst);

                    }
                    return instruments;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_PaymentStatus> GetStatuses()
        {
            var statuses = new List<Mst_PaymentStatus>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentStatusTypes);
                    while (ireader.Read())
                    {
                        var status = new Mst_PaymentStatus
                        {
                            PaymentStatusId = ireader.GetInt16(CommonColumns.PaymentStatusId),
                            PaymentDesc = ireader.GetString(CommonColumns.PaymentStatus)
                        };
                        statuses.Add(status);

                    }
                    return statuses;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_UserType> GetCompanyNames()
        {
            var users = new List<Mst_UserType>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UserType);
                    while (ireader.Read())
                    {
                        var user = new Mst_UserType
                        {
                            UserTypeId = ireader.GetInt16(CommonColumns.UserTypeId),
                            UserName = ireader.GetString(CommonColumns.PaidTo)
                        };
                        users.Add(user);

                    }
                    return users;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //public static bool SavePaymentInvoice(PurchaseInvoicePayment objPayment)
        //{
        //    using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(objPayment.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
        //            cmd.AddParameters(objPayment.UserId, CommonConstants.UserId, SqlDbType.Int);
        //            cmd.AddParameters(objPayment.Date, CommonConstants.Date, SqlDbType.DateTime);
        //            cmd.AddParameters(objPayment.Amount, CommonConstants.Amount, SqlDbType.Decimal);
        //            cmd.AddParameters(objPayment.PaidTo, CommonConstants.PaidTo, SqlDbType.SmallInt);
        //            cmd.AddParameters(objPayment.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
        //            cmd.AddParameters(objPayment.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
        //            cmd.AddParameters(objPayment.PaymentStatusId, CommonConstants.PaymentStatusId, SqlDbType.SmallInt);
        //            cmd.AddParameters(objPayment.ReceivedFor, CommonConstants.InvoiceNumber, SqlDbType.VarChar);

        //            cmd.AddParameters(1, "@ItemId", SqlDbType.BigInt);
        //            //cmd.AddParameters(objPayment.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.Int);
        //            cmd.ExecuteNonQuery(SqlProcedures.Insert_PaymentInvoices);
        //        }
        //        catch (Exception ex)
        //        { }
        //    }

        //    return true;
        //}

        // 09th
        //public static IList<PurchaseOrder> GetPOInvoiceDetails(int? businessId, int? userId)

        public static IList<PurchaseOrder> GetPOInvoiceDetails(int businessId)
        {
            List<PurchaseOrder> pOInvoiceDetails = new List<PurchaseOrder>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_POrders);
                    while (ireader.Read())
                    {
                        var pOInvoiceDetail = new PurchaseOrder
                        {
                            //BusinessID = ireader.GetInt32("BusinessID"),
                            //BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            PurchaseOrderId = ireader.GetInt16(CommonColumns.PurchaseOrderId),
                            POTitle = ireader.GetString(CommonColumns.POTitle),
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

        public static IList<Mst_InvoiceDueTerm> GetInvoiceDueTerms()
        {
            List<Mst_InvoiceDueTerm> pOInvoiceDetails = new List<Mst_InvoiceDueTerm>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoiceDueTerms);
                    while (ireader.Read())
                    {
                        var pOInvoiceDetail = new Mst_InvoiceDueTerm
                        {
                            //BusinessId = ireader.GetInt32("BusinessID"),
                            DueTermId = ireader.GetInt16(CommonColumns.DueTermId),
                            Term = ireader.GetString(CommonColumns.Term),
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

        public static List<UMst_BusinessItems> GetPOBusinessItems(int businessId, int userId)
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
                            Title = ireader[CommonColumns.Title] != System.DBNull.Value ? ireader.GetString(CommonColumns.Title) : string.Empty
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

        public static IList<PurchaseOrder> GetPOInvoiceDetails(int businessId, int userId)
        {
            List<PurchaseOrder> pOInvoiceDetails = new List<PurchaseOrder>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_POrders);
                    while (ireader.Read())
                    {
                        var pOInvoiceDetail = new PurchaseOrder
                        {
                            //BusinessID = ireader.GetInt32("BusinessID"),
                            //BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            PurchaseOrderId = ireader.GetInt16(CommonColumns.PurchaseOrderId),
                            POTitle = ireader.GetString(CommonColumns.POTitle),
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

        public static bool SaveInvoicePayment(PurchaseInvoicePayment payment)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(payment.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(payment.CreatedByUserId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(payment.Date, CommonConstants.Date, SqlDbType.DateTime);
                    cmd.AddParameters(payment.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                    cmd.AddParameters(payment.PaidToTypeId, CommonConstants.PaidTo, SqlDbType.SmallInt);
                    cmd.AddParameters(payment.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.SmallInt);
                    cmd.AddParameters(payment.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
                    cmd.AddParameters(payment.PaymentStatusId, CommonConstants.PaymentStatusId, SqlDbType.SmallInt);
                    cmd.AddParameters(payment.ReceivedFor, CommonConstants.InvoiceNumber, SqlDbType.VarChar);
                    cmd.AddParameters(payment.ItemId, CommonConstants.ItemId, SqlDbType.Int);
                    cmd.AddParameters(payment.BankAccountId, CommonConstants.BankAccountsId, SqlDbType.Int);

                    if (payment.PurchaseInvoicePaymentId > 0)
                    {
                        cmd.AddParameters(payment.PurchaseInvoicePaymentId, CommonConstants.PurchaseInvoicePaymentId, SqlDbType.Int);
                        cmd.ExecuteNonQuery(SqlProcedures.Update_InvoicePayments);
                    }
                    else
                        cmd.ExecuteNonQuery(SqlProcedures.Insert_PaymentInvoices);

                    return true;

                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static List<UserBankAccount> GetBankAccountNames(int businessId, int userId, string keyword)
        {
            var statuses = new List<UserBankAccount>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(keyword, CommonConstants.Keyword, SqlDbType.VarChar);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BankAccountNames);
                    while (ireader.Read())
                    {
                        var status = new UserBankAccount
                        {
                            BankAccountsId = ireader.GetInt16(CommonColumns.BankAccountsId),
                            BankName = ireader.GetString(CommonColumns.BankAccountName)
                        };
                        statuses.Add(status);

                    }
                    return statuses;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<UserCustomer> GetSalesReceiptsCustomers(int businessId, int userId, string keyword)
        {
            var statuses = new List<UserCustomer>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(keyword, CommonConstants.Keyword, SqlDbType.VarChar);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_CustomersList);
                    while (ireader.Read())
                    {
                        var status = new UserCustomer
                        {
                            UserCustomerId = ireader.GetInt16(CommonColumns.UserCustomerId),
                            CompanyName = ireader.GetString(CommonColumns.CompanyName)
                        };
                        statuses.Add(status);

                    }
                    return statuses;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static IList<SalesReceipt> GetInvoiceReceipts(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            List<SalesReceipt> InvoiceReceipts = new List<SalesReceipt>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(invoiceType, CommonColumns.InvoiceType, System.Data.SqlDbType.VarChar);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoiceReceipts);
                    while (ireader.Read())
                    {
                        var receipt = new SalesReceipt
                        {
                            SNo = ireader.GetInt32(CommonColumns.SNo),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            ReceiptNum = ireader.GetString(CommonColumns.ReceiptNum),
                            Description = ireader.GetString(CommonColumns.Description),
                            Customer = ireader.GetString(CommonColumns.Customer),
                            PaymentMethod = ireader.GetString(CommonColumns.PaymentMethod)
                        };
                        InvoiceReceipts.Add(receipt);
                    }
                    return InvoiceReceipts;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool SaveInvoiceReceipts(SalesReceipt item)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(item.BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.Amount, CommonConstants.Amount, System.Data.SqlDbType.Money);

                    cmd.AddParameters(item.Date, CommonConstants.Date, System.Data.SqlDbType.DateTime);
                    cmd.AddParameters(item.UserCustomerId, CommonConstants.CustomerId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.PaymentTypesId, CommonConstants.PaymentTypesId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.BankAccountsId, CommonConstants.BankAccountsId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.InvoiceNumber, CommonConstants.InvoiceNumber, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(item.PaymentStatusId, CommonConstants.PaymentStatusId, System.Data.SqlDbType.Int);
                    if (item.ItemId > 0)
                        cmd.AddParameters(item.ItemId, CommonConstants.ItemId, System.Data.SqlDbType.Int);

                    cmd.ExecuteNonQuery(SqlProcedures.Insert_InvoiceReceipts);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<Mst_Invoicestatus> GetInvoiceStatuses()
        {
            var statuses = new List<Mst_Invoicestatus>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoiceStatuses);
                    while (ireader.Read())
                    {
                        var status = new Mst_Invoicestatus
                        {
                            InvoicestatusId = ireader.GetInt16(CommonColumns.InvoiceStatusId),
                            InvoicestatusDesc = ireader.GetString(CommonColumns.Status)
                        };
                        statuses.Add(status);
                    }
                    return statuses;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Estimate> GetEstimatesByCustomer(int BusinessId, int UserId, long CustomerId)
        {
            var estimates = new List<Estimate>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(CustomerId, CommonConstants.CustomerId, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_EstimatesByCustomer);
                    while (ireader.Read())
                    {
                        var estimate = new Estimate
                        {
                            EstimatesId = ireader.GetInt16(CommonColumns.EstimatesId),
                            EstimateNo = ireader.GetString(CommonColumns.Estimate)
                        };
                        estimates.Add(estimate);
                    }
                    return estimates;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        public static IList<PurchaseOrderDetail> GetEstimateDetailsByID(int businessId, int userId, int vendorId, int poId)
        {
            List<PurchaseOrderDetail> poDetails = new List<PurchaseOrderDetail>();

            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(vendorId, CommonColumns.CustomerId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(poId, CommonColumns.EstimatesId, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_EstimateDetails);
                    while (ireader.Read())
                    {
                        var item = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = ireader.GetInt64(CommonColumns.EstimatesId),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            ItemId = ireader.GetInt64(CommonColumns.ItemId),
                            Description = ireader.GetString(CommonColumns.Description),
                            Quantity = ireader.GetInt32(CommonColumns.Quantity),
                            //Tax = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Tax))

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

        public static bool SaveSalesInvoice(PurchaseInvoice invoice)
        {
            //List<PurchaseInvoiceDetail> plist = new List<PurchaseInvoiceDetail>();
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(invoice.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(invoice.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(invoice.UserVendorId, CommonConstants.CustomerId, SqlDbType.BigInt);
                    cmd.AddParameters(invoice.InvoiceTitle, CommonConstants.InvoiceTitle, SqlDbType.VarChar);
                    cmd.AddParameters(invoice.PurchaseOrderId, CommonConstants.EstimatesId, SqlDbType.BigInt);
                    cmd.AddParameters(invoice.DueTermId, CommonConstants.DueTermId, SqlDbType.SmallInt);
                    cmd.AddParameters(invoice.DueDate, CommonConstants.DueDate, SqlDbType.DateTime);
                    cmd.AddParameters(invoice.Amount, CommonConstants.Amount, SqlDbType.Decimal);

                    var _SalesInvoiceId = cmd.ExecuteScalar(SqlProcedures.Insert_SalesInvoices);

                    int SalesInvoiceId = Convert.ToInt32(_SalesInvoiceId);

                    foreach (PurchaseInvoiceDetail item in invoice.PurchaseInvoiceDetails)
                    {
                        BAL.Invoices.SaveSalesInvoiceDetails(SalesInvoiceId, item);
                    }

                    return true;

                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static bool SaveSalesInvoiceDetails(int SalesInvoiceId, PurchaseInvoiceDetail item)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(item.BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(SalesInvoiceId, CommonConstants.SalesInvoiceId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.ItemId, CommonConstants.ItemId, System.Data.SqlDbType.BigInt);
                    cmd.AddParameters(item.Description, CommonConstants.Description, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(item.Quantity, CommonConstants.Quantity, System.Data.SqlDbType.Int);
                    cmd.AddParameters(item.Amount, CommonConstants.Amount, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.Tax, CommonConstants.Tax, System.Data.SqlDbType.Money);
                    cmd.AddParameters(item.TaxPercent, CommonConstants.TaxPercent, System.Data.SqlDbType.Decimal);

                    cmd.ExecuteNonQuery(SqlProcedures.Insert_SalesInvoiceDetails);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<PurchaseInvoice> GetInvoiceNumbers(int businessId, int userId, string keyword)
        {
            var invoiceslist = new List<PurchaseInvoice>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(keyword, CommonConstants.Keyword, SqlDbType.VarChar);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoiceNumbers);
                    while (ireader.Read())
                    {
                        var invocie = new PurchaseInvoice
                        {
                            PurchaseInvoiceId = ireader.GetInt16(CommonColumns.InvoiceId),
                            InvoiceNumber = ireader.GetString(CommonColumns.InvoiceNumber)
                        };
                        invoiceslist.Add(invocie);

                    }
                    return invoiceslist;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool UpdateInvoicestatus(PurchaseInvoice objPurchase)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(objPurchase.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(objPurchase.StatusId, CommonConstants.InvoiceStatusId, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(objPurchase.PurchaseInvoiceId, CommonConstants.PurchaseInvoiceId, System.Data.SqlDbType.BigInt);

                    var _PurchaseInvoiceId = cmd.ExecuteScalar(SqlProcedures.Update_PurchaseInvoiceStatus);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<PurchaseInvoice> GetPurchaseInvoiceByInvID(int businessId, int userId, int InvoiceId)
        {
            var polist = new List<PurchaseInvoice>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(InvoiceId, CommonConstants.PurchaseInvoiceId, SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PurchaseInvoiceByInvoiceID);
                    while (ireader.Read())
                    {
                        var po = new PurchaseInvoice
                        {
                            InvoiceNumber = ireader.GetString(CommonColumns.InvoiceNumber),
                            DueDate = ireader.GetDateTime(CommonColumns.DueDate),
                            Customer = ireader.GetString(CommonColumns.Vendor),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            InvoiceStatus = ireader.GetString(CommonColumns.Status),
                            InvoiceTitle = ireader.GetString(CommonColumns.InvoiceTitle),
                            PurchaseInvoiceId = ireader.GetInt32(CommonColumns.PurchaseInvoiceId),
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            // CreatedByUserId = ireader.GetInt32(CommonColumns.UserVendorId),
                            StatusId = ireader.GetInt16(CommonColumns.StatusId),
                            UserVendorId = ireader.GetInt32(CommonColumns.UserVendorId),
                            PurchaseOrderId = ireader.GetInt64(CommonColumns.PurchaseOrderId),
                            PurchaseOrderName = ireader.GetString(CommonColumns.PurchaseOrder),
                            DueTermId = ireader.GetInt16(CommonColumns.DueTermId),
                            Term = ireader.GetString(CommonColumns.Term),
                            POInvoicetype = ireader.GetString(CommonColumns.InvocieType)
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

        public static string GetInvoiceTages(int businessId, int userId, long InvoiceId)
        {
            string invoicetags = string.Empty;
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.BigInt);
                    cmd.AddParameters(InvoiceId, CommonConstants.InvoiceId, SqlDbType.BigInt);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoiceTags);
                    while (ireader.Read())
                    {
                        invoicetags += ireader.GetString(CommonColumns.Item) + "/";
                    }
                    invoicetags = invoicetags.Substring(0, invoicetags.LastIndexOf('/') - 1);
                    return invoicetags;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static PurchaseInvoicePayment GetInvoicePaymentInvoiceByInvoiceId(int InvoiceId, int BusinessID, int UserId)
        {
            var payment = new PurchaseInvoicePayment();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(InvoiceId, CommonConstants.PurchaseInvoicePaymentId, SqlDbType.BigInt);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_InvoicePaymentFields);
                    while (ireader.Read())
                    {
                        var po = new PurchaseInvoicePayment 
                        {
                            PurchaseInvoicePaymentId = ireader.GetInt64(CommonColumns.PurchaseInvoicePaymentId),
                            PurchaseInvoiceId = ireader.GetInt64(CommonColumns.PurchaseInvoiceId),
                            InvoiceNumber = ireader.GetString(CommonColumns.InvoiceNumber),
                            ReceivedFor = ireader.GetString(CommonColumns.InvoiceNumber),
                            Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            PaidToTypeId = ireader.GetInt16(CommonColumns.PaidToTypeId),
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            InstrumentNum = ireader.GetString(CommonColumns.InstrumentNum),
                            // CreatedByUserId = ireader.GetInt32(CommonColumns.UserVendorId),
                            PaymentStatusId = ireader.GetInt16(CommonColumns.PaymentStatusId),
                            BankAccountId = ireader.GetInt32(CommonColumns.BankAccountsId),
                            CurrentPaymentStatusId = ireader.GetInt32(CommonColumns.CurrentPaymentStatusId),
                            NextPaymentStatusId = ireader.GetInt32(CommonColumns.NextPaymentStatusId),
                            CurrentPaymentStatus = ireader.GetString(CommonColumns.CurrentPaymentStatus),
                            NextPaymentStatus = ireader.GetString(CommonColumns.NextPaymentStatus),
                            AccountName = ireader.GetString(CommonColumns.BankName)
                        };
                        payment = po;

                    }
                    return payment;
                }
                catch (Exception ex)
                {
                    return null;
                }

                //return payment;

            }
        }


    }
}
