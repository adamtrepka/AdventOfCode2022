using AdventOfCode2022.Common.Abstraction;

namespace AdventOfCode2022.Day._04
{
    public class Solver : ISolver
    {
        public string Title => "2022 - ";

        public async Task<string> PartOne()
        {
            var counter = 0;
            foreach (var line in await File.ReadAllLinesAsync("./202204.txt"))
            {
                var pairs = line.Split(",");

                var firstRangeIds = pairs[0].Split("-").Select(int.Parse);
                var secondRangeIds = pairs[1].Split("-").Select(int.Parse);

                var firstRange = new Range(firstRangeIds.First(), firstRangeIds.Last());
                var secondRange = new Range(secondRangeIds.First(), secondRangeIds.Last());

                if (Contains(firstRange, secondRange) || Contains(secondRange,firstRange))
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        public async Task<string> PartTwo()
        {
            var counter = 0;
            foreach (var line in await File.ReadAllLinesAsync("./202204.txt"))
            {
                var pairs = line.Split(",");

                var firstRangeIds = pairs[0].Split("-").Select(int.Parse);
                var secondRangeIds = pairs[1].Split("-").Select(int.Parse);

                var firstRange = new Range(firstRangeIds.First(), firstRangeIds.Last());
                var secondRange = new Range(secondRangeIds.First(), secondRangeIds.Last());

                if (Overlap(firstRange, secondRange) || Overlap(secondRange, firstRange))
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        private bool Contains(Range firstRange, Range secondRange)
        {
            return firstRange.from <= secondRange.from && secondRange.to <= firstRange.to;
        }

        private bool Overlap(Range firstRange, Range secondRange)
        {
            return firstRange.to >= secondRange.from && firstRange.from <= secondRange.to;
        }

        private record struct Range(int from, int to);
    }


}