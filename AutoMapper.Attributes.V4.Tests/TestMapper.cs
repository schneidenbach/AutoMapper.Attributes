using AutoMapper.Attributes.Tests.TestAssembly.SubclassTests;

namespace AutoMapper.Attributes.Tests
{
    public static class TestMapper
    {
        public static MapperConfiguration MapperConfiguration { get; set; }
        public static IMapper Mapper { get; set; }

        static TestMapper()
        {
            MapperConfiguration = new MapperConfiguration(cfg => {
                typeof(Person).Assembly.MapTypes(cfg);
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        static public void AssertConfigurationIsValid<TSource, TDestination>()
        {
            TypeMap map = MapperConfiguration.FindTypeMapFor<TSource, TDestination>();
            NUnit.Framework.Assert.IsNotNull(map, "Could not find type map from {0} to {1}", typeof(TSource), typeof(TDestination));
            MapperConfiguration.AssertConfigurationIsValid(map);
        }
    }
}