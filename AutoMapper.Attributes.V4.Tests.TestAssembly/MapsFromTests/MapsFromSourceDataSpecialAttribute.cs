namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromTests
{
    public class MapsFromSourceDataSpecialAttribute : MapsFromAttribute
    {
        public MapsFromSourceDataSpecialAttribute() : base(typeof(SourceDataSpecialAttribute))
        {
        }

        public void ConfigureMapping(IMappingExpression<SourceDataSpecialAttribute, DestinationData> mappingExpression)
        {
            mappingExpression
                .ForMember(d => d.Name, expression => expression.MapFrom(s => s.AnotherName));
        }
    }
}