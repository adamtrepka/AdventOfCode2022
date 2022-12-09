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
            var lengthOfSnake = 2;

            var snake = Enumerable.Range(0, lengthOfSnake).Select(x => new Point()).ToArray();
            var visited = new HashSet<Point>();

            var lines = await System.IO.File.ReadAllLinesAsync("./202209.txt");

            foreach (var line in lines)
            {
                var command = line.Split(' ');

                var direction = command.First();
                var length = int.Parse(command.Last());

                var moveAction = GetActionToMoveTheSnakeHead(direction);

                for (int i = 0; i < length; i++)
                {
                    ApplyMoveForSnake(snake, moveAction);
                    visited.Add(snake[lengthOfSnake -1]);
                }
            }
            return visited.Count.ToString();
        }

        public async Task<string> PartTwo()
        {
            var lengthOfSnake = 10;

            var snake = Enumerable.Range(0, lengthOfSnake).Select(x => new Point()).ToArray();
            var visited = new HashSet<Point>();

            var lines = await System.IO.File.ReadAllLinesAsync("./202209.txt");

            foreach (var line in lines)
            {
                var command = line.Split(' ');

                var direction = command.First();
                var length = int.Parse(command.Last());

                var moveAction = GetActionToMoveTheSnakeHead(direction);

                for (int i = 0; i < length; i++)
                {
                    ApplyMoveForSnake(snake, moveAction);
                    visited.Add(snake[lengthOfSnake - 1]);
                }
            }
            return visited.Count.ToString();
        }

        private Action<Point[]> GetActionToMoveTheSnakeHead(string direction)
        {
            return direction switch
            {
                right => (point) => ++point[0].X,
                left => (point) => --point[0].X,
                up => (point) => ++point[0].Y,
                down => (point) => --point[0].Y
            };
        }

        private void ApplyMoveForSnake(Point[] snake, Action<Point[]> moveHeadAction)
        {
            moveHeadAction.Invoke(snake);

            for (var i = 1; i < snake.Length; ++i)
            {
                var current = snake[i];
                var previous = snake[i - 1];

                if (Math.Abs(previous.X - current.X) > 1)
                {
                    snake[i].X += previous.X > current.X ? 1 : -1;

                    if (previous.Y != current.Y)
                    {
                        snake[i].Y += previous.Y > current.Y ? 1 : -1;
                    }
                }
                else if (Math.Abs(previous.Y - current.Y) > 1)
                {
                    snake[i].Y += previous.Y > current.Y ? 1 : -1;
                    if (previous.X != current.X)
                    {
                        snake[i].X += previous.X > current.X ? 1 : -1;
                    }
                }
            }
        }
    }
}