using AdventOfCode2022.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day._01
{
    internal class Solver : ISolver
    {
        public string Title => "2022 - Day 1: Calorie Counting";

        public async Task<string> PartOne()
        {
            var sumOfCalories = 0;
            var elfs = new List<int>();

            foreach (var line in await File.ReadAllLinesAsync("./202201.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfs.Add(sumOfCalories);
                    sumOfCalories = 0;
                    continue;
                }

                sumOfCalories += int.Parse(line);
            }
            var firstPartAnswer = elfs.Max();

            return firstPartAnswer.ToString();

        }

        public async Task<string> PartTwo()
        {
            var sumOfCalories = 0;
            var elfs = new List<int>();

            foreach (var line in await File.ReadAllLinesAsync("./202201.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfs.Add(sumOfCalories);
                    sumOfCalories = 0;
                    continue;
                }

                sumOfCalories += int.Parse(line);
            }
            var secondPartAnswer = elfs.OrderByDescending(x => x).Take(3).Sum();

            return secondPartAnswer.ToString();
        }
    }
}
