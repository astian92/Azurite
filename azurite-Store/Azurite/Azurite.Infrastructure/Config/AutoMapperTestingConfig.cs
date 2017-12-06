using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
