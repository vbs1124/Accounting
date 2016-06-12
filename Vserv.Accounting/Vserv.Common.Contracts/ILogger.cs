using System;
using Vserv.Common.Contracts.Enums;

namespace Vserv.Common.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Type to be used for the Logger
        /// </summary>
        /// <value>
        /// The type of the logger.
        /// </value>
        Type LoggerType {get;set;}

        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"><see cref="System.String" />Message to be logged</param>
        /// <returns></returns>
        Guid Log(LogLevel level, String message);

        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"><see cref="System.Exception" />Exception to be logged</param>
        /// <returns></returns>
        Guid Log(LogLevel level, Exception exception);

        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"><see cref="System.Exception" />Exception to be logged</param>
        /// <param name="message"><see cref="System.String" />Message to be logged</param>
        /// <returns></returns>
        Guid Log(LogLevel level, Exception exception, String message);
    }
}
