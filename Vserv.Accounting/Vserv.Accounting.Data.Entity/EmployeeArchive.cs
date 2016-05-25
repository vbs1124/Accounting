//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vserv.Accounting.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeArchive
    {
        public int EmployeeArchiveId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PermanentAccountNumber { get; set; }
        public string UniversalAccountNumber { get; set; }
        public string EPFNumber { get; set; }
        public string AADHAARNumber { get; set; }
        public string ESINumber { get; set; }
        public string MobileNumber { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string PersonalEmailAddress { get; set; }
        public System.DateTime BirthDay { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> RelievingDate { get; set; }
        public Nullable<System.DateTime> ResignationDate { get; set; }
        public string VBS_Id { get; set; }
        public int DesignationId { get; set; }
        public int SalutationId { get; set; }
        public int GenderId { get; set; }
        public int OfficeBranchId { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentZipCode { get; set; }
        public Nullable<int> PermanentStateId { get; set; }
        public Nullable<int> PermanentCountryId { get; set; }
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingZipCode { get; set; }
        public Nullable<int> MailingStateId { get; set; }
        public Nullable<int> MailingCountryId { get; set; }
        public bool IsMetro { get; set; }
        public string BankAccountNumber { get; set; }
        public Nullable<int> BankId { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankMICRCode { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual State State { get; set; }
        public virtual OfficeBranch OfficeBranch { get; set; }
        public virtual State State1 { get; set; }
        public virtual Salutation Salutation { get; set; }
    }
}