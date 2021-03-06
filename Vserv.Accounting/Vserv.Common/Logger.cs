﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using log4net;
using Vserv.Common.Contracts;
using Vserv.Common.Contracts.Enums;

namespace Vserv.Common
{

    /// <summary>
    /// Custom logging class that abstracts the third party logging framework
    /// </summary>
    [Export(typeof(ILogger))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
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
        /// <param name="level"><see /></param>
        /// <param name="message">Message to log</param>
        public Guid Log(LogLevel level, String message)
        {
            return Log(level, null, message);
        }

        /// <summary>
        /// Logs an exception object
        /// </summary>
        /// <see cref="System.Exception"/>
        public Guid Log(LogLevel level, Exception exception)
        {
            return Log(level, exception, null);
        }

        /// <summary>
        /// Logs an exception with an extra message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"><see cref="System.Exception"/></param>
        /// <param name="message">Message to log</param>
        public Guid Log(LogLevel level, Exception exception, String message)
        {
            GetLoggerInstance();
            Guid logId = Guid.Empty;
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

            var idMessage = logId != Guid.Empty ? string.Format("MessageId:{0} Message: {1}", logId, message) : message;
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
