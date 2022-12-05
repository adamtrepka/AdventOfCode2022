using AdventOfCode2022.Common.Abstraction;

namespace AdventOfCode2022.Day._05
{
    public class Solver : ISolver
    {
        public string Title => "2022 - Day 5: Supply Stacks";

        public async Task<string> PartOne()
        {
            var stack = new List<List<string?>>();

            foreach (var line in await File.ReadAllLinesAsync("./202205_Input.txt"))
            {
                var newLine = line.Replace("    ", " [empty]");

                var cols = newLine.Split(" ").ToList();

                stack.Add(cols);

            }

            var stackArrays = stack.Select(x => x.ToArray()).ToArray();

            string[,] matrix = new string[stackArrays.Length, stackArrays[0].Length];
            for (int i = 0; i < stackArrays.Length; i++)
            {
                for (int j = 0; j < stackArrays[i].Length; j++)
                {
                    matrix[i, j] = stackArrays[i][j];
                }
            }

            var columns = new List<List<string>>();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                columns.Add(GetColumn(matrix, i).Where(x => x != "[empty]").ToList());
            }

            foreach (var line in await File.ReadAllLinesAsync("./202205.txt"))
            {
                var commands = line.Split(new[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var numberOfElementsToMove = commands[0];
                var sourceColumnNumber = commands[1] - 1;
                var targetColumnNumber = commands[2] - 1;

                var sourceElements = columns[sourceColumnNumber].Take(numberOfElementsToMove).Reverse();

                columns[targetColumnNumber].InsertRange(0, sourceElements);

                columns[sourceColumnNumber].RemoveRange(0, numberOfElementsToMove);
            }

            return string.Join("", columns.Select(x => x.First()));
        }

        public async Task<string> PartTwo()
        {
            var stack = new List<List<string?>>();

            foreach (var line in await File.ReadAllLinesAsync("./202205_Input.txt"))
            {
                var newLine = line.Replace("    ", " [empty]");

                var cols = newLine.Split(" ").ToList();

                stack.Add(cols);

            }

            var stackArrays = stack.Select(x => x.ToArray()).ToArray();

            string[,] matrix = new string[stackArrays.Length, stackArrays[0].Length];
            for (int i = 0; i < stackArrays.Length; i++)
            {
                for (int j = 0; j < stackArrays[i].Length; j++)
                {
                    matrix[i, j] = stackArrays[i][j];
                }
            }

            var columns = new List<List<string>>();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                columns.Add(GetColumn(matrix, i).Where(x => x != "[empty]").ToList());
            }

            foreach (var line in await File.ReadAllLinesAsync("./202205.txt"))
            {
                var commands = line.Split(new[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var numberOfElementsToMove = commands[0];
                var sourceColumnNumber = commands[1] - 1;
                var targetColumnNumber = commands[2] - 1;

                var sourceElements = columns[sourceColumnNumber].Take(numberOfElementsToMove);

                columns[targetColumnNumber].InsertRange(0, sourceElements);

                columns[sourceColumnNumber].RemoveRange(0, numberOfElementsToMove);
            }

            return string.Join("", columns.Select(x => x.First()));
        }

        public T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
    }


}