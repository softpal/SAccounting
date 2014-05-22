using System.Collections.Generic;
using System.Linq;
using System.Web;
using StratusAccounting.Models;

namespace StratusAccounting.Helper
{
    public class Helper
    {
        public string GetBusinessStatus(BusinessStatus businessStatus)
        {
            string verificationValue = string.Empty;
            switch (businessStatus)
            {
                case BusinessStatus.Pending:
                    verificationValue = StratusResources.AccountVerification;
                    break;
                case BusinessStatus.Expired:
                    verificationValue = StratusResources.AccountExpired;
                    break;
                case BusinessStatus.Closed:
                    verificationValue = StratusResources.AccountClosed;
                    break;
            }
            return verificationValue;
        }

        public static string GetApplicationParamValue(string paramName)
        {
            var appParam = (List<ApplicationParameters>)HttpContext.Current.Application["AppParam"];
            return appParam.First(item => item.ParamName.Equals(paramName)).ParamValue;
        }

        internal static void ClearCache()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            var cacheItems = new List<string>();

            while (enumerator.MoveNext())
                cacheItems.Add(enumerator.Key.ToString());

            foreach (string key in cacheItems)
                HttpRuntime.Cache.Remove(key);
        }
    }
}