using System;
using System.Collections.Generic;
using System.Linq;
using StratusAccounting.Models;
using StratusAccounting.DAL;
namespace StratusAccounting.BAL
{
    public static class Assets
    {
        public static  IList<Asset> GetAssets(int businessId,int userId,int? pageNo=null,int? pageSize=null,string sortDirection=null,string sortColumn=null)
        {
            List<Asset> assets = new List<Asset>();
            using (DBSqlCommand cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageNo, CommonColumns.PageNo, System.Data.SqlDbType.Int);
                    cmd.AddParameters(pageSize, CommonColumns.PageSize, System.Data.SqlDbType.Int);
                    cmd.AddParameters(sortDirection, CommonColumns.SortDirection, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(sortColumn, CommonColumns.SortColumn, System.Data.SqlDbType.VarChar);
                    

                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Assets);

                    while (ireader.Read())
                    {
                        //System.DateTime CreatedDate = ireader.GetDateTime(CommonColumns.CreatedDate);
                        var asset = new Asset
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                            AssetsId = ireader.GetInt64(CommonColumns.AssetsId),
                            // BusinessID=ireader.GetInt32(CommonColumns.BusinessID),
                            AssetDescription = ireader.GetString(CommonColumns.AssetDescription),
                              AssetDispositionId=ireader.GetInt16(CommonColumns.AssetDispositionId),
                               AssetTypeId=ireader.GetInt16(CommonColumns.AssetTypeId),
                                PurchasedDate=ireader.GetDateTime(CommonColumns.PurchasedDate),
                                 UserVendorId=ireader.GetInt64(CommonColumns.UserVendorId),
                                 InitialValue=ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.InitialValue)),
                                  LocatedAt=ireader.GetString(CommonColumns.LocatedAt),
                                  //PONumber=ireader.GetString(CommonColumns.PONumber),
                                  //DepreciationGLAccountCode=ireader.GetString(CommonColumns.DepreciationGLAccountCode),
                                  GLAccountCode=ireader.GetString(CommonColumns.GLAccountCode),
                                  CreatedDate = ireader.GetDateTime(CommonColumns.Date)
                        };
                        assets.Add(asset);

                    }
                    return assets;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public static bool SaveAssets(Asset asset)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                
                cmd.AddParameters(asset.AssetDescription, CommonConstants.AssetDescription, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(asset.AssetDispositionId, CommonConstants.AssetDispositionId, System.Data.SqlDbType.Int);
                cmd.AddParameters(asset.AssetTypeId, CommonConstants.AssetTypeId, System.Data.SqlDbType.Int);
                cmd.AddParameters(asset.GLAccountCode, CommonConstants.GLAccountCode, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(asset.InitialValue, CommonConstants.InitialValue, System.Data.SqlDbType.Money);
                cmd.AddParameters(asset.CurrentValue, CommonConstants.CurrentValue, System.Data.SqlDbType.Money);
                cmd.AddParameters(asset.LocatedAt, CommonConstants.LocatedAt, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(asset.PurchasedDate, CommonConstants.PurchasedDate, System.Data.SqlDbType.DateTime);
                cmd.AddParameters(asset.DepreciationGLAccountCode, CommonConstants.DepreciationGLAccountCode, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(asset.UserVendorId, CommonConstants.UserVendorId, System.Data.SqlDbType.BigInt);
                cmd.AddParameters(asset.PONumber, CommonConstants.PONumber, System.Data.SqlDbType.VarChar);

                var assets = GetAssets(asset.BusinessID, asset.CreatedByUserId);
                var isExists = assets.Any(a => a.AssetsId == asset.AssetsId);
                if (isExists)
                {
                    cmd.AddParameters(asset.AssetsId, CommonConstants.AssetsId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(asset.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Update_Assets);
                }
                else
                {
                    cmd.AddParameters(asset.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(asset.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.Insert_Assets);
                }

            }
            return true;
           

        }
        public static bool DeleteAsset(int businessId, int userId, long assetsId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(assetsId, CommonColumns.AssetsId, System.Data.SqlDbType.BigInt);
                    cmd.ExecuteNonQuery(SqlProcedures.Delete_Assets);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return false;
        }
        public static IList<Mst_AssetType> GetAssetsTypeList(int BusinessId)
        {
             List<Mst_AssetType> assets = new List<Mst_AssetType>();
             using (DBSqlCommand cmd = new DBSqlCommand())
             {
                 try
                 {
                     cmd.AddParameters(BusinessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                     System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_AssetsTypeList);

                     while (ireader.Read())
                     {
                         var asset = new Mst_AssetType
                         {
                             // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                              AssetTypeId = ireader.GetInt16(CommonColumns.AssetTypeId),
                             AssetName = ireader.GetString(CommonColumns.AssetTypeName),


                         };
                         assets.Add(asset);

                     }
                     return assets;
                 }
                 catch (Exception ex)
                 {
                     return null;
                 }
             }

        }
        public static IList<Mst_AssetDisposition> GetAssetsDispositionList()
        {
            List<Mst_AssetDisposition> assets = new List<Mst_AssetDisposition>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    //cmd.AddParameters(isActive, CommonColumns.IsActive, System.Data.SqlDbType.Bit);

                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_AssetDispositionTypes);//Get_FDispositionList

                    while (ireader.Read())
                    {
                        var asset = new Mst_AssetDisposition
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                            AssetDispositionId = ireader.GetInt16(CommonColumns.AssetDispositionId),
                            AssetDispositionName = ireader.GetString(CommonColumns.AssetDispositionName)
                        };
                        assets.Add(asset);

                    }
                    return assets;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public static IList<UserVendor> GetVendorsList(int businessId)
        {
            List<UserVendor> assets = new List<UserVendor>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);


                    //System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_VendorsList);

                    //while (ireader.Read())
                    //{
                        var asset = new UserVendor
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                            //UserVendorId = ireader.GetInt64(CommonColumns.UserVendorId),
                            //VendorFirstName = ireader.GetString(CommonColumns.VendorFirstName),
                            UserVendorId = 1,
                            VendorFirstName = "Sample Vendor",


                        };
                        assets.Add(asset);

                    //}
                    return assets;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public static IList<BusinessAssetsToEmployee_DTO> GetBusinessAssetsToEmployee(int businessId,long assetsId,int userId)
        {
            List<BusinessAssetsToEmployee_DTO> assets = new List<BusinessAssetsToEmployee_DTO>();
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(assetsId, CommonColumns.AssetsId, System.Data.SqlDbType.BigInt);


                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_AssetsToEmployee);

                    while (ireader.Read())
                    {
                        var asset = new BusinessAssetsToEmployee_DTO
                        {
                            // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                             //UserEmployeeId = ireader.GetInt32(CommonColumns.UserEmployeeId),
                             //EmployeeName = ireader.GetString(CommonColumns.EmployeeName),


                        };
                        assets.Add(asset);

                    }
                    return assets;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool DeleteBusinessAssetsToEmployee(long assetsId,int businessId,int userId,long userEmployeeId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(assetsId, CommonColumns.AssetsId, System.Data.SqlDbType.BigInt);
                    //cmd.AddParameters(userEmployeeId, CommonColumns.UserEmployeeId, System.Data.SqlDbType.BigInt);
                    //cmd.ExecuteNonQuery(SqlProcedures.Delete_AssetsToEmployee);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            return false;
        }

        public static bool DeleteAssetType(int businessId, int userId, int assetTypeId)
        {
            //using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            //{
            //    try
            //    {
                   
            //        cmd.AddParameters(assetTypeId, CommonColumns.AssetTypeId, System.Data.SqlDbType.Int);
                 
            //        cmd.ExecuteNonQuery(SqlProcedures.Delete_AssetType);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            return false;
        }

        public static bool InsertAssetType(int BusinessId, int UserId, string asset)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                cmd.AddParameters(BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(UserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(asset,CommonConstants.AssetName,System.Data.SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinessAssetTypes);
            }
            return true;
        }
    }
}
