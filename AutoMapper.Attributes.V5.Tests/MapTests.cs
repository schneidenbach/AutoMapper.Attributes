using System.Reflection;
using AutoMapper.Attributes.TestAssembly.SubclassTests;
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
            Mapper.Initialize(config =>
            {
                typeof(Person).GetTypeInfo().Assembly.MapTypes(config);
            });
        }
    }
}