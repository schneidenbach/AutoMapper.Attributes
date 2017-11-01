using System;

namespace AutoMapper.Attributes.TestAssembly.MapsFromTests
{
    public class MapsFromSourceDataNormalAttribute : MapsFromAttribute
    {
        public MapsFromSourceDataNormalAttribute() : base(typeof(SourceDataNormalAttribute))
        {
        }

        [Obsolete("This was used in a previous version of the library that is no longer supported.")]
        public void ConfigureMapping(IMappingExpression mappingExpression)
        {
            mappingExpression.ForMember(nameof(DestinationData.Name), expression => expression.MapFrom(nameof(SourceDataNormalAttribute.YetAnotherName)));
        }
    }
}