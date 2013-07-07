using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SsepsII.Synchronisation.Services
{
    public static class MyConnectionString
    {
        public static string ConnectionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SsepsII.Synchronisation\\1.0", false);

                //sb.Append("metadata=res://*/SsepsIISynModel.csdl|res://*/SsepsIISynModel.ssdl|res://*/SsepsIISynModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;");
                sb.Append(string.Format("data source={0}\\{1};", (string)myKey.GetValue("server"), (string)myKey.GetValue("database")));
                sb.Append("initial catalog=SSEPS;");
                sb.Append(string.Format("User ID={0};", (string)myKey.GetValue("UserID")));
                sb.Append(string.Format("Password={0};", (string)myKey.GetValue("Password")));
                //sb.Append(string.Format("database={0} \"", (string)myKey.GetValue("database")));
                sb.Append("MultipleActiveResultSets=True;App=EntityFramework&quot;");

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
