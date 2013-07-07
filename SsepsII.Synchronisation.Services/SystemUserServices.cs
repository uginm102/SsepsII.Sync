using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SsepsII.Synchronisation.DAL;

namespace SsepsII.Synchronisation.Services
{
    public class SystemUserServices
    {
        public SystemUser LogIn(string username, string password)
        {//MyConnectionString.ConnectionString
            if (username.IsNotNullOrWhiteSpace() && password.IsNotNullOrWhiteSpace())
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    if (ents.SystemUsers.Where(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)).Count() == 1)
                    {
                        SystemUser systemUser = ents.SystemUsers
                            .Include("SystemUserMdas.MdaGovernmentMapping.Government")
                            .Include("SystemUserSupervisedMdas.MdaGovernmentMapping.Government")
                            .Include("SystemUserRoles.SystemRole")
                            .Include("Employee")
                            .Single(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                        if (systemUser != null && systemUser.Locked == false)
                        {
                            var passwordHash = new EncryptionService().GetSHA256(password, systemUser.PasswordSalt, true);
                            bool isValid = systemUser.Password.IsNotNullOrWhiteSpace() && systemUser.Password == passwordHash && systemUser.Active;
                            if (isValid)
                            {
                                return systemUser;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
