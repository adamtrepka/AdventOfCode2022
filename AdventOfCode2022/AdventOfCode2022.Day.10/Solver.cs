using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2022.Day._10
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        private const string NOOP = "noop";
        private const string ADDX = "addx";

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 10: Cathode-Ray Tube";

        public async Task<string> PartOne()
        {

            var lines = await _textFileReader.ReadAllLinesAsync("./202210.txt");

            var cycles = new List<Cycle>()
            {
                new Cycle(0,1)
            };

            foreach (var line in lines)
            {
                ParseCommand(line, cycles);
            }

            var _20Cycles = cycles[19].X * 20; //420 = 20 * 21
            var _60Cycles = cycles[59].X * 60; //1140 = 60 * 19
            var _100Cycles = cycles[99].X * 100; //1800 = 100 * 18
            var _140Cycles = cycles[139].X * 140; //2940 = 140 * 21
            var _180Cycles = cycles[179].X * 180; //2880 = 180 * 16
            var _220Cycles = cycles[219].X * 220; //3960 = 220 * 18

            return (_20Cycles + _60Cycles + _100Cycles + _140Cycles + _180Cycles + _220Cycles).ToString();
        }

        public async Task<string> PartTwo()
        {
            var lines = await _textFileReader.ReadAllLinesAsync("./202210.txt");

            var cycles = new List<Cycle>()
            {
                new Cycle(0,1)
            };

            foreach (var line in lines)
            {
                ParseCommand(line, cycles);
            }

            var stringBuilder = string.Empty;

            foreach (var signal in cycles)
            {
                var spriteMiddle = signal.X;
                var screenColumn = signal.Index % 40;

                stringBuilder += Math.Abs(spriteMiddle - screenColumn) < 2 ? "X" : ".";

                if(screenColumn == 39)
                {
                    stringBuilder += "\n";
                }
            }
            var result = stringBuilder.ToString();

            return result;
        }

        private void ParseCommand(string line, List<Cycle> cycles)
        {
            if (line.Equals(NOOP))
            {
                cycles.Add(new Cycle(cycles.Last().Index + 1, cycles.Last().X));
            }
            else
            {
                var command = line.Split(" ");
                var value = int.Parse(command.Last());

                cycles.Add(new Cycle(cycles.Last().Index + 1, cycles.Last().X));
                cycles.Add(new Cycle(cycles.Last().Index + 1, cycles.Last().X + value));
            }
        }
    }

    internal record Cycle(int Index, int X);
}