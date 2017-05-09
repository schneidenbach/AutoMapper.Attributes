namespace AutoMapper.Attributes.V5.TestAssembly.MapsToAndFromTests
{
    [MapsFrom(typeof(SourceData), ReverseMap = true)]
    public class DestinationData
    {
        [MapsToAndFromProperty(typeof(SourceData), nameof(SourceData.Name))]
        public string MapsToPropertyName { get; set; }
        public string MapsFromPropertyAddress { get; set; }
    }
}
