#region Namespaces
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.ServiceModel.Activation;
using Vserv.Accounting.Data;
//using Vserv.Accounting.Data.Contracts; 
#endregion

namespace Vserv.Accounting.Business.Managers
{
    public class VservHostFactory : ServiceHostFactory, IDisposable
    {
        #region Variables
        private IUnityContainer _serviceContainer = null;
        private IUserProfileRepository _userProfileRepository = null;
        private IEmployeeRepository _employeeRepository = null; 
        #endregion

        #region Methods
        public IUserProfileRepository GetUserProfileRepositoryInstance()
        {
            if (_userProfileRepository == null)
            {
                _userProfileRepository = _serviceContainer.Resolve<IUserProfileRepository>("IUserProfileRepository");
            }

            return _userProfileRepository;
        }

        public IEmployeeRepository GetEmployeeRepositoryInstance()
        {
            if (_employeeRepository == null)
            {
                _employeeRepository = _serviceContainer.Resolve<IEmployeeRepository>("IEmployeeRepository");
            }

            return _employeeRepository;
        }

        public VservHostFactory(string configSectionName)
        {
            if (_serviceContainer != null)
            {
                return;
            }
            _serviceContainer = new UnityContainer();

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection(configSectionName);
            section.Configure(_serviceContainer);
            _serviceContainer.LoadConfiguration();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposed)
        {
            if (isDisposed)
            {
                // TODO: Manually dispose any unused objects.
            }
        }
        #endregion
    }
}
