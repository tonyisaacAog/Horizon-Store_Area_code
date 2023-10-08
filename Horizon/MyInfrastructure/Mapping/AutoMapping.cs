using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace MyInfrastructure.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            //var types = Assembly.GetEntryAssembly()
            //    .GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    .SelectMany(x => x.GetExportedTypes());
                
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !t.IsAbstract &&
                        !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destinsation = t
                        }).ToArray();

            
            foreach (var map in maps)
            {
                CreateMap(map.Source, map.Destinsation).ReverseMap();
            }


            var mapscustom = (from t in types
                              from i in t.GetInterfaces()
                              where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                              select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in mapscustom)
            {
                map.CreateMapping(this);
            }
        }

    }
}
