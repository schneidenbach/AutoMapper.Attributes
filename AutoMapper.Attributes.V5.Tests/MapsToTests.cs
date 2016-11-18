using AutoMapper.Attributes.V5.TestAssembly.MapsToTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class MapsToTests : MapTests
    {
        public MapsToTests()
        {
            SourceData = new SourceData
            {
                Name = Grandma
            };
        }
        
        public SourceData SourceData { get; }

        [Test]
        public void MapperMapsNameProperty()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(SourceData);
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = TestMapper.Mapper.Map<DestinationDataSpecialAttribute>(SourceData);
            Assert.That(destination.AnotherName, Is.EqualTo(Grandma));
        }
    }
}
