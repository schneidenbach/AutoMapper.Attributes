namespace AutoMapper.Attributes.TestAssembly.MapsFromTests
{
    public class MapsFromSourceDataSpecialAttribute : MapsFromAttribute
    {
        public MapsFromSourceDataSpecialAttribute() : base(typeof(SourceDataForTheSpecialAttribute))
        {
        }

        public void ConfigureMapping(IMappingExpression<SourceDataForTheSpecialAttribute, DestinationData> mappingExpression)
        {
            mappingExpression
                .ForMember(d => d.WillNotMapTo, exp => exp.Ignore())
                .ForMember(d => d.WillAlsoNotMapTo, exp => exp.Ignore())
                .ForMember(d => d.Name, expression => expression.MapFrom(s => s.AnotherName));
        }
    }
}