using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vserv.Common.Contracts.Enums;

namespace Vserv.Common.Contracts
{
    public interface ILogger
    {
        /// <summary>
        /// Type to be used for the Logger
        /// </summary>
        Type LoggerType {get;set;}
        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"><see cref="Servpro.iHub.Common.LogLevel"/>Logging level</param>
        /// <param name="message"><see cref="System.String"/>Message to be logged</param>
        Guid Log(LogLevel level, String message);
        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"><see cref="Servpro.iHub.Common.LogLevel"/>Logging level</param>
        /// <param name="exception"><see cref="System.Exception"/>Exception to be logged</param>
        Guid Log(LogLevel level, Exception exception);
        /// <summary>
        /// Logs a message to the logging implementation
        /// </summary>
        /// <param name="level"><see cref="Servpro.iHub.Common.LogLevel"/>Logging level</param>
        /// <param name="exception"><see cref="System.Exception"/>Exception to be logged</param>
        /// <param name="message"><see cref="System.String"/>Message to be logged</param>
        Guid Log(LogLevel level, Exception exception, String message);
    }
}
