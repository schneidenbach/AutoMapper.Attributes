namespace AutoMapper.Attributes.Tests.TestAssembly.SubclassTests
{
    [MapsFrom(typeof(SourceEmployee))]
    public class TargetEmployee : Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}