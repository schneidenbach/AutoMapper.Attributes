using System;
using System.Reflection;

namespace AutoMapper.Attributes
{
    internal class PropertyMapInfo
    {
        public PropertyInfo[] SourcePropertyInfos { get; set; }
        public Type SourceType { get; set; }
        public PropertyInfo TargetPropertyInfo { get; set; }
        public Type TargetType { get; set; }
        public bool IgnoreMapping { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the property map info targets a member from the source type.
        /// </summary>
        public bool UseSourceMember { get; set; }
    }
}