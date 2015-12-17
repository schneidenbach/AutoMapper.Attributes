using System;
using System.Reflection;

namespace AutoMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MapsPropertyAttribute : Attribute
    {
        internal MapsPropertyAttribute() {}

        internal abstract PropertyMapInfo GetPropertyMapInfo(PropertyInfo targetProperty);
    }
}