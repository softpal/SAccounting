using System.Data;
using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;

namespace StratusAccounting.BAL
{
    public class Transactions
    {
        public static ObservableCollection<Transaction> GetJournals(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null,DateTime? FromDate=null,DateTime? ToDate=null)
        {

            ObservableCollection<Transaction> JournalsList = new ObservableCollection<Transaction>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(FromDate, CommonColumns.FromDate, SqlDbType.DateTime);
                    cmd.AddParameters(ToDate, CommonColumns.ToDate, SqlDbType.DateTime);

                    //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);


                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Journals);

                    while (ireader.Read())
                    {
                        var Journals = new Transaction
                        {
                            SNo = ireader.GetInt32(CommonColumns.SNo),
                            JournalNumber = ireader.GetString(CommonColumns.JournalNumber),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            Account = ireader.GetString(CommonColumns.Account),
                           // Credit = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Credits)),
                            //Debit = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Debits)),
                            Description = ireader.GetString(CommonColumns.Description),

                        };
                        JournalsList.Add(Journals);
                        return JournalsList;

                    }
                    return JournalsList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public static ObservableCollection<Transaction> GetPayments(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null, int? PaymentTypesId=null)
        {

            ObservableCollection<Transaction> PaymentsList = new ObservableCollection<Transaction>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    cmd.AddParameters(PaymentTypesId, CommonColumns.PaymentTypesId, SqlDbType.Int);

                    //cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    //cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);


                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Payments);

                    while (ireader.Read())
                    {
                        if (@PaymentTypesId == 1)
                        {
                            var Payments = new Transaction
                            {
                                SNo = ireader.GetInt32(CommonColumns.SNo),
                                PaymentTypesId = ireader.GetInt32(CommonColumns.PaymentTypesId),
                                VoucherNumber = ireader.GetString(CommonColumns.VoucherNumber),
                                PaymentType = ireader.GetString(CommonColumns.PaymentType),
                                Date = ireader.GetDateTime(CommonColumns.Date),
                                //Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                                StrPaidBy = ireader.GetString(CommonColumns.PaidBy),
                                PaidFor = ireader.GetString(CommonColumns.PaidFor),
                                StrPaidTo = ireader.GetString(CommonColumns.IssuedTo),
                                FromBankAccount = ireader.GetString(CommonColumns.FromBankAccount),
                                InstrumentNum = ireader.GetString(CommonColumns.InstrumentNum),
                            };
                            PaymentsList.Add(Payments);
                        }
                        if (@PaymentTypesId == 2)
                        {
                            var Payments = new Transaction
                            {
                                SNo = ireader.GetInt32(CommonColumns.SNo),
                                PaymentTypesId = ireader.GetInt32(CommonColumns.PaymentTypesId),
                                VoucherNumber = ireader.GetString(CommonColumns.VoucherNumber),
                                PaymentType = ireader.GetString(CommonColumns.PaymentType),
                                Date = ireader.GetDateTime(CommonColumns.Date),
                                //Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                                StrPaidBy = ireader.GetString(CommonColumns.PaidBy),
                                PaidFor = ireader.GetString(CommonColumns.PaidFor),
                                CardTitle = ireader.GetString(CommonColumns.InstrumentNum),
                                StrPaidTo = ireader.GetString(CommonColumns.IssuedTo),
                                FromBankAccount = ireader.GetString(CommonColumns.FromBankAccount),

                            };
                            PaymentsList.Add(Payments);
                        }
                        if (@PaymentTypesId == 3)
                        {
                            var Payments = new Transaction
                            {

                                SNo = ireader.GetInt32(CommonColumns.SNo),
                                PaymentTypesId = ireader.GetInt32(CommonColumns.PaymentTypesId),
                                VoucherNumber = ireader.GetString(CommonColumns.VoucherNumber),
                                PaymentType = ireader.GetString(CommonColumns.PaymentType),
                                Date = ireader.GetDateTime(CommonColumns.Date),
                                //Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                                StrPaidBy = ireader.GetString(CommonColumns.PaidBy),
                                PaidFor = ireader.GetString(CommonColumns.PaidFor),
                                CheckNum = ireader.GetString(CommonColumns.InstrumentNum),
                                StrPaidTo = ireader.GetString(CommonColumns.IssuedTo),
                                FromBankAccount = ireader.GetString(CommonColumns.FromBankAccount),
                                IssuedTo = ireader.GetString(CommonColumns.IssuedTo),
                            };
                            PaymentsList.Add(Payments);

                        }




                       

                    }
                    return PaymentsList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public static ObservableCollection<Transaction> GetRevenues(int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            //var vendors = new List<Add_Vendor>();

            ObservableCollection<Transaction> RevenuesList = new ObservableCollection<Transaction>();
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


                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Revenues);

                    while (ireader.Read())
                    {
                        var Revenues = new Transaction
                        {
                            SNo = ireader.GetInt32(CommonColumns.SNo),
                            VoucherNumber = ireader.GetString(CommonColumns.VoucherNumber),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            //Amount = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Amount)),
                            BankName = ireader.GetString(CommonColumns.BankName),
                            InstrumentNum = ireader.GetString(CommonColumns.InstrumentNum),
                            DepositBy = ireader.GetString(CommonColumns.DepositBy),
                            DepositFor = ireader.GetString(CommonColumns.DepositFor),

                        };
                        RevenuesList.Add(Revenues);
                        return RevenuesList;

                    }
                    return RevenuesList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        public static bool SaveJournals(Transaction Journal)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(Journal.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(Journal.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Journal.JournalNumber, CommonConstants.JournalNumber, SqlDbType.VarChar);
                    cmd.AddParameters(Journal.Date, CommonConstants.Date, SqlDbType.DateTime);
                    cmd.AddParameters(Journal.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.Int);
                    cmd.AddParameters(Journal.Debit, CommonConstants.Debit, SqlDbType.Money);
                    cmd.AddParameters(Journal.Credit, CommonConstants.Credit, SqlDbType.Money);
                    cmd.AddParameters(Journal.Description, CommonConstants.Description, SqlDbType.VarChar);

                    cmd.ExecuteScalar(SqlProcedures.Insert_Journals);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static bool SaveRevenues(Transaction Revenues)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(Revenues.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(Revenues.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Revenues.JournalNumber, CommonConstants.JournalNumber, SqlDbType.VarChar);
                    cmd.AddParameters(Revenues.Date, CommonConstants.Date, SqlDbType.DateTime);
                    cmd.AddParameters(Revenues.Amount, CommonConstants.Amount, SqlDbType.Money);
                    cmd.AddParameters(Revenues.BankAccountsId, CommonConstants.BankAccountsId, SqlDbType.BigInt);
                    cmd.AddParameters(Revenues.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
                    cmd.AddParameters(Revenues.DepositById, CommonConstants.DepositById, SqlDbType.BigInt);
                    cmd.AddParameters(Revenues.ItemId, CommonConstants.ItemId, SqlDbType.BigInt);
                    cmd.AddParameters(Revenues.DepositByTypeId, CommonConstants.DepositByTypeId, SqlDbType.BigInt);

                    cmd.ExecuteScalar(SqlProcedures.Insert_Revenues);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static bool SavePayment(Transaction Payment)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(Payment.BusinessID, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(Payment.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(Payment.PaymentTypesId, CommonConstants.PaymentTypesId, SqlDbType.Int);
                    cmd.AddParameters(Payment.UserAccountsId, CommonConstants.UserAccountsId, SqlDbType.Int);
                    cmd.AddParameters(Payment.VoucherNumber, CommonConstants.VoucherNumber, SqlDbType.VarChar);
                    cmd.AddParameters(Payment.Date, CommonConstants.Date, SqlDbType.DateTime);
                    cmd.AddParameters(Payment.Amount, CommonConstants.Amount, SqlDbType.Money);
                    cmd.AddParameters(Payment.PaidTo, CommonConstants.PaidTo, SqlDbType.BigInt);
                    cmd.AddParameters(Payment.PayeeType, CommonConstants.PayeeType, SqlDbType.SmallInt);
                    cmd.AddParameters(Payment.PaidByEmployeeId, CommonConstants.PaidByEmployeeId, SqlDbType.BigInt);
                    cmd.AddParameters(Payment.CardTitle, CommonConstants.CardTitle, SqlDbType.VarChar);
                    cmd.AddParameters(Payment.InstrumentNum, CommonConstants.InstrumentNum, SqlDbType.VarChar);
                    cmd.AddParameters(Payment.BankAccountsId, CommonConstants.BankAccountsId, SqlDbType.BigInt);

                    cmd.ExecuteScalar(SqlProcedures.Insert_Payment);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return true;
        }
    }
}
