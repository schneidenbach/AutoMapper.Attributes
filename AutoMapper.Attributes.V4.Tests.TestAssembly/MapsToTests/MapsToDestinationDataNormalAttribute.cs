namespace AutoMapper.Attributes.Tests.TestAssembly.MapsToTests
{
    public class MapsToDestinationDataNormalAttribute : MapsToAttribute
    {
        public MapsToDestinationDataNormalAttribute() : base(typeof(DestinationDataNormalAttribute))
        {
        }

        public override void ConfigureMapping(IMappingExpression mappingExpression)
        {
            mappingExpression.ForMember(nameof(DestinationDataNormalAttribute.YetAnotherName), expression => expression.MapFrom(nameof(SourceData.Name)));
        }
    }
}