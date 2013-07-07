using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Ninject;
using SsepsII.Services.Caching;

namespace SsepsII.Services.Security.Providers
{
    public class SsepsIIRoleProvider : RoleProvider
    {
        [Inject]
        public ISystemUserService SystemUserService { get; set; }

        [Inject]
        public ICacheService CacheService { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Contains(roleName);
        }

        private const string GetRolesForUserCacheKey = "SystemUserService.SystemUser.user.roles.{0}";
        public override string[] GetRolesForUser(string username)
        {
            return CacheService.Get(GetRolesForUserCacheKey.F(username), CacheTimeSpan.TwoMinutes,
                                    () =>
                                    {
                                        var user = SystemUserService.GetUserByUsername(username, true);
                                        var roles = SystemUserService.GetUserRoles(user.SystemUserId, true);
                                        return roles.IsNull() || roles.IsEmpty()
                                                   ? new String[] { }
                                                   : roles.Select(role => role.SystemRole.SystemName).ToArray();
                                    });
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
