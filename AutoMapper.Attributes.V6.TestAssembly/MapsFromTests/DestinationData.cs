namespace AutoMapper.Attributes.TestAssembly.MapsFromTests
{
    [MapsFrom(typeof(SourceData))]
    [MapsFromSourceDataSpecial]
    public class DestinationData
    {
        public string Name { get; set; }
        [IgnoreMapFrom(typeof(SourceData))]
        public string WillNotMapTo { get; set; }
        public string WillAlsoNotMapTo { get; set; }
    }
}
