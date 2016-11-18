using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes.Tests.TestAssembly.SubclassTests
{
    public abstract class Person
    {
        public string Code { get; set; }

        [MapsFromProperty(typeof(SourceEmployee), "SocialSecurityNumber")]
        public string Ssn { get; set; }
    }
}
