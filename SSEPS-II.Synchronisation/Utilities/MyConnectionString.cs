using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SSEPS_II.Synchronisation
{
    public static class MyConnectionString
    {
        public static string ConnectionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SsepsII.Synchronisation\\1.0", false);

                sb.Append(string.Format("{0}", (string)myKey.GetValue("initial")));
                sb.Append(string.Format("server={0};", (string)myKey.GetValue("server")));
                sb.Append(string.Format("User ID={0};", (string)myKey.GetValue("UserID")));
                sb.Append(string.Format("Password={0};", (string)myKey.GetValue("Password")));
                sb.Append(string.Format("database={0}", (string)myKey.GetValue("database")));
                sb.Append(string.Format("{0};", (string)myKey.GetValue("final")));

                return sb.ToString();
            }
        }

        public static bool TestConnection
        {
            get
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SsepsII.ImportUtility\\1.0", false);
                return myKey != null ? true : false;
            }
        }
    }
}
