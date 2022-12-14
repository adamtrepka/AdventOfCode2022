using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Extensions;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2022.Day._14
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 14: Regolith Reservoir";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202214.txt");

            var map = new HashSet<(int x, int y)>();

            foreach (var line in input)
            {
                var points = line.Split(" -> ")
                                .Select(x => x.Split(','))
                                .Select(cords => (x: int.Parse(cords.First()), y: int.Parse(cords.Last())));

                var paths = points.Zip(points.Skip(1), (a, b) => new[] { a, b })
                                  .Select(points => (start: points.First(), end: points.Last()));

                foreach (var path in paths)
                {
                    if (path.start.x == path.end.x)
                    {
                        var min = Math.Min(path.start.y, path.end.y);
                        var max = Math.Max(path.start.y, path.end.y);

                        for (int y = min; y <= max; y++)
                        {
                            map.Add((x: path.start.x, y: y));
                        }
                    }
                    else
                    {
                        var min = Math.Min(path.start.x, path.end.x);
                        var max = Math.Max(path.start.x, path.end.x);

                        for (int x = min; x <= max; x++)
                        {
                            map.Add((x: x, y: path.start.y));
                        }
                    }
                }
            }

            var result = 0;
            var maxY = map.Max(x => x.y);

            while (true)
            {
                var position = (x: 500, y: 0);

                if (map.Contains(position))
                {
                    return result.ToString();
                }

                while (true)
                {
                    var nextPosition = (x: position.x, y: position.y + 1);

                    if (nextPosition.y > maxY)
                    {
                        return result.ToString();
                    }

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    nextPosition = (position.x - 1, position.y + 1);

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    nextPosition = (position.x + 1, position.y + 1);

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    break;
                }

                map.Add(position);
                result++;
            }

        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202214.txt");

            var map = new HashSet<(int x, int y)>();

            foreach (var line in input)
            {
                var points = line.Split(" -> ")
                                .Select(x => x.Split(','))
                                .Select(cords => (x: int.Parse(cords.First()), y: int.Parse(cords.Last())));

                var paths = points.Zip(points.Skip(1), (a, b) => new[] { a, b })
                                  .Select(points => (start: points.First(), end: points.Last()));

                foreach (var path in paths)
                {
                    if (path.start.x == path.end.x)
                    {
                        var min = Math.Min(path.start.y, path.end.y);
                        var max = Math.Max(path.start.y, path.end.y);

                        for (int y = min; y <= max; y++)
                        {
                            map.Add((x: path.start.x, y: y));
                        }
                    }
                    else
                    {
                        var min = Math.Min(path.start.x, path.end.x);
                        var max = Math.Max(path.start.x, path.end.x);

                        for (int x = min; x <= max; x++)
                        {
                            map.Add((x: x, y: path.start.y));
                        }
                    }
                }
            }

            var result = 0;
            var maxY = map.Max(x => x.y) + 2;
            var minX = map.Min(x => x.x);
            var maxX = map.Max(x => x.x);

            for (var x = minX - maxY; x < maxX + maxY; x++)
            {
                map.Add((x, maxY));
            }

            while (true)
            {
                var position = (x: 500, y: 0);

                if (map.Contains(position))
                {
                    return result.ToString();
                }

                while (true)
                {
                    var nextPosition = (x: position.x, y: position.y + 1);

                    if (nextPosition.y > maxY)
                    {
                        return result.ToString();
                    }

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    nextPosition = (position.x - 1, position.y + 1);

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    nextPosition = (position.x + 1, position.y + 1);

                    if (!map.Contains(nextPosition))
                    {
                        position = nextPosition;
                        continue;
                    }

                    break;
                }

                map.Add(position);
                result++;
            }
        }


    }
}