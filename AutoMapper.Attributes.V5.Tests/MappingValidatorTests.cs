using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class MappingValidatorTests
    {
        [Test]
        public void AssertConfigurationIsValid()
        {
            TestMapper.MapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
