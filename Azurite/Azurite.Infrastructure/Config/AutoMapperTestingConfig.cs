using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;

namespace Azurite.Infrastructure.Config
{
    public class AutoMapperTestingConfig : AutoMapperConfig
    {
        public static void RegisterMappings(string assembly)
        {
            var types = Assembly.Load(assembly).GetExportedTypes();

            MapperConfigurationExpression expr = new MapperConfigurationExpression();

            LoadStandardMappings(types, expr);
            LoadCustomMappings(types, expr);

            Mapper.Initialize(expr);
        }
    }
}
