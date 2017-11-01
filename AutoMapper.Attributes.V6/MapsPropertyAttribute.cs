using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Base class for mapping properties to other properties.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MapsPropertyAttribute : Attribute
    {
        internal MapsPropertyAttribute() {}

        internal abstract IEnumerable<PropertyMapInfo> GetPropertyMapInfo(PropertyInfo targetProperty, Type sourceType = null);
    }
}