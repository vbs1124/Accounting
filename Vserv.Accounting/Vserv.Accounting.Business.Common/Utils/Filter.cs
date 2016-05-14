using System;
using System.Runtime.Serialization;
using Vserv.Accounting.Common.Enums;

namespace Vserv.Accounting.Business.Common
{
    [DataContract]
    public class Filter
    {
        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public FilterOperator Operator { get; set; }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string CollectionType { get; set; }

        [DataMember]
        public string CollectionPropertyName { get; set; }

        [DataMember]
        public bool IsCollectionQuery { get; set; }

        public Type EntityType
        {
            // Bug 19310: We have business level objects NAMED client.  Original logic didn't allow them to be used.
            get
            {
                if (!string.IsNullOrEmpty(Type))
                    if (Type.Contains(".Business."))
                        return System.Type.GetType(Type);
                    else
                        return System.Type.GetType(Type.Replace(".Client", ".Business"));
                return null;
            }
        }

        public Type CollectionEntityType
        {
            get
            {
                if (!string.IsNullOrEmpty(CollectionType))
                    if (CollectionType.Contains(".Business."))
                        return System.Type.GetType(CollectionType);
                    else
                        return System.Type.GetType(CollectionType.Replace(".Client", ".Business"));
                return null;
            }
        }
        
    }
}
