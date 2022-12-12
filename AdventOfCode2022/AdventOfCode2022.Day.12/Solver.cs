using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2022.Day._12
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }


        private readonly string _aplhabet = "abcdefghijklmnopqrstuvwxyz";
        private const char _startingPoint = 'S';
        private const char _targetPoint = 'E';
        public string Title => "2022 - Day 12: Hill Climbing Algorithm";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202212.txt");

            List<Map.Point> currentPosition = new();
            Dictionary<Map.Point, int> visitedPlaces = new();

            var map = ParseMap(input, (y, x, value, map) =>
            {
                return value switch
                {
                    _startingPoint => SetStart(y, x),
                    _targetPoint => SetTarget(y, x),
                    _ => new Map.Point(y, x, value - 'a')
                };

                Map.Point SetStart(int y, int x)
                {
                    var point = new Map.Point(y, x, 0);
                    map.StartingPoints.Add(point);

                    return point;
                }

                Map.Point SetTarget(int y, int x)
                {
                    var point = new Map.Point(y, x, 25);
                    map.Target = point;

                    return point;
                }
            });

            map.StartingPoints.ForEach(point =>
            {
                currentPosition.Add(point);
                visitedPlaces.Add(point, 0);
            });

            Solve(map, currentPosition, visitedPlaces);

            return visitedPlaces[map.Target].ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202212.txt");

            List<Map.Point> currentPosition = new();
            Dictionary<Map.Point, int> visitedPlaces = new();

            var map = ParseMap(input, (y, x, value, map) =>
            {
                return value switch
                {
                    'a' => SetStart(y, x),
                    _startingPoint => SetStart(y, x),
                    _targetPoint => SetTarget(y, x),
                    _ => new Map.Point(y, x, value - 'a')
                };

                Map.Point SetStart(int y, int x)
                {
                    var point = new Map.Point(y, x, 0);
                    map.StartingPoints.Add(point);

                    return point;
                }

                Map.Point SetTarget(int y, int x)
                {
                    var point = new Map.Point(y, x, 25);
                    map.Target = point;

                    return point;
                }
            });

            map.StartingPoints.ForEach(point =>
            {
                currentPosition.Add(point);
                visitedPlaces.Add(point, 0);
            });

            Solve(map, currentPosition, visitedPlaces);

            return visitedPlaces[map.Target].ToString();
        }

        public Map ParseMap(IReadOnlyList<string> input, Func<int, int, char, Map, Map.Point> mappingFunc)
        {
            var map = new Map();

            map.Value = new Map.Point[input.Count, input[0].Length];

            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    var value = input[y][x];

                    map.Value[y, x] = mappingFunc.Invoke(y, x, value, map);
                }
            }

            return map;
        }

        public IEnumerable<(int y, int x)> GetMoves(Map.Point currentPoint)
        {
            yield return new(currentPoint.Y, currentPoint.X - 1);
            yield return new(currentPoint.Y, currentPoint.X + 1);
            yield return new(currentPoint.Y - 1, currentPoint.X);
            yield return new(currentPoint.Y + 1, currentPoint.X);
        }

        public void Solve(Map map, List<Map.Point> currentPosition, Dictionary<Map.Point,int> visitedPlaces)
        {
            while (!visitedPlaces.ContainsKey(map.Target))
            {
                var copyOfCurrentPositions = currentPosition.ToList();

                for (int i = 0; i < copyOfCurrentPositions.Count; i++)
                {
                    var position = currentPosition[i];

                    var currentDistance = visitedPlaces[position];
                    var currentPostionHeight = position.Height;

                    foreach (var move in GetMoves(position))
                    {
                        var newY = move.y;
                        var newX = move.x;

                        if (!map.CanMove(newY, newX)) continue; //check if the new location is off the map

                        var newPosition = map.Value[newY, newX]; //get new position point on map

                        if (visitedPlaces.ContainsKey(newPosition)) continue; //check if the place has already been visited

                        if (newPosition.Height > currentPostionHeight + 1) continue; //check the height of the new position

                        currentPosition.Add(newPosition);
                        visitedPlaces.Add(newPosition, currentDistance + 1);
                    }
                }

                currentPosition.RemoveAll(x => copyOfCurrentPositions.Contains(x)); //remove all recently checked points

            }
        }

        public class Map
        {
            public Point[,] Value { get; set; }

            public int Width => Value.GetLength(1);
            public int Height => Value.GetLength(0);

            public List<Point> StartingPoints { get; set; } = new();
            public Point Target { get; set; }

            public bool CanMove(int y, int x)
            {
                return !(x < 0 || y < 0 || x >= Width || y >= Height);
            }

            public record Point(int Y, int X, int Height);
        }
    }
}