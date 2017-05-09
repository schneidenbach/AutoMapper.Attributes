using AutoMapper.Attributes.V5.TestAssembly.SubclassTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class SubclassTests : MapTests
    {
        static TargetEmployee GetMappedObject()
        {
            var sourceEmployee = new SourceEmployee
            {
                EmployeeId = Grandma,
                FirstName = Grandma,
                LastName = Grandma,
                Notes = Grandma,
                Comments = Grandma,
                SocialSecurityNumber = Grandma
            };
            return TestMapper.Mapper.Map<TargetEmployee>(sourceEmployee);
        }

        [Test]
        public void SocialSecurityNumberIsMappedFromMapsFromPropertyAttribute()
        {
            var targetEmployee = GetMappedObject();
            Assert.That(targetEmployee.Ssn, Is.EqualTo(Grandma));
        }

        [Test]
        public void EmployeeCodeIsMappedFromMapsToPropertyAttribute()
        {
            var targetEmployee = GetMappedObject();
            Assert.That(targetEmployee.Code, Is.EqualTo(Grandma));
        }

        [Test]
        public void NotesIsMappedFromMapsToAndFromPropertyAttribute()
        {
            var targetEmployee = GetMappedObject();
            Assert.That(targetEmployee.SomeNotes, Is.EqualTo(Grandma));
        }

        [Test]
        public void CommentsIsMappedFromMapsToAndFromPropertyAttribute()
        {
            var targetEmployee = GetMappedObject();
            Assert.That(targetEmployee.SomeComments, Is.EqualTo(Grandma));
        }
    }
}
 