using AdventOfCode2022.Common.Abstraction;

namespace AdventOfCode2022.Common.Infrastraction
{
    internal class TextFileReader : ITextFileReader
    {
        public Task<string[]> ReadAllLinesAsync(string fileName)
        {
            return File.ReadAllLinesAsync(fileName);
        }

        public Task<string> ReadAllTextAsync(string fileName)
        {
            return File.ReadAllTextAsync(fileName);
        }
    }
}
