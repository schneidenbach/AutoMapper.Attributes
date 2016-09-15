namespace AutoMapper.Attributes.V5.TestAssembly.PropertyMapTests
{
    [MapsFrom(typeof(DeepContainer))]
    public class Container
    {
        public DeepContainer DeepContainer { get; set; }
        public string DeeperContainersName { get; set; }
        
        [MapsFromProperty(typeof(DeepContainer), "DeeperContainer.Name")]
        public string OtherDeeperContainerName { get; set; }
    }

    public class DeepContainer
    {
        public DeeperContainer DeeperContainer { get; set; }
    }

    [MapsTo(typeof(Container))]
    public class DeeperContainer
    {
        [MapsToProperty(typeof(Container), "DeeperContainersName")]
        public string Name { get; set; }
    }
}
