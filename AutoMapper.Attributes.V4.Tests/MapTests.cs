using AutoMapper.Attributes.Tests.TestAssembly.SubclassTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    [TestFixture]
    public abstract class MapTests
    {
        protected const string Grandma = "Grandma";

        [SetUp]
        public void Setup()
        {
            typeof(Person).Assembly.MapTypes();
        }
    }
}