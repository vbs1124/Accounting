#region Namespaces
//using Vserv.Accounting.Data.Contracts;
#endregion

using Vserv.Accounting.Data;
namespace Vserv.Accounting.Business.Managers.Managers
{
    public class HomeManager : ManagerBase
    {
        #region Properties
        public IHomeRepository HomeRepository
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public HomeManager()
        {
            CreateFactoryInstance();
        }
        #endregion
    }
}
