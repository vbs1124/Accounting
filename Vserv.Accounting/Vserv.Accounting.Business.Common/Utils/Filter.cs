using System;
using System.Runtime.Serialization;
using Vserv.Accounting.Common;

namespace Vserv.Accounting.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Filter
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        [DataMember]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        [DataMember]
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember]
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the collection.
        /// </summary>
        /// <value>
        /// The type of the collection.
        /// </value>
        [DataMember]
        public string CollectionType { get; set; }

        /// <summary>
        /// Gets or sets the name of the collection property.
        /// </summary>
        /// <value>
        /// The name of the collection property.
        /// </value>
        [DataMember]
        public string CollectionPropertyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is collection query.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is collection query; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsCollectionQuery { get; set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
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

        /// <summary>
        /// Gets the type of the collection entity.
        /// </summary>
        /// <value>
        /// The type of the collection entity.
        /// </value>
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
