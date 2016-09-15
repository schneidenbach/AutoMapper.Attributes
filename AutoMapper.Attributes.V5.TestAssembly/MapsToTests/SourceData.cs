namespace AutoMapper.Attributes.V5.TestAssembly.MapsToTests
{
    [MapsTo(typeof(DestinationData))]
    [MapsToDestinationDataSpecial]
    [MapsToDestinationDataNormal]
    public class SourceData
    {
        public string Name { get; set; }
    }
}
