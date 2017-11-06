namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromMemberListTests
{
    [MapsFrom(typeof(SourceData), MemberList = MemberList.Destination)]
    [MapsFrom(typeof(SourceDataExtraMember), MemberList = MemberList.Source)]
    public class DestinationData
    {
        public string Name { get; set; }
    }
}
