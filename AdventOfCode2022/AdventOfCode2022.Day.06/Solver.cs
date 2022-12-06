using AdventOfCode2022.Common.Abstraction;
using System.IO;

namespace AdventOfCode2022.Day._06
{
    public class Solver : ISolver
    {
        public string Title => "2022 - Day 6: Tuning Trouble";

        public async Task<string> PartOne()
        {
            var sizeOfPacket = 4;

            var stream = await File.ReadAllTextAsync("./202206.txt");

            var package = new List<char>();

            var indexOfStartOfStream = 0;

            foreach (var letter in stream)
            {
                indexOfStartOfStream++;
                
                if(package.Count == sizeOfPacket)
                {
                    package.RemoveAt(0);
                }
                
                package.Add(letter);

                if(package.Count == sizeOfPacket && package.GroupBy(x => x).All(x => x.Count() == 1))
                {
                    break;
                }
            }


            return indexOfStartOfStream.ToString();
        }

        public async Task<string> PartTwo()
        {
            
            var sizeOfMessage = 14;

            var stream = await File.ReadAllTextAsync("./202206.txt");

            var package = new List<char>();

            var indexOfStartOfStream = int.Parse(await PartOne());
            var indexOfStartMessage = 0;

            foreach (var letter in stream.Skip(indexOfStartOfStream))
            {
                indexOfStartMessage++;

                if (package.Count == sizeOfMessage)
                {
                    package.RemoveAt(0);
                }

                package.Add(letter);

                if (package.Count == sizeOfMessage && package.GroupBy(x => x).All(x => x.Count() == 1))
                {
                    break;
                }
            }


            return (indexOfStartOfStream + indexOfStartMessage).ToString();
        }
    }
}