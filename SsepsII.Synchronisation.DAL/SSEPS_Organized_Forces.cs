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
    
    public partial class SSEPS_Organized_Forces
    {
        public string EmployeeID { get; set; }
        public string Prefix { get; set; }
        public int EmployeePIN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string PageNo { get; set; }
        public string PostNo { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> IsPensionable { get; set; }
        public Nullable<bool> JobGradeClassification { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> PayGrade { get; set; }
        public Nullable<int> IncrementPoint { get; set; }
        public int DirectorateID { get; set; }
        public string DirectorateName { get; set; }
        public int MdaID { get; set; }
        public string MdaName { get; set; }
        public int PayscaleID { get; set; }
        public string PayScaleName { get; set; }
        public string PayMethod { get; set; }
        public Nullable<int> BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<System.DateTime> DateOfFirstAppointment { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string EstablishmentNo { get; set; }
    }
}
