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
    
    public partial class PayScale
    {
        public PayScale()
        {
            this.MdaStructurePayScaleMappings = new HashSet<MdaStructurePayScaleMapping>();
            this.PayItemGradeMappings = new HashSet<PayItemGradeMapping>();
        }
    
        public int payScaleID { get; set; }
        public string payScaleName { get; set; }
        public string payGradeXML { get; set; }
        public bool IsPensionable { get; set; }
        public System.DateTime dateCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoCreated { get; set; }
        public string whoUpdated { get; set; }
    
        public virtual ICollection<MdaStructurePayScaleMapping> MdaStructurePayScaleMappings { get; set; }
        public virtual ICollection<PayItemGradeMapping> PayItemGradeMappings { get; set; }
    }
}
