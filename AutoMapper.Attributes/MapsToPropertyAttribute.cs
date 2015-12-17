using System;
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
        public Type DestinationType { get; }

        /// <summary>
        /// The name of the property to map to.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates the MapsToProperty attribute.
        /// </summary>
        /// <param name="destinationType">The type whose property the target property will be mapped to.</param>
        /// <param name="propertyName">The name of the property to map to. Supports dot notation.</param>
        public MapsToPropertyAttribute(Type destinationType, string propertyName)
        {
            DestinationType = destinationType;
            PropertyName = propertyName;
        }
        
        internal override PropertyMapInfo GetPropertyMapInfo(PropertyInfo targetProperty)
        {
            return new PropertyMapInfo
            {
                DestinationType = DestinationType,
                DestinationPropertyInfo = DestinationType.FindProperty(PropertyName),
                SourceType = targetProperty.DeclaringType,
                SourcePropertyInfo = targetProperty
            };
        }
    }
}