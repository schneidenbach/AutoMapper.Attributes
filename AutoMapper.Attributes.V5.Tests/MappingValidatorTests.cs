using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
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
