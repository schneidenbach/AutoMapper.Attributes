using AutoMapper.Attributes.V5.TestAssembly.SubclassTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
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
                typeof(Person).Assembly.MapTypes(config);
                
                //config.CreateMap<SourceData, DestinationData>()
                //    .ForMember<string>(data => data.Name, expression => expression.MapFrom(data => data.Name));
            });
        }
    }
}