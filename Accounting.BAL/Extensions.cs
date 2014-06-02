using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    
        public static class Extensions
        {
            public static string GetString(this System.Data.IDataRecord record, string fieldname)
            {
                return GetString(record, fieldname, string.Empty);
            }

            public static string GetString(this System.Data.IDataRecord record, string fieldname, string defaultIfNull)
            {
                object value = record[fieldname];

                if (value is string)
                    return (string)value;
                else
                    return value == DBNull.Value ? defaultIfNull : value.ToString();
            }

            public static string GetString(this System.Data.IDataRecord record, int index, string defaultIfNull)
            {
                object value = record[index];

                if (value is string)
                    return (string)value;
                else
                    return value == DBNull.Value ? defaultIfNull : value.ToString();
            }

            public static string GetShortDate(this System.Data.IDataRecord record, string fieldname)
            {
                object value = record[fieldname];

                DateTime? date;
                if (value is DateTime)
                    date = (DateTime)value;
                else if (value == DBNull.Value)
                    date = null;
                else
                    date = Convert.ToDateTime(value);

                return string.Format("{0:M/d/yyyy}", date);
            }

            public static Int16 GetInt16(this System.Data.IDataRecord record, string fieldname)
            {
                return GetInt16(record, fieldname, 0);
            }

            public static Int16 GetInt16(this System.Data.IDataRecord record, string fieldname, Int16 defaultIfNull)
            {
                object value = record[fieldname];

                if (value is Int16)
                    return (Int16)value;
                else
                    return value == DBNull.Value ? defaultIfNull : Convert.ToInt16(value);
            }

            public static Int32 GetInt32(this System.Data.IDataRecord record, string fieldname)
            {
                return GetInt32(record, fieldname, 0);
            }

            public static Int32 GetInt32(this System.Data.IDataRecord record, string fieldname, Int32 defaultIfNull)
            {
                object value = record[fieldname];

                if (value is Int32)
                    return (Int32)value;
                else
                    return value == DBNull.Value ? defaultIfNull : Convert.ToInt32(value);
            }

            public static Int64 GetInt64(this System.Data.IDataRecord record, string fieldname)
            {
                return GetInt64(record, fieldname, 0);
            }

            public static Int64 GetInt64(this System.Data.IDataRecord record, string fieldname, Int64 defaultIfNull)
            {
                object value = record[fieldname];

                if (value is Int64)
                    return (Int64)value;
                else
                    return value == DBNull.Value ? defaultIfNull : Convert.ToInt64(value);
            }

            public static bool GetBoolean(this System.Data.IDataRecord record, string fieldname)
            {
                return GetBoolean(record, fieldname, false);
            }

            public static bool GetBoolean(this System.Data.IDataRecord record, string fieldname, bool defaultIfNull)
            {
                object value = record[fieldname];

                if (value is bool)
                    return (bool)value;
                else
                    return value == DBNull.Value ? defaultIfNull : Convert.ToBoolean(value);
            }

            public static DateTime GetDateTime(this System.Data.IDataRecord record, string fieldname)
            {
                object value = record[fieldname];

                if (value is DateTime)
                    return (DateTime)value;
                else
                    return ((value == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(value));
            }

            public static DateTime? GetNullDateTime(this System.Data.IDataRecord record, string fieldname)
            {
                object value = record[fieldname];

                if (value is DateTime)
                    return (DateTime)value;
                else if (value == DBNull.Value)
                    return null;
                else
                    return Convert.ToDateTime(value);
            }

            public static DateTime? GetNullLocalDateTime(this System.Data.IDataRecord record, string fieldname)
            {
                object value = record[fieldname];

                if (value is DateTime)
                {
                    DateTime val = (DateTime)value;
                    return TimeZone.CurrentTimeZone.ToLocalTime(val);
                }
                else if (value == DBNull.Value)
                    return null;
                else
                    return Convert.ToDateTime(value);
            }

            public static DateTime GetLocalDateTime(this System.Data.IDataRecord record, string fieldname)
            {
                object value = record[fieldname];
                if (value is DateTime)
                {
                    DateTime val = (DateTime)value;
                    return TimeZone.CurrentTimeZone.ToLocalTime(val);
                }
                else
                    return ((value == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(value));
            }


            public class DbEnumerable : IEnumerable<System.Data.IDataRecord>
            {
                private System.Data.IDataReader reader;

                public DbEnumerable(System.Data.IDataReader reader)
                {
                    this.reader = reader;
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return reader.GetEnumerator();
                }

                IEnumerator<System.Data.IDataRecord> IEnumerable<System.Data.IDataRecord>.GetEnumerator()
                {
                    return this.OfType<System.Data.IDataRecord>().GetEnumerator();
                }
            }

            public static IEnumerable<System.Data.IDataRecord> GetEnumerable(this System.Data.IDataReader reader)
            {
                return new DbEnumerable(reader);
            }

            public static System.Data.Common.DbEnumerator GetEnumerator(this System.Data.IDataReader reader)
            {
                return new System.Data.Common.DbEnumerator(reader);
            }

            public static byte[] GetBytes(this System.Data.IDataRecord record, string fieldName)
            {
                int Index = record.GetOrdinal(fieldName);
                object Value = record[Index];

                if (Value == DBNull.Value)
                {
                    return new byte[] { };
                }
                else
                    return (byte[])Value;
            }
        }
    
}
