using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public interface IPermissionService
    {
        void DeleteSystemPermission(SystemPermission permission);
        void DeleteSystemRolePermission(SystemRolePermission rolePermission);
        SystemPermission GetSystemPermissionById(int permissionId);
        SystemPermission GetSystemPermissionBySystemName(string systemName, bool userCache = false);
        IList<SystemPermission> GetAllSystemPermissions();
        void InsertSystemRolePermission(SystemRolePermission rolePermissionRecord);
        void InsertSystemPermission(SystemPermission permission);
        void UpdatePermissionRecord(SystemPermission permission);
        void InstallPermissions(IPermissionProvider permissionProvider);
        void UninstallPermissions(IPermissionProvider permissionProvider);
        bool Authorize(string permissionRecordSystemName, IUserContext userContext);
        bool Authorize(SystemPermission permission, IUserContext userContext);
        List<SystemPermission> GetSystemRolePermissionsForAssignment(int systemRoleId);
        void AssignUnassignPermission(int systemRoleId, int permissionId, bool assign, string whoCreated);
    }
}
