using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that this property will not be mapped from the given type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreMapFromAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// Gets the source type.
        /// </summary>
        /// <value>
        /// The source type.
        /// </value>
        public Type SourceType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreMapFromAttribute"/> class.
        /// </summary>
        /// <param name="sourceType">The source type containing the property the mapper will ignore.</param>
        public IgnoreMapFromAttribute(Type sourceType)
        {
            SourceType = sourceType;
        }

        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType = null)
        {
            yield return new PropertyMapInfo
            {
                TargetType = targetProperty.DeclaringType,
                SourceType = SourceType,
                TargetPropertyInfo = targetProperty,
                IgnoreMapping = true,
                SourcePropertyInfos = Enumerable.Empty<PropertyInfo>().ToArray()
            };
        }
    }
}
