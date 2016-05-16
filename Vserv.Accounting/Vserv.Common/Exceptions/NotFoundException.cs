using System;

namespace Vserv.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class NotFoundException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public NotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
