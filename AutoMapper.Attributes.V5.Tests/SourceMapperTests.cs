using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes.V5.TestAssembly.MapsFromTests;
using AutoMapper.Configuration;
using NUnit.Framework;

namespace AutoMapper.Attributes.V5.Tests
{
    /// <summary>
    /// tried to figure out why this didn't work https://github.com/AutoMapper/AutoMapper/issues/1556
    /// </summary>
    [TestFixture]
    public class SourceMapperTests
    {
        [Test]
        public void FindsIgnoreMapFromAttribute()
        {
            var mappings = Extensions.GetSourceTypeMappings(new[] { typeof(SourceData) }, typeof(DestinationData));

            var hasIgnoreSourceTo = mappings.Any(m => m.IgnoreMapping &&
                                                      m.UseSourceMember &&
                                                      m.TargetType == typeof(DestinationData) &&
                                                      m.SourcePropertyInfos.Any(s => s.Name == nameof(SourceData.WillNotMapTo)));

            Assert.That(hasIgnoreSourceTo, Is.True);
        }


        [Test]
        public void ExpressionContainsIgnoreMapFromAttribute()
        {
            var mappings = Extensions.GetSourceTypeMappings(new[] { typeof(SourceData) }, typeof(DestinationData));
            var exp = Extensions.MapTypes(typeof(SourceData), typeof(DestinationData), mappings,
                new MapperConfigurationExpression());

        }
    }
}
