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
    
    public partial class EmployeePayroll
    {
        public long EmployeeID { get; set; }
        public int payrollPeriod { get; set; }
        public int mdaID { get; set; }
        public int directorateID { get; set; }
        public System.DateTime payrollDate { get; set; }
        public string payItemsXML { get; set; }
        public Nullable<decimal> grossPay { get; set; }
        public Nullable<decimal> netPay { get; set; }
        public string payrollPreparedBy { get; set; }
        public string payrollVerifiedBy { get; set; }
        public string payrollApprovedBy { get; set; }
        public System.DateTime dateCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoCreated { get; set; }
        public string whoUpdated { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
