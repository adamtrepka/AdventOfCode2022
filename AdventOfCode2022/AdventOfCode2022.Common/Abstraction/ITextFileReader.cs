namespace AdventOfCode2022.Common.Abstraction
{
    public interface ITextFileReader
    {
        Task<string> ReadAllTextAsync(string fileName);
        Task<string[]> ReadAllLinesAsync(string fileName);
    }
}
