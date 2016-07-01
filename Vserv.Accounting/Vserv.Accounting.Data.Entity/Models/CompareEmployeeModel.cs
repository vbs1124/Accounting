using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity
{
    public class CompareEmployeeModel
    {
        public EmployeeMapper PreviousEmployeeInfo { get; set; }

        public EmployeeMapper CurrentEmployeeInfo { get; set; }

        public int ModifiedColumnCount { get; set; }
    }

    public class EmployeeMapper
    {
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
        public string DesignationName { get; set; }
        public string SalutationName { get; set; }
        public string GenderName { get; set; }
        public string OfficeBranchName { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentZipCode { get; set; }
        public string PermanentStateName { get; set; }
        public string PermanentCountryName { get; set; }
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingZipCode { get; set; }
        public string MailingStateName { get; set; }
        public string MailingCountryName { get; set; }
        public bool IsMetro { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankMICRCode { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
