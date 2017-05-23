namespace AutoMapper.Attributes.TestAssembly.PropertyMapTests
{
    [MapsFrom(typeof(DeepContainer))]
    public class Container
    {
        [IgnoreMapFrom(typeof(DeepContainer))]
        [IgnoreMapFrom(typeof(DeeperContainer))]
        public DeepContainer DeepContainer { get; set; }

        [IgnoreMapFrom(typeof(DeepContainer))]
        public string DeeperContainersName { get; set; }
        
        [MapsFromProperty(typeof(DeepContainer), "DeeperContainer.Name")]
        [IgnoreMapFrom(typeof(DeeperContainer))]
        public string OtherDeeperContainerName { get; set; }
    }
}
