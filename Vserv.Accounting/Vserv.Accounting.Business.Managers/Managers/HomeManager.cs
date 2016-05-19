#region Namespaces
//using Vserv.Accounting.Data.Contracts;
#endregion
using Vserv.Accounting.Data;

namespace Vserv.Accounting.Business.Managers
{
    public class HomeManager : ManagerBase
    {
        #region Properties

        #endregion

        #region Methods

        public int GetEmployeeCount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return _employeeRepository.GetEmployeeCount();
            });
        }
        #endregion

        #region Constructor

        #endregion
    }
}
