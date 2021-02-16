namespace AutoMapper.Attributes.Tests.TestAssembly.MapsToMemberListTests
{
    [MapsTo(typeof(DestinationData), MemberList = MemberList.Destination)]
    [MapsTo(typeof(DestinationDataBis), MemberList = MemberList.Source)]
    [MapsTo(typeof(DestinationDataExtraMember), MemberList = MemberList.Destination)]
    [MapsTo(typeof(DestinationDataBisExtraMember), MemberList = MemberList.Source)]
    public class SourceData
    {
        public string Name { get; set; }
    }
}
