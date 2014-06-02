using System;
using System.Collections.Generic;
using StratusAccounting.DAL;
using StratusAccounting.Models;

namespace StratusAccounting.BAL
{
  public  class RegisterAppParameters
    {
      public static IList<ApplicationParameters> GetApplicationParameters()
      {
          var applicationParameters = new List<ApplicationParameters>();
          using (var cmd = new DBSqlCommand())
          {
              try
              {
                  var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_ApplicationParameters);
                  while (ireader.Read())
                  {
                      var applicationParameter = new ApplicationParameters
                      {
                          ParamName = ireader.GetString(CommonColumns.ParamName),
                          ParamValue = ireader.GetString(CommonColumns.ParamValue)
                      };

                      applicationParameters.Add(applicationParameter);

                  }
                  return applicationParameters;
              }
              catch (Exception ex)
              {
                  return null;
              }
          }
      }
    }
}
