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

            var container = Mapper.Map<Container>(deep.DeeperContainer);
            Assert.That(container.DeeperContainersName, Is.EqualTo(Grandma));
        }
    }
}