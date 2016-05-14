using System.ComponentModel.DataAnnotations;
using System;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Web.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Vserv.Accounting.Web.Code
{
    public struct RegularExpressions
    {
        public static string EmailAddress = @"^[a-zA-Z0-9_\+-]+(\.[a-zA-Z0-9_\+-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.([a-zA-Z]{2,4})$";
    }

    public class ValidEmailAddressAttribute : RegularExpressionAttribute
    {
        public ValidEmailAddressAttribute() : base(RegularExpressions.EmailAddress) { }
    }

    public class ValidEmployeeIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string VBS_Id = value.ToString().ToLower();

                if (VBS_Id.StartsWith("vbs"))
                {
                    EmployeeManager employeeManager = new EmployeeManager();
                    Boolean isexists = employeeManager.IsEmployeeIdAlreadyRegistered(VBS_Id, ((Vserv.Accounting.Web.Models.EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

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

    public class EmailAddressExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string emailAddress = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsEmailAlreadyRegistered(emailAddress, ((Vserv.Accounting.Web.Models.EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

                if (isexists)
                {
                    return new ValidationResult("Employee Id already registered.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class MobileNumberExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string mobileNumber = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsMobileNumberAlreadyRegistered(mobileNumber, ((Vserv.Accounting.Web.Models.EmployeeModel)(validationContext.ObjectInstance)).EmployeeId);

                if (isexists)
                {
                    return new ValidationResult("Mobile Number already registered.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class DesignationExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string designationName = value.ToString();
                EmployeeManager employeeManager = new EmployeeManager();
                Boolean isexists = employeeManager.IsDesignationExists(designationName, ((Vserv.Accounting.Web.Models.DesignationModel)(validationContext.ObjectInstance)).DesignationId);

                if (isexists)
                {
                    return new ValidationResult("Designation already exists.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class ValidPasswordComplexityAttribute : ValidationAttribute
    {
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
}