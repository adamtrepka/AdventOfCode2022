using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2022.Day._15
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 15: Beacon Exclusion Zone";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202215.txt");

            var map = ParseMap(input);

            var row = 2000000;

            var result = CountPositionsThatCannotContainsBeacon(row, map);

            return result.ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202215.txt");

            var map = ParseMap(input);

            var maximumSize = 4_000_000;

            var result = CountFrequencyForOnlyPossiblePositionOfBeacon(maximumSize, map);

            return result.ToString();
        }

        public HashSet<(Sensor Sensor, Beacon Beacon)> ParseMap(string[] input)
        {
            var map = new HashSet<(Sensor, Beacon)>();

            foreach (var line in input)
            {
                var points = line.Split(':').Select(x => x.Trim());

                var sensor = new Sensor(0, 0);
                var beacon = new Beacon(0, 0);

                foreach (var point in points)
                {
                    if (point.TryMatchPattern("Sensor at (.*)", out var sensorCords))
                    {
                        var cords = sensorCords.Split(',').Select(x => x.Trim());

                        foreach (var cord in cords)
                        {
                            if (cord.TryMatchPattern("x=(-?\\d+)", int.Parse, out var x))
                            {
                                sensor.X = x;
                            }

                            if (cord.TryMatchPattern("y=(-?\\d+)", int.Parse, out var y))
                            {
                                sensor.Y = y;
                            }
                        }
                    }

                    if (point.TryMatchPattern("closest beacon is at (.*)", out var beaconCords))
                    {
                        var cords = beaconCords.Split(',').Select(x => x.Trim());
                        foreach (var cord in cords)
                        {
                            if (cord.TryMatchPattern("x=(-?\\d+)", int.Parse, out var x))
                            {
                                beacon.X = x;
                            }

                            if (cord.TryMatchPattern("y=(-?\\d+)", int.Parse, out var y))
                            {
                                beacon.Y = y;
                            }
                        }
                    }

                }

                map.Add((sensor, beacon));
            }

            return map;
        }

        public int CountPositionsThatCannotContainsBeacon(int row, HashSet<(Sensor Sensor, Beacon Beacon)> map)
        {
            var data = map.Select(x => (Points: x, Distance: Math.Abs(x.Sensor.X - x.Beacon.X) + Math.Abs(x.Sensor.Y - x.Beacon.Y))).ToList();

            var minX = data.Select(x => x.Points.Sensor.X - (x.Distance - Math.Abs(row - x.Points.Sensor.Y))).Min();
            var maxX = data.Select(x => x.Points.Sensor.X + (x.Distance - Math.Abs(row - x.Points.Sensor.Y))).Max();

            var points = Enumerable.Range(minX, maxX - minX + 1)
                                   .Where(x => !data.Any(s => (s.Points.Beacon.X, s.Points.Beacon.Y) == (x, row)))
                                   .Where(x => data.Any(s => Math.Abs(x - s.Points.Sensor.X) + Math.Abs(row - s.Points.Sensor.Y) <= s.Distance))
                                   .Count();
            return points;
        }

        public long CountFrequencyForOnlyPossiblePositionOfBeacon(int maximumSize, HashSet<(Sensor Sensor, Beacon Beacon)> map)
        {
            var data = map.Select(x => (Points: x, Distance: Math.Abs(x.Sensor.X - x.Beacon.X) + Math.Abs(x.Sensor.Y - x.Beacon.Y))).ToList();

            var minimumSize = 0;
            var frequencyMultiplicationConstant = 4_000_000L;

            foreach (var element in data)
            {
                var start = Math.Max(0, element.Points.Sensor.X - element.Distance);
                var end = Math.Min(maximumSize,element.Points.Sensor.X + element.Distance);

                for (int i = start; i <= end; i++)
                {
                    var positiveY = Math.Min(element.Points.Sensor.Y + element.Distance + 1 - Math.Abs(element.Points.Sensor.X - i), maximumSize);
                    var negativeY = Math.Max(element.Points.Sensor.Y - element.Distance + 1 - Math.Abs(element.Points.Sensor.X - i), minimumSize);

                    if(data.All(s => Math.Abs(s.Points.Sensor.X - i) + Math.Abs(s.Points.Sensor.Y - positiveY) > s.Distance))
                    {
                        return (i * frequencyMultiplicationConstant) + positiveY;
                    }

                    if (data.All(s => Math.Abs(s.Points.Sensor.X - i) + Math.Abs(s.Points.Sensor.Y - negativeY) > s.Distance))
                    {
                        return (i * frequencyMultiplicationConstant) + negativeY;
                    }
                }
            }

            return 0;
        }
    }

    public abstract record Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
    public record Sensor : Point
    {
        public Sensor(int X, int Y) : base(X, Y)
        {
        }
    }

    public record Beacon : Point
    {
        public Beacon(int X, int Y) : base(X, Y)
        {
        }
    }
}