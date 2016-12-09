using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Attributes
{
    public static class Extensions
    {
        /// <summary>
        /// Maps all types with the MapsTo/MapsFrom attributes to the type specified in MapTo/MapFrom.
        /// </summary>
        /// <param name="assembly">The assembly to search for types.</param>
        /// <param name="mapperConfiguration">The mapper configuration.</param>
        public static void MapTypes(this Assembly assembly, IMapperConfigurationExpression mapperConfiguration)
        {
            var assemblyTypes = assembly.GetTypes();
            var mappedTypes = 
                assemblyTypes.Select(t => new
                {
                    Type = t,
                    MapsToAttributes = t.GetCustomAttributes(typeof(MapsToAttribute), true).Cast<MapsToAttribute>(),
                    MapsFromAttributes = t.GetCustomAttributes(typeof(MapsFromAttribute), true).Cast<MapsFromAttribute>()
                })
                .Where(t => t.MapsToAttributes.Any() || t.MapsFromAttributes.Any());
            
            foreach (var type in mappedTypes)
            {
                ProcessMapsToAttributes(type.Type, type.MapsToAttributes, mapperConfiguration, assemblyTypes);
                ProcessMapsFromAttributes(type.Type, type.MapsFromAttributes, mapperConfiguration, assemblyTypes);
            }
        }

        private static void ProcessMapsFromAttributes(
            Type targetType, 
            IEnumerable<MapsFromAttribute> mapsFromAttributes, 
            IMapperConfigurationExpression mapperConfiguration, 
            Type[] types)
        {
            foreach (var mapsFromAttribute in mapsFromAttributes)
            {
                var sourceType = mapsFromAttribute.SourceType;
                Mapptivate(mapsFromAttribute, sourceType, targetType, mapperConfiguration, types, mapsFromAttribute.ReverseMap);
            }
        }
        
        private static void ProcessMapsToAttributes(
            Type sourceType, 
            IEnumerable<MapsToAttribute> mapsToAttributes, 
            IMapperConfigurationExpression mapperConfiguration, 
            Type[] types)
        {
            foreach (var mapsToAttribute in mapsToAttributes)
            {
                var targetType = mapsToAttribute.TargetType;
                Mapptivate(mapsToAttribute, sourceType, targetType, mapperConfiguration, types, mapsToAttribute.ReverseMap);
            }
        }

        private static PropertyMapInfo[] GetMappedProperties(Type[] typesToSearch, Type sourceType, Type targetType)
        {
            return new[]
            {
                typesToSearch
                    .Where(sourceType.IsAssignableFrom)
                    .SelectMany(t =>
                        t.GetProperties().Select(p => new
                        {
                            Property = p,
                            MapsToAttributes =
                                p.GetCustomAttributes<MapsToPropertyAttribute>()
                                    .Where(pt => pt.TargetType.IsAssignableFrom(targetType))
                        }))
                    .SelectMany(p => p.MapsToAttributes.Select(a => a.GetPropertyMapInfo(p.Property))),

                typesToSearch
                    .Where(targetType.IsAssignableFrom)
                    .SelectMany(t =>
                        t.GetProperties().Select(p => new
                        {
                            Property = p,
                            MapsToAttributes =
                                p.GetCustomAttributes<MapsFromPropertyAttribute>()
                                    .Where(pt => pt.SourceType.IsAssignableFrom(sourceType))
                        }))
                    .SelectMany(p => p.MapsToAttributes.Select(a => a.GetPropertyMapInfo(p.Property)))
                    .ToArray()
            }.SelectMany(p => p).ToArray();
        }

        /// <summary>
        /// All these map puns are giving me a headache.
        /// </summary>
        private static void Mapptivate(Mapptribute mapsToAttribute, Type sourceType, Type targetType, IMapperConfigurationExpression mapperConfiguration, Type[] types, bool reverseMap)
        {
            var configureMappingGenericMethod = GetConfigureMappingGenericMethod(mapsToAttribute, sourceType, targetType);
            var mappedProperties = GetMappedProperties(types, sourceType, targetType);
            var mappingExpression = MapTypes(sourceType, targetType, mappedProperties, mapperConfiguration);
            configureMappingGenericMethod?.Invoke(mapsToAttribute, new[] { mappingExpression });

            if (reverseMap)
            {
                Mapptivate(mapsToAttribute, targetType, sourceType, mapperConfiguration, types, false);
            }
        }

        private static MethodInfo GetConfigureMappingGenericMethod(Mapptribute mapsToAttribute, Type sourceType, Type targetType)
        {
            return mapsToAttribute.GetType()
                .GetMethod("ConfigureMapping",
                           new[] { typeof(IMappingExpression<,>).MakeGenericType(sourceType, targetType) });
        }

        private static object MapTypes(Type sourceType,
            Type targetType,
            PropertyMapInfo[] propertyMapInfos,
            IMapperConfigurationExpression mapperConfiguration)
        {
            var createMapMethodInfo = GenericCreateMap.MakeGenericMethod(sourceType, targetType);

            //actually create the mapping
            var mapObject = createMapMethodInfo.Invoke(mapperConfiguration, new object[] { });
            Debug.WriteLine($"Mapping created for source type {sourceType.Name} to target type {targetType.Name}");

            var mapObjectExpression = Expression.Constant(mapObject);
            var sourceTypeParameter = Expression.Parameter(sourceType);
 
            foreach (var propMapInfo in propertyMapInfos)
            {
                var sourcePropertyInfos = propMapInfo.SourcePropertyInfos;
                var targetPropertyInfo = propMapInfo.TargetPropertyInfo;

                var targetPropertyName = targetPropertyInfo.Name;
                Debug.WriteLine($"-- Mapping property {string.Join(".", sourcePropertyInfos.Select(s => s.Name))} to target type {targetPropertyName}");
                
                var finalSourcePropertyType = sourcePropertyInfos.Last().PropertyType;

                var memberConfigType = typeof(IMemberConfigurationExpression<,,>)
                    .MakeGenericType(sourceType, targetType, typeof(object));
                var memberConfigTypeParameter = Expression.Parameter(memberConfigType);

                var propertyExpression =
                    sourcePropertyInfos.Aggregate<PropertyInfo, Expression>(null, (current, prop) => current == null
                        ? Expression.Property(sourceTypeParameter, prop)
                        : Expression.Property(current, prop));
                
                var memberOptions = Expression.Call(memberConfigTypeParameter,
                    nameof(IMemberConfigurationExpression.MapFrom),
                    new Type[] { finalSourcePropertyType },
                    Expression.Lambda(
                        propertyExpression,
                        sourceTypeParameter
                    ));
                
                var lambdaExpression = Expression.Lambda(memberOptions, memberConfigTypeParameter);
                var forMemberMethodExpression = 
                    Expression.Call(
                    mapObjectExpression,
                    nameof(IMappingExpression<object, object>.ForMember),
                    Type.EmptyTypes,
                    Expression.Constant(targetPropertyInfo.Name),
                    lambdaExpression);
                
                //TODO: cache this somewhere for better repeat performance
                Expression.Lambda(forMemberMethodExpression).Compile().DynamicInvoke();
            }

            return mapObject;
        }

        static Extensions()
        {
            GenericCreateMap =
                typeof(IProfileExpression).GetMethods()
                    .Single(m => m.Name == nameof(IMapperConfigurationExpression.CreateMap) &&
                                 m.IsGenericMethodDefinition &&
                                 m.GetGenericArguments().Length == 2 &&
                                 !m.GetParameters().Any());
        }

        private static MethodInfo GenericCreateMap { get; }
    }
}