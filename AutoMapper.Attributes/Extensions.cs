using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapper.Attributes
{
    public static class Extensions
    {
        static Extensions()
        {
            GenericCreateMap =
                typeof (Mapper).GetMethods()
                    .Single(m => m.Name == "CreateMap" && 
                                 m.IsGenericMethodDefinition && 
                                 m.GetGenericArguments().Count() == 2 &&
                                 !m.GetParameters().Any());
        }

        private static MethodInfo GenericCreateMap { get; }

        /// <summary>
        /// Maps all types with the MapsTo attribute to the type specified in MapTo.
        /// </summary>
        /// <param name="assembly">The assembly to search for types.</param>
        public static void MapTypes(this Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Select(t => new
                {
                    Type = t,
                    MapsToAttributes = t.GetCustomAttributes<MapsToAttribute>(),
                    MapsFromAttributes = t.GetCustomAttributes<MapsFromAttribute>()
                })
                .Where(t => t.MapsToAttributes?.Any() == true || t.MapsFromAttributes?.Any() == true);

            foreach (var t in types)
            {
                foreach (var mapsToAttribute in t.MapsToAttributes)
                {
                    var sourceType = t.Type;
                    var destinationType = mapsToAttribute.DestinationType;
                    
                    Mapptivate(mapsToAttribute, sourceType, destinationType);
                }

                foreach (var mapsFromAttribute in t.MapsFromAttributes)
                {
                    var sourceType = mapsFromAttribute.SourceType;
                    var destinationType = t.Type;

                    Mapptivate(mapsFromAttribute, sourceType, destinationType);
                }
            }
        }

        /// <summary>
        /// All these map puns are giving me a headache.
        /// </summary>
        private static void Mapptivate(Mapptribute mapsToAttribute, Type sourceType, Type destinationType)
        {
            var configureMappingGenericMethod = GetConfigureMappingGenericMethod(mapsToAttribute, sourceType, destinationType);

            if (configureMappingGenericMethod == null)
            {
                var map = Mapper.CreateMap(sourceType, destinationType);
                mapsToAttribute.ConfigureMapping(map);
            }
            else
            {
                var genericMethod = GenericCreateMap.MakeGenericMethod(sourceType, destinationType);
                var map = genericMethod.Invoke(mapsToAttribute, new object[] {});
                configureMappingGenericMethod.Invoke(mapsToAttribute, new[] {map});
            }

            if (mapsToAttribute.ReverseMap)
            {
                Mapper.CreateMap(destinationType, sourceType);
            }
        }

        private static MethodInfo GetConfigureMappingGenericMethod(Mapptribute mapsToAttribute, Type sourceType, Type destinationType)
        {
            return mapsToAttribute.GetType()
                .GetMethod(nameof(Mapptribute.ConfigureMapping), new[] {typeof (IMappingExpression<,>)
                .MakeGenericType(sourceType, destinationType)});
        }
    }
}
