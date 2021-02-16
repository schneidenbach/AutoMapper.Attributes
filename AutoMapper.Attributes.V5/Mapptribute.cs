﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes
{
    /// <summary>
    /// The base class for attributes that map objects to other objects using AutoMapper.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class Mapptribute : Attribute
    {
        internal Mapptribute() { }

        /// <summary>
        /// If true, mapping will be configured in reverse as well.
        /// </summary>
        public bool ReverseMap { get; set; }

        /// <summary>
        /// Specify the member list to validate against during configuration validation. Default <see cref="MemberList.Destination"/>.
        /// </summary>
        public MemberList MemberList { get; set; } = MemberList.Destination;
    }
}
