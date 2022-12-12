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
        private readonly char _startingPoint = 'S';
        private readonly char _targetPoint = 'E';
        public string Title => "2022 - Day 12: Hill Climbing Algorithm";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202212.txt");

            var visitedPoints = new HashSet<Point>();

            var map = JaggedToMultidimensional(input.Select(line => line.Select(letter => letter).ToArray()).ToArray());

            var startingPoint = FindPoint(map, _startingPoint);
            var targetPoint = FindPoint(map, _targetPoint);

            var currentPoint = startingPoint;

            foreach (var path in FindShortestPath(map, startingPoint, targetPoint, visitedPoints))
            {

            }

            return visitedPoints.Count.ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllTextAsync("./202212.txt");

            throw new NotImplementedException();
        }

        private int GetHeightForLetter(char letter)
        {
            if (_aplhabet.Contains(letter)) return _aplhabet.IndexOf(letter);
            else if (letter == _startingPoint) return _aplhabet.IndexOf('a');
            else if (letter == _targetPoint) return _aplhabet.IndexOf('z');
            throw new ArgumentOutOfRangeException();
        }

        public T[,] JaggedToMultidimensional<T>(T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                cols = jaggedArray[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }

        public Point FindPoint(char[,] map, char letter)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == letter)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new ApplicationException("Map does not contain starting point");
        }

        public IEnumerable<HashSet<Point>> FindShortestPath(char[,] map, Point startingPoint, Point targetPoint, HashSet<Point> visitedPoints)
        {
            var newPath = visitedPoints;

            var currentPoint = startingPoint;

            newPath.Add(currentPoint);

            var moves = new[] {
                    new Point(currentPoint.X, currentPoint.Y - 1),
                    new Point(currentPoint.X, currentPoint.Y + 1),
                    new Point(currentPoint.X - 1, currentPoint.Y),
                    new Point(currentPoint.X + 1, currentPoint.Y)
                }
            .Where(point => point.Y > -1
                            && point.X > -1
                            && point.Y < map.GetLength(0)
                            && point.X < map.GetLength(1)
                            && newPath.Contains(point) == false)
            .ToArray();

            var possibleMoves = moves.Select(point => new
            {
                Point = point,
                Height = GetHeightForLetter(map[point.Y, point.X]),
                Distance = Math.Abs(targetPoint.X - point.X) + Math.Abs(targetPoint.Y - point.Y)
            }).Where(point =>
            {
                var currentPointHeight = GetHeightForLetter(map[currentPoint.Y, currentPoint.X]);

                return Math.Abs(point.Height - currentPointHeight) <= 1;
            }).ToArray();

            foreach (var possibleMove in possibleMoves)
            {
                foreach (var path in FindShortestPath(map, possibleMove.Point, targetPoint, newPath))
                {
                    yield return path;
                }
            }

        }
    }
}