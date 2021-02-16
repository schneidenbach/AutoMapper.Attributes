namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromMemberListTests
{
    [MapsFrom(typeof(SourceData), MemberList = MemberList.Source)]
    [MapsFrom(typeof(SourceDataExtraMember), MemberList = MemberList.Destination)]
    public class DestinationDataBis
    {
        public string Name { get; set; }
    }
}
