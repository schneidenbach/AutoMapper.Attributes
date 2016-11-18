using AutoMapper.Attributes.V5.TestAssembly.PropertyMapTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    public class MapsFromPropertyTests : MapTests
    {
        [Test]
        public void TestDeepMapFromProperty()
        {
            var deep = new DeepContainer
            {
                DeeperContainer = new DeeperContainer
                {
                    Name = Grandma
                }
            };

            var container = TestMapper.Mapper.Map<Container>(deep);
            Assert.That(container.OtherDeeperContainerName, Is.EqualTo(Grandma));
        }

        [Test]
        public void TestDeepMapFromNullProperty()
        {
            var deep = new DeepContainer();

            var container = TestMapper.Mapper.Map<Container>(deep);
            Assert.That(container.DeeperContainersName, Is.Null);
        }

        [Test]
        public void TestDeepMapFromNullProperty_Static()
        {
            var deep = new DeepContainer();

            var container = Mapper.Map<Container>(deep);
            Assert.That(container.DeeperContainersName, Is.Null);
        }
    }
}
