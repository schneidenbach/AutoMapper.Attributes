namespace AutoMapper.Attributes.V5.TestAssembly.SubclassTests
{
    public abstract class Person
    {
        public string Code { get; set; }

        [MapsFromProperty(typeof(SourceEmployee), "SocialSecurityNumber")]
        public string Ssn { get; set; }
    }
}
