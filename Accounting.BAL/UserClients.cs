using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StratusAccounting.DAL;

namespace StratusAccounting.BAL
{
    public class UserClients
    {
        public static IList<BusinessUsersBasicView> GetUserCliets()
        {
            try
            {
                using (var db = new DBSqlCommand())
                {
                    db.AddParameters(1, CommonConstants.UserId, System.Data.SqlDbType.Int);
                    var idataReader = db.ExecuteDataReader("[Security].[Get_SAClients]");
                    var listUsers = new List<BusinessUsersBasicView>();
                    while (idataReader.Read())
                    {
                        var userInfo = new BusinessUsersBasicView
                                 {
                                     SNo = idataReader.GetInt32(CommonColumns.SNo),
                                     BusinessId = idataReader.GetInt32(CommonColumns.BusinessID),
                                     Business = idataReader.GetString("BusinessName"),
                                     RegisterdOn = idataReader.GetDateTime("RegisteredOn"),
                                     ExpiresOn = idataReader.GetDateTime("ExpiresOn"),
                                     AdminEmail = idataReader.GetString("AdminEmail"),
                                     Licenses = idataReader.GetInt32("Licences"),
                                     Status = idataReader.GetString("BusinessStatus"),
                                     BusinessStatusId = idataReader.GetInt32("BusinessStatusId"),
                                     LastLogin = idataReader.GetNullDateTime("LastLogin")
                                 };
                        listUsers.Add(userInfo);
                    }
                    return listUsers;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Exception: exception occured in GetUserClients -" + ex.Message);
            }
        }
    }
}
