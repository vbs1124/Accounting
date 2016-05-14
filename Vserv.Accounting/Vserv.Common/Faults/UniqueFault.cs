using System.Runtime.Serialization;

namespace Vserv.Common.Faults
{
    [DataContract]
    public class UniqueFault
    {
        [DataMember]
        public string Message { get; set; }

        public UniqueFault(string message)
        {
            Message = message;
        }
    }
}
