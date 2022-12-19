using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static AdventOfCode2022.Day._16.Solver;

namespace AdventOfCode2022.Day._16
{
    public class Solver : ISolver
    {
        private readonly ITextFileReader _textFileReader;

        public Solver(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        }

        public string Title => "2022 - Day 16: Proboscidea Volcanium";

        public async Task<string> PartOne()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202216.txt");

            var scan = Parse(input).ToList();

            var importantValves = scan.Where(x => x.FlowRate > 0);
            var startValve = scan.First(x => x.Name == "AA");

            var distances = new Dictionary<(Valve start, Valve end), int>();

            foreach(var start in scan.Where(x => x.Name == "AA" || x.FlowRate > 0))
            foreach(var end in scan.Where(x => x.Name == "AA" || x.FlowRate > 0))
                {
                    distances.Add((start, end), FindShortestPath(start, end).Count);
                }

            int maxPreasure = 0;

            Search(startValve, importantValves, (30, 0));

            void Search(Valve from, IEnumerable<Valve> targets, (int time, int preasure) previous)
            {
                foreach (var target in targets)
                {
                    var move = Move(from, target, previous);
                    if (move.time >= 0)
                    {
                        if (move.preasure > maxPreasure)
                        {
                            maxPreasure = move.preasure;
                        }

                        if(targets.Count() > 1)
                        {
                            Search(target, targets.Where(x => x.Name != target.Name), move);
                        }
                    }
                }
            }

            (int time, int preasure) Move(Valve from, Valve to, (int time, int preasure) previous)
            {

                var path = distances[(from, to)];
                var newTime = previous.time - path - 1;
                var newPreasure = previous.preasure + (newTime * to.FlowRate);

                return (newTime, newPreasure);
            }

            return maxPreasure.ToString();
        }

        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202216.txt");

            var scan = Parse(input).ToList();

            var importantValves = scan.Where(x => x.FlowRate > 0);
            var startValve = scan.First(x => x.Name == "AA");

            var distances = new Dictionary<(Valve start, Valve end), int>();

            foreach (var start in scan.Where(x => x.Name == "AA" || x.FlowRate > 0))
                foreach (var end in scan.Where(x => x.Name == "AA" || x.FlowRate > 0))
                {
                    distances.Add((start, end), FindShortestPath(start, end).Count);
                }

            int maxPreasure = 0;

            Search(startValve, importantValves, (26, 0),false);

            void Search(Valve from, IEnumerable<Valve> targets, (int time, int preasure) previous, bool elephant)
            {
                foreach (var target in targets)
                {
                    var move = Move(from, target, previous);
                    if (move.time >= 0)
                    {
                        if (move.preasure > maxPreasure)
                        {
                            maxPreasure = move.preasure;
                        }
                        

                        if (targets.Count() > 1)
                        {
                            Search(target, targets.Where(x => x.Name != target.Name), move,elephant);
                        }
                    }
                    else if(!elephant && previous.preasure >= maxPreasure / 2)
                    {
                        Search(startValve, targets, (26, previous.preasure),true);
                    }
                }
            }

            (int time, int preasure) Move(Valve from, Valve to, (int time, int preasure) previous)
            {

                var path = distances[(from, to)];
                var newTime = previous.time - path - 1;
                var newPreasure = previous.preasure + (newTime * to.FlowRate);

                return (newTime, newPreasure);
            }

            return maxPreasure.ToString();
        }

        public List<Valve> Parse(string[] input)
        {
            var result = new List<Valve>();

            var regex = new Regex("Valve (?<name>\\w+) has flow rate=(?<flowRate>\\d+); (tunnels|tunnel) (lead|leads) to (valve|valves) (?<valves>.*)");
            foreach (var line in input)
            {
                var match = regex.Match(line);
                result.Add(new Valve(match.Groups["name"].Value,
                                       int.Parse(match.Groups["flowRate"].Value),
                                       match.Groups["valves"].Value.Split(",").Select(v => v.Trim()).ToList()));
            }

            foreach (var valve in result)
            {
                valve.AvailableValves = result.Where(x => valve.AvailableValvesNames.Contains(x.Name));
            }

            return result;
        }

        public List<Valve> FindShortestPath(Valve start, Valve end)
        {
            Dictionary<Valve, int> smallestWeights = new();
            smallestWeights.Add(start, 0);

            Dictionary<Valve, Valve> prevNodes = new();
            Queue<Valve> nodesToVisitQueue = new();

            HashSet<Valve> visitedNodes = new();
            visitedNodes.Add(start);

            Valve currentNode = start;

            while (true)
            {
                int dist = smallestWeights[currentNode];
                Dictionary<Valve, int> childNodes = currentNode.AvailableValves.ToDictionary(x => x, x => 1);

                foreach (var entry in childNodes)
                {
                    var childNode = entry.Key;
                    var weight = entry.Value;

                    if (!visitedNodes.Contains(childNode))
                    {
                        nodesToVisitQueue.Enqueue(childNode);
                    }

                    var thisDist = dist + weight;

                    if (prevNodes.ContainsKey(childNode))
                    {
                        int altDist = smallestWeights[childNode];

                        if (thisDist < altDist)
                        {
                            prevNodes[childNode] = currentNode;
                            smallestWeights[childNode] = thisDist;
                        }
                    }
                    else
                    {
                        prevNodes[childNode] = currentNode;
                        smallestWeights[childNode] = thisDist;
                    }
                }

                visitedNodes.Add(currentNode);

                if (nodesToVisitQueue.Count == 0)
                {
                    break;
                }

                currentNode = nodesToVisitQueue.Dequeue();
            }

            var path = new List<Valve>();
            currentNode = end;
            while (currentNode != start)
            {
                path.Add(currentNode);

                currentNode = prevNodes[currentNode];
            }
            //path.Add(start);

            path.Reverse();

            return path;
        }

        public record Valve
        {
            public Valve(string Name, int FlowRate, List<string> AvailableValvesNames)
            {
                this.Name = Name;
                this.FlowRate = FlowRate;
                this.AvailableValvesNames = AvailableValvesNames;
            }

            public string Name { get; set; }
            public int FlowRate { get; set; }
            public List<string> AvailableValvesNames { get; set; }
            public IEnumerable<Valve> AvailableValves { get; set; }
        }
    }
}