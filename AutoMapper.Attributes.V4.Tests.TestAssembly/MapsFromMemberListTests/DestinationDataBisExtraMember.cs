namespace AutoMapper.Attributes.Tests.TestAssembly.MapsFromMemberListTests
{
    [MapsFrom(typeof(SourceData), MemberList = MemberList.Source)]
    public class DestinationDataBisExtraMember
    {
        public string Name { get; set; }
        public string ExtraDestinationMember { get; set; }
    }
}
