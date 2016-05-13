using log4net;
using System;
using System.Diagnostics;
using System.Linq;
using Vserv.Common.Contracts;
using Vserv.Common.Contracts.Enums;

namespace Vserv.Common
{
    public sealed class Logger : ILogger
    {
        private static ILog _logger;
        public Type LoggerType { get; set; }

        /// <summary>
        /// Get an instance of the logging framework ex. log4net
        /// </summary>
        private void GetLoggerInstance()
        {
            var caller = LoggerType ??
                (
                    from frame in new StackTrace().GetFrames()
                    let type = frame.GetMethod().ReflectedType
                    where type != typeof(Logger)
                    select type
                ).First();

            _logger = LogManager.GetLogger(caller);
        }

        /// <summary>
        /// Logs a string message
        /// </summary>
        /// <param name="level"><see cref="MCCA.MMS.Core.Enums.LogLevel"/></param>
        /// <param name="message">Message to log</param>
        public Guid Log(LogLevel level, String message)
        {
            return this.Log(level, null, message);
        }

        /// <summary>
        /// Logs an exception object
        /// </summary>
        /// <param name="level"><see cref="MCCA.MMS.Core.Enums.LogLevel"/></param>
        /// <param name="exception"><see cref="System.Exception"/></param>
        public Guid Log(LogLevel level, Exception exception)
        {
            return this.Log(level, exception, null);
        }

        /// <summary>
        /// Logs an exception with an extra message
        /// </summary>
        /// <param name="level"><see cref="MCCA.MMS.Core.Enums.LogLevel"/></param>
        /// <param name="exception"><see cref="System.Exception"/></param>
        /// <param name="message">Message to log</param>
        public Guid Log(LogLevel level, Exception exception, String message)
        {
            this.GetLoggerInstance();
            Guid logId = Guid.Empty;
            string idMessage = string.Empty;
            switch (level)
            {
                case LogLevel.Error:
                    if (_logger.IsErrorEnabled)
                        logId = Guid.NewGuid();
                    break;
                case LogLevel.Fatal:
                    if (_logger.IsFatalEnabled)
                        logId = Guid.NewGuid();
                    break;
                case LogLevel.Warning:
                    if (_logger.IsWarnEnabled)
                        logId = Guid.NewGuid();
                    break;
            }

            if (logId != Guid.Empty)
                idMessage = string.Format("MessageId:{0} Message: {1}", logId.ToString(), message);
            else
                idMessage = message;
            if (idMessage != null)
            {
                if (idMessage.Length > 2000)
                    idMessage = idMessage.Substring(0, 2000);
            }
            switch (level)
            {
                case LogLevel.Debug:
                    if (_logger.IsDebugEnabled)
                        _logger.Debug(idMessage, exception);
                    break;
                case LogLevel.Error:
                    _logger.Error(idMessage, exception);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(idMessage, exception);
                    break;
                case LogLevel.Information:
                    _logger.Info(idMessage, exception);
                    break;
                case LogLevel.Warning:
                    _logger.Warn(idMessage, exception);
                    break;
                default:
                    _logger.Info(idMessage, exception);
                    break;
            }
            return logId;
        }
    }
}
