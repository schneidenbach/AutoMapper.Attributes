namespace AutoMapper.Attributes.TestAssembly.PropertyMapTests
{
    [MapsTo(typeof(Container))]
    public class DeeperContainer
    {
        [MapsToProperty(typeof(Container), "DeeperContainersName")]
        public string Name { get; set; }
    }
}