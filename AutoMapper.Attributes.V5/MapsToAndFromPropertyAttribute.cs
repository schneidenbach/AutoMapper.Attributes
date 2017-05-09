using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target property maps to and from the property of the specified type.
    /// </summary>
    /// <seealso cref="AutoMapper.Attributes.MapsPropertyAttribute" />
    public class MapsToAndFromPropertyAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// The type whose property the target property will be mapped to.
        /// </summary>
        public Type SourceOrTargetType { get; }

        /// <summary>
        /// The name of the property to map to/from.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates the MapsToAndFromProperty attribute.
        /// </summary>
        /// <param name="sourceOrTargetType">The type whose property the target property will be mapped to and from.</param>
        /// <param name="propertyName">The name of the property to map to. Supports dot notation.</param>
        public MapsToAndFromPropertyAttribute(Type sourceOrTargetType, string propertyName)
        {
            SourceOrTargetType = sourceOrTargetType;
            PropertyName = propertyName;
        }

        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType = null)
        {
            var propName = targetProperty.Name;
            //this if is probably wrong
            if (targetProperty.DeclaringType.IsAssignableFrom(sourceType) == true)
            {
                var sourcePropertyInfo = SourceOrTargetType.FindProperties(PropertyName);
                yield return new PropertyMapInfo
                {
                    TargetType = targetProperty.DeclaringType,
                    TargetPropertyInfo = targetProperty,
                    SourceType = SourceOrTargetType,
                    SourcePropertyInfos = sourcePropertyInfo.ToArray()
                };
                yield break;
            }

            var destinationPropertyInfo = SourceOrTargetType.FindProperties(PropertyName);
            yield return new PropertyMapInfo
            {
                TargetType = SourceOrTargetType,
                TargetPropertyInfo = destinationPropertyInfo.First(),
                SourceType = targetProperty.DeclaringType,
                SourcePropertyInfos = new[] { targetProperty }
            };
        }
    }
}