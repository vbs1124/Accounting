#region Namespaces

using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Vserv.Accounting.Business.Bootstrapper;
using Vserv.Common;
using Vserv.Common.Contracts;
using Vserv.Common.Contracts.Enums;
using Vserv.Common.Exceptions;
using Vserv.Common.Faults;
using Vserv.Common.Extensions;

#endregion

namespace Vserv.Accounting.Business.Managers
{
    public class ManagerBase
    {
        #region Variables
        [Import]
        protected IDataRepositoryFactory _dataRepositoryFactory;

        [Import]
        protected ILogger _logger;
        #endregion

        #region Methods
        public ManagerBase()
        {
            if (DependencyHelper.Container.IsNull())
            {
                DependencyHelper.Container = MEFLoader.Initialize();
            }

            DependencyHelper.Container.SatisfyImportsOnce(this);
            _logger.LoggerType = GetType();
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                string method = codeToExecute.Method.Name;
                string typeName = _logger.LoggerType.Name;
                _logger.Log(LogLevel.Information,
                 string.Format("Start ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                T returnValue = codeToExecute.Invoke();
                _logger.Log(LogLevel.Information,
                  string.Format("End ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                return returnValue;
            }
            catch (FaultException<NotFoundException> exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (FaultException<AuthorizationValidationException> exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (FaultException<UniqueFault> exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException != null ? exception.InnerException.Message : null;
                string message = exception.Message;
                string stackTrace = exception.StackTrace;
                _logger.Log(LogLevel.Error, exception, message);
                throw new FaultException(String.Format("Message: {0} \n\r\n\r Inner Exception: {1} \n\r\n\r Stack trace: {2}", message, innerException, stackTrace));
            }
        }

        protected void ExecuteFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                string method = codeToExecute.Method.Name;
                string typeName = GetType().Name;
                _logger.Log(LogLevel.Information,
                    string.Format("Start ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                codeToExecute.Invoke();
                _logger.Log(LogLevel.Information,
                   string.Format("End ExecuteFaultHandledOperation for {0} {1}", typeName, method));
            }
            catch (FaultException exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                throw exception;
            }
            catch (Exception exception)
            {
                string message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
                _logger.Log(LogLevel.Error, exception, message);
                throw new FaultException(message);
            }
        }
        #endregion
    }
}
