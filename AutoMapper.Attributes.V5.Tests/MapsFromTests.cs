using AutoMapper.Attributes.TestAssembly.MapsFromTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
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
        }

        [Test]
        public void MapperDoesNotMapWillNotMapToProperties()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(new SourceData
            {
                Name = Grandma,
                WillNotMapTo = Grandma,
                WillAlsoNotMapTo = Grandma
            });
            Assert.That(destination.WillAlsoNotMapTo, Is.Null);
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(new SourceDataForTheSpecialAttribute
            {
                AnotherName = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }
    }
}