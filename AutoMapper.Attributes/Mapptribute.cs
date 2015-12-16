using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class Mapptribute : Attribute
    {
        internal protected Mapptribute()
        {
            
        }
        
        /// <summary>
        /// If true, mapping will be configured in reverse as well.
        /// </summary>
        public bool ReverseMap { get; set; }
        
        /// <summary>
        /// Allows configuration of the mapping after mapping is done. Meant to be 
        /// </summary>
        /// <param name="mappingExpression"></param>
        public virtual void ConfigureMapping(IMappingExpression mappingExpression)
        {
        }
    }
}
