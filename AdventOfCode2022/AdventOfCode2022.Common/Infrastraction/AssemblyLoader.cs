using System.Reflection;

namespace AdventOfCode2022.Common.Infrastraction
{
    public static class AssemblyLoader
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }
    }
}
