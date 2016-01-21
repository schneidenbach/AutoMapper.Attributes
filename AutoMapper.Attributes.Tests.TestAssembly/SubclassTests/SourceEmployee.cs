namespace AutoMapper.Attributes.Tests.TestAssembly.SubclassTests
{
    public class SourceEmployee
    {
        [MapsToProperty(typeof(Person), "Code")]
        public string EmployeeId { get; set; }

        public string SocialSecurityNumber { get; set; }
    }
}