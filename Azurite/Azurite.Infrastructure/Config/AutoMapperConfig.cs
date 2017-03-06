using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Azurite.Infrastructure.Mapping.Contracts;

namespace Azurite.Infrastructure.Config
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var types = Assembly.GetCallingAssembly().GetExportedTypes();

            MapperConfigurationExpression expr = new MapperConfigurationExpression();

            LoadStandardMappings(types, expr);
            LoadCustomMappings(types, expr);

            Mapper.Initialize(expr);
        }

        protected static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression expr)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface &&
                              !typeof(IHaveCustomMappings).IsAssignableFrom(t)
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                expr.CreateMap(map.Source, map.Destination);
                expr.CreateMap(map.Destination, map.Source);
            }
        }

        protected static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression expr)
        {
            var maps = types
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IHaveCustomMappings).IsAssignableFrom(t))
                .Select(t => (IHaveCustomMappings)Activator.CreateInstance(t))
                .ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(expr);
            }
        }
    }
}
