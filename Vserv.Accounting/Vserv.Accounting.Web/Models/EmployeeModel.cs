using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EmployeeModel
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [ScaffoldColumn(true)]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "First Name is too short.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [Display(Name = "Middle Name")]
        [MaxLength(250, ErrorMessage = "Middle Name is too short.")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "Last Name")]
        [StringLength(250, ErrorMessage = "Last Name is too short.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the father.
        /// </summary>
        /// <value>
        /// The name of the father.
        /// </value>
        [Required(ErrorMessage = "Father's Name is required.")]
        [Display(Name = "Father's Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Father's Name is too short.")]
        public string FatherName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mother.
        /// </summary>
        /// <value>
        /// The name of the mother.
        /// </value>
        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        /// <summary>
        /// Gets or sets the universal account number.
        /// </summary>
        /// <value>
        /// The universal account number.
        /// </value>
        [Required(ErrorMessage = "UAN Number is required.")]
        [Display(Name = "UAN Number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "UAN Number should Be 12 digits.")]
        public string UniversalAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the permanent account number.
        /// </summary>
        /// <value>
        /// The permanent account number.
        /// </value>
        [Required(ErrorMessage = "PAN Number is required.")]
        [Display(Name = "PAN Number")]
        public string PermanentAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the aadhaar number.
        /// </summary>
        /// <value>
        /// The aadhaar number.
        /// </value>
        [Display(Name = "AADHAAR Number")]
        public string AADHAARNumber { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [Required(ErrorMessage = "Mobile Number is required.")]
        [Display(Name = "Mobile Number")]
        [MobileNumberExists]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number should be 10 digits.")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [ValidEmailAddress(ErrorMessage = "Invalid Email address.")]
        [EmailAddressExists]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the birth day.
        /// </summary>
        /// <value>
        /// The birth day.
        /// </value>
        [Required(ErrorMessage = "Date of Birth is required.")]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the joining date.
        /// </summary>
        /// <value>
        /// The joining date.
        /// </value>
        [Display(Name = "Joining Date")]
        [Required(ErrorMessage = "Joining Date is required.")]
        public DateTime? JoiningDate { get; set; }

        /// <summary>
        /// Gets or sets the relieving date.
        /// </summary>
        /// <value>
        /// The relieving date.
        /// </value>
        [Display(Name = "Relieving Date")]
        public Nullable<DateTime> RelievingDate { get; set; }

        /// <summary>
        /// Gets or sets the vb s_ identifier.
        /// </summary>
        /// <value>
        /// The vb s_ identifier.
        /// </value>
        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "Employee Id is required.")]
        [ValidEmployeeId]
        public string VBS_Id { get; set; }

        /// <summary>
        /// Gets or sets the designation identifier.
        /// </summary>
        /// <value>
        /// The designation identifier.
        /// </value>
        [Required(ErrorMessage = "Designation is required.")]
        [Display(Name = "Designation")]
        public int? DesignationId { get; set; }

        /// <summary>
        /// Gets or sets the salutation identifier.
        /// </summary>
        /// <value>
        /// The salutation identifier.
        /// </value>
        [Required(ErrorMessage = "Salutation is required.")]
        [Display(Name = "Salutation")]
        public int? SalutationId { get; set; }

        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>
        /// The gender identifier.
        /// </value>
        [Required(ErrorMessage = "Gender is required.")]
        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        /// <summary>
        /// Gets or sets the office branch identifier.
        /// </summary>
        /// <value>
        /// The office branch identifier.
        /// </value>
        [Required(ErrorMessage = "Office Branch is required.")]
        [Display(Name = "Office Branch")]
        public int? OfficeBranchId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        [Display(Name = "UpdatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Display(Name = "Created Date")]
        public System.DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        /// <value>
        /// The designation.
        /// </value>
        public virtual DesignationModel Designation { get; set; }
        /// <summary>
        /// Gets or sets the office branch.
        /// </summary>
        /// <value>
        /// The office branch.
        /// </value>
        public virtual OfficeBranchModel OfficeBranch { get; set; }
        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        public virtual SalutationModel Salutation { get; set; }

        // Lookup Datas
        /// <summary>
        /// Gets or sets the address types.
        /// </summary>
        /// <value>
        /// The address types.
        /// </value>
        public List<AddressTypeModel> AddressTypes { get; set; }

        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public List<DepartmentModel> Departments { get; set; }

        /// <summary>
        /// Gets or sets the designations.
        /// </summary>
        /// <value>
        /// The designations.
        /// </value>
        public List<DesignationModel> Designations { get; set; }

        /// <summary>
        /// Gets or sets the office branches.
        /// </summary>
        /// <value>
        /// The office branches.
        /// </value>
        public List<OfficeBranchModel> OfficeBranches { get; set; }

        /// <summary>
        /// Gets or sets the salutations.
        /// </summary>
        /// <value>
        /// The salutations.
        /// </value>
        public List<SalutationModel> Salutations { get; set; }

        /// <summary>
        /// Gets or sets the genders.
        /// </summary>
        /// <value>
        /// The genders.
        /// </value>
        public List<SelectListItem> Genders { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        /// <value>
        /// The states.
        /// </value>
        public List<StateModel> States { get; set; }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        /// <value>
        /// The cities.
        /// </value>
        public List<CityModel> Cities { get; set; }

        /// <summary>
        /// Gets or sets the zip codes.
        /// </summary>
        /// <value>
        /// The zip codes.
        /// </value>
        public List<ZipCodeModel> ZipCodes { get; set; }

        /// <summary>
        /// Gets or sets the permanent address.
        /// </summary>
        /// <value>
        /// The permanent address.
        /// </value>
        public AddressModel PermanentAddress { get; set; }

        /// <summary>
        /// Gets or sets the mailing address.
        /// </summary>
        /// <value>
        /// The mailing address.
        /// </value>
        public AddressModel MailingAddress { get; set; }
    }

    public partial class AddressModel
    {
        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>
        /// The address identifier.
        /// </value>
        public int AddressId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        [Required(ErrorMessage = "Address 1 is required.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Address 1 is too short.")]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required(ErrorMessage = "City is required.")]
        [MaxLength(250, ErrorMessage = "City is too long.")]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [Required(ErrorMessage = "Pin Code is required.")]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = "Invalid Pin Code.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pin Code should be 6 digits.")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [Display(Name = "Latitude")]
        public Nullable<decimal> Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [Display(Name = "LongitDude")]
        public Nullable<decimal> Longitude { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is communication address.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is communication address; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "IsCommunicationAddress")]
        public bool IsCommunicationAddress { get; set; }

        /// <summary>
        /// Gets or sets the address type identifier.
        /// </summary>
        /// <value>
        /// The address type identifier.
        /// </value>
        [Display(Name = "Address Type")]
        public int AddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the address.
        /// </summary>
        /// <value>
        /// The type of the address.
        /// </value>
        [Display(Name = "Address Type")]
        public virtual AddressTypeModel AddressType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class AddressTypeModel
    {
        /// <summary>
        /// Gets or sets the address type identifier.
        /// </summary>
        /// <value>
        /// The address type identifier.
        /// </value>
        public int AddressTypeId { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public virtual ICollection<AddressModel> Addresses { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class DepartmentModel
    {
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class EmployeeAddressModel
    {
        /// <summary>
        /// Gets or sets the employee address identifier.
        /// </summary>
        /// <value>
        /// The employee address identifier.
        /// </value>
        public int EmployeeAddressId { get; set; }
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>
        /// The address identifier.
        /// </value>
        public int AddressId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class EPFNumberModel
    {
        /// <summary>
        /// Gets or sets the epf number identifier.
        /// </summary>
        /// <value>
        /// The epf number identifier.
        /// </value>
        public int EPFNumberId { get; set; }
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Gets or sets the epf office identifier.
        /// </summary>
        /// <value>
        /// The epf office identifier.
        /// </value>
        public int EPFOfficeId { get; set; }
        /// <summary>
        /// Gets or sets the establishment code.
        /// </summary>
        /// <value>
        /// The establishment code.
        /// </value>
        public string EstablishmentCode { get; set; }
        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; set; }
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class EPFOffice
    {
        /// <summary>
        /// Gets or sets the epf office identifier.
        /// </summary>
        /// <value>
        /// The epf office identifier.
        /// </value>
        public int EPFOfficeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the epf office.
        /// </summary>
        /// <value>
        /// The name of the epf office.
        /// </value>
        public string EPFOfficeName { get; set; }
        /// <summary>
        /// Gets or sets the epf office code.
        /// </summary>
        /// <value>
        /// The epf office code.
        /// </value>
        public string EPFOfficeCode { get; set; }
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public string StateId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class OfficeBranchModel
    {


        /// <summary>
        /// Gets or sets the office branch identifier.
        /// </summary>
        /// <value>
        /// The office branch identifier.
        /// </value>
        public int OfficeBranchId { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class SalutationModel
    {
        /// <summary>
        /// Gets or sets the salutation identifier.
        /// </summary>
        /// <value>
        /// The salutation identifier.
        /// </value>
        public int SalutationId { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}