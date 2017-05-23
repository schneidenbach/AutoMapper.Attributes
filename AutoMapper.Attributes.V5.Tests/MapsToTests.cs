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
                Name = Grandma,
                NotNullableInt = 456,
                SourceNullableInt = 567,
                NotMappableFrom = Grandma
            };
        }
        
        public SourceData SourceData { get; }

        [Test]
        public void MapperMapsNameProperty()
        {
            var destination = TestMapper.Mapper.Map<DestinationData>(SourceData);
            Assert.That(destination.Name, Is.EqualTo(Grandma));
            Assert.That(destination.NullableInt, Is.EqualTo(456));
            Assert.That(destination.TargetDestinationNonNullableInt, Is.EqualTo(567));
            Assert.That(destination.NotMappableFrom, Is.Null);
            Assert.That(destination.NotExistentNotMappableFrom, Is.Null);
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = TestMapper.Mapper.Map<DestinationDataSpecialAttribute>(SourceData);
            Assert.That(destination.AnotherName, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperDoesNotMapWillNotMapToPropertyWithIgnoreMapToAttribute()
        {
            var destination = TestMapper.Mapper.Map<TestAssembly.MapsFromTests.DestinationData>(new TestAssembly.MapsFromTests.SourceData
            {
                Name = Grandma,
                WillNotMapTo = Grandma,
                WillAlsoNotMapTo = Grandma
            });
            Assert.That(destination.WillNotMapTo, Is.Null);
        }
    }
}
