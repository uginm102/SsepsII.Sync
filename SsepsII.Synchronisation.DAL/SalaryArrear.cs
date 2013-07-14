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
    
    public partial class SalaryArrear
    {
        public int ArrearsID { get; set; }
        public long EmployeeID { get; set; }
        public int PayitemID { get; set; }
        public int arrearAssignID { get; set; }
        public decimal ArrearsAmount { get; set; }
        public int StartPeriod { get; set; }
        public int EndPeriod { get; set; }
        public decimal repaymentAmount { get; set; }
        public bool status { get; set; }
        public string endorsedBy { get; set; }
        public System.DateTime endorsedOn { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
        public string LogRefID { get; set; }
    
        public virtual ArrearsAssignment ArrearsAssignment { get; set; }
        public virtual EmployeeAssignment EmployeeAssignment { get; set; }
        public virtual PayItem PayItem { get; set; }
    }
}
