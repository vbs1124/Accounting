#region Namespaces

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

        #endregion Methods
    }
}
