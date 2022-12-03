using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Infrastraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Common
{
    public static class Extensions
    {
        public static IServiceCollection AddSolvers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.Scan(scan => scan.FromAssemblies(assemblies)
                                      .AddClasses(classes => classes.AssignableTo<ISolver>()
                                                                    .WithoutAttribute<DecoratorAttribute>())
                                      .AsImplementedInterfaces()
                                      .WithSingletonLifetime());

            return services;
        }
    }
}
