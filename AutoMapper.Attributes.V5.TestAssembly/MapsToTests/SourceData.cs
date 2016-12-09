namespace AutoMapper.Attributes.V5.TestAssembly.MapsToTests
{
    [MapsTo(typeof(DestinationData), ReverseMap = true)]
    [MapsToDestinationDataSpecial]
    public class SourceData
    {
        public string Name { get; set; }

        public int NotNullableInt { get; set; }
        public int? SourceNullableInt { get; set; }
    }
}
