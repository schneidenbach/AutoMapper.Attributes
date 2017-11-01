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
        public static IEnumerable<PropertyInfo> FindProperties(this Type type, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            PropertyInfo property = null;
            foreach (var propName in name.Split('.'))
            {
                if (property == null)
                {
                    property = type.GetProperty(propName, bindingFlags);
                    ThrowIfPropertyNull(property, name);
                    yield return property;
                    continue;
                }

                property = property.PropertyType.GetProperty(propName, bindingFlags);
                ThrowIfPropertyNull(property, name);
                yield return property;
            }
        }

        public static void ThrowIfPropertyNull(PropertyInfo propertyInfo, string name)
        {
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(name), $"Property name {name} is not valid.");
        }

#if NETSTANDARD1_3
        public static bool IsAssignableFrom(this Type type, Type possibleSubType)
        {
            return type.GetTypeInfo().IsSubclassOf(possibleSubType);
        }

        public static IEnumerable<Attribute> GetCustomAttributes(this Type type, Type attributeType, bool inherit)
        {
            return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>();
        }
#endif
    }
}
