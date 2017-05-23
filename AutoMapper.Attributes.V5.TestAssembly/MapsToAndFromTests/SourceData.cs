namespace AutoMapper.Attributes.TestAssembly.MapsToAndFromTests
{
    public class SourceData
    {
        public string Name { get; set; }
        [MapsToAndFromProperty(typeof(DestinationData), nameof(DestinationData.MapsFromPropertyAddress))]
        public string Address { get; set; }
    }
}