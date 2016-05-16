using System.Runtime.Serialization;

namespace Vserv.Common.Faults
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UniqueFault
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueFault"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UniqueFault(string message)
        {
            Message = message;
        }
    }
}
