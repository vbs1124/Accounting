using System.ComponentModel.DataAnnotations;
using System;

namespace Vserv.Accounting.Web.Code
{
    public struct RegularExpressions
    {
        public static string EmailAddress = @"^[a-zA-Z0-9_\+-]+(\.[a-zA-Z0-9_\+-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.([a-zA-Z]{2,4})$";

        //public static string BirthDay = String.Empty;

        //public static string JoiningDate = String.Empty;

        public static string EmployeeId = "^(vbs):";
    }

    public class ValidEmailAddressAttribute : RegularExpressionAttribute
    {
        public ValidEmailAddressAttribute() : base(RegularExpressions.EmailAddress) { }
    }

    //public class ValidBirthDayAttribute : RegularExpressionAttribute
    //{
    //    public ValidBirthDayAttribute() : base(RegularExpressions.BirthDay) { }
    //}

    //public class ValidJoiningDateAttribute : RegularExpressionAttribute
    //{
    //    public ValidJoiningDateAttribute() : base(RegularExpressions.JoiningDate) { }
    //}
    public class ValidEmployeeIdAttribute : RegularExpressionAttribute
    {
        public ValidEmployeeIdAttribute() : base(RegularExpressions.EmployeeId) { }
    }
}