using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StratusAccounting.DAL;
using System.Data;

namespace StratusAccounting.BAL
{
    public class UserValidation
    {
        public LoggedUser IsValidUser(string userName, string password)
        {
            //string encrptPWD = password.GetSHA1();
            LoggedUser logedUser = null;// new LoggedUser() { IsActive = true, UserId = 2, BusinessId = 2, EmailId = "ramana@gmail.com", RoleName = "", RoleId = 1 };
            //LoggedUser logedUser = new LoggedUser() { IsActive = true, UserId = 1, BusinessId = 1, EmailId = "superadmin@gmail.com", RoleName = "Super Admin", RoleId = 2 };
            using (DBSqlCommand cmd = new DBSqlCommand())
            {
                cmd.AddParameters(userName, CommonConstants.UserName, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(password, "@Password", System.Data.SqlDbType.VarChar);
                IDataReader reader = cmd.ExecuteDataReader(SqlProcedures.User_Verification);
                while (reader.Read())
                {
                    logedUser = new LoggedUser();
                    logedUser.BusinessId = reader[CommonColumns.BusinessID] == null ? 0 : Convert.ToInt32(reader[CommonColumns.BusinessID]);
                    logedUser.UserId = reader[CommonColumns.UserId] == null ? 0 : Convert.ToInt32(reader[CommonColumns.UserId]);
                    logedUser.UserName = reader[CommonColumns.UserName].ToString();
                    logedUser.RoleId = reader[CommonColumns.RoleId] == null ? 0 : Convert.ToInt32(reader[CommonColumns.RoleId]);
                    logedUser.RoleName = reader[CommonColumns.RoleName].ToString();
                    logedUser.EmailId = userName;
                    logedUser.IsActive = (bool)reader[CommonColumns.UserIsActive];
                    logedUser.BusinessStatus = reader[CommonColumns.BusinessStatus].ToString();
                    return logedUser;
                }
            }
            return logedUser;// as LoggedUser;
        }

        public void VerifyUser(string userId)
        {
            using (DBSqlCommand cmd = new DBSqlCommand(CommandType.StoredProcedure))
            {
                try
                {
                    cmd.AddParameters(userId, CommonConstants.UserId, SqlDbType.Int);
                    cmd.ExecuteNonQuery(SqlProcedures.BusinessRegistrationVerification);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    public class LoggedUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BusinessId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public string BusinessStatus { get; set; }
    }
}
