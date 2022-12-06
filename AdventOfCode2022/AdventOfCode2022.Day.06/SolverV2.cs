using AdventOfCode2022.Common.Abstraction;

namespace AdventOfCode2022.Day._06
{
    public class SolverV2 : ISolver
    {
        public string Title => "2022 - Day 6v2: Tuning Trouble";

        public async Task<string> PartOne()
        {
            var sizeOfPackage = 4;
            var stream = await File.ReadAllTextAsync("./202206.txt");

            for (int i = 0; i < stream.Length; i++)
            {
                if(stream.Substring(i,sizeOfPackage).Distinct().Count() == sizeOfPackage)
                {
                    return (i + sizeOfPackage).ToString();
                }
            }

            return "Not found";
        }

        public async Task<string> PartTwo()
        {
            var sizeOfPackage = 14;
            var stream = await File.ReadAllTextAsync("./202206.txt");

            for (int i = 0; i < stream.Length; i++)
            {
                if (stream.Substring(i, sizeOfPackage).Distinct().Count() == sizeOfPackage)
                {
                    return (i + sizeOfPackage).ToString();
                }
            }

            return "Not found";
        }
    }
}