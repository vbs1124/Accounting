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
        public IUserProfileRepository UserProfileRepository { get; set; }
        #endregion

        #region Constructor
        public AccountManager()
        {
            
        }
        #endregion

        #region UserProfile

        public UserProfile GetUserProfile(string userName)
        {
            CreateFactoryInstance();
            using (UserProfileRepository = VservHostFactory.GetUserProfileRepositoryInstance())
            {
                return UserProfileRepository.GetUserProfile(userName);
            }

            //return ExecuteFaultHandledOperation(() =>
            //{
            //    var _userProfileRepository = new UserProfileRepository();// _dataRepositoryFactory.GetDataRepository<IUserProfileRepository>();
            //    var result = _userProfileRepository.GetUserProfile(userName);
            //    return result;
            //});
        }

        #endregion
    }
}
