using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes.Tests.TestAssembly.MapsToTests;
using AutoMapper.Attributes.Tests.TestAssembly;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
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
            var destination = Mapper.Map<DestinationData>(SourceData);
            Assert.That(destination.Name, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsUsingGenericConfigureMethod()
        {
            var destination = Mapper.Map<DestinationDataSpecialAttribute>(SourceData);
            Assert.That(destination.AnotherName, Is.EqualTo(Grandma));
        }

        [Test]
        public void MapperMapsUsingNormalConfigureMethod()
        {
            var destination = Mapper.Map<DestinationDataNormalAttribute>(SourceData);
            Assert.That(destination.YetAnotherName, Is.EqualTo(Grandma));
        }
    }
}
