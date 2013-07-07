using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public class StandardPermissionProvider : IPermissionProvider
    {
        #region Administration Permissions
        public static readonly SystemPermission AccessAdminArea = new SystemPermission { Name = "Access Administration Area", SystemName = "AccessAdminArea", Category = "Standard" };
        public static readonly SystemPermission ManageUsers = new SystemPermission { Name = "Manage Users", SystemName = "ManageUsers", Category = "SystemUsers" };
        public static readonly SystemPermission ViewUsers = new SystemPermission { Name = "View Users", SystemName = "ViewUsers", Category = "SystemUsers" };
        public static readonly SystemPermission ManageRoles = new SystemPermission { Name = "Manage Roles", SystemName = "ManageRoles", Category = "SystemUsers" };
        public static readonly SystemPermission ManageSettings = new SystemPermission { Name = "Manage Settings", SystemName = "ManageSettings", Category = "SystemUsers" };
        #endregion

        #region Employee Management Permissions
        public static readonly SystemPermission EnrollEmployee = new SystemPermission { Name = "Enroll Employee", SystemName = "EnrollEmployee", Category = "Standard" };
        public static readonly SystemPermission EditEmployeeInfo = new SystemPermission { Name = "Edit Employee Information", SystemName = "EditEmployeeInfo", Category = "Standard" };
        public static readonly SystemPermission PromoteEmployee = new SystemPermission { Name = "Promote Employee", SystemName = "PromoteEmployee", Category = "Standard" };
        public static readonly SystemPermission DemoteEmployee = new SystemPermission { Name = "Demote Employee", SystemName = "DemoteEmployee", Category = "Standard" };
        public static readonly SystemPermission ExternalTransfer = new SystemPermission { Name = "External Employee Transfer", SystemName = "ExternalTransfer", Category = "Standard" };
        public static readonly SystemPermission InternalTransfer = new SystemPermission { Name = "Internal Employee Transfer", SystemName = "InternalTransfer", Category = "Standard" };
        public static readonly SystemPermission IncrementEmployee = new SystemPermission { Name = "Increment Employee", SystemName = "EmployeeIncrement", Category = "Standard" };
        #endregion

        #region Payroll Permissions
        public static readonly SystemPermission CalculatePayroll = new SystemPermission { Name = "Calculate Payroll", SystemName = "CalculatePayroll", Category = "Standard" };
        public static readonly SystemPermission SavePayroll = new SystemPermission { Name = "Save Payroll", SystemName = "SavePayroll", Category = "Standard" };
        public static readonly SystemPermission PrintPaySheet = new SystemPermission { Name = "Print PaySheet", SystemName = "PrintPaySheet", Category = "Standard" };
        #endregion

        #region Nominal Roll Permissions
        public static readonly SystemPermission ViewNominalRoll = new SystemPermission { Name = "View Nominal Roll", SystemName = "ViewNominalRoll", Category = "Standard" };
        public static readonly SystemPermission PrintNominalRoll = new SystemPermission { Name = "Print Nominal Roll", SystemName = "PrintNominalRoll", Category = "Standard" };
        public static readonly SystemPermission EditNominalRoll = new SystemPermission { Name = "Edit Nominal Roll", SystemName = "EditNominalRoll", Category = "Standard" };
        public static readonly SystemPermission TransitionNominalRoll = new SystemPermission { Name = "Transition Nominal Roll", SystemName = "TransitionNominalRoll", Category = "Standard" };
        public static readonly SystemPermission ApproveNominalRoll = new SystemPermission { Name = "Approve Nominal Roll", SystemName = "ApproveNominalRoll", Category = "Standard" };
        #endregion

        #region Other Permissions
        public static readonly SystemPermission ApproveChanges = new SystemPermission { Name = "Approve Changes", SystemName = "ApproveChanges", Category = "Standard" };
        public static readonly SystemPermission SynchroniseChganges = new SystemPermission { Name = "Synchronise Changes", SystemName = "SynchroniseChanges", Category = "Standard" };
        #endregion

        public virtual IEnumerable<Dal.SystemPermission> GetPermissions()
        {
            return new[] 
            {
                AccessAdminArea,
                ManageUsers,
                ViewUsers,
                ManageRoles,
                ManageSettings,

                EnrollEmployee,
                EditEmployeeInfo,
                PromoteEmployee,
                DemoteEmployee,
                ExternalTransfer,
                InternalTransfer,
                IncrementEmployee,

                CalculatePayroll,
                SavePayroll,
                PrintPaySheet,

                ViewNominalRoll,
                PrintNominalRoll,
                EditNominalRoll,
                TransitionNominalRoll,
                ApproveNominalRoll,

                ApproveChanges,
                SynchroniseChganges
            };
        }

        public IEnumerable<DefaultSystemPermission> GetDefaultPermissions()
        {
            return new[] 
            {
                new DefaultSystemPermission 
                {
                    RoleSystemName = SystemRoleNames.Administrators,
                    SystemPermissions = new[] 
                    {
                        AccessAdminArea,
                        ManageUsers,
                        ViewUsers,
                        EnrollEmployee,
                        EditEmployeeInfo,
                        //ManagePayroll,
                        ViewNominalRoll
                    }
                },
            };
        }
    }
}
