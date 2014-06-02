using System;
using System.Collections.Generic;
using StratusAccounting.Models;
using StratusAccounting.DAL;
namespace StratusAccounting.BAL
{
    //public class Enums
    //{
    //     Enum Preference
    //    {
    //        DateFormat,
    //        NumberFormat,
    //        FiscalYearFormat,
    //        PrimaryCurrencyFormat

    //    }
    //}
    public static class Preferences
    {
        public static List<UMst_BusinessPreferences> GetBusinessPreferences(int businessId)
        {
            var preferences = new List<UMst_BusinessPreferences>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UMst_BusinessPreferences);

                    while (ireader.Read())
                    {
                        var obj = new Mst_PreferenceValues
                        {
                            PreferenceValue = ireader.GetString(CommonColumns.PreferenceValue)
                        };
                        var preference = new UMst_BusinessPreferences
                        {
                            BusinessID = ireader.GetInt16(CommonColumns.BusinessID),
                            PreferenceFieldsId = ireader.GetInt32(CommonColumns.PreferenceFieldsId),
                            PreferenceValuesId = ireader.GetInt16(CommonColumns.PreferenceValuesId),
                            Mst_PreferenceValues = obj
                        };
                        preferences.Add(preference);
                    }
                    return preferences;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Models.BusinessRegistration> GetBusinessRegistration(int businessId)
        {
            var registrations = new List<Models.BusinessRegistration>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_BusinessDetails);
                    while (ireader.Read())
                    {
                        var registration = new Models.BusinessRegistration
                        {
                            BusinessID = ireader.GetInt16(CommonColumns.BusinessID),
                            BusinessName = ireader.GetString(CommonColumns.CompanyName),
                            BusinessTypeId = ireader.GetInt16(CommonColumns.BusinessTypeId),
                            Email = ireader.GetString(CommonColumns.Email),
                            Phone = ireader.GetString(CommonColumns.BusinessPhone),
                            AddressLine1 = ireader.GetString(CommonColumns.AddressLine1),
                            Zip = ireader.GetString(CommonColumns.Zip),
                            CountryId = ireader.GetInt16(CommonColumns.CountryId),
                            DUNS_ = ireader.GetString(CommonColumns.DUNS_),
                            DBA = ireader.GetString(CommonColumns.DBA),
                            Licences = ireader.GetInt32(CommonColumns.Licences)
                        };
                        registrations.Add(registration);
                    }
                    return registrations;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<UMst_UserCustomizeFields> GetCustomizeFields(int businessId)
        {
            var customizeFields = new List<UMst_UserCustomizeFields>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UMst_UserCustomizeFields);
                    while (ireader.Read())
                    {
                        var field = new UMst_UserCustomizeFields
                        {
                            BusinessID = ireader.GetInt16(CommonColumns.BusinessID),
                            CustomersRenameId = ireader.GetInt16(CommonColumns.CustomersRenameId),
                            InvoiceTerms = ireader.GetString(CommonColumns.InvoiceTerms),
                            POTerms = ireader.GetString(CommonColumns.POTerms),
                        };
                        customizeFields.Add(field);
                    }
                    return customizeFields;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<UMst_UserCustomFields> GetCustomFields(int businessId)
        {
            var customFields = new List<UMst_UserCustomFields>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UMst_UserCustomFields);
                    while (ireader.Read())
                    {
                        var field = new UMst_UserCustomFields
                        {
                            BusinessID = ireader.GetInt16(CommonColumns.BusinessID),
                            CustomField1Label = ireader.GetString(CommonColumns.CustomField1Label),
                            CustomField2Label = ireader.GetString(CommonColumns.CustomField2Label),
                            CustomField3Label = ireader.GetString(CommonColumns.CustomField3Label),
                        };
                        customFields.Add(field);
                    }
                    return customFields;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<UMst_TaxDetails> GetTaxDetails(int businessId)
        {
            var taxDetails = new List<UMst_TaxDetails>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(businessId, CommonColumns.BusinessID, System.Data.SqlDbType.Int);
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_UMst_TaxDetails);
                    while (ireader.Read())
                    {
                        var tax = new UMst_TaxDetails
                        {
                             // BusinessID = ireader.GetInt32(CommonColumns.BusinessID"),
                             BusinessID = businessId,
                             TaxNumber = ireader[CommonColumns.TaxNumber] != System.DBNull.Value?ireader.GetString(CommonColumns.TaxNumber):string.Empty,
                            TaxTypesId = ireader.GetInt16(CommonColumns.TaxTypesId),
                             TaxValue = ireader[CommonColumns.TaxValue] != System.DBNull.Value ? ireader.GetDecimal(ireader.GetOrdinal(CommonColumns.TaxValue)) : 0,
                             TaxName = ireader.GetString(CommonColumns.TaxName)

                        };
                        taxDetails.Add(tax);
                    }
                    return taxDetails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_PreferenceValues> GetPreferenceValues()
        {
            var prefValues = new List<Mst_PreferenceValues>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {

                    System.Data.IDataReader ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Mst_PreferenceValues);

                    while (ireader.Read())
                    {
                        var pref = new Mst_PreferenceValues
                        {
                            PreferenceFieldsId = ireader.GetInt16(CommonColumns.PreferenceFieldsId),
                            PreferenceValue = ireader.GetString(CommonColumns.PreferenceValue),
                            PreferenceValuesId = ireader.GetInt16(CommonColumns.PreferenceValuesId)
                        };
                        prefValues.Add(pref);

                    }
                    return prefValues;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_BusinessType> GetBusinessTypes()
        {
            var busTypes = new List<Mst_BusinessType>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Mst_BusinessType);
                    while (ireader.Read())
                    {
                        var bus = new Mst_BusinessType
                        {
                            BusinessTypeId = ireader.GetInt16(CommonColumns.BusinessTypeId),
                            BusinessType = ireader.GetString(CommonColumns.BusinessType)
                        };
                        busTypes.Add(bus);

                    }
                    return busTypes;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_Countries> GetCountries()
        {
            var countries = new List<Mst_Countries>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Mst_Countries);
                    while (ireader.Read())
                    {
                        var country = new Mst_Countries
                        {
                            CountryId = ireader.GetInt16(CommonColumns.CountryId),
                            Country = ireader.GetString(CommonColumns.Country)
                        };
                        countries.Add(country);
                    }
                    return countries;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_GreetingsList> GetGreetingsList()
        {
            var greetings = new List<Mst_GreetingsList>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Mst_GreetingsList);
                    while (ireader.Read())
                    {
                        var greeting = new Mst_GreetingsList
                        {
                            GreetingName = ireader.GetString(CommonColumns.GreetingName),
                            GreetingsListId = ireader.GetInt16(CommonColumns.GreetingsListId)
                        };
                        greetings.Add(greeting);

                    }
                    return greetings;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Mst_CustomersRename> GetCustomersRename()
        {
            var renames = new List<Mst_CustomersRename>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_Mst_CustomersRename);
                    while (ireader.Read())
                    {
                        var rename = new Mst_CustomersRename
                        {
                            CustomersRename = ireader.GetString(CommonColumns.CustomersRename),
                            CustomersRenameId = ireader.GetInt16(CommonColumns.CustomersRenameId)
                        };
                        renames.Add(rename);
                    }
                    return renames;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool SavePreferences(BusinessPreferences_DTO preferences)
        {
            UpdateBusinessRegistration(preferences.BusiReg);
            UpdateBusinessPreferences(preferences.DateFormatPreference, 1);
            UpdateBusinessPreferences(preferences.NumberFormatPreference, 2);
            UpdateBusinessPreferences(preferences.FiscalYearFormat, 3);
            UpdateBusinessPreferences(preferences.PrimaryCurrency, 4);
            foreach (var taxDetails in preferences.TaxDetails)
            {
                UpdateTaxDetails(taxDetails);
            }
            UpdateUserCustomizeFields(preferences.CustomFields);
            return true;
        }

        public static bool SaveBusinessInformation(Models.BusinessRegistration objRegistration)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(objRegistration.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(objRegistration.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objRegistration.BusinessName, CommonConstants.BusinessName, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.BusinessTypeId, CommonConstants.BusinessTypeId, System.Data.SqlDbType.TinyInt);
                cmd.AddParameters(objRegistration.Email, CommonConstants.Email, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.Phone, CommonConstants.Phone, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.Licences, CommonConstants.Licences, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.CityId, CommonConstants.CityID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objRegistration.StateId, CommonConstants.StateID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objRegistration.AddressLine1, CommonConstants.AddressLine1, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.CountryId, CommonConstants.CountryId, System.Data.SqlDbType.TinyInt);
                cmd.AddParameters(objRegistration.Zip, CommonConstants.Zip, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.DUNS_, CommonConstants.DUNS_, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objRegistration.DBA, CommonConstants.DBA, System.Data.SqlDbType.VarChar);
                //
                cmd.ExecuteNonQuery(SqlProcedures.Update_Business_Details);
            }
            return true;
        }

        public static bool SaveGreeting(UMst_UserGreetingEmails objEmail)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(objEmail.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(objEmail.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objEmail.Subject, CommonConstants.GreetingName, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objEmail.GreetingsListId, CommonConstants.GreetingsListId, System.Data.SqlDbType.TinyInt);
                cmd.AddParameters(objEmail.Message, CommonConstants.Message, System.Data.SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Insert_Update_GreetingEmails);
            }
            return true;
        }

        public static bool SaveCustomize(UMst_UserCustomizeFields objCustomize, UMst_UserCustomFields objCustom)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(objCustomize.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(objCustomize.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objCustomize.CustomersRenameId, CommonConstants.CustomersRenameId, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objCustomize.InvoiceTerms, CommonConstants.InvoiceTerms, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objCustomize.POTerms, CommonConstants.POTerms, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objCustomize.Logo, CommonConstants.Logo, System.Data.SqlDbType.VarBinary);
                cmd.AddParameters(objCustom.CustomField1Label, CommonConstants.CustomField1Label, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objCustom.CustomField2Label, CommonConstants.CustomField2Label, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objCustom.CustomField3Label, CommonConstants.CustomField3Label, System.Data.SqlDbType.VarChar);

                cmd.ExecuteNonQuery(SqlProcedures.Insert_Update_CustomizeFields);
            }
            return true;
        }

        public static bool SavePreferenceInformation(BusinessPreferences_DTO objbusinessPrefDto)
        {

            if (objbusinessPrefDto.DateFormatPreference != null)
            {
                UpdatePreferenceInformation(objbusinessPrefDto.DateFormatPreference);

            }
            if (objbusinessPrefDto.NumberFormatPreference != null)
            {
                UpdatePreferenceInformation(objbusinessPrefDto.NumberFormatPreference);
            }
            if (objbusinessPrefDto.FiscalYearFormat != null)
            {
                UpdatePreferenceInformation(objbusinessPrefDto.FiscalYearFormat);
            }
            if (objbusinessPrefDto.PrimaryCurrency != null)
            {
                UpdatePreferenceInformation(objbusinessPrefDto.PrimaryCurrency);
            }
            return true;
        }

        public static bool SaveTaxInformation(UMst_TaxDetails objTaxDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(objTaxDetails.CreatedByUserId, CommonColumns .UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(objTaxDetails.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(objTaxDetails.TaxNumber, CommonConstants.TaxNumber, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(objTaxDetails.TaxValue, CommonConstants.TaxValue, System.Data.SqlDbType.Decimal);
                cmd.AddParameters(objTaxDetails.TaxTypesId, CommonConstants.TaxTypesId, System.Data.SqlDbType.SmallInt);
                cmd.ExecuteNonQuery(SqlProcedures.Insert_Update_BusinessTaxDetails);
            }
            return true;
        }

        public static bool UpdatePreferenceInformation(UMst_BusinessPreferences preference)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(preference.CreatedByUserId, CommonConstants.UserId, System.Data.SqlDbType.Int);
                cmd.AddParameters(preference.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(preference.PreferenceFieldsId, CommonConstants.PreferenceFieldsId, System.Data.SqlDbType.SmallInt);
                cmd.AddParameters(preference.PreferenceValuesId, CommonConstants.PreferenceValuesId, System.Data.SqlDbType.SmallInt);
                cmd.ExecuteNonQuery(SqlProcedures.Insert_Update_BusinessPreferences);
            }
            return true;
        }

        public static bool UpdateBusinessRegistration(Models.BusinessRegistration registration)
        {
            try
            {
                using (var cmd = new DBSqlCommand())
                {
                    cmd.AddParameters(registration.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                    cmd.AddParameters(registration.AddressLine1, CommonConstants.AddressLine1, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.CityId, CommonConstants.City, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.Zip, CommonConstants.Zip, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.CountryId, CommonConstants.CountriesId, System.Data.SqlDbType.TinyInt);
                    cmd.AddParameters(registration.Phone, CommonConstants.Phone, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.Email, CommonConstants.Email, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.DUNS_, CommonConstants.DUNS_, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.DBA, CommonConstants.DBA, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.BusinessTypeId, CommonConstants.BusinessTypeId, System.Data.SqlDbType.TinyInt);
                    cmd.AddParameters(registration.BusinessName, CommonConstants.BusinessName, System.Data.SqlDbType.VarChar);
                    cmd.AddParameters(registration.ModifiedByUserId, CommonConstants.ModifiedByUserId, System.Data.SqlDbType.Int);
                    cmd.AddParameters(registration.ModifiedDate, CommonConstants.ModifiedDate, System.Data.SqlDbType.DateTime);
                    //
                    cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessRegistration);
                }
                return true;
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public static bool UpdateTaxDetails(UMst_TaxDetails taxDetails)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(taxDetails.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(taxDetails.TaxTypesId, CommonConstants.TaxTypesId, System.Data.SqlDbType.TinyInt);
                cmd.AddParameters(taxDetails.TaxNumber, CommonConstants.TaxNumber, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(taxDetails.TaxValue, CommonConstants.TaxValue, System.Data.SqlDbType.Money);
                cmd.AddParameters(taxDetails.ModifiedDate, CommonConstants.ModifiedDate, System.Data.SqlDbType.DateTime);
                cmd.AddParameters(taxDetails.ModifiedByUserId, CommonConstants.ModifiedByUserId, System.Data.SqlDbType.Int);
                cmd.ExecuteNonQuery(SqlProcedures.Update_TaxDetails);
            }
            return true;
        }

        public static bool UpdateBusinessPreferences(UMst_BusinessPreferences preference, int formatType)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(preference.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(preference.PreferenceValuesId, CommonConstants.PreferenceValuesId, System.Data.SqlDbType.SmallInt);
                cmd.AddParameters(formatType, CommonConstants.PreferenceFieldId, System.Data.SqlDbType.Int);
                cmd.AddParameters(preference.ModifiedByUserId, CommonConstants.ModifiedByUserId, System.Data.SqlDbType.Int);
                cmd.ExecuteNonQuery(SqlProcedures.Update_BusinessPreferences);
            }
            return true;
        }

        public static bool UpdateUserCustomizeFields(UMst_UserCustomizeFields customizeFields)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(customizeFields.BusinessID, CommonConstants.BusinessID, System.Data.SqlDbType.Int);
                cmd.AddParameters(customizeFields.InvoiceTerms, CommonConstants.InvoiceTerms, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(customizeFields.POTerms, CommonConstants.POTerms, System.Data.SqlDbType.VarChar);
                cmd.AddParameters(customizeFields.Logo, CommonConstants.Logo, System.Data.SqlDbType.Binary);
                cmd.AddParameters(customizeFields.CustomersRenameId, CommonConstants.CustomersRenameId, System.Data.SqlDbType.TinyInt);
                cmd.AddParameters(customizeFields.ModifiedByUserId, CommonConstants.ModifiedByUserId, System.Data.SqlDbType.Int);
                cmd.ExecuteNonQuery(SqlProcedures.Udpate_UserCustomizeFields);
            }
            return true;
        }

        public static List<UMst_UserGreetingEmails> GetGreetingMessages(int BusinessId)
        {
            List<UMst_UserGreetingEmails> lstEmails = new List<UMst_UserGreetingEmails>();
            using (var cmd = new DBSqlCommand())
            {
                try
                {
                    cmd.AddParameters(BusinessId, CommonConstants.BusinessID, System.Data.SqlDbType.Int);

                    var ireader = cmd.ExecuteDataReader(SqlProcedures.Get_GreetingEmails);
                    while (ireader.Read())
                    {
                        var email = new UMst_UserGreetingEmails
                        {
                            GreetingsListId = ireader.GetInt16(CommonColumns.GreetingsListId),
                            Message = ireader.GetString(CommonColumns.Message)
                        };
                        lstEmails.Add(email);
                    }
                    return lstEmails;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
