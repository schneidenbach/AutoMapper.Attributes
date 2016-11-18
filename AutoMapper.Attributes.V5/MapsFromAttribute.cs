using System;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// Specifies that the target class maps from the specified type. The attributed class is the target, the type specified is the source.
    /// </summary>
    public class MapsFromAttribute : Mapptribute
    {
        /// <summary>
        /// The type that the target class will be mapped to.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Creates the MapsTo attribute.
        /// </summary>
        /// <param name="sourceType">The type that the target class maps to.</param>
        public MapsFromAttribute(Type sourceType)
        {
            SourceType = sourceType;
        }
    }
}