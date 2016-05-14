using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    public partial class EmployeeModel
    {
        [ScaffoldColumn(true)]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name:")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "First Name is too short.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name:")]
        [MaxLength(250, ErrorMessage = "Middle Name is too short.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name:")]
        [StringLength(250, ErrorMessage = "Last Name is too short.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Father's Name is required.")]
        [Display(Name = "Father's Name:")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Father's Name is too short.")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name:")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "UAN Number is required.")]
        [Display(Name = "UAN Number:")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "UAN Number should Be 12 digits.")]
        public string UniversalAccountNumber { get; set; }

        [Required(ErrorMessage = "PAN Number is required.")]
        [Display(Name = "PAN Number:")]
        public string PermanentAccountNumber { get; set; }

        [Display(Name = "AADHAAR:")]
        public string AADHAARNumber { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Display(Name = "Mobile Number:")]
        [MobileNumberExists]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number should be 10 digits.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address:")]
        [ValidEmailAddress(ErrorMessage = "Invalid Email address.")]
        [EmailAddressExists]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [Display(Name = "Date of Birth:")]
        public Nullable<DateTime> BirthDay { get; set; }

        [Display(Name = "Joining Date:")]
        [Required(ErrorMessage = "Joining Date is required.")]
        public Nullable<DateTime> JoiningDate { get; set; }

        [Display(Name = "Relieving Date:")]
        public Nullable<DateTime> RelievingDate { get; set; }

        [Display(Name = "Employee Id:")]
        [Required(ErrorMessage = "Employee Id is required.")]
        [ValidEmployeeId]
        public string VBS_Id { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [Display(Name = "Designation:")]
        public int? DesignationId { get; set; }

        [Required(ErrorMessage = "Salutation is required.")]
        [Display(Name = "Salutation:")]
        public int? SalutationId { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [Display(Name = "Gender:")]
        public int? GenderId { get; set; }

        [Required(ErrorMessage = "Office Branch is required.")]
        [Display(Name = "Office Branch:")]
        public int? OfficeBranchId { get; set; }

        [Display(Name = "Department:")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Active:")]
        public bool IsActive { get; set; }

        [Display(Name = "CreatedById")]
        public int? CreatedById { get; set; }

        [Display(Name = "UpdatedById")]
        public Nullable<int> UpdatedById { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime? CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }

        public virtual DesignationModel Designation { get; set; }
        public virtual OfficeBranchModel OfficeBranch { get; set; }
        public virtual SalutationModel Salutation { get; set; }

        // Lookup Datas
        public List<AddressTypeModel> AddressTypes { get; set; }

        public List<DepartmentModel> Departments { get; set; }

        public List<DesignationModel> Designations { get; set; }

        public List<OfficeBranchModel> OfficeBranches { get; set; }

        public List<SalutationModel> Salutations { get; set; }

        public List<SelectListItem> Genders { get; set; }

        public List<StateModel> States { get; set; }

        public List<CityModel> Cities { get; set; }

        public List<ZipCodeModel> ZipCodes { get; set; }

        public AddressModel PermanentAddress { get; set; }

        public AddressModel MailingAddress { get; set; }
    }

    public partial class AddressModel
    {
        public int AddressId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Address 1 is required.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Address 1 is too short.")]
        [Display(Name = "Address 1:")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2:")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(250, ErrorMessage = "City is too long.")]
        [Display(Name = "City:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pin Code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pin Code should be 6 digits.")]
        [Display(Name = "Pin Code:")]
        public string ZipCode { get; set; }

        [Display(Name = "Latitude:")]
        public Nullable<decimal> Latitude { get; set; }

        [Display(Name = "Longitude:")]
        public Nullable<decimal> Longitude { get; set; }

        [Display(Name = "IsCommunicationAddress:")]
        public bool IsCommunicationAddress { get; set; }

        [Display(Name = "Address Type:")]
        public int AddressTypeId { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State:")]
        public int? StateId { get; set; }

        [Display(Name = "Country:")]
        public int? CountryId { get; set; }

        [Display(Name = "Active:")]
        public bool IsActive { get; set; }

        [Display(Name = "Created By Id")]
        public int CreatedById { get; set; }

        [Display(Name = "Updated By Id")]
        public Nullable<int> UpdatedById { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Display(Name = "Address Type")]
        public virtual AddressTypeModel AddressType { get; set; }
    }

    public partial class AddressTypeModel
    {
        public int AddressTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<AddressModel> Addresses { get; set; }
    }

    public partial class DepartmentModel
    {
        public int DepartmentId { get; set; }
        public int DisplayOrder { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    public partial class EmployeeAddressModel
    {
        public int EmployeeAddressId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
    }

    public partial class EPFNumberModel
    {
        public int EPFNumberId { get; set; }
        public int EmployeeId { get; set; }
        public int EPFOfficeId { get; set; }
        public string EstablishmentCode { get; set; }
        public string Extension { get; set; }
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    public partial class EPFOffice
    {
        public int EPFOfficeId { get; set; }
        public string EPFOfficeName { get; set; }
        public string EPFOfficeCode { get; set; }
        public string StateId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    public partial class OfficeBranchModel
    {


        public int OfficeBranchId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }

    public partial class SalutationModel
    {
        public int SalutationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> UpdatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}