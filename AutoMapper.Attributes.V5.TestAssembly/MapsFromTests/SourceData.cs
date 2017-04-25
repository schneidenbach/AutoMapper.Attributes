namespace AutoMapper.Attributes.V5.TestAssembly.MapsFromTests
{
    [DoNotMapPropertyTo(typeof(DestinationData), nameof(DestinationData.WillNotMapTo))]
    public class SourceData
    {
        public string Name { get; set; }
        public string WillNotMapTo { get; set; }
    }
}