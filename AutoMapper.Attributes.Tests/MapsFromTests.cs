using AutoMapper.Attributes.Tests.TestAssembly.MapsFromTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class MapsFromTests : MapTests
    {
        [Test]
        public void MapperMapsNameProperty()
        {
            var destination = Mapper.Map<DestinationData>(new SourceData
            {
                Name = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = Mapper.Map<DestinationData>(new SourceDataSpecialAttribute
            {
                AnotherName = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsUsingNormalConfigureMethod()
        {
            var destination = Mapper.Map<DestinationData>(new SourceDataNormalAttribute
            {
                YetAnotherName = Grandma
            });
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }
    }
}