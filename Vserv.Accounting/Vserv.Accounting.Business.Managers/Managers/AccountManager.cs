#region Namespaces

using System.Collections.Generic;
using Vserv.Accounting.Data;
//using Vserv.Accounting.Data.Contracts;
using Vserv.Accounting.Data.Entity;

#endregion

namespace Vserv.Accounting.Business.Managers
{
    public class AccountManager : ManagerBase
    {
        #region Properties
        #endregion

        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public UserProfile GetUserProfile(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userProfileRepository = DataRepositoryFactory.GetDataRepository<IUserProfileRepository>();
                return userProfileRepository.GetUserProfile(userName);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SecurityQuestion> GetSecurityQuestions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISecurityQuestionRepository securityQuestionRepository = DataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return securityQuestionRepository.GetSecurityQuestions();
            });
        }

        public bool IsRegisteredUser(ForgotPassword forgotPassword)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISecurityQuestionRepository securityQuestionRepository = DataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return securityQuestionRepository.IsRegisteredUser(forgotPassword);
            });
        }

        public UserSecurityQuestion GetRandomSecurityQuestion(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISecurityQuestionRepository securityQuestionRepository = DataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return securityQuestionRepository.GetRandomSecurityQuestion(userName);
            });
        }

        #endregion Methods
    }
}
