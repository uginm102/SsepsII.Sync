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
    
    public partial class TransLog
    {
        public int TransLogID { get; set; }
        public string ActionType { get; set; }
        public string ColumnName { get; set; }
        public string ColumnKey { get; set; }
        public string TableName { get; set; }
        public System.DateTime TransDate { get; set; }
        public Nullable<bool> IsSynched { get; set; }
        public Nullable<int> SyncPeriod { get; set; }
        public string UserId { get; set; }
    }
}