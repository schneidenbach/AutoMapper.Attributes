﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target property maps from a property from the specified type.
    /// </summary>
    public class MapsFromPropertyAttribute : MapsPropertyAttribute
    {
        /// <summary>
        /// The type whose property the target property will be mapped from.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// The name of the property to map from.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates the MapsFromProperty attribute.
        /// </summary>
        /// <param name="sourceType">The type whose property the target property will be mapped from.</param>
        /// <param name="propertyName">The name of the property to map from. Supports dot notation.</param>
        public MapsFromPropertyAttribute(Type sourceType, string propertyName)
        {
            SourceType = sourceType;
            PropertyName = propertyName;
        }

        internal override IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType)
        {
            var sourcePropertyInfo = SourceType.FindProperties(PropertyName);
            yield return new PropertyMapInfo
            {
                TargetType = targetProperty.DeclaringType,
                TargetPropertyInfo = targetProperty,
                SourceType = SourceType,
                SourcePropertyInfos = sourcePropertyInfo.ToArray()
            };
        }
    }
}
