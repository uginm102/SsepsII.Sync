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
    
    public partial class SystemUserRole
    {
        public int SystemUserId { get; set; }
        public int SystemRoleId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string WhoCreated { get; set; }
    
        public virtual SystemRole SystemRole { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
