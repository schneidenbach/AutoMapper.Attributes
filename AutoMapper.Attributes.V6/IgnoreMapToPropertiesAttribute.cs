using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that this type will not attempt to map one or more properties to a given type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IgnoreMapToPropertiesAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// Gets the target type.
        /// </summary>
        /// <value>
        /// The target type.
        /// </value>
        public Type TargetType { get; }

        /// <summary>
        /// Gets the name of the property to ignore.
        /// </summary>
        /// <value>
        /// The name of the property to ignore.
        /// </value>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the additional properties to ignore.
        /// </summary>
        /// <value>
        /// The additional properties to ignore.
        /// </value>
        public string[] AdditionalProperties { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreMapToPropertiesAttribute"/> class.
        /// </summary>
        /// <param name="targetType">The target type containing the property to ignore.</param>
        /// <param name="propertyName">The name of the property to ignore on the target type.</param>
        /// <param name="additionalProperties">The names of additional properties to ignore on the target type.</param>
        public IgnoreMapToPropertiesAttribute(Type targetType, string propertyName, params string[] additionalProperties)
        {
            TargetType = targetType;
            PropertyName = propertyName;
            AdditionalProperties = additionalProperties;
        }

        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType)
        {
            var destinationPropertyInfo = TargetType.FindProperties(PropertyName);
            yield return new PropertyMapInfo
            {
                IgnoreMapping = true,
                TargetType = TargetType,
                SourceType = targetProperty.DeclaringType,
                TargetPropertyInfo = destinationPropertyInfo.First()
            };

            if (AdditionalProperties != null)
            {
                foreach (var prop in AdditionalProperties)
                {
                    var targetPropertyInfo = TargetType.FindProperties(prop);
                    yield return new PropertyMapInfo
                    {
                        IgnoreMapping = true,
                        TargetType = TargetType,
                        SourceType = targetProperty.DeclaringType,
                        TargetPropertyInfo = targetPropertyInfo.First()
                    };
                }
            }
        }
    }
}