using AutoMapper.Attributes.V5.TestAssembly.MapsFromTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class MapsFromTests : MapTests
    {
        [Test]
        public void MapperMapsNameProperty()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(new SourceData
            {
                Name = Grandma,
                WillNotMapTo = Grandma,
                WillAlsoNotMapTo = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
            Assert.That(destination.WillNotMapTo, Is.Null);
            Assert.That(destination.WillAlsoNotMapTo, Is.Null);
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(new SourceDataSpecialAttribute
            {
                AnotherName = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }
    }
}