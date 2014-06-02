using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using StratusAccounting.DAL;

namespace StratusAccounting.BAL
{
    internal class Helper
    {
    }

    public static class ExtensionMethods
    {
        public static string GetSHA1(this string password)
        {
            var UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(password);

            var hashString = new SHA1Managed();

            byte[] hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + String.Format("{0:x2}", x));
        }

        public static decimal? GetNullableDecimal(this IDataReader reader, int column)
        {
            return reader.IsDBNull(column) ? (decimal?)null :
                Convert.ToDecimal(reader.GetDecimal(column).ToString("f"));
        }

        public static decimal GetFormatDecimal(this IDataReader reader, int column)
        {
            return Convert.ToDecimal(reader.GetDecimal(column).ToString("f"));
        }

        public static string ToPreferenceDateTime(this DateTime dateTime, string dateFormat)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(dateTime.ToString(), dateFormat, null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate.ToString();
            }
            return dateTime.ToString("MM/dd/yyyy");
        }

        public static string CreateGuidFolder()
        {
            string path;
            try
            {
                Guid fileGuid = Guid.NewGuid();
                path = AppDomain.CurrentDomain.BaseDirectory;
                if (!Directory.Exists(path + "Documents"))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path + "Documents");
                }
                path += "Documents\\" + fileGuid.ToString();
                // Determine whether the directory exists. 
                Directory.CreateDirectory(path);
                return fileGuid.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void AddFiletoGuid(string path, string file)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                string location = Path.Combine(path, fileName);
                byte[] Input = null;
                System.IO.Stream myStream;

                System.IO.FileStream newFile = new System.IO.FileStream(location, System.IO.FileMode.Create);

                // Write data to the file
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    Input = new Byte[fs.Length];
                    int length = Convert.ToInt32(fs.Length);
                    fs.Read(Input, 0, length);
                }

                newFile.Write(Input, 0, Input.Length);
                // Close file
                newFile.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void SaveDocumentPath(int businessID, int userID, int screenId, string guid)
        {
            using (var cmd = new DBSqlCommand())
            {
                cmd.AddParameters(businessID, CommonConstants.BusinessID, SqlDbType.Int);
                cmd.AddParameters(userID, CommonConstants.UserId, SqlDbType.Int);
                cmd.AddParameters(screenId, CommonConstants.ScreenId, SqlDbType.Int);
                cmd.AddParameters(guid, CommonConstants.FileGUID, SqlDbType.VarChar);
                cmd.ExecuteNonQuery(SqlProcedures.Insert_UploadDocuments);
            }
        }
    }


}
