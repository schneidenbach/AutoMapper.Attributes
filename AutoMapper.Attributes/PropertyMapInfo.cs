using System;
using System.Reflection;

namespace AutoMapper.Attributes
{
    internal class PropertyMapInfo
    {
        public PropertyInfo SourcePropertyInfo { get; set; }
        public Type SourceType { get; set; }
        public PropertyInfo DestinationPropertyInfo { get; set; }
        public Type DestinationType { get; set; }
    }
}