using AutoMapper.Attributes.Tests.TestAssembly.MapsFromTests;
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
            typeof(SourceData).Assembly.MapTypes();
        }

    }
}