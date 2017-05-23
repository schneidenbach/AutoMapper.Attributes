using System;

namespace AutoMapper.Attributes.TestAssembly.MapsToTests
{
    public class MapsToDestinationDataNormalAttribute : MapsToAttribute
    {
        public MapsToDestinationDataNormalAttribute() : base(typeof(DestinationDataNormalAttribute))
        {
        }

        [Obsolete("This was used in a previous version of the library that is no longer supported.")]
        public void ConfigureMapping(IMappingExpression mappingExpression)
        {
            mappingExpression.ForMember(nameof(DestinationDataNormalAttribute.YetAnotherName), expression => expression.MapFrom(nameof(SourceData.Name)));
        }
    }
}