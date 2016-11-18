using System;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target class maps to the specified type. The target class is the source, the type specified is the destination.
    /// </summary>
    public class MapsToAttribute : Mapptribute
    {
        /// <summary>
        /// The type that the target class will be mapped to.
        /// </summary>
        public Type DestinationType { get; }

        /// <summary>
        /// Creates the MapsTo attribute.
        /// </summary>
        /// <param name="destinationType">The type that the target class maps to.</param>
        public MapsToAttribute(Type destinationType)
        {
            DestinationType = destinationType;
        }
    }
}