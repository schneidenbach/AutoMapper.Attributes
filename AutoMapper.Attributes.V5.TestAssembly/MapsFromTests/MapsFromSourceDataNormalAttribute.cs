namespace AutoMapper.Attributes.V5.TestAssembly.MapsFromTests
{
    public class MapsFromSourceDataNormalAttribute : MapsFromAttribute
    {
        public MapsFromSourceDataNormalAttribute() : base(typeof(SourceDataNormalAttribute))
        {
        }

        public override void ConfigureMapping(IMappingExpression mappingExpression)
        {
            mappingExpression.ForMember(nameof(DestinationData.Name), expression => expression.MapFrom(nameof(SourceDataNormalAttribute.YetAnotherName)));
        }
    }
}