using System;
using System.Collections.Generic;
using System.Linq;
using StratusAccounting.DAL;
using StratusAccounting.Models;

namespace StratusAccounting.BAL
{
    public class CountryStateCity
    {
        public static List<CountryStateCityDto> GetCountryStateCity(string searchKey, string column, int? itemId)
        {
            //using (var db = new AccountingEntities())
            //{
            //    List<CountryStateCityDto> listItem = db.Get_MasterAddress(0).Select(
            //        item => new CountryStateCityDto
            //        {
            //            CountryName = item.Country,
            //            CountryId = item.CountryId
            //        }
            //        ).Where(item => item.CountryName.StartsWith(searchKey)).ToList();
            //    return listItem;
            //}


            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_MasterAddress);
                    var listItems = new List<CountryStateCityDto>();
                    while (ireader.Read())
                    {
                        var item = new CountryStateCityDto
                        {
                            CountryId = ireader.GetInt16("CountryId"),
                            CountryName = ireader.GetString(CommonColumns.Country),
                            CityId = ireader.GetInt16(CommonColumns.CityId),
                            CityName = ireader.GetString(CommonColumns.City),
                            StateId = ireader.GetInt16(CommonColumns.StateId),
                            StateName = ireader.GetString(CommonColumns.State)
                        };
                        listItems.Add(item);
                    }
                    switch (column)
                    {
                        case "country":
                            return listItems.Where(item => item.CountryName.ToLower().StartsWith(searchKey)).ToList(); 
                        case "state":
                            return listItems.Where(item => item.StateName.ToLower().StartsWith(searchKey) && item.CountryId.Equals(itemId)).ToList(); 
                        case "city":
                            return listItems.Where(item => item.CityName.ToLower().StartsWith(searchKey) && item.StateId.Equals(itemId)).ToList(); 
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
