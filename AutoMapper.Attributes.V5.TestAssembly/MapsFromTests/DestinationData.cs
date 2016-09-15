namespace AutoMapper.Attributes.V5.TestAssembly.MapsFromTests
{
    [MapsFrom(typeof(SourceData))]
    [MapsFromSourceDataNormal]
    [MapsFromSourceDataSpecial]
    public class DestinationData
    {
        public string Name { get; set; }
    }
}
