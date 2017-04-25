namespace AutoMapper.Attributes.V5.TestAssembly.PropertyMapTests
{
    [MapsTo(typeof(Container))]
    public class DeeperContainer
    {
        [MapsToProperty(typeof(Container), "DeeperContainersName")]
        public string Name { get; set; }
    }
}