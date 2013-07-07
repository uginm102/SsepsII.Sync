using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Ninject;
using SsepsII.Dal;
using SsepsII.Services.Web;

namespace SsepsII.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        [Inject]
        public ISystemUserService SystemUserService { get; set; }
        [Inject]
        public IEncryptionService EncryptionService { get; set; }

        public static bool AccountLocked = false;

        public bool ValidateCustomer(string username, string password)
        {
            var isValid = false;
            AccountLocked = false;
            if (username.IsNotNullOrWhiteSpace() && password.IsNotNullOrWhiteSpace())
            {
                var systemUser = SystemUserService.GetUserByUsername(username, false);
                if (systemUser != null && systemUser.Locked == false)
                {
                    var adminEmail = Constant.GetSSEPS_CONFIG_KeyValue(Constant.SSEPS_CONFIG_ADMIN_EMAIL);
                    var passwordHash = EncryptionService.GetSHA256(password, systemUser.PasswordSalt, true);
                    isValid = systemUser.Password.IsNotNullOrWhiteSpace() &&
                              systemUser.Password == passwordHash && systemUser.Active;
                    if (isValid)
                    {
                        systemUser.UserLoginCount = 0;
                        systemUser.LastLoginDate = DateTime.Now;
                        systemUser.LastIpAddress = WebHelper.GetCurrentIpAddress();
                        SystemUserService.UpdateUser(systemUser);
                    }
                    else
                    {
                        systemUser.UserLoginCount = systemUser.UserLoginCount + 1;
                        if (systemUser.UserLoginCount > Math.Min(3, Int32.Parse(Constant.GetSSEPS_CONFIG_KeyValue(Constant.SSEPS_CONFIG_MAX_LOGIN_RETRIES))))
                        {
                            systemUser.Locked = true;
                        }
                        SystemUserService.UpdateUser(systemUser);
                        try
                        {
                            if (systemUser.Locked && adminEmail != null && adminEmail.Trim() != string.Empty)
                            {
                                //Send notification to admin email 
                                AccountLocked = true;
                            }
                        }
                        catch (Exception ex)
                        { 
                            throw ex; 
                        }
                    }
                }
            }
            return isValid;
        }

        public void SignIn(SystemUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Username,
                now,
                now.Add(FormsAuthentication.Timeout),
                createPersistentCookie,
                user.Username,
                FormsAuthentication.FormsCookiePath);

            FormsAuthentication.SetAuthCookie(user.Username, createPersistentCookie);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Expires = now.Add(FormsAuthentication.Timeout),
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
