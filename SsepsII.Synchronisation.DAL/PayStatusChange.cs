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
    
    public partial class PayStatusChange
    {
        public int payChangeID { get; set; }
        public int payStatusID { get; set; }
        public long EmployeeId { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public string reasonForChange { get; set; }
        public byte[] whoAuthorized { get; set; }
        public string whoChanged { get; set; }
        public System.DateTime dateChanged { get; set; }
        public string LogRefID { get; set; }
    }
}
