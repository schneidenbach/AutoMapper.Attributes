using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes
{
    internal static class ReflectionExtensions
    {
        public static PropertyInfo FindProperty(this Type type, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            PropertyInfo property = null;
            foreach (var propName in name.Split('.'))
            {
                if (property == null)
                {
                    property = type.GetProperty(propName, bindingFlags);
                    ThrowIfPropertyNull(property, name);
                    continue;
                }

                property = property.PropertyType.GetProperty(propName, bindingFlags);
                ThrowIfPropertyNull(property, name);
            }

            return property;
        }

        public static void ThrowIfPropertyNull(PropertyInfo propertyInfo, string name)
        {
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(name), $"Property name {name} is not valid.");
        }
    }
}
