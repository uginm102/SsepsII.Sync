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
    
    public partial class SSEPSRule
    {
        public int payItemID { get; set; }
        public string Description { get; set; }
        public string formulaText { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoUpdated { get; set; }
        public string LogRefID { get; set; }
    
        public virtual PayItem PayItem { get; set; }
    }
}
