using System;
using System.Reflection;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that this type will not attempt to map a property to a given type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DoNotMapPropertyToAttribute : MapsPropertyAttribute
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
        /// Initializes a new instance of the <see cref="DoNotMapPropertyToAttribute"/> class.
        /// </summary>
        /// <param name="targetType">The target type containing the property to ignore.</param>
        /// <param name="propertyName">The name of the property to ignore on the garget type.</param>
        public DoNotMapPropertyToAttribute(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
        }

        internal override PropertyMapInfo GetPropertyMapInfo(PropertyInfo targetProperty)
        {
            throw new NotImplementedException();
        }
    }
}