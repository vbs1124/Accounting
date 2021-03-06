﻿
namespace Vserv.Accounting.Common
{
    public class CommonConstants
    {
        #region log4Net

        /// <summary>
        /// The log4 net configuration key name
        /// </summary>
        public const string LOG4_NET_CONFIG_KEY_NAME = "log4NetConfig";

        /// <summary>
        /// Value is numeric 1.
        /// </summary>
        public const int INT_ONE = 1;

        /// <summary>
        ///  Value is numeric 2.
        /// </summary>
        public const int INT_TWO = 2;

        /// <summary>
        ///  Value is numeric 3.
        /// </summary>
        public const int INT_THREE = 3;

        /// <summary>
        /// UserSecurityQuestion1
        /// </summary>
        public const string USER_SECURITY_QUESTION_1 = "UserSecurityQuestion1";

        /// <summary>
        /// UserSecurityQuestion2
        /// </summary>
        public const string USER_SECURITY_QUESTION_2 = "UserSecurityQuestion2";

        /// <summary>
        /// UserSecurityQuestion3
        /// </summary>
        public const string USER_SECURITY_QUESTION_3 = "UserSecurityQuestion3";

        public const string MESSAGE = "MESSAGE";
        #endregion

        #region Messages

        public const string INVALID_SPECIAL_ALLOWANCE = "Calculated Special Allowance is coming negative, so please very the field values.";

        public const string SALARY_BREAKUP_SUCCESS_MESSAGE = "Salary breakup generated successfully.";

        #endregion
    }
}
