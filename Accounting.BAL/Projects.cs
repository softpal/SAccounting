using StratusAccounting.Models;
using StratusAccounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    public class Projects
    {
        //public static bool SaveProjects(UMst_Projects projects)
        //{
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        cmd.AddParameters(projects.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
        //        cmd.AddParameters(projects.ProjectsTitle, CommonConstants.ProjectsTitle, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(projects.ProjectsUniqueId, CommonConstants.ProjectsUniqueId, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(projects.ProjectsDesc, CommonConstants.ProjectsDesc, System.Data.SqlDbType.VarChar);
        //        cmd.AddParameters(projects.CostPrice, CommonConstants.CostPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(projects.DiscountPrice, CommonConstants.DiscountPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(projects.PreferredPrice, CommonConstants.PreferredPrice, System.Data.SqlDbType.Money);
        //        cmd.AddParameters(projects.UserAccountsId, CommonConstants.UserAccountsId, System.Data.SqlDbType.Int);
        //        cmd.AddParameters(projects.Taxable, CommonConstants.Taxable, System.Data.SqlDbType.Bit);
              
                
        //        if (projects.ProjectsId > 0)
        //        {
        //            cmd.AddParameters(projects.ProjectsId, CommonConstants.ProjectsId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(projects.ModifiedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessProjects);
        //    }
        //        else
        //        {
        //            cmd.AddParameters(projects.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
        //            cmd.ExecuteNonQuery(SqlProcedures.Insert_BusinessProjects);
        //        }
               
        //    }
        //    return true;
        //}

        //public static IList<UMst_Projects> GetProjects(int businessId, int userId)
        //{
        //    List<UMst_Projects> projects = new List<UMst_Projects>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessProjects);

        //            while (ireader.Read())
        //            {
        //                var project = new UMst_Projects
        //                {
        //                    // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
        //                    ProjectsId = ireader.GetInt64(CommonColumns.ProjectsId),
        //                    ProjectsTitle = ireader.GetString(CommonColumns.ProjectsTitle),
        //                    ProjectsDesc = ireader.GetString(CommonColumns.ProjectsDesc),
        //                    ProjectsUniqueId = ireader.GetString(CommonColumns.ProjectsUniqueId),
        //                    CostPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.CostPrice)),
        //                    DiscountPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.DiscountPrice)),
        //                    PreferredPrice = ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.PreferredPrice)),
        //                    AccountName = ireader.GetString(CommonColumns.AccountName),
        //                    UserAccountsId = ireader.GetInt64(CommonColumns.UserAccountsId),
        //                    Taxable = ireader.GetBoolean(CommonColumns.Taxable)
        //                    //CreatedByUserId = ireader.GetInt32("CreatedByUserId"),
        //                    //ModifiedByUserId = ireader.GetInt32("ModifiedByUserId"),
        //                    //CreatedDate = ireader.GetDateTime("CreatedDate"),
        //                    //ModifiedDate = ireader.GetDateTime("ModifiedDate"),
        //                    //IsActive = ireader.GetBoolean("IsActive")

        //                };
        //                projects.Add(project);

        //            }
        //            return projects;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        //}

        public static bool DeleteProject(int businessId, int userId, long projectId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(userId, CommonColumns.UserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(projectId, CommonColumns.ProjectsId, System.Data.SqlDbType.BigInt);


                    cmd.ExecuteNonQuery(SqlProcedures.Delete_BusinessProjects);
                    return true;

                }
                catch (Exception ex)
                { }
            }
            return false;
        }

        //public static IList<UMst_Projects> SearchProjects(int BusinessId, int UserId, string keyword)
        //{
        //    List<UMst_Projects> projects = new List<UMst_Projects>();
        //    using (DBSqlCommand cmd = new DBSqlCommand(System.Data.CommandType.StoredProcedure))
        //    {
        //        try
        //        {
        //            cmd.AddParameters(BusinessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(UserId, CommonColumns.UserId, System.Data.SqlDbType.Int);
        //            cmd.AddParameters(keyword, "@keyword", System.Data.SqlDbType.VarChar);

        //            System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_SearchBusinessProjects);

        //            while (ireader.Read())
        //            {
        //                var project = new UMst_Projects
        //                {
        //                    // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
        //                    ProjectsId = ireader.GetInt64(CommonColumns.ProjectsId),
        //                    ProjectsTitle = ireader.GetString(CommonColumns.ProjectsTitle)
        //                };
        //                projects.Add(project);

        //            }
        //            return projects;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
