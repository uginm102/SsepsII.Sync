using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using SsepsII.Services.Caching;
using SsepsII.Services.Web;

namespace SsepsII.Services.Security.Providers
{
    public class SsepsIIMembershipProvider : MembershipProvider
    {

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        #region Instance Properties

        public override string ApplicationName
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
            set { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new Exception("SsepsIIMembershipProvider has no implementation for this method."); }
        }

        #endregion

        public SsepsIIMembershipProvider()
        {
            var userService = new SystemUserService();
            userService.EncryptionService = new EncryptionService();
            userService.CacheService = new CacheService();
            var authService = new AuthenticationService { EncryptionService = userService.EncryptionService, SystemUserService = userService };
            this.AuthenticationService = authService;            
        }

        #region Instance Methods

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override bool UnlockUser(string userName)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new Exception("SsepsIIMembershipProvider has no implementation for this method.");
        }

        public override bool ValidateUser(string username, string password)
        {   
            return AuthenticationService.ValidateCustomer(username, password);
        }

        #endregion
    }
}
