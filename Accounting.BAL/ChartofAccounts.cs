using System;
using System.Collections.Generic;
using System.Data;
using StratusAccounting.Models;
using StratusAccounting.DAL;

namespace StratusAccounting.BAL
{
    public class ChartofAccounts
    {
        public static bool SaveChartofAccounts(ChartofAccounts_DTO chartofAccountsDto)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(chartofAccountsDto.BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(chartofAccountsDto.UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(chartofAccountsDto.AccountName, CommonConstants.AccountName, SqlDbType.VarChar);
                    cmd.AddParameters(chartofAccountsDto.AccountNo, CommonConstants.AccountNum, SqlDbType.VarChar);
                    cmd.AddParameters(chartofAccountsDto.AccountTypeId, CommonConstants.AccountTypeId, SqlDbType.Int);
                    //cmd.ExecuteNonQuery(SqlProcedures.Insert_ChartofAccounts);
                    cmd.ExecuteNonQuery("[Security].[Insert_ItemsCategory]");
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<ChartofAccounts_DTO> GetChartofAccounts(int businessId, int userId)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var listAccounts = new List<ChartofAccounts_DTO>();
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.Int);
                    IDataReader reader = cmd.ExecuteDataReader(SqlProcedures.Get_ChartofAccounts);
                    while (reader.Read())
                    {
                        var charofAccountsDto = new ChartofAccounts_DTO
                        {
                            AccountName = reader.GetString(CommonColumns.AccountName)
                            ,
                            AccountNo = reader.GetString(CommonColumns.AccountNum)
                            ,
                            AccountTypeId = reader.GetInt32(CommonColumns.BankAccountTypeId)
                            ,
                            Balance = reader.GetInt32(CommonColumns.Balance)
                            ,
                            BusinessId = reader.GetInt32(CommonColumns.BusinessID)
                            ,
                            UserId = reader.GetInt32(CommonColumns.UserId)
                            ,
                            AccountTypeName = reader.GetString(CommonColumns.AccountTypeName)
                        };
                        listAccounts.Add(charofAccountsDto);
                    }
                    return listAccounts;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
