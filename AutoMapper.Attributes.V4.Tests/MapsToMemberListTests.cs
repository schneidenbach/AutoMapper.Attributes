using AutoMapper.Attributes.Tests.TestAssembly.MapsToMemberListTests;
using NUnit.Framework;

namespace AutoMapper.Attributes.Tests
{
    public class MapsToMemberListTests : MapTests
    {
        [Test]
        public void TestMemberListDestination()
        {
            Assert.DoesNotThrow(
                TestMapper.AssertConfigurationIsValid<SourceData, DestinationData>);
        }

        [Test]
        public void TestMemberListSource()
        {
            Assert.DoesNotThrow(
                TestMapper.AssertConfigurationIsValid<SourceData, DestinationDataBis>);
        }

        [Test]
        public void TestExtraSourceMemberListFail()
        {
            Assert.Throws(
                typeof(AutoMapperConfigurationException),
                TestMapper.AssertConfigurationIsValid<SourceDataExtraMember, DestinationData>);
        }

        [Test]
        public void TestExtraSourceMemberList()
        {
            Assert.DoesNotThrow(
                TestMapper.AssertConfigurationIsValid<SourceDataExtraMember, DestinationDataBis>);
        }

        [Test]
        public void TestExtraDestinationMemberListFail()
        {
            Assert.Throws(
                typeof(AutoMapperConfigurationException),
                TestMapper.AssertConfigurationIsValid<SourceData, DestinationDataExtraMember>);
        }

        [Test]
        public void TestExtraDestinationMemberList()
        {
            Assert.DoesNotThrow(
                TestMapper.AssertConfigurationIsValid<SourceData, DestinationDataBisExtraMember>);
        }
    }
}
