using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsepsII.Synchronisation.Services
{
    public static class Constant
    {
        public const string SITE_ID = "SiteID";
        public const string INSTALLATION_TOKEN = "InstallationToken";
        public const string MANAGED_SITE = "ManagedSite";
        public const string MDAS_ON_SITE = "SiteMDA";
        public const string SITE_TYPE = "SiteType";

        //Table names for sync
        public const string TRANSDATA_ACTION_TYPE_DELETE = "DELETE";
        public const string TRANSDATA_ACTION_TYPE_INSERT = "INSERT";
        public const string TRANSDATA_ACTION_TYPE_UPDATE = "UPDATE";
        public const string TRANSDATA_ACTION_TYPE_EDIT = "EDIT";

        public const string EMPLOYEE_TABLE = "Employee";
        public const string EMPLOYEE_EVENT_TABLE = "EmployeeEvent";
        public const string TRANSDATA_TABLE = "TRANSDATA";
        public const string EMPLOYEE_PAYROLLHISTORY_TABLE = "EmployeePayrollHistory";
        public const string EMPLOYEE_ASSIGNMENT_TABLE = "EmployeeAssignment";
        public const string EMPLOYEE_PAYITEM_TABLE = "EmployeePayItem";
        public const string EMPLOYEE_BANK_ACCOUNT_TABLE = "EmployeeBankAccount";

        public const string SSEPS_CONFIG_ENROLLMENT_EVENTID = "EnrollmentEventId";
        public const string SSEPS_CONFIG_TRANSFER_EVENTID = "TransferEventId";
        public const string SSEPS_CONFIG_INTERNAL_TRANSFER_EVENTID = "InternalTransferId";
        public const string SSEPS_CONFIG_PROMOTION_EVENTID = "PromotionEventId";
        public const string SSEPS_CONFIG_DEMOTION_EVENTID = "DemotionEventId";
        public const string SSESP_CONFIG_LEAVE_EVENTID = "LeaveEventId";
        public const string SSEPS_CONFIG_ACTING_EVENTID = "ActingEventId";
        public const string SSEPS_CONFIG_SUSPENSION_EVENITID = "SuspensionEventId";
        public const string SSEPS_CONFIG_REINSTATE_EVENTID = "ReinstateEventId";
        public const string SSEPS_CONFIG_TERMINATION_EVENTID = "TerminationEventId";
        public const string SSEPS_CONFIG_SECONDEMENT_EVENTID = "SecondementEventId";
        public const string SSEPS_CONFIG_INCREMENT_EVENTID = "IncrementEventId";
        public const string SSEPS_CONFIG_NO_EFFECT_ON_SALARY = "NoEffectOnSalaryId";
        public const string SSEPS_CONFIG_NO_SALARY_PAY = "NoSalaryPayId";
        public const string SSEPS_CONFIG_HALF_SALARY_PAY = "HalfSalaryPayId";
        public const string SSEPS_CONFIG_CONTRACT_EMPLOYMENT_TERM = "ContractEmploymentTermId";
        public const string SSEPS_CONFIG_CENTRAL_GOSS_ID = "CentralGossId";
        public const string SSEPS_CONFIG_COUNTY_GOVT_LEVEL_ID = "CountyGovernmentLevelId";
        public const string SSEPS_CONFIG_STATE_GOVT_LEVEL_ID = "StateGovernmentLevelId";
    }
}
