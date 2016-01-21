using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes.Tests.TestAssembly.SubclassTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class SubclassTests : MapTests
    {
        [Test]
        public void SocialSecurityNumberIsMappedFromMapsFromPropertyAttribute()
        {
            var sourceEmployee = new SourceEmployee { SocialSecurityNumber = Grandma };
            var targetEmployee = Mapper.Map<TargetEmployee>(sourceEmployee);
            Assert.That(targetEmployee.Ssn, Is.EqualTo(Grandma));
        }

        [Test]
        public void EmployeeCodeIsMappedFromMapsToPropertyAttribute()
        {
            var sourceEmployee = new SourceEmployee { EmployeeId = Grandma };
            var targetEmployee = Mapper.Map<TargetEmployee>(sourceEmployee);
            Assert.That(targetEmployee.Code, Is.EqualTo(Grandma));
        }
    }
}
