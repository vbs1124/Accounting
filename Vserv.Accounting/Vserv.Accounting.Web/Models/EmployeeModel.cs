﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [ScaffoldColumn(true)]
        [Display(Name = @"Employee Id")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = @"First Name is required.")]
        [Display(Name = @"First Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = @"First Name is too short.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [Display(Name = @"Middle Name")]
        [MaxLength(250, ErrorMessage = @"Middle Name is too short.")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = @"Last Name")]
        [StringLength(250, ErrorMessage = @"Last Name is too short.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the father.
        /// </summary>
        /// <value>
        /// The name of the father.
        /// </value>
        [Required(ErrorMessage = @"Father's Name is required.")]
        [Display(Name = @"Father's Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = @"Father's Name is too short.")]
        public string FatherName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mother.
        /// </summary>
        /// <value>
        /// The name of the mother.
        /// </value>
        [Display(Name = @"Mother's Name")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = @"Mother's Name is too short.")]
        public string MotherName { get; set; }

        /// <summary>
        /// Gets or sets the universal account number.
        /// </summary>
        /// <value>
        /// The universal account number.
        /// </value>
        [Required(ErrorMessage = @"UAN Number is required.")]
        [Display(Name = @"UAN Number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = @"UAN Number should be 12 digits.")]
        public string UniversalAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the permanent account number.
        /// </summary>
        /// <value>
        /// The permanent account number.
        /// </value>
        [Required(ErrorMessage = @"PAN Number is required.")]
        [Display(Name = @"PAN Number")]
        [ValidPermanentAccountNumber(ErrorMessage = @"Invalid PAN Number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = @"PAN Number should be 10 characters.")]
        public string PermanentAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the aadhaar number.
        /// </summary>
        /// <value>
        /// The aadhaar number.
        /// </value>
        [Display(Name = @"AADHAAR Number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = @"AADHAAR Number should be 12 digits.")]
        public string AADHAARNumber { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [Required(ErrorMessage = @"Mobile Number is required.")]
        [Display(Name = @"Mobile Number")]
        [MobileNumberExists]
        [DataType(DataType.PhoneNumber, ErrorMessage = @"Invalid phone number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = @"Mobile Number should be 10 digits.")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the birth day.
        /// </summary>
        /// <value>
        /// The birth day.
        /// </value>
        [Required(ErrorMessage = @"Date of Birth is required.")]
        [Display(Name = @"Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the joining date.
        /// </summary>
        /// <value>
        /// The joining date.
        /// </value>
        [Display(Name = @"Joining Date")]
        [Required(ErrorMessage = @"Joining Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? JoiningDate { get; set; }

        /// <summary>
        /// Gets or sets the relieving date.
        /// </summary>
        /// <value>
        /// The relieving date.
        /// </value>
        [Display(Name = @"Relieving Date")]
        public DateTime? RelievingDate { get; set; }

        /// <summary>
        /// Gets or sets the vb s_ identifier.
        /// </summary>
        /// <value>
        /// The vb s_ identifier.
        /// </value>
        [Display(Name = @"Employee Id")]
        [Required(ErrorMessage = @"Employee Id is required.")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = @"Employee Id should be numeric and should not start with 0.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = @"Employee Id should be 4 digits.")]
        [ValidEmployeeId]
        public string VBS_Id { get; set; }

        /// <summary>
        /// Gets or sets the designation identifier.
        /// </summary>
        /// <value>
        /// The designation identifier.
        /// </value>
        [Required(ErrorMessage = @"Designation is required.")]
        [Display(Name = @"Designation")]
        public int? DesignationId { get; set; }

        /// <summary>
        /// Gets or sets the salutation identifier.
        /// </summary>
        /// <value>
        /// The salutation identifier.
        /// </value>
        [Required(ErrorMessage = @"Salutation is required.")]
        [Display(Name = @"Salutation")]
        public int? SalutationId { get; set; }

        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>
        /// The gender identifier.
        /// </value>
        [Required(ErrorMessage = @"Gender is required.")]
        [Display(Name = @"Gender")]
        public int? GenderId { get; set; }

        /// <summary>
        /// Gets or sets the office branch identifier.
        /// </summary>
        /// <value>
        /// The office branch identifier.
        /// </value>
        [Required(ErrorMessage = @"Office Branch is required.")]
        [Display(Name = @"Office Location")]
        public int? OfficeBranchId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        [Display(Name = @"Department")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = @"Active Employee?")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Display(Name = @"CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        [Display(Name = @"UpdatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Display(Name = @"Created Date")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [Display(Name = @"Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the epf number.
        /// </summary>
        /// <value>
        /// The epf number.
        /// </value>
        [Display(Name = @"EPF Number")]
        public string EPFNumber { get; set; }

        /// <summary>
        /// Gets or sets the esi number.
        /// </summary>
        /// <value>
        /// The esi number.
        /// </value>
        [Display(Name = @"ESI Number")]
        public string ESINumber { get; set; }

        /// <summary>
        /// Gets or sets the official email address.
        /// </summary>
        /// <value>
        /// The official email address.
        /// </value>
        [Required(ErrorMessage = @"Official Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = @"Official Email address")]
        [ValidEmailAddress(ErrorMessage = @"Invalid Official Email address.")]
        [EmailAddressExists]
        public string OfficialEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the personal email address.
        /// </summary>
        /// <value>
        /// The personal email address.
        /// </value>
        [Required(ErrorMessage = @"Personal Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = @"Personal Email address")]
        [ValidEmailAddress(ErrorMessage = @"Invalid Personal Email address.")]
        public string PersonalEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the resignation date.
        /// </summary>
        /// <value>
        /// The resignation date.
        /// </value>
        [Display(Name = @"Resignation Date")]
        public DateTime? ResignationDate { get; set; }

        /// <summary>
        /// Gets or sets the permanent address1.
        /// </summary>
        /// <value>
        /// The permanent address1.
        /// </value>
        [Required(ErrorMessage = @"Address 1 is required.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = @"Address 1 is too short.")]
        [Display(Name = @"Address 1")]
        public string PermanentAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the permanent address2.
        /// </summary>
        /// <value>
        /// The permanent address2.
        /// </value>
        [Display(Name = @"Address 2")]
        public string PermanentAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the permanent city.
        /// </summary>
        /// <value>
        /// The permanent city.
        /// </value>
        [Required(ErrorMessage = @"City is required.")]
        [MaxLength(250, ErrorMessage = @"City is too long.")]
        [Display(Name = @"City")]
        public string PermanentCity { get; set; }

        /// <summary>
        /// Gets or sets the permanent zip code.
        /// </summary>
        /// <value>
        /// The permanent zip code.
        /// </value>
        [Required(ErrorMessage = @"Pin Code is required.")]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = @"Invalid Pin Code.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = @"Pin Code should be 6 digits.")]
        [Display(Name = @"Pin Code")]
        public string PermanentZipCode { get; set; }

        /// <summary>
        /// Gets or sets the permanent state identifier.
        /// </summary>
        /// <value>
        /// The permanent state identifier.
        /// </value>
        [Required(ErrorMessage = @"State is required.")]
        [Display(Name = @"State")]
        public int? PermanentStateId { get; set; }

        /// <summary>
        /// Gets or sets the permanent country identifier.
        /// </summary>
        /// <value>
        /// The permanent country identifier.
        /// </value>
        [Display(Name = @"Country")]
        public int? PermanentCountryId { get; set; }

        /// <summary>
        /// Gets or sets the mailing address1.
        /// </summary>
        /// <value>
        /// The mailing address1.
        /// </value>
        [Required(ErrorMessage = @"Address 1 is required.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = @"Address 1 is too short.")]
        [Display(Name = @"Address 1")]
        public string MailingAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the mailing address2.
        /// </summary>
        /// <value>
        /// The mailing address2.
        /// </value>
        [Display(Name = @"Address 2")]
        public string MailingAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the mailing city.
        /// </summary>
        /// <value>
        /// The mailing city.
        /// </value>
        [Required(ErrorMessage = @"City is required.")]
        [MaxLength(250, ErrorMessage = @"City is too long.")]
        [Display(Name = @"City")]
        public string MailingCity { get; set; }

        /// <summary>
        /// Gets or sets the mailing zip code.
        /// </summary>
        /// <value>
        /// The mailing zip code.
        /// </value>
        [Required(ErrorMessage = @"Pin Code is required.")]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = @"Invalid Pin Code.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = @"Pin Code should be 6 digits.")]
        [Display(Name = @"Pin Code")]
        public string MailingZipCode { get; set; }

        /// <summary>
        /// Gets or sets the mailing state identifier.
        /// </summary>
        /// <value>
        /// The mailing state identifier.
        /// </value>
        [Required(ErrorMessage = @"State is required.")]
        [Display(Name = @"State")]
        public int? MailingStateId { get; set; }

        /// <summary>
        /// Gets or sets the mailing country identifier.
        /// </summary>
        /// <value>
        /// The mailing country identifier.
        /// </value>
        [Display(Name = @"Country")]
        public int? MailingCountryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is metro.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is metro; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = @"Metro/Non Metro")]
        public bool IsMetro { get; set; }

        /// <summary>
        /// Gets or sets the bank account number.
        /// </summary>
        /// <value>
        /// The bank account number.
        /// </value>
        [Display(Name = @"Bank Account Number")]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        [Display(Name = @"Bank Name")]
        public int? BankId { get; set; }

        /// <summary>
        /// Gets or sets the bank ifsc code.
        /// </summary>
        /// <value>
        /// The bank ifsc code.
        /// </value>
        [Display(Name = @"IFSC Code")]
        public string BankIFSCCode { get; set; }

        /// <summary>
        /// Gets or sets the bank micr code.
        /// </summary>
        /// <value>
        /// The bank micr code.
        /// </value>
        [Display(Name = @"MICR Code")]
        public string BankMICRCode { get; set; }

        /// <summary>
        /// Gets or sets the bank.
        /// </summary>
        /// <value>
        /// The bank.
        /// </value>
        [Display(Name = @"Bank")]
        public virtual BankModel Bank { get; set; }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        /// <value>
        /// The designation.
        /// </value>
        [Display(Name = @"Designation")]
        public virtual DesignationModel Designation { get; set; }

        /// <summary>
        /// Gets or sets the office branch.
        /// </summary>
        /// <value>
        /// The office branch.
        /// </value>
        [Display(Name = @"OfficeBranch")]
        public virtual OfficeBranchModel OfficeBranch { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        [Display(Name = @"Salutation")]
        public virtual SalutationModel Salutation { get; set; }
    }

    public class BankModel
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }

    public class AddressModel
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
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [Display(Name = "LongitDude")]
        public decimal? Longitude { get; set; }

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
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }

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
    public class AddressTypeModel
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }

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
    public class DepartmentModel
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EPFNumberModel
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EPFOffice
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OfficeBranchModel
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }

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
    public class SalutationModel
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
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}