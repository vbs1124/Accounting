using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string UniversalAccountNumber { get; set; }
        public string PermanentAccountNumber { get; set; }
        public string AADHAARNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime BirthDay { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> RelievingDate { get; set; }
        public string VBS_Id { get; set; }
        public int DesignationId { get; set; }
        public int SalutationId { get; set; }
        public int GenderId { get; set; }
        public int OfficeBranchId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
