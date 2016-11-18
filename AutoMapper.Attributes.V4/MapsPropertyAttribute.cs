using System;
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

        internal abstract PropertyMapInfo GetPropertyMapInfo(PropertyInfo targetProperty);
    }
}