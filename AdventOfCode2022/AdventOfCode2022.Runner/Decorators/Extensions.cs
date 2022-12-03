using AdventOfCode2022.Common.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Runner.Decorators
{
    internal static class Extensions
    {
        public static IServiceCollection AddDecorators(this IServiceCollection services)
        {
            services.TryDecorate<ISolver, SolverLoggerDecorator>();

            return services;
        }
    }
}
