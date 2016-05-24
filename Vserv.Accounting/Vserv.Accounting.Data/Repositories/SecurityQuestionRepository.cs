#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(ISecurityQuestionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityQuestionRepository : DataRepositoryBase<SecurityQuestion>, ISecurityQuestionRepository
    {
        /// <summary>
        /// Retrieves the list of all the Security Questions.
        /// </summary>
        /// <returns></returns>
        public List<SecurityQuestion> GetSecurityQuestions()
        {
            using (var context = new VservAccountingDBEntities())
            {
                return context.SecurityQuestions.ToList();
            }
        }

        public bool IsRegisteredUser(ForgotPassword forgotPassword)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.ValidateUser(forgotPassword.UserName, forgotPassword.SecurityQuestionId, forgotPassword.SecurityQuestionAnswer, forgotPassword.EmailAddress, forgotPassword.MobileNumber).FirstOrDefault();
                return result.IsNotNull() && result.HasValue && result.Value;
            }
        }

        public UserSecurityQuestion GetRandomSecurityQuestion(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new UserSecurityQuestion();
            }

            using (var context = new VservAccountingDBEntities())
            {
                UserProfile userProfile = context.UserProfiles.FirstOrDefault(user => user.LoweredUserName == userName);

                if (userProfile.IsNotNull())
                {
                    UserSecurityQuestion userSecurityQuestion = context.UserSecurityQuestions.Include("SecurityQuestion").Include("UserProfile")
                  .Where(question => question.UserId == userProfile.UserId)
                  .OrderBy(order => Guid.NewGuid()).FirstOrDefault();
                    return userSecurityQuestion;
                }
            }

            return new UserSecurityQuestion();
        }
    }
}
