#region Namespaces
using System;
using System.Collections.Generic;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
#endregion

namespace Vserv.Accounting.Data
{
    public interface ISecurityQuestionRepository : IDataRepository<SecurityQuestion>
    {
        #region Methods

        /// <summary>
        /// Gets the Security Questions.
        /// </summary>
        /// <returns></returns>
        List<SecurityQuestion> GetSecurityQuestions();

        bool IsRegisteredUser(ForgotPassword forgotPassword);

        UserSecurityQuestion GetRandomSecurityQuestion(string userName);

        #endregion
    }
}
