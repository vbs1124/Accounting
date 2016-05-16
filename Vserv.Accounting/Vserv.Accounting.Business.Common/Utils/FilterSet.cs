using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Vserv.Accounting.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class FilterSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterSet" /> class.
        /// </summary>
        public FilterSet()
        {
            Filters = new List<ICollection<Filter>>();
        }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>
        /// The filters.
        /// </value>
        [DataMember]
        public ICollection<ICollection<Filter>> Filters { get; set; }

        /// <summary>
        /// Gets or sets the sort list.
        /// </summary>
        /// <value>
        /// The sort list.
        /// </value>
        [DataMember]
        public string SortList { get; set; }

    }
}
