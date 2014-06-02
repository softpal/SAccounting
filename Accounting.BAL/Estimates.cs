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
    public class Estimates
    {
        public static bool SaveEstimate(EstimatesDetail estimate)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(estimate.EstimatesId, CommonConstants.EstimatesId, SqlDbType.BigInt);
                cmd.AddParameters(estimate.ItemId, CommonConstants.ItemId, SqlDbType.BigInt);
                cmd.AddParameters(estimate.ItemTypeId, CommonConstants.ItemTypeId, SqlDbType.SmallInt);
                cmd.AddParameters(estimate.EstimatesDetailsId, CommonConstants.EstimatesDetailsId, SqlDbType.BigInt);
                cmd.AddParameters(estimate.Description, CommonConstants.Description, SqlDbType.VarChar);
                cmd.AddParameters(estimate.Amount, CommonConstants.Amount, SqlDbType.Decimal);
                cmd.AddParameters(estimate.Quantity, CommonConstants.Quantity, SqlDbType.Int);
                cmd.AddParameters(estimate.Rate, CommonConstants.Rate, SqlDbType.Decimal);
                cmd.AddParameters(estimate.ServiceDate, CommonConstants.ServiceDate, SqlDbType.DateTime);
                cmd.AddParameters(estimate.CreatedByUserId, CommonConstants.CreatedByUserId, SqlDbType.VarChar);
                cmd.AddParameters(estimate.CreatedDate, CommonConstants.CreatedDate, SqlDbType.DateTime);
                cmd.AddParameters(estimate.ModifiedDate, CommonConstants.ModifiedDate, SqlDbType.DateTime);
                cmd.AddParameters(estimate.ModifiedByUserId, CommonConstants.ModifiedByUserId, SqlDbType.Int);
                cmd.AddParameters(estimate.IsActive, CommonConstants.IsActive, SqlDbType.Bit);

                if (estimate.EstimatesId > 0)
                {
                    cmd.AddParameters(estimate.EstimatesId, CommonConstants.EstimatesId, SqlDbType.Int);
                    cmd.AddParameters(estimate.ModifiedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Update_TransactionEstimate);
                }
                else
                {
                    cmd.AddParameters(estimate.CreatedByUserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_TransactionEstimate);
                }
            }
            return true;
        }

        public static IList<Estimate> GetEstimates(int businessId, int userId)
        {
            var estimates = new List<Estimate>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, SqlDbType.Int);

                    IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_TransactionEstimates);

                    while (ireader.Read())
                    {
                        var estimate = new Estimate
                        {
                            EstimatesId = ireader.GetInt32(CommonColumns.EstimatesId),
                            BusinessID = ireader.GetInt32(CommonColumns.BusinessID),
                            UserCustomerId = ireader.GetInt64(CommonConstants.UserCustomerId),
                            EstimateDate = ireader.GetDateTime(CommonColumns.EstimateDate),
                            ExpiryDate = ireader.GetDateTime(CommonColumns.ExpiryDate),
                            CustomField1 = ireader.GetString(CommonColumns.CustomField1),
                            CustomField2 = ireader.GetString(CommonColumns.CustomField2),
                            CustomField3 = ireader.GetString(CommonColumns.CustomField3),
                            Amount = Convert.ToDecimal(ireader.GetString(CommonColumns.Amount)),
                            CreatedByUserId = ireader.GetInt32(CommonColumns.CreatedByUserId),
                            ModifiedByUserId = ireader.GetInt32(CommonColumns.ModifiedByUserId),
                            CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate),
                            ModifiedDate = ireader.GetDateTime(CommonColumns.ModifiedDate),
                            IsActive = ireader.GetBoolean(CommonColumns.IsActive)
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


        public static bool SaveNewEstimate(int BusinessId, int UserId,int estimateNum, string custName, DateTime expiryDate, string[][] estimDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                cmd.AddParameters(estimateNum, CommonConstants.EstimateNum, SqlDbType.BigInt);
                //cmd.AddParameters(custName, CommonConstants.CustomerFirstName, SqlDbType.VarChar);
                cmd.AddParameters(1, CommonConstants.CustomerId, SqlDbType.BigInt);//need to ask customer id
                cmd.AddParameters(expiryDate, CommonConstants.ExpiryDate, SqlDbType.DateTime);
                cmd.AddParameters(0, CommonConstants.Amount, SqlDbType.Money);//need to ask amount
                cmd.AddParameters(1, CommonConstants.StatusId, SqlDbType.SmallInt);//need to ask status

                long EstimateId = Convert.ToInt64(cmd.ExecuteScalar(SqlProcedures.Insert_TransactionEstimate));

                foreach (string[] detail in estimDetails)
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, SqlDbType.Int);
                    cmd.AddParameters(UserId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.AddParameters(EstimateId, CommonConstants.EstimatesId, SqlDbType.BigInt);
                    cmd.AddParameters(Convert.ToInt64(detail[0]), CommonConstants.ItemId, SqlDbType.BigInt);                   
                    cmd.AddParameters(detail[1], CommonConstants.Description, SqlDbType.VarChar);
                    cmd.AddParameters(detail[2], CommonConstants.Quantity, SqlDbType.Int);
                    cmd.AddParameters(detail[3], CommonConstants.Amount, SqlDbType.Money);

                    cmd.ExecuteNonQuery(SqlProcedures.Insert_TransactionEstimateDetail);
                }
            }
            return true;
        }
    }
}
