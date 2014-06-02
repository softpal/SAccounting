using StratusAccounting.DAL;
using StratusAccounting.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StratusAccounting.BAL
{
    public class BankAccounts
    {
        public static bool SaveBankAccounts(UserBankAccount bankAccounts)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(bankAccounts.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(bankAccounts.BankName, CommonConstants.BankName, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.BankAccountTypeId, CommonConstants.BankAccountTypeId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(bankAccounts.UserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(bankAccounts.UserTypeId, CommonConstants.UserTypeId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(bankAccounts.AccountNum, CommonConstants.AccountNum, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.OpeningBalance, CommonConstants.OpeningBalance, System.Data.SqlDbType.Money);
                    cmd.AddParameters(bankAccounts.SwiftCode, CommonConstants.SwiftCode, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.RountingNum, CommonConstants.RountingNum, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.Phone, CommonConstants.Phone, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.Address, CommonConstants.Address, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.AccountHolder1Name, CommonConstants.AccountHolder1Name, SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.AccountHolder2Name, CommonConstants.AccountHolder2Name, SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.AccountHolder3Name, CommonConstants.AccountHolder3Name, SqlDbType.VarChar);
                    cmd.AddParameters(bankAccounts.Fax, CommonConstants.Fax, System.Data.SqlDbType.VarChar);
                    if (bankAccounts.BankAccountsId > 0)
                    {
                        cmd.AddParameters(bankAccounts.BankAccountsId, CommonConstants.BankAccountsId, System.Data.SqlDbType.BigInt);
                        cmd.ExecuteNonQuery(SqlProcedures.Update_BankAccounts);
                    }
                    else
                    {
                        cmd.ExecuteNonQuery(SqlProcedures.Insert_BankAccounts);
                    }
                }
                catch (Exception ex)
                { }
            }
            return true;
        }

        public static IList<UserBankAccount> GetBankAccount(int businessId, int userId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                IList<UserBankAccount> bankAccounts = new List<UserBankAccount>();
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    IDataReader reader = cmd.ExecuteDataReader(SqlProcedures.GetUserBankAccount);
                    while (reader.Read())
                    {
                        UserBankAccount account = new UserBankAccount();
                        //assing the values to account object
                        account.BankAccountsId = reader.GetInt32(CommonColumns.BankAccountsId);
                        //account.UserId = reader.GetInt32(CommonColumns.UserId);
                        account.BankName = reader.GetString(CommonColumns.BankName);
                        account.BankAccountTypeId = reader.GetInt16(CommonColumns.BankAccountTypeId);
                        //account.UserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                        //account.UserTypeId = reader.GetInt16(CommonColumns.UserTypeId);
                        account.AccountNum = reader.GetString(CommonColumns.AccountNum);
                        account.Mst_BankAccountTypes = new Mst_BankAccountTypes();
                        account.Mst_BankAccountTypes.AccountTypeDesc = reader.GetString(CommonColumns.AccountTypeDesc);
                        account.OpeningBalance = reader.GetFormatDecimal(reader.GetOrdinal(CommonColumns.OpeningBalance));
                        // account.AccountHolderName = reader.GetString(CommonColumns.AccountHolderName);
                        account.AccountHolder1Name = reader.GetString(CommonColumns.AccountHolder1Name);
                        account.AccountHolder2Name = reader.GetString(CommonColumns.AccountHolder2Name);
                        account.AccountHolder3Name = reader.GetString(CommonColumns.AccountHolder3Name);
                        account.SwiftCode = reader.GetString(CommonColumns.SwiftCode);
                        account.RountingNum = reader.GetString(CommonColumns.RountingNum);
                        //account.Phone = reader[CommonColumns.Phone].ToString();
                        account.Phone = reader.GetString(CommonColumns.Phone);
                        // account.Phone2 = reader.GetString(CommonColumns.Phone2);
                        account.Address = reader.GetString(CommonColumns.Address);
                        //account.AddressLine2 = reader.GetString(CommonColumns.AddressLine2);
                        //account.City = reader.GetString(CommonColumns.City);
                        //account.State = reader.GetString(CommonColumns.State);
                        //account.CountriesId = reader.GetInt16(CommonColumns.CountriesId);
                        // account.Zip = reader.GetString(CommonColumns.Zip);
                        account.Fax = reader.GetString(CommonColumns.Fax);
                        //account.Email = reader.GetString(CommonColumns.Email);
                        //account.CreatedDate = Convert.ToDateTime(reader[CommonColumns.CreatedDate]);
                        //account.CreatedByUserId = Convert.ToInt32(reader[CommonColumns.CreatedByUserId]);
                        //account.ModifiedDate = Convert.ToDateTime(reader[CommonColumns.ModifiedDate]);
                        //account.ModifiedByUserId =Convert.ToInt32( reader[CommonColumns.ModifiedByUserId]);
                        //account.IsActive = reader.GetBoolean(CommonColumns.IsActive);

                        bankAccounts.Add(account);
                    }
                    return bankAccounts;
                }
                catch (Exception ex)
                { }
            }
            return null;
        }
        public static IList<Mst_PaymentTypes> GetPaymentTypes()
        {
            List<Mst_PaymentTypes> paymentTypes = new List<Mst_PaymentTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_PaymentTypes);
                    while (ireader.Read())
                    {
                        var paymentType = new Mst_PaymentTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            PaymentTypesId = ireader.GetInt16(CommonColumns.PaymentTypesId),
                            PaymentDesc = ireader.GetString(CommonColumns.PaymentDesc)
                        };

                        paymentTypes.Add(paymentType);

                    }
                    return paymentTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static IList<Mst_BankAccountTypes> GetBankAccountTypes()
        {
            List<Mst_BankAccountTypes> bankAccountTypes = new List<Mst_BankAccountTypes>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BankAccountTypes);
                    while (ireader.Read())
                    {
                        var bankAccountType = new Mst_BankAccountTypes
                        {
                            // BusinessID = ireader.GetInt32("BusinessID"),
                            BankAccountTypeId = ireader.GetInt16(CommonColumns.BankAccountTypeId),
                            AccountTypeDesc = ireader.GetString(CommonColumns.AccountTypeDesc)
                        };

                        bankAccountTypes.Add(bankAccountType);

                    }
                    return bankAccountTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool IsExistedAccountNum(string accountNum)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(accountNum, CommonColumns.AccountNum, SqlDbType.VarChar);

                    cmd.ExecuteNonQuery(SqlProcedures.GetAccountNum_IfExisted);
                    return true;
                }
                catch (Exception)
                { }
            }
            return false;
        }

        public static bool DeleteBankAccount(int businessId, int userId, long bankAccountsId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(bankAccountsId, CommonColumns.BankAccountsId, System.Data.SqlDbType.BigInt);


                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BankAccounts);
                    return true;

                }
                catch (Exception ex)
                { }
            }
            return false;
        }
    }
}
