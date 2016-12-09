using AutoMapper.Attributes.V5.TestAssembly.SubclassTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class SubclassTests : MapTests
    {
        [Test]
        public void SocialSecurityNumberIsMappedFromMapsFromPropertyAttribute()
        {
            var sourceEmployee = new SourceEmployee { SocialSecurityNumber = Grandma };
            var targetEmployee = TestMapper.Mapper.Map<TargetEmployee>(sourceEmployee);
            Assert.That(targetEmployee.Ssn, Is.EqualTo(Grandma));
        }

        [Test]
        public void EmployeeCodeIsMappedFromMapsToPropertyAttribute()
        {
            var sourceEmployee = new SourceEmployee { EmployeeId = Grandma, FirstName = Grandma, LastName = Grandma};
            var targetEmployee = TestMapper.Mapper.Map<TargetEmployee>(sourceEmployee);
            Assert.That(targetEmployee.Code, Is.EqualTo(Grandma));
        }
    }
}
 