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
    
    public partial class EmployeeOvertimeHistory
    {
        public int payPeriod { get; set; }
        public long EmployeeID { get; set; }
        public int hoursWeekEndPublicHoliday { get; set; }
        public int hoursNormalWorkingday { get; set; }
        public string endorsedBy { get; set; }
        public System.DateTime endorsedOn { get; set; }
        public bool status { get; set; }
        public int payItemID { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
        public string LogRefID { get; set; }
    
        public virtual EmployeeAssignment EmployeeAssignment { get; set; }
        public virtual PayItem PayItem { get; set; }
    }
}
