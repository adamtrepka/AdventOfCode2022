//https://adventofcode.com/2022/day/3

await PartOne();
await PartTwo();

static async Task PartOne()
{
    var aplhabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    var acumulator = 0;

    foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
    {
        var rucksacks = line.Chunk(line.Length / 2);

        var intersect = rucksacks.First().Intersect(rucksacks.Skip(1).First()).FirstOrDefault();

        acumulator += (aplhabet.IndexOf(intersect) + 1);
    }

    Console.WriteLine(acumulator);
}

static async Task PartTwo()
{
    var aplhabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    var acumulator = 0;

    var ruckascks = new List<string>();

    foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
    {
        ruckascks.Add(line);
    }

    var groups = ruckascks.Chunk(3);

    foreach(var group in groups)
    {
        var first = group.First();
        var second = group.Skip(1).First();
        var third = group.Skip(2).First();

        var intersect = first.Intersect(second).Intersect(third).FirstOrDefault();

        acumulator += (aplhabet.IndexOf(intersect) + 1);
    }

    Console.WriteLine(acumulator);
}