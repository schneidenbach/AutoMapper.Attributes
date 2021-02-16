namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromMemberListTests
{
    [MapsFrom(typeof(SourceData), MemberList = MemberList.Destination)]
    [MapsFrom(typeof(SourceDataExtraMember))]
    public class DestinationDataExtraMember
    {
        public string Name { get; set; }
        public string ExtraDestinationMember { get; set; }
    }
}
