//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SsepsII.Synchronisation.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MdaGovernmentMapping
    {
        public MdaGovernmentMapping()
        {
            this.Budgets = new HashSet<Budget>();
            this.DirectorateListings = new HashSet<DirectorateListing>();
            this.EmployeeAssignments = new HashSet<EmployeeAssignment>();
            this.EmployeePayrollHistories = new HashSet<EmployeePayrollHistory>();
            this.InstallationConfigs = new HashSet<InstallationConfig>();
            this.Jobs = new HashSet<Job>();
            this.MDA_CONFIG = new HashSet<MDA_CONFIG>();
            this.NominalRolls = new HashSet<NominalRoll>();
            this.SystemUserMdas = new HashSet<SystemUserMda>();
            this.SystemUserSupervisedMdas = new HashSet<SystemUserSupervisedMda>();
        }
    
        public int mdaID { get; set; }
        public int mdaStructureID { get; set; }
        public int governmentID { get; set; }
        public string mdaCode { get; set; }
        public string chartOfAccountCode { get; set; }
        public string mdaName { get; set; }
        public string AccountingOfficerName { get; set; }
        public string AccountingOfficerMobileNumber { get; set; }
        public string PayrollManagerName { get; set; }
        public string PayrollManagerMobileNumber { get; set; }
        public string mdaAddress { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
    
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<DirectorateListing> DirectorateListings { get; set; }
        public virtual ICollection<EmployeeAssignment> EmployeeAssignments { get; set; }
        public virtual ICollection<EmployeePayrollHistory> EmployeePayrollHistories { get; set; }
        public virtual Government Government { get; set; }
        public virtual ICollection<InstallationConfig> InstallationConfigs { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<MDA_CONFIG> MDA_CONFIG { get; set; }
        public virtual MdaStructure MdaStructure { get; set; }
        public virtual ICollection<NominalRoll> NominalRolls { get; set; }
        public virtual ICollection<SystemUserMda> SystemUserMdas { get; set; }
        public virtual ICollection<SystemUserSupervisedMda> SystemUserSupervisedMdas { get; set; }
    }
}
