using AutoMapper.Attributes.Tests.TestAssembly.PropertyMapTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class MapsToPropertyTests : MapTests
    {
        [Test]
        public void TestMapToProperty()
        {
            var deep = new DeepContainer
            {
                DeeperContainer = new DeeperContainer
                {
                    Name = Grandma
                }
            };

            var container = TestMapper.Mapper.Map<Container>(deep.DeeperContainer);
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