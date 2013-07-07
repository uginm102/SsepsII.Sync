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
    
    public partial class EmployeeAssignment
    {
        public EmployeeAssignment()
        {
            this.EmployeeEvents = new HashSet<EmployeeEvent>();
        }
    
        public long EmployeeID { get; set; }
        public Nullable<int> evidenceReferenceNo { get; set; }
        public int employementTermID { get; set; }
        public Nullable<int> governmentLevel { get; set; }
        public Nullable<int> fundSource { get; set; }
        public System.DateTime dateStart { get; set; }
        public string jobKey { get; set; }
        public Nullable<int> jobID { get; set; }
        public string JobTitleNominalRoll { get; set; }
        public string jobTitleAgainst { get; set; }
        public int payScaleID { get; set; }
        public int jobGrade { get; set; }
        public int jobGradeClassification { get; set; }
        public int paySegment { get; set; }
        public int payStatusID { get; set; }
        public bool IsPensionable { get; set; }
        public int mdaID { get; set; }
        public int directorateID { get; set; }
        public Nullable<int> job_worklocation_StateID { get; set; }
        public string FileIndex { get; set; }
        public int payType { get; set; }
        public int payMethod { get; set; }
        public int payFrequency { get; set; }
        public string authorisedBy { get; set; }
        public System.DateTime authorisedOn { get; set; }
        public int eventId { get; set; }
        public int workflowComplete { get; set; }
        public System.DateTime dateCreated { get; set; }
        public Nullable<System.DateTime> dateUpdated { get; set; }
        public string whoCreated { get; set; }
        public string whoUpdated { get; set; }
        public string LogRefID { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual MdaGovernmentMapping MdaGovernmentMapping { get; set; }
        public virtual ICollection<EmployeeEvent> EmployeeEvents { get; set; }
    }
}