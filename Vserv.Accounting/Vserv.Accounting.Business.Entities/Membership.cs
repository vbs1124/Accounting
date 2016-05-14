using System;
using System.Collections.Generic;

namespace Vserv.Accounting.Business.Entities
{
    public partial class Membership
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ConfirmationToken { get; set; }
        public string IsApproved { get; set; }
        public bool IsConfirmed { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public Nullable<System.DateTime> PasswordChangedDate { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordVerificationToken { get; set; }
        public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public string Comment { get; set; }
    }
}
