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
                var _userProfileRepository = _dataRepositoryFactory.GetDataRepository<IUserProfileRepository>();
                return _userProfileRepository.GetUserProfile(userName);
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
                ISecurityQuestionRepository _securityQuestionRepository = _dataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return _securityQuestionRepository.GetSecurityQuestions();
            });
        }

        public bool IsRegisteredUser(ForgotPassword forgotPassword)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISecurityQuestionRepository _securityQuestionRepository = _dataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return _securityQuestionRepository.IsRegisteredUser(forgotPassword);
            });
        }

        public UserSecurityQuestion GetRandomSecurityQuestion(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISecurityQuestionRepository _securityQuestionRepository = _dataRepositoryFactory.GetDataRepository<ISecurityQuestionRepository>();
                return _securityQuestionRepository.GetRandomSecurityQuestion(userName);
            });
        }

        #endregion Methods
    }
}
