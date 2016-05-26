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
using Vserv.Accounting.Common;

#endregion

namespace Vserv.Accounting.Business.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class ManagerBase
    {
        #region Variables
        /// <summary>
        /// The _data repository factory
        /// </summary>
        [Import]
        protected IDataRepositoryFactory _dataRepositoryFactory;

        /// <summary>
        /// The _logger
        /// </summary>
        [Import]
        protected ILogger _logger;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase"/> class.
        /// </summary>
        public ManagerBase()
        {
            if (DependencyHelper.Container.IsNull())
            {
                DependencyHelper.Container = MEFLoader.Initialize();
            }

            DependencyHelper.Container.SatisfyImportsOnce(this);
            _logger.LoggerType = GetType();
        }

        /// <summary>
        /// Executes the fault handled operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeToExecute">The code to execute.</param>
        /// <returns></returns>
        /// <exception cref="FaultException"></exception>
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

        /// <summary>
        /// Executes the fault handled operation.
        /// </summary>
        /// <param name="codeToExecute">The code to execute.</param>
        /// <exception cref="FaultException"></exception>
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
