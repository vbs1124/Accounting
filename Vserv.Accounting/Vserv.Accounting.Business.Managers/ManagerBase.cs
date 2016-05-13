using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Vserv.Accounting.Business.Bootstrapper;
using Vserv.Common;
using Vserv.Common.Contracts;
using Vserv.Common.Contracts.Enums;
using Vserv.Common.Exceptions;
using Vserv.Common.Faults;

namespace Vserv.Accounting.Business.Managers
{
    public class ManagerBase : IDisposable
    {
        //[Import]
        //protected IDataRepositoryFactory _dataRepositoryFactory;

        //[Import]
        //protected ILogger _logger;

        public ManagerBase()
        {
            //if (DependencyHelper.Container == null)
            //{
            //    DependencyHelper.Container = MEFLoader.Initialize();
            //}

            //DependencyHelper.Container.SatisfyImportsOnce(this);

            //  _logger.LoggerType = GetType();
            //OperationContext context = OperationContext.Current;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                string method = codeToExecute.Method.Name;
                // string typeName = _logger.LoggerType.Name;
                //  _logger.Log(LogLevel.Information,
                //   string.Format("Start ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                T returnValue = codeToExecute.Invoke();
                //  _logger.Log(LogLevel.Information,
                //    string.Format("End ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                return returnValue;
            }
            catch (FaultException<NotFoundException> exception)
            {
                // _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (FaultException<AuthorizationValidationException> exception)
            {
                //  _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (FaultException<UniqueFault> exception)
            {
                // _logger.Log(LogLevel.Error, exception, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException != null ? exception.InnerException.Message : null;
                string message = exception.Message;
                string stackTrace = exception.StackTrace;
                //  _logger.Log(LogLevel.Error, exception, message);
                throw new FaultException(String.Format("Message: {0} \n\r\n\r Inner Exception: {1} \n\r\n\r Stack trace: {2}", message, innerException, stackTrace));
            }
        }

        protected void ExecuteFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                string method = codeToExecute.Method.Name;
                string typeName = GetType().Name;
                //  _logger.Log(LogLevel.Information,
                //      string.Format("Start ExecuteFaultHandledOperation for {0} {1}", typeName, method));
                codeToExecute.Invoke();
                //  _logger.Log(LogLevel.Information,
                //     string.Format("End ExecuteFaultHandledOperation for {0} {1}", typeName, method));
            }
            catch (FaultException exception)
            {
                //  _logger.Log(LogLevel.Error, exception, exception.Message);
                throw exception;
            }
            catch (Exception exception)
            {
                string message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
                //  _logger.Log(LogLevel.Error, exception, message);
                throw new FaultException(message);
            }
        }

        VservHostFactory _vservHostFactory = null;
        protected void CreateFactoryInstance()
        {
            if (_vservHostFactory == null)
            {
                _vservHostFactory = new VservHostFactory("unity");
            }
        }

        public VservHostFactory VservHostFactory
        {
            get
            {
                if (_vservHostFactory == null)
                {
                    throw new ArgumentException("VservHostFactory is not yet initialized. Need to initialize it before use.");
                }
                return _vservHostFactory;
            }
            set
            {
                _vservHostFactory = value;
            }
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
                if (_vservHostFactory != null)
                {
                    _vservHostFactory.Dispose();
                    _vservHostFactory = null;
                }
            }
        }
    }
}
