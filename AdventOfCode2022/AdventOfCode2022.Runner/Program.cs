using Microsoft.Extensions.DependencyInjection;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Infrastraction;
using AdventOfCode2022.Runner.Decorators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using AdventOfCode2022.Common.Extensions;

var assemblies = AssemblyLoader.GetAssemblies();

var serviceProvider = new ServiceCollection().AddLogging(config => config.AddConsole())
                                             .AddSolvers(assemblies)
                                             .AddDecorators()
                                             .BuildServiceProvider();

var solvers = serviceProvider.GetService<IEnumerable<ISolver>>();

foreach (var solver in solvers)
{
    _ = await solver.PartOne();
    _ = await solver.PartTwo();
}

Console.ReadLine();