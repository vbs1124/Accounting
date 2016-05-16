using System;

namespace Vserv.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class AuthorizationValidationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationValidationException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public AuthorizationValidationException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public AuthorizationValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
