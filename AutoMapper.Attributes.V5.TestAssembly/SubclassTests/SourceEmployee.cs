namespace AutoMapper.Attributes.V5.TestAssembly.SubclassTests
{
    public class SourceEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MapsToProperty(typeof(Person), "Code")]
        public string EmployeeId { get; set; }

        public string SocialSecurityNumber { get; set; }
    }
}