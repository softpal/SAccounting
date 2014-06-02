using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StratusAccounting.Models;
using StratusAccounting.DAL;
using System.Data;

namespace StratusAccounting.BAL
{
    public static class ChartAccounts
    {
        public static IList<UMst_UserAccounts> GetUserAccounts(int businessId, int? pageNo = null, int? pageSize = null, string sortDirection = null, string sortColumn = null)
        {
            var userAccounts = new List<UMst_UserAccounts>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Accounts);
                    while (ireader.Read())
                    {
                        var userAccount = new UMst_UserAccounts
                        {
                            Sno = ireader.GetInt32(CommonColumns.SNo),
                            AccountName = ireader.GetString(CommonColumns.AccountName),
                            UserAccountsId = ireader.GetInt32(CommonColumns.UserAccountsId),
                            Balance = ireader.GetInt32(CommonColumns.Balance),
                            AccountNum = ireader.GetInt32(CommonColumns.AccountNum),
                            CategoryType = ireader.GetString(CommonColumns.CategoryType)
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

        public static bool InsertAccount(ChartofAccounts_DTO chartofAccountsDto)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(chartofAccountsDto.BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(chartofAccountsDto.UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(chartofAccountsDto.AccountName, CommonConstants.AccountName, SqlDbType.VarChar);
                    cmd.AddParameters(chartofAccountsDto.AccountNo, CommonConstants.AccountNum, SqlDbType.VarChar);
                    cmd.AddParameters(chartofAccountsDto.AccountTypeId, CommonConstants.ParentAccountId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_Accounts);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
