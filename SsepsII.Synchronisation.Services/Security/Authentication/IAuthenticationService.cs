using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public interface IAuthenticationService
    {
        bool ValidateCustomer(string username, string password);
        void SignIn(SystemUser user, bool createPersistentCookie);
        void SignOut();
    }
}
