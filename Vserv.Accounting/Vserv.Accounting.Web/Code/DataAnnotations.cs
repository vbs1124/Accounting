using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;

namespace Vserv.Accounting.Web.Code
{
    /// <summary>
    /// 
    /// </summary>
    public struct RegularExpressions
    {
        /// <summary>
        /// The email address
        /// </summary>
        public static string EmailAddress = @"^[a-zA-Z0-9_\+-]+(\.[a-zA-Z0-9_\+-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.([a-zA-Z]{2,4})$";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.RegularExpressionAttribute" />
    public class ValidEmailAddressAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidEmailAddressAttribute"/> class.
        /// </summary>
        public ValidEmailAddressAttribute() : base(RegularExpressions.EmailAddress) { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class ValidEmployeeIdAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string VBS_Id = value.ToString().ToLower();

                if (VBS_Id.StartsWith("vbs"))
                {
                    EmployeeManager employeeManager = new EmployeeManager();
                    Boolean isexists = employeeManager.IsEmployeeIdAlreadyRegistered(VBS_Id, ((EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

                    if (isexists)
                    {
                        return new ValidationResult("Employee Id already registered.");
                    }
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Employee Id should start with VBS.");
                }
            }
            else
            {
                return new ValidationResult(String.Format("{0} is required.", validationContext.DisplayName));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class EmailAddressExistsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string emailAddress = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsEmailAlreadyRegistered(emailAddress, ((EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

                if (isexists)
                {
                    return new ValidationResult("Official Email address already registered.");
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class MobileNumberExistsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string mobileNumber = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsMobileNumberAlreadyRegistered(mobileNumber, ((EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

                if (isexists)
                {
                    return new ValidationResult("Mobile Number already registered.");
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class DesignationExistsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string designationName = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsDesignationExists(designationName, ((DesignationModel)(validationContext.ObjectInstance)).DesignationId);

                if (isexists)
                {
                    return new ValidationResult("Designation already exists.");
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class ValidPasswordComplexityAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                HashSet<char> specialCharacters = new HashSet<char>() { '@', '%', '$', '#' };
                HashSet<char> excludedCharacters = new HashSet<char>() { '(', ')', '[', ']', '.', ';', ':', '\\' };
                string password = value.ToString();
                RegisterModel registerModel = (RegisterModel)(validationContext.ObjectInstance);

                if (password.Length > 8 && !password.Any(char.IsWhiteSpace) && !password.Any(excludedCharacters.Contains) && !password.Contains(registerModel.UserName) && password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(specialCharacters.Contains))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    StringBuilder errorMessage = new StringBuilder();
                    errorMessage.Append("Use at least 8 characters.");
                    errorMessage.Append("Don't put a password similar to your User name.");
                    errorMessage.Append("It should mandatorily contain digits, special characters(@, #, $ or %) and letters(lower and caps both).");
                    errorMessage.Append("No spaces in-between password.");
                    errorMessage.Append(@"The password cannot contain following characters ( ) [ ] . ; : \ ");
                    errorMessage.Append("");
                    return new ValidationResult(errorMessage.ToString());
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class ValidChangePasswordComplexityAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                HashSet<char> specialCharacters = new HashSet<char>() { '@', '%', '$', '#' };
                HashSet<char> excludedCharacters = new HashSet<char>() { '(', ')', '[', ']', '.', ';', ':', '\\' };
                string password = value.ToString();
                UserProfileModel registerModel = (UserProfileModel)(validationContext.ObjectInstance);
                registerModel.UserName = String.IsNullOrWhiteSpace(registerModel.UserName) ? " " : registerModel.UserName;
                if (password.Length > 8 && !password.Any(char.IsWhiteSpace) && !password.Any(excludedCharacters.Contains) && !password.Contains(Convert.ToString(registerModel.UserName)) && password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(specialCharacters.Contains))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    StringBuilder errorMessage = new StringBuilder();
                    errorMessage.Append("Use at least 8 characters.");
                    errorMessage.Append("Don't put a password similar to your User name.");
                    errorMessage.Append("It should mandatorily contain digits, special characters(@, #, $ or %) and letters(lower and caps both).");
                    errorMessage.Append("No spaces in-between password.");
                    errorMessage.Append(@"The password cannot contain following characters ( ) [ ] . ; : \ ");
                    errorMessage.Append("");
                    return new ValidationResult(errorMessage.ToString());
                }
            }

            return ValidationResult.Success;
        }
    }

    public class IsRegisteredUserAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("User Name is Required.");
            }
            else
            {
                string userName = value.ToString();
                AccountManager _manager = new AccountManager();
                UserProfile userProfile = _manager.GetUserProfile(userName);

                if (userProfile == null)
                {
                    return new ValidationResult("No account found with that User Name.");
                }
            }

            return ValidationResult.Success;
        }
    }
}