using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Ninject;
using SsepsII.Dal;
using SsepsII.Services.Caching;

namespace SsepsII.Services.Security
{
    public class PermissionService : IPermissionService
    {
        [Inject]
        public ICacheService CacheService { get; set; }
        [Inject]
        public ISystemUserService SystemUserService { get; set; }

        private static readonly string m_RoleCacheKeyPattern = "Role.Permissions.";
        private static readonly string m_PermissionCacheKeyPattern = "Role.Permissions.{0}";
        private static readonly string m_RolePermissionsCacheKey = "Role.Permissions.role.{0}";

        private void ClearCache()
        {
            CacheService.RemoveByPattern(m_RoleCacheKeyPattern);
            CacheService.RemoveByPattern("UserMenuCacheKey.");
        }

        #region Implementation of IPermissionService

        /// <summary>
        /// Delete a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void DeleteSystemPermission(SystemPermission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");
            using (var context = new SsepsIIEntities())
            {
                context.SystemPermissions.Attach(permission);
                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(permission, System.Data.EntityState.Modified);
                context.SystemPermissions.Remove(permission);
                context.SaveChanges();
            }
            ClearCache();
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        public virtual SystemPermission GetSystemPermissionById(int permissionId)
        {
            if (permissionId == 0)
                return null;
            using (var context = new SsepsIIEntities())
            {
                return context
                    .SystemPermissions
                    .Where(pr => pr.SystemPermissionId == permissionId)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        public virtual SystemPermission GetSystemPermissionBySystemName(string systemName, bool userCache = false)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;
            using (var context = new SsepsIIEntities())
            {
                if (userCache)
                {
                    return CacheService.Get(m_PermissionCacheKeyPattern.F(systemName),
                                              CacheTimeSpan.Infinite,
                                              () => (from perm in context.SystemPermissions
                                                     orderby perm.SystemPermissionId
                                                     where perm.SystemName == systemName
                                                     select perm).FirstOrDefault());
                }

                var query = from perm in context.SystemPermissions
                            orderby perm.SystemPermissionId
                            where perm.SystemName == systemName
                            select perm;

                var permission = query.FirstOrDefault();
                return permission;
            }
        }

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IList<SystemPermission> GetAllSystemPermissions()
        {
            using (var context = new SsepsIIEntities())
            {
                return (from cr in context.SystemPermissions.Include("SystemRolePermissions")
                        orderby cr.Name
                        select cr).ToArray().ToList();
            }
        }

        public void InsertSystemRolePermission(SystemRolePermission rolePermissionRecord)
        {
            using (var context = new SsepsIIEntities())
            {
                context.SystemRolePermissions.Add(rolePermissionRecord);
                context.SaveChanges();
            }

            ClearCache();
        }

        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void InsertSystemPermission(SystemPermission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");
            using (var context = new SsepsIIEntities())
            {
                context.SystemPermissions.Add(permission);
                context.SaveChanges();
            }

            ClearCache();
        }

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void UpdatePermissionRecord(SystemPermission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            using (var context = new SsepsIIEntities())
            {
                context.SystemPermissions.Attach(permission);
                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(permission, System.Data.EntityState.Modified);
                context.SaveChanges();
            }

            ClearCache();
        }

        /// <summary>
        /// Install permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        {
            //NB Can we find a way to automatically delete permissions?

            //install new permissions
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetSystemPermissionBySystemName(permission.SystemName);
                if (permission1 == null)
                {
                    //new permission (install it)
                    permission1 = new SystemPermission
                    {
                        Name = permission.Name,
                        SystemName = permission.SystemName,
                        Category = permission.Category,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };

                    //save new permission
                    InsertSystemPermission(permission1);

                    //default customer role mappings
                    var defaultPermissions = permissionProvider.GetDefaultPermissions();
                    foreach (var defaultPermission in defaultPermissions)
                    {
                        var role = SystemUserService.GetSystemRoleBySystemName(defaultPermission.RoleSystemName);
                        if (role == null)
                        {
                            //new role (save it)
                            role = new SystemRole
                            {
                                Name = defaultPermission.RoleSystemName,
                                Active = true,
                                SystemName = defaultPermission.RoleSystemName,
                                DateCreated = DateTime.UtcNow,
                                DateModified = DateTime.UtcNow
                            };
                            SystemUserService.InsertRole(role);
                        }

                        role = SystemUserService.GetSystemRoleById(role.SystemRoleId, false);

                        var defaultMappingProvided = (from p in defaultPermission.SystemPermissions
                                                      where p.SystemName == permission1.SystemName
                                                      select p).Any();
                        var mappingExists = (from p in role.SystemRolePermissions.Select(rpr => rpr.SystemPermission)
                                             where p.SystemName == permission1.SystemName
                                             select p).Any();
                        if (defaultMappingProvided && !mappingExists)
                        {
                            InsertSystemRolePermission(new SystemRolePermission
                            {
                                DateCreated = DateTime.UtcNow,
                                SystemRoleId = role.SystemRoleId,
                                SystemPermissionId = permission1.SystemPermissionId
                            });
                        }
                    }


                }
            }

            ClearCache();
        }



        /// <summary>
        /// Uninstall permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetSystemPermissionBySystemName(permission.SystemName);
                if (permission1 != null)
                {
                    DeleteSystemPermission(permission1);
                }
            }

            ClearCache();
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, IUserContext userContext)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var permission = GetSystemPermissionBySystemName(permissionRecordSystemName, true);
            return Authorize(permission, userContext);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(SystemPermission permission, IUserContext userContext)
        {

            if (permission == null)
                return false;

            if (userContext.CurrentUser == null)
                return false;

            var customerRoles = userContext.CurrentUser.SystemUserRoles.Where(cr => cr.SystemRole.Active);
            foreach (var role in customerRoles)
            {

                foreach (var permission1 in GetSystemRolePermissions(role.SystemRoleId))
                {
                    if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        private IEnumerable<SystemPermission> GetSystemRolePermissions(int systemRoleId)
        {
            using (var context = new SsepsIIEntities())
            {
                return CacheService.Get(m_RolePermissionsCacheKey.F(systemRoleId),
                                      CacheTimeSpan.Infinite,
                                      () => context.
                                                SystemRolePermissions.Where(
                                                    p => p.SystemRoleId == systemRoleId).
                                                Select(p => p.SystemPermission).ToArray());
            }

        }

        public void DeleteSystemRolePermission(SystemRolePermission rolePermission)
        {
            using (var context = new SsepsIIEntities())
            {
                context.SystemRolePermissions.Attach(rolePermission);
                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(rolePermission, System.Data.EntityState.Modified);
                context.SystemRolePermissions.Remove(rolePermission);
                context.SaveChanges();
            }
            ClearCache();
        }

        public List<SystemPermission> GetSystemRolePermissionsForAssignment(int systemRoleId)
        {
            using (var context = new SsepsIIEntities())
            {
                var assignedList = context.SystemRolePermissions.Where(r => r.SystemRoleId == systemRoleId).Select(r => r.SystemPermissionId).ToList<int>();
                var permissions = context.SystemPermissions.OrderBy(p => p.SystemName).ToList();
                foreach (var permission in permissions)
                    permission.IsAssignedToRole = assignedList.Contains(permission.SystemPermissionId);
                return permissions;
            }
        }

        public void AssignUnassignPermission(int systemRoleId, int permissionId, bool assign, string whoCreated)
        {
            using (var context = new SsepsIIEntities())
                context.AssignUnassignPermissionToRole(systemRoleId, permissionId, assign, whoCreated);
        }

        #endregion
    }
}
