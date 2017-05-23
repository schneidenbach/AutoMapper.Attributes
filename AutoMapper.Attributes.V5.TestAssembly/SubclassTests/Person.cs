namespace AutoMapper.Attributes.TestAssembly.SubclassTests
{
    public abstract class Person
    {
        public string Code { get; set; }

        [MapsFromProperty(typeof(SourceEmployee), "SocialSecurityNumber")]
        public string Ssn { get; set; }
        
        public string SomeNotes { get; set; }

        [MapsToAndFromProperty(typeof(SourceEmployee), nameof(SourceEmployee.Comments))]
        public string SomeComments { get; set; }
    }
}
