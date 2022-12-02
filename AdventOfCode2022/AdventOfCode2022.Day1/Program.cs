//https://adventofcode.com/2022/day/1

var sumOfCalories = 0;
var elfs = new List<int>();

foreach(var line in await File.ReadAllLinesAsync("./input.txt"))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        elfs.Add(sumOfCalories);
        sumOfCalories = 0;
        continue;
    }

    sumOfCalories += int.Parse(line);
}

var firstPartAnswer = elfs.Max();
var secondPartAnswer = elfs.OrderByDescending(x => x).Take(3).Sum();

Console.ReadKey();

