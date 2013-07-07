using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SsepsII.Synchronisation.DAL;
using SsepsII.Synchronisation.Services;

namespace SSEPS_II.Synchronisation
{
    public class ConfigurationManager
    {
        public bool IsLoggedIn
        {
            get
            {
                return LoggedInUser != null ? true : false;
            }
        }
        public SystemUser LoggedInUser { get; set; }
        private SystemUserServices _systemUserServices = new SystemUserServices();
        public ConfigurationManager()
        {
            
        }

        public void Logout()
        {
            LoggedInUser = null;
        }

        public void Login(string username, string password)
        {
            LoggedInUser = _systemUserServices.LogIn(username, password);
        }
    }
}
