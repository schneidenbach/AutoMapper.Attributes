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
}
