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
    
    public partial class PayItemGovernmentMapping
    {
        public int PayitemID { get; set; }
        public int governmentID { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
    
        public virtual Government Government { get; set; }
        public virtual PayItem PayItem { get; set; }
    }
}
