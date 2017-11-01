using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that this property will not be mapped to any properties of objects of the given type.
    /// </summary>
    [Obsolete("CANNOT BE USED - see https://github.com/AutoMapper/AutoMapper/issues/1556", true)]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreMapToAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// Gets the source type.
        /// </summary>
        /// <value>
        /// The source type.
        /// </value>
        public Type TargetType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreMapFromAttribute"/> class.
        /// </summary>
        /// <param name="targetType">The source type containing the property the mapper will ignore.</param>
        public IgnoreMapToAttribute(Type targetType)
        {
            TargetType = targetType;
        }

        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType = null)
        {
            yield return new PropertyMapInfo
            {
                TargetType = TargetType,
                SourceType = targetProperty.DeclaringType,
                TargetPropertyInfo = null,
                IgnoreMapping = true,
                UseSourceMember = true,
                SourcePropertyInfos = new [] {targetProperty}
            };
        }
    }
}