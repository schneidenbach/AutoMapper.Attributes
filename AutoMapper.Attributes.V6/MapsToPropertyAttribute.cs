using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target property maps to a property from the specified type.
    /// </summary>
    public class MapsToPropertyAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// The type whose property the target property will be mapped to.
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// The name of the property to map to.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates the MapsToProperty attribute.
        /// </summary>
        /// <param name="targetType">The type whose property the target property will be mapped to.</param>
        /// <param name="propertyName">The name of the property to map to. Supports dot notation.</param>
        public MapsToPropertyAttribute(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
        }
        
        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType = null)
        {
            var targetPropertyInfo = TargetType.FindProperties(PropertyName);
            yield return new PropertyMapInfo
            {
                TargetType = TargetType,
                TargetPropertyInfo = targetPropertyInfo.First(),
                SourceType = targetProperty.DeclaringType,
                SourcePropertyInfos = new [] {targetProperty}
            };
        }
    }
}