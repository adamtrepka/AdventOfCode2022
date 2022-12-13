using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Extensions;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2022.Day._13
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 13: Distress Signal";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllTextAsync("./202213.txt");

            var pairs = input.SplitByEmptyLine();

            var sum = 0;

            for (int index = 0; index < pairs.Length; index++)
            {
                string pair = pairs[index];
                var packets = pair.SplitByLine().Select(Parse).ToList();

                var left = packets.First();
                var right = packets.Last();

                var compareResult = CompareItem(left, right);
                if (compareResult < 0)
                {
                    sum += index + 1;
                }
            }

            return sum.ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllTextAsync("./202213.txt");

            var packets = input.SplitByLine()
                               .Where(x => x != string.Empty)
                               .Select(Parse);

            var additionalPackets = new[]
            {
                Parse("[[2]]"),
                Parse("[[6]]")
            };

            var ordered = packets.Concat(additionalPackets).ToList();

            ordered.Sort(Comparer<Packet>.Create(CompareItem));

            var result = additionalPackets
            .Select(packet => ordered.IndexOf(packet) + 1)
            .Aggregate(1, (a, b) => a * b);


            return result.ToString();
        }

        public abstract record Packet;
        public record PacketValue(int Value) : Packet;
        public record PacketCollection(List<Packet> Packets) : Packet;
        public Packet Parse(string input)
        {
            return Parse(JsonSerializer.Deserialize<JsonElement>(input));
        }
        public Packet Parse(JsonElement json)
        {
            if (json.ValueKind == JsonValueKind.Number)
            {
                return new PacketValue(json.GetInt32());
            }
            else
            {
                return new PacketCollection(json.EnumerateArray().Select(Parse).ToList());
            }
        }

        public int CompareItem(Packet left, Packet right) =>
            (left, right) switch
            {
                (Packet l, null) => +1,
                (null, Packet r) => -1,
                (PacketValue l, PacketValue r) =>
                    Comparer<int>.Default.Compare(l.Value, r.Value),
                (PacketValue l, PacketCollection r) =>
                    CompareItem(new PacketCollection(new() { l }), r),
                (PacketCollection l, PacketValue r) =>
                    CompareItem(l, new PacketCollection(new() { r })),
                (PacketCollection l, PacketCollection r) =>
                    CompareItem(l.Packets, r.Packets).SkipWhile(x => x == 0)
                                                     .FirstOrDefault(),
            };

        public IEnumerable<int> CompareItem(List<Packet> left, List<Packet> right)
        {
            var leftEnumerator = left.GetEnumerator();
            var rightEnumerator = right.GetEnumerator();

            while (true)
            {
                var elementsToCompare = 2;

                var leftPacket = default(Packet);
                var rightPacket = default(Packet);

                if (leftEnumerator.MoveNext())
                {
                    leftPacket = leftEnumerator.Current;
                    elementsToCompare--;
                }

                if (rightEnumerator.MoveNext())
                {
                    rightPacket = rightEnumerator.Current;
                    elementsToCompare--;
                }

                if (elementsToCompare >= 2)
                    yield break;

                yield return CompareItem(leftPacket, rightPacket);
            }
        }
    }
}