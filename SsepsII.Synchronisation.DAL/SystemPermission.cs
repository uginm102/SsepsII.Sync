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
    
    public partial class SystemPermission
    {
        public SystemPermission()
        {
            this.SystemRolePermissions = new HashSet<SystemRolePermission>();
        }
    
        public int SystemPermissionId { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Category { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string WhoCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<System.DateTime> WhoUpdated { get; set; }
    
        public virtual ICollection<SystemRolePermission> SystemRolePermissions { get; set; }
    }
}
