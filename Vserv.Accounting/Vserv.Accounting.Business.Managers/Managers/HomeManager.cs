#region Namespaces
//using Vserv.Accounting.Data.Contracts;
#endregion
using System.Collections.Generic;
using Vserv.Accounting.Data;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Business.Managers
{
    public class HomeManager : ManagerBase
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeCount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var employeeRepository = DataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                return employeeRepository.GetEmployeeCount();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excludeInactiveFeatures"></param>
        /// <returns></returns>
        public List<Feature> GetFeatures(bool excludeInactiveFeatures)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IFeatureRepository featureRepository = DataRepositoryFactory.GetDataRepository<IFeatureRepository>();
                return featureRepository.GetFeatures(excludeInactiveFeatures);
            });

        }

        #endregion

        #region Constructor

        #endregion
    }
}
