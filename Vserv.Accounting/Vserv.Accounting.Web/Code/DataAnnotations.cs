using System.ComponentModel.DataAnnotations;
using System;
using Vserv.Accounting.Business.Managers;

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
}