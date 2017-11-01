using AutoMapper.Attributes.TestAssembly.PropertyMapTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class MapsToPropertyTests : MapTests
    {
        [Test]
        public void TestMapToProperty()
        {
            var deeperContainer = new DeeperContainer
            {
                Name = Grandma
            };

            var container = TestMapper.Mapper.Map<Container>(deeperContainer);
            Assert.That(container.DeeperContainersName, Is.EqualTo(Grandma));
        }

        [Test]
        public void TestMapToNullProperty()
        {
            var deep = new DeeperContainer();

            var container = TestMapper.Mapper.Map<Container>(deep);
            Assert.That(container.DeeperContainersName, Is.Null);
        }
    }
}