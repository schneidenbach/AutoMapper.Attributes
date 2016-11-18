using System;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target class maps to the specified type. The attributed class is the source, the type specified is the target.
    /// </summary>
    public class MapsToAttribute : Mapptribute
    {
        /// <summary>
        /// The type that the target class will be mapped to.
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// Creates the MapsTo attribute.
        /// </summary>
        /// <param name="targetType">The type that the target class maps to.</param>
        public MapsToAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}