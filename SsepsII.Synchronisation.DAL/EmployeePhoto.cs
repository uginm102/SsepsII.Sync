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
    
    public partial class EmployeePhoto
    {
        public long EmployeeID { get; set; }
        public byte[] EmployeePhoto1 { get; set; }
        public byte[] LeftThump { get; set; }
        public byte[] LeftFourFingers { get; set; }
        public byte[] RightThump { get; set; }
        public byte[] RightFourFingers { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
        public string LogRefID { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
