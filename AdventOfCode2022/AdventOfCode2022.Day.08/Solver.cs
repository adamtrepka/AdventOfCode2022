﻿using AdventOfCode2022.Common.Abstraction;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Day._08
{
    public class Solver : ISolver
    {
        public string Title => "2022 - Day 8: Treetop Tree House";

        public async Task<string> PartOne()
        {
            var lines = await System.IO.File.ReadAllLinesAsync("./202208.txt");

            int[][] list = lines
                   .Select(l => l.Select(i => int.Parse(i.ToString())).ToArray())
                   .ToArray();

            var grid = JaggedToMultidimensional(list);

            var xMin = 0;
            var xMax = list.First().Length - 1;
            var yMin = 0;
            var yMax = list.Length - 1;

            var counter = 0;

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var neighbours = new List<int>();

                    var tree = grid[y, x];

                    if (y == 0 || y == yMax || x == 0 || x == xMax)
                    {
                        counter++;
                        continue;
                    }

                    var left = GetRow(grid, y).Take(x).All(neighbour => neighbour < tree);
                    var right = GetRow(grid, y).Skip(x + 1).All(neighbour => neighbour < tree);

                    var top = GetColumn(grid, x).Take(y).All(neighbour => neighbour < tree);
                    var bottom = GetColumn(grid, x).Skip(y + 1).All(neighbour => neighbour < tree);

                    if (left || right || top || bottom)
                    {
                        counter++;
                    }
                }
            }

            return counter.ToString();
        }

        public async Task<string> PartTwo()
        {
            throw new NotImplementedException();
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

        public T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public T[] GetRow<T>(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}