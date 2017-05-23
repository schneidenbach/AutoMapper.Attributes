namespace AutoMapper.Attributes.TestAssembly.PropertyMapTests
{
    [IgnoreMapToProperties(typeof(Container), nameof(Container.DeepContainer), nameof(Container.DeeperContainersName))]
    public class DeepContainer
    {
        public DeeperContainer DeeperContainer { get; set; }
    }
}