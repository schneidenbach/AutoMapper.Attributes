using System.Reflection;
using AutoMapper.Attributes.TestAssembly.SubclassTests;

namespace AutoMapper.Attributes.Tests
{
    public static class TestMapper
    {
        public static MapperConfiguration MapperConfiguration { get; set; }
        public static IMapper Mapper { get; set; }

        static TestMapper()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                typeof(Person).GetTypeInfo().Assembly.MapTypes(cfg);
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
    }
}