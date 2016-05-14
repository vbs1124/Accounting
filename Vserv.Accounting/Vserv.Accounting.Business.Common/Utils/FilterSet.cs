using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Vserv.Accounting.Business.Common
{
    [DataContract]
    public class FilterSet
    {
        public FilterSet()
        {
            Filters = new List<ICollection<Filter>>();
        }

        [DataMember]
        public ICollection<ICollection<Filter>> Filters { get; set; }

        [DataMember]
        public string SortList { get; set; }

    }
}
