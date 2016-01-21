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
    /// <summary>
    /// Contains the method to map the assembly to attributes.
    /// </summary>
    public static class Extensions
    {
        static Extensions()
        {
            GenericCreateMap =
                typeof (Mapper).GetMethods()
                    .Single(m => m.Name == nameof(Mapper.CreateMap) && 
                                 m.IsGenericMethodDefinition && 
                                 m.GetGenericArguments().Count() == 2 &&
                                 !m.GetParameters().Any());
        }

        private static MethodInfo GenericCreateMap { get; }

        /// <summary>
        /// Maps all types with the MapsTo/MapsFrom attributes to the type specified in MapTo/MapFrom.
        /// </summary>
        /// <param name="assembly">The assembly to search for types.</param>
        public static void MapTypes(this Assembly assembly)
        {
            var types = assembly.GetTypes();
            var mappedTypes = types
                .Select(t => new
                {
                    Type = t,
                    MapsToAttributes = t.GetCustomAttributes(typeof(MapsToAttribute), true).Cast<MapsToAttribute>(),
                    MapsFromAttributes = t.GetCustomAttributes(typeof(MapsFromAttribute), true).Cast<MapsFromAttribute>()
                })
                .Where(t => t.MapsToAttributes.Any() || t.MapsFromAttributes.Any());
            
            var sourceAndDestinationTypes = new Dictionary<Type, HashSet<Type>>();
            
            foreach (var type in mappedTypes)
            {
                ProcessMapsToAttributes(type.Type, type.MapsToAttributes, sourceAndDestinationTypes);
                ProcessMapsFromAttributes(type.Type, type.MapsFromAttributes, sourceAndDestinationTypes);
            }

            ProcessMapsPropertyToAttributes(sourceAndDestinationTypes);
            ProcessMapsPropertyFromAttributes(sourceAndDestinationTypes);
        }

        private static void ProcessMapsFromAttributes(Type type, IEnumerable<MapsFromAttribute> mapsFromAttributes, Dictionary<Type, HashSet<Type>> sourceAndDestinationTypes)
        {
            foreach (var mapsFromAttribute in mapsFromAttributes)
            {
                var sourceType = mapsFromAttribute.SourceType;
                var destinationType = type;

                Mapptivate(mapsFromAttribute, sourceType, destinationType);
                AddToDictionary(sourceAndDestinationTypes, sourceType, destinationType);

                if (mapsFromAttribute.ReverseMap)
                {
                    AddToDictionary(sourceAndDestinationTypes, destinationType, sourceType);
                }
            }
        }

        private static void ProcessMapsToAttributes(Type type, IEnumerable<MapsToAttribute> mapsToAttributes, Dictionary<Type, HashSet<Type>> sourceAndDestinationTypes)
        {
            foreach (var mapsToAttribute in mapsToAttributes)
            {
                var sourceType = type;
                var destinationType = mapsToAttribute.DestinationType;

                Mapptivate(mapsToAttribute, sourceType, destinationType);
                AddToDictionary(sourceAndDestinationTypes, sourceType, destinationType);

                if (mapsToAttribute.ReverseMap)
                {
                    AddToDictionary(sourceAndDestinationTypes, destinationType, sourceType);
                }
            }
        }

        private static void AddToDictionary(Dictionary<Type, HashSet<Type>> dict, Type sourceType, Type destinationType)
        {
            if (!dict.ContainsKey(sourceType))
                dict[sourceType] = new HashSet<Type>();

            dict[sourceType].Add(destinationType);
        }

        private static void ProcessMapsPropertyFromAttributes(Dictionary<Type, HashSet<Type>> sourceAndDestinationTypes)
        {
            var destinationTypes = sourceAndDestinationTypes.SelectMany(t => t.Value).Distinct();
            var typesWithMapToProperties = destinationTypes.Select(t => new
            {
                Type = t,
                MapFromProperties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new
                {
                    Property = p,
                    MapFromAttributes = p.GetCustomAttributes(typeof(MapsFromPropertyAttribute), true).Cast<MapsFromPropertyAttribute>().ToList()
                }).Where(p => p.MapFromAttributes.Any()).ToList()
            }).Where(t => t.MapFromProperties.Any()).ToList();

            foreach (var t in typesWithMapToProperties)
            {
                var destinationType = t.Type;
                foreach (var p in t.MapFromProperties)
                {
                    foreach (var mapToAttribute in p.MapFromAttributes)
                    {
                        var propMapInfo = mapToAttribute.GetPropertyMapInfo(p.Property);
                        if (propMapInfo.DestinationType.IsAssignableFrom(destinationType))
                        {
                            propMapInfo.DestinationType = destinationType;
                            MapProperties(propMapInfo);
                        }
                    }
                }
            }
        }

        private static void ProcessMapsPropertyToAttributes(Dictionary<Type, HashSet<Type>> sourceAndDestinationTypes)
        {
            var sourceTypes = sourceAndDestinationTypes.Keys;
            var typesWithMapToProperties = sourceTypes.Select(t => new
            {
                Type = t,
                MapToProperties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new
                {
                    Property = p,
                    MapToAttributes = p.GetCustomAttributes(typeof(MapsToPropertyAttribute), true).Cast<MapsToPropertyAttribute>().ToList()
                }).Where(p => p.MapToAttributes.Any()).ToList()
            }).Where(t => t.MapToProperties.Any()).ToList();

            foreach (var t in typesWithMapToProperties)
            {
                var sourceType = t.Type;
                foreach (var p in t.MapToProperties)
                {
                    foreach (var mapToAttribute in p.MapToAttributes)
                    {
                        var propMapInfo = mapToAttribute.GetPropertyMapInfo(p.Property);
                        foreach (var destinationType in sourceAndDestinationTypes[sourceType])
                        {
                            if (sourceType.IsAssignableFrom(propMapInfo.SourceType))
                            {
                                propMapInfo.DestinationType = destinationType;
                                MapProperties(propMapInfo);
                            }
                        }
                    }
                }
            }
        }

        private static void MapProperties(PropertyMapInfo propMapInfo)
        {
            var sourceType = propMapInfo.SourceType;
            var sourceProperty = propMapInfo.SourcePropertyInfos;
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

            var finalPropertyType = sourceProperty.Last().PropertyType;
            var propertyExpression = sourceProperty.Aggregate<PropertyInfo, Expression>(null, (current, prop) => current == null ? Expression.Property(sourceParameter, prop) : Expression.Property(current, prop));

            var memberOptions = Expression.Call(memberConfigTypeParameter,
                nameof(IMemberConfigurationExpression<object>.MapFrom),
                new Type[] {finalPropertyType},
                Expression.Lambda(
                    propertyExpression,
                    sourceParameter
                    ));
            
            var forMemberMethod = Expression.Call(mapObjectExpression,
                nameof(IMappingExpression.ForMember),
                Type.EmptyTypes,
                destinationMember,
                Expression.Lambda(memberOptions, memberConfigTypeParameter));

            Expression.Lambda(forMemberMethod).Compile().DynamicInvoke();
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
