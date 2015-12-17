using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var types = assembly.GetTypes();
            var mappedTypes = types
                .Select(t => new
                {
                    Type = t,
                    MapsToAttributes = t.GetCustomAttributes<MapsToAttribute>(),
                    MapsFromAttributes = t.GetCustomAttributes<MapsFromAttribute>()
                })
                .Where(t => t.MapsToAttributes.Any() || t.MapsFromAttributes.Any());
            
            var sourceTypes = new HashSet<Type>();
            var destinationTypes = new HashSet<Type>();

            foreach (var t in mappedTypes)
            {
                foreach (var mapsToAttribute in t.MapsToAttributes)
                {
                    var sourceType = t.Type;
                    var destinationType = mapsToAttribute.DestinationType;
                    
                    Mapptivate(mapsToAttribute, sourceType, destinationType);
                    sourceTypes.Add(sourceType);
                    destinationTypes.Add(destinationType);
                }

                foreach (var mapsFromAttribute in t.MapsFromAttributes)
                {
                    var sourceType = mapsFromAttribute.SourceType;
                    var destinationType = t.Type;

                    Mapptivate(mapsFromAttribute, sourceType, destinationType);
                    sourceTypes.Add(sourceType);
                    destinationTypes.Add(destinationType);
                }
            }

            ProcessMapsPropertyFromAttributes(destinationTypes);
            ProcessMapsPropertyToAttributes(sourceTypes);
        }

        private static void ProcessMapsPropertyToAttributes(HashSet<Type> sourceTypes)
        {
            var typesWithMapToProperties = sourceTypes.Select(t => new
            {
                Type = t,
                MapToProperties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new
                {
                    Property = p,
                    MapToAttributes = p.GetCustomAttributes<MapsToPropertyAttribute>().ToList()
                }).Where(p => p.MapToAttributes.Any()).ToList()
            }).Where(t => t.MapToProperties.Any()).ToList();

            foreach (var t in typesWithMapToProperties)
            {
                foreach (var p in t.MapToProperties)
                {
                    foreach (var mapToAttribute in p.MapToAttributes)
                    {
                        var propMapInfo = mapToAttribute.GetPropertyMapInfo(p.Property);
                        MapProperties(propMapInfo);
                    }
                }
            }
        }

        private static void MapProperties(PropertyMapInfo propMapInfo)
        {
            var sourceType = propMapInfo.SourceType;
            var sourceProperty = propMapInfo.SourcePropertyInfo;
            var destinationType = propMapInfo.DestinationType;
            var destinationPropertyInfo = propMapInfo.DestinationPropertyInfo;

            var createMapMethod = GenericCreateMap.MakeGenericMethod(sourceType, destinationType);
            var mapObject = createMapMethod.Invoke(null, new object[] {});
            var mapObjectExpression = Expression.Constant(mapObject);

            var sourceParameter = Expression.Parameter(sourceType);
            var destinationParameter = Expression.Parameter(destinationType);

            var destinationMember = Expression.Lambda(
                Expression.Convert(
                    Expression.Property(destinationParameter, destinationPropertyInfo),
                    typeof (object)
                    ),
                destinationParameter
                );

            var memberConfigType = typeof (IMemberConfigurationExpression<>).MakeGenericType(sourceType);
            var memberConfigTypeParameter = Expression.Parameter(memberConfigType);

            var memberOptions = Expression.Call(memberConfigTypeParameter,
                "MapFrom",
                new Type[] {sourceProperty.PropertyType},
                Expression.Lambda(
                    Expression.Property(sourceParameter, sourceProperty),
                    sourceParameter
                    ));

            var emptyTypes = Enumerable.Empty<Type>().ToArray();
            var forMemberMethod = Expression.Call(mapObjectExpression,
                "ForMember",
                emptyTypes,
                destinationMember,
                Expression.Lambda(memberOptions, memberConfigTypeParameter));

            Expression.Lambda(forMemberMethod).Compile().DynamicInvoke();
        }

        private static void ProcessMapsPropertyFromAttributes(HashSet<Type> destinationTypes)
        {
            var typesWithMapToProperties = destinationTypes.Select(t => new
            {
                Type = t,
                MapFromProperties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new
                {
                    Property = p,
                    MapFromAttributes = p.GetCustomAttributes<MapsFromPropertyAttribute>().ToList()
                }).Where(p => p.MapFromAttributes.Any()).ToList()
            }).Where(t => t.MapFromProperties.Any()).ToList();

            foreach (var t in typesWithMapToProperties)
            {
                foreach (var p in t.MapFromProperties)
                {
                    foreach (var mapToAttribute in p.MapFromAttributes)
                    {
                        var propMapInfo = mapToAttribute.GetPropertyMapInfo(p.Property);
                        MapProperties(propMapInfo);
                    }
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
                var createMapMethod = GenericCreateMap.MakeGenericMethod(sourceType, destinationType);
                var map = createMapMethod.Invoke(null, new object[] {});
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
