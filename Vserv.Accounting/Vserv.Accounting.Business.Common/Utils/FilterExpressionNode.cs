using System.Collections.Generic;
using System.Runtime.Serialization;
using Vserv.Accounting.Common.Enums;

namespace Vserv.Accounting.Business.Common
{
    public class FilterExpressionNode
    {
        [DataMember]
        public FilterLogic Logic { get; set; }

        [DataMember]
        public List<FilterExpressionNode> FilterExpressionNodes { get; set; }

        [DataMember]
        public List<Filter> Filters { get; set; }
    }
}
