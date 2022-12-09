using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Day._09
{
    public class Solver : ISolver
    {
        private const string up = "U";
        private const string down = "D";
        private const string left = "L";
        private const string right = "R";

        public string Title => "2022 - Day 9: Rope Bridge";

        public async Task<string> PartOne()
        {
            var head = new Point(0, 0);
            var tail = new Point(0, 0);

            var tailPositions = new HashSet<string>();

            var lines = await System.IO.File.ReadAllLinesAsync("./202209.txt");

            foreach (var line in lines)
            {
                var command = line.Split(' ');

                var direction = command.First();
                var length = int.Parse(command.Last());

                for (int i = 0; i < length; i++)
                {
                    switch (direction)
                    {
                        case up:
                            head.Y--;
                            break;
                        case down:
                            head.Y++;
                            break;
                        case left:
                            head.X--;
                            break;
                        case right:
                            head.X++;
                            break;
                    }

                    if (head.X == tail.X && Math.Abs(head.Y - tail.Y) == 1)
                    {
                        tail.Y = head.Y;
                    }
                    else if (head.Y == tail.Y && Math.Abs(head.X - tail.X) == 1)
                    {
                        tail.X = head.X;
                    }
                    else if (Math.Abs(head.X - tail.X) == 1 && Math.Abs(head.Y - tail.Y) == 1)
                    {
                        tail.X = head.X;
                        tail.Y = head.Y;
                    }
                    else if (Math.Abs(head.X - tail.X) == 2 && Math.Abs(head.Y - tail.Y) == 2)
                    {
                        if (head.X < tail.X)
                        {
                            tail.X--;
                        }
                        else
                        {
                            tail.X++;
                        }
                        if (head.Y < tail.Y)
                        {
                            tail.Y--;
                        }
                        else
                        {
                            tail.Y++;
                        }
                    }

                    tailPositions.Add($"{tail.X},{tail.Y}");

                }
            }

            return tailPositions.Count.ToString();
        }

        public async Task<string> PartTwo()
        {
            throw new NotImplementedException();
        }
    }
}