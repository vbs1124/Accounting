using System.Collections.Generic;
using System.Runtime.Serialization;
using Vserv.Accounting.Common.Enums;

namespace Vserv.Accounting.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterExpressionNode
    {
        /// <summary>
        /// Gets or sets the logic.
        /// </summary>
        /// <value>
        /// The logic.
        /// </value>
        [DataMember]
        public FilterLogic Logic { get; set; }

        /// <summary>
        /// Gets or sets the filter expression nodes.
        /// </summary>
        /// <value>
        /// The filter expression nodes.
        /// </value>
        [DataMember]
        public List<FilterExpressionNode> FilterExpressionNodes { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>
        /// The filters.
        /// </value>
        [DataMember]
        public List<Filter> Filters { get; set; }
    }
}
