using System;
using System.Reflection;

namespace AutoMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MapsPropertyAttribute : Attribute
    {
        internal MapsPropertyAttribute() {}

        internal abstract PropertyMapInfo GetPropertyMapInfo(PropertyInfo targetProperty);
    }
}