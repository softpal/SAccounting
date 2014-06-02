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
    public class BusinessRegistration
    {
        //nt _userId = 0;
        public bool SaveBusingessRegistration(UsersRegistrations userRegistration, out int userId)
        {

            //using (var dbEntities = new AccountingEntities())
            //{
            //    dbEntities.Business_SuperUser_Registration
            //        (
            //        user_firstname: userRegistration.UsersInfo.UserFirstName,
            //        user_lastname: userRegistration.UsersInfo.UserLastName,
            //        user_email: userRegistration.UsersInfo.Email,
            //        user_pwd: userRegistration.UsersInfo.UserPassword,
            //        businessName: userRegistration.BusinessRegInfo.BusinessName,
            //        businesaddress: userRegistration.BusinessRegInfo.AddressLine1,
            //        cityId: userRegistration.BusinessRegInfo.CityId,
            //        stateId: userRegistration.BusinessRegInfo.StateId,
            //        countryId: userRegistration.BusinessRegInfo.CountryId,
            //        businessPhone: userRegistration.BusinessRegInfo.Phone,
            //        businessFax: userRegistration.BusinessRegInfo.Fax,
            //        licencesRequired: userRegistration.BusinessRegInfo.Licences,
            //        signupPricingId: 1,
            //        signupRenewalId: 1
            //        );
            //}

            using (DBSqlCommand cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(userRegistration.UsersInfo.UserFirstName, CommonConstants.UserFirstName, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.UsersInfo.UserLastName, CommonConstants.UserLastName, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.UsersInfo.Email, "user_email", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.UsersInfo.UserPassword, "user_pwd", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.BusinessName, "businessName", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.AddressLine1, "businesaddress", System.Data.SqlDbType.VarChar);
                    //cmd.AddParameters(userRegistration.BusinessRegInfo.AddressLine1, CommonConstants.AddressLine2, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(1, "cityId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(1, "stateId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.CountryId, "countryId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.Phone, "businessPhone", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.Fax, "businessFax", System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(userRegistration.BusinessRegInfo.Licences, "licencesRequired", System.Data.SqlDbType.Int);
                    cmd.AddParameters(1, "signupPricingId", System.Data.SqlDbType.Int);
                    cmd.AddParameters(1, "signupRenewalId", System.Data.SqlDbType.Int);
                    //cmd.AddParameters(_userId, CommonConstants.UserId, System.Data.SqlDbType.Int, System.Data.ParameterDirection.Output);
                    var _userId = cmd.ExecuteScalar(SqlProcedures.Business_SuperUser_Registration);
                    userId = Convert.ToInt32(_userId);
                    return true;
                }
                catch (Exception ex)
                { }
            }
            userId = 0;
            return false;
        }

        public static bool IsExistedEmial(string email)
        {
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(email, CommonColumns.BusinessID, SqlDbType.VarChar);

                    cmd.ExecuteNonQuery(SqlProcedures.GetEmail_IfExisted);
                    return true;
                }
                catch (Exception)
                { }
            }
            return false;
        }

        public static IList<User> GetRegisteredUsers()
        {
            using (var dbContext = new AccountingEntities())
            { 
                //dbContext.get
            }
            return null;
        }
    }
}
