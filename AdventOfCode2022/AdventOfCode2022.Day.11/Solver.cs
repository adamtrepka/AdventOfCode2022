using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2022.Day._11
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 11: Monkey in the Middle";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllTextAsync("./202211.txt");

            var setups = input.Split("\r\n\r\n");
            var monkeys = setups.Select(ParseSetup).ToList();

            for (int i = 0; i < 20; i++)
            {
                Round(monkeys, item => item / 3);
            }

            var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedItems)
                                        .Take(2)
                                        .Aggregate(1L, (value, monkey) => value * monkey.InspectedItems);

            return monkeyBusiness.ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllTextAsync("./202211.txt");

            var setups = input.Split("\r\n\r\n");

            var monkeys = setups.Select(ParseSetup).ToList();

            var mod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.Modulo);

            for (int i = 0; i < 10_000; i++)
            {
                Round(monkeys, item => item % mod);
            }

            var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedItems)
                                        .Take(2)
                                        .Aggregate(1L, (value, monkey) => value * monkey.InspectedItems);

            return monkeyBusiness.ToString();
        }

        private Monkey ParseSetup(string setup)
        {
            var lines = setup.Split("\n").Select(x => x.Trim());

            var monkey = new Monkey();

            foreach (var line in lines)
            {
                if (line.StartsWith("Monkey"))
                {
                    var value = int.Parse(line.Replace("Monkey", "").Replace(":", ""));
                    monkey.Index = value;
                }
                else if (line.StartsWith("Starting items:"))
                {
                    var items = line.Replace("Starting items:", "").Split(",").Select(x => long.Parse(x)).ToList();
                    monkey.Items = new Queue<long>(items);
                }
                else if (line.StartsWith("Operation: new = old * old"))
                {
                    monkey.Operation = (input) => input * input;
                }
                else if (line.StartsWith("Operation: new = old *"))
                {
                    var value = long.Parse(line.Replace("Operation: new = old *", ""));

                    monkey.Operation = (input) => input * value;
                }
                else if (line.StartsWith("Operation: new = old +"))
                {
                    var value = long.Parse(line.Replace("Operation: new = old +", ""));

                    monkey.Operation = (input) => input + value;
                }
                else if (line.StartsWith("Test: divisible by "))
                {
                    var value = int.Parse(line.Replace("Test: divisible by ", ""));
                    monkey.Modulo = value;
                }
                else if (line.StartsWith("If true: throw to monkey "))
                {
                    var value = int.Parse(line.Replace("If true: throw to monkey ", ""));
                    monkey.ThrowToMonkeyIfTrue = value;
                }
                else if (line.StartsWith("If false: throw to monkey "))
                {
                    var value = int.Parse(line.Replace("If false: throw to monkey ", ""));
                    monkey.ThrowToMonkeyIfFalse = value;
                }
            }

            return monkey;
        }

        private void Round(List<Monkey> monkeys, Func<long, long> updateWorryLevel)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.InspectedItems++;

                    var item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);
                    item = updateWorryLevel(item);

                    int targetMonkey = monkey.CheckTheTargetMonkey(item);

                    monkeys[targetMonkey].Items.Enqueue(item);
                }
            }
        }
    }

    internal class Monkey
    {
        public int Index { get; set; }
        public Queue<long> Items { get; set; }
        public Func<long, long> Operation { get; set; }
        public int Modulo { get; set; }
        public int ThrowToMonkeyIfTrue { get; set; }
        public int ThrowToMonkeyIfFalse { get; set; }
        public long InspectedItems { get; set; }
        public int CheckTheTargetMonkey(long item)
        {
            return item % Modulo == 0 ? ThrowToMonkeyIfTrue : ThrowToMonkeyIfFalse;
        }

    }
}