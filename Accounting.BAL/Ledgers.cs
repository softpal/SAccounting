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
    public class Ledgers
    {
        public static IList<UMst_UserAccounts> GetLedgerAccounts(int businessId, int userId)
        {
            List<UMst_UserAccounts> userAccounts = new List<UMst_UserAccounts>();
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader("[Transaction].[Get_LedgerAccounts]");
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

        public static IList<Ledger_DTO> GetLedgers(int businessId, int userId, LedgerTypes ledgerType, DateTime? fromDate, DateTime? toDate, int? accountId, int? pageNo, int? pageSize, string sortDirection, string sortColumn)
        {
            List<Ledger_DTO> ledgers = new List<Ledger_DTO>();
            System.Data.IDataReader ireader = null;
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
                    cmd.AddParameters(fromDate, "@FromDate", System.Data.SqlDbType.DateTime);
                    cmd.AddParameters(toDate, "@ToDate", System.Data.SqlDbType.DateTime);
                    cmd.AddParameters(accountId, "@AccountsId", System.Data.SqlDbType.Int);
                    //cmd.AddParameters(ledgerType.ToString(), "@LedgerType", System.Data.SqlDbType.VarChar);
                    switch (ledgerType)
                    {
                        case LedgerTypes.Ledger:
                            ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Ledgers_Rpt);
                            break;
                        case LedgerTypes.DayWise:
                            ireader = cmd.ExecuteDataReader(SqlProcedures.Get_DayWiseLedgers_Rpt);
                            break;
                        case LedgerTypes.MonthWise:
                            ireader = cmd.ExecuteDataReader(SqlProcedures.Get_MonthWiseLedgers_Rpt);
                            break;
                        case LedgerTypes.ScheduleWise:
                            ireader = cmd.ExecuteDataReader(SqlProcedures.Get_ScheduleWiseLedgers_Rpt);
                            break;
                    }
                    //System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Ledgers_Rpt);
                    while (ireader.Read())
                    {
                        var item = new Ledger_DTO
                        {
                            Sno = ireader.GetInt32(CommonColumns.SNo),
                            Description = ireader.GetString(CommonColumns.Description),
                            Credit = ireader.GetInt32(CommonColumns.Credit),// ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Credit)),
                            Debit = ireader.GetInt32(CommonColumns.Debit),// ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Debit)),
                            Balance = ireader.GetInt32(CommonColumns.Balance),//.GetNullableDecimal(ireader.GetOrdinal(CommonColumns.Balance)),
                            Date = ireader.GetDateTime(CommonColumns.Date),
                            IsSummery = Convert.ToBoolean(ireader["IsSummary"]),
                            Account = ireader["Account"].ToString()
                        };
                        ledgers.Add(item);

                    }

                    return ledgers;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //public static IList<Ledger_DTO> SearchLedgers(Ledger_DTO ledger, string? cretiria, int businessId, int userId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        //{
        //    List<Ledger_DTO> ledgers = new List<Ledger_DTO>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
        //            cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.GetLedger);

        //            if (cretiria.Value != null)
        //            {
        //                BAL.Ledgers.GetAllSearchLedgers(cretiria.Value, businessId, userId);
        //            }
        //            return ledgers;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public static IList<Ledger_DTO> GetAllSearchLedgers(int businessId, int userId, string? cretiria)
        //{
        //    List<Ledger_DTO> ledgers = new List<Ledger_DTO>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_SearchLedgers);

        //            if (cretiria.Value == Convert.ToString(PresentCretiria.Today))
        //            {
        //            }
        //            else if (cretiria.Value == Convert.ToString(PresentCretiria.WeekWise))
        //            {
        //            }
        //            else if (cretiria.Value == Convert.ToString(PresentCretiria.MonthWise))
        //            {
        //            }
        //            else if (cretiria.Value == Convert.ToString(PresentCretiria.Quarter))
        //            {
        //            }
        //            else if (cretiria.Value == Convert.ToString(PresentCretiria.YearWise))
        //            {
        //            }
        //            else
        //            {
        //            }

        //            while (ireader.Read())
        //            {
        //                var item = new Ledger_DTO
        //                {
        //                    //LedgerId = ireader.GetInt32(CommonColumns.LedgerId),
        //                    Description = ireader.GetString(CommonColumns.Description),
        //                    //Credit = ireader.GetDecimal(CommonColumns.Credit),
        //                    //Debit = ireader.GetDecimal(CommonColumns.Debit),
        //                    //Balance = ireader.GetDecimal(CommonColumns.Balance),
        //                    //Balance = ireader.GetDecimal(CommonColumns.Balance),
        //                    CreationDate = ireader.GetDateTime(CommonColumns.CreationDate)

        //                };
        //                ledgers.Add(item);

        //            }

        //            return ledgers;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public static IList<Ledger_DTO> GetTrailBalance(int businessId, int userId, DateTime? fromDate, DateTime? toDate, int? pageNo, int? pageSize, string sortColumn, string sortOrder)
        {
            List<Ledger_DTO> ledgers = new List<Ledger_DTO>();
            //
            using (DBSqlCommand cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    cmd.AddParameters(sortOrder, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(fromDate, "@FromDate", System.Data.SqlDbType.DateTime);
                    cmd.AddParameters(toDate, "@ToDate", System.Data.SqlDbType.DateTime);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_TrailBalance_Rpt);//[Rpt_TrialBalance]
                    while (ireader.Read())
                    {
                        var item = new Ledger_DTO
                        {
                            Sno = ireader.GetInt32(CommonColumns.SNo),
                            Description = ireader.GetString(CommonColumns.Account),
                            Credit = ireader.GetInt32(CommonColumns.Credit),// ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Credit)),
                            Debit = ireader.GetInt32(CommonColumns.Debit),// ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.Debit)),
                        };
                        ledgers.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Exception: Exception occured in GetTrailbalance, exception message -" + ex.Message);
                }
                return ledgers;
            }
        }
    }
}

enum LedgerEnums
{
    None,
    NameOfAccounts = 14,

};

enum PresentCretiria
{
    Today,
    DayWise,
    WeekWise,
    MonthWise,
    Quarter,
    YearWise

};