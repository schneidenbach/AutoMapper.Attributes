namespace AutoMapper.Attributes.Tests.TestAssembly.MapsToMemberListTests
{
    [MapsTo(typeof(DestinationData), MemberList = MemberList.Source)]
    [MapsTo(typeof(DestinationDataBis), MemberList = MemberList.Destination)]
    [MapsTo(typeof(DestinationDataExtraMember))]
    public class SourceDataExtraMember
    {
        public string Name { get; set; }
        public string ExtraSourceMember { get; set; }
    }
}
