using AutoMapper.Attributes.TestAssembly.MapsToAndFromTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class MapsToAndFromPropertyTests : MapTests
    {
        [Test]
        public void MapperMapsPropertiesFromSourceToDestination()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(new SourceData
            {
                Name = Grandma,
                Address = Grandma
            });
            Assert.That(destination.MapsFromPropertyAddress, Is.EqualTo(Grandma));
            Assert.That(destination.MapsToPropertyName, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsPropertiesFromDestinationToSource()
        {
            var destination = TestMapper.Mapper.Map<SourceData>(new DestinationData
            {
                MapsToPropertyName = Grandma,
                MapsFromPropertyAddress = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
            Assert.That(destination.Address, Is.EqualTo(Grandma));
        }
    }
}
