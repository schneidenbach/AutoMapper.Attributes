namespace AutoMapper.Attributes.TestAssembly.MapsToTests
{
    [MapsTo(typeof(DestinationData), ReverseMap = true)]
    [MapsToDestinationDataSpecial]
    public class SourceData
    {
        public string Name { get; set; }

        [IgnoreMapFrom(typeof(DestinationData))]
        public int NotNullableInt { get; set; }
        [IgnoreMapFrom(typeof(DestinationData))]
        public int? SourceNullableInt { get; set; }
        public string NotMappableFrom { get; set; }
    }
}
