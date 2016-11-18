namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromTests
{
    [MapsFrom(typeof(SourceData))]
    [MapsFromSourceDataNormal]
    [MapsFromSourceDataSpecial]
    public class DestinationData
    {
        public string Name { get; set; }
    }
}
