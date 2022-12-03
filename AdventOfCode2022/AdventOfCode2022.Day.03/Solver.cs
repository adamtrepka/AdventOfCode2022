using AdventOfCode2022.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day._03
{
    internal class Solver : ISolver
    {
        public string Title => "2022 - Day 3: Rucksack Reorganization";

        public async Task<string> PartOne()
        {
            var aplhabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var acumulator = 0;

            foreach (var line in await File.ReadAllLinesAsync("./202203.txt"))
            {
                var rucksacks = line.Chunk(line.Length / 2);

                var intersect = rucksacks.First().Intersect(rucksacks.Skip(1).First()).FirstOrDefault();

                acumulator += (aplhabet.IndexOf(intersect) + 1);
            }

            return acumulator.ToString();
        }

        public async Task<string> PartTwo()
        {
            var aplhabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var acumulator = 0;

            var ruckascks = new List<string>();

            foreach (var line in await File.ReadAllLinesAsync("./202203.txt"))
            {
                ruckascks.Add(line);
            }

            var groups = ruckascks.Chunk(3);

            foreach (var group in groups)
            {
                var first = group.First();
                var second = group.Skip(1).First();
                var third = group.Skip(2).First();

                var intersect = first.Intersect(second).Intersect(third).FirstOrDefault();

                acumulator += (aplhabet.IndexOf(intersect) + 1);
            }

            return acumulator.ToString();
        }
    }
}
