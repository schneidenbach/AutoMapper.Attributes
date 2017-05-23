namespace AutoMapper.Attributes.V5.TestAssembly.MapsToTests
{
    public class MapsToDestinationDataSpecialAttribute : MapsToAttribute
    {
        public MapsToDestinationDataSpecialAttribute() : base(typeof(DestinationDataSpecialAttribute))
        {
        }

        public void ConfigureMapping(IMappingExpression<SourceData, DestinationDataSpecialAttribute> mappingExpression)
        {
            mappingExpression
                .ForMember(d => d.AnotherName, expression => expression.MapFrom(s => s.Name));
        }
    }
}