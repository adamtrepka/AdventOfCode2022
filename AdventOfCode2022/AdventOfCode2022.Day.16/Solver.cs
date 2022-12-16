using AdventOfCode2022.Common;
using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
            var maxPreasure = 0;
            var input = await _textFileReader.ReadAllLinesAsync("./202216.txt");

            var scan = Parse(input).ToList();

            var nonZero = scan.Where(x => x.FlowRate > 0 && x.Name != "AA").ToList();

            foreach (var permutation in Permutate(nonZero, nonZero.Count()))
            {
                var releasedPreasure = 0;
                var moves = new Queue<Valve>(permutation);
                var currentValue = scan.First(x => x.Name.Equals("AA"));
                var selectedPath = new Queue<Valve>(FindShortestPath(currentValue, moves.Dequeue()));

                for (int i = 0; i < 30; i++)
                {
                    releasedPreasure += GetFlowForOpenedValves(scan);
                    if (selectedPath?.Count > 0)
                    {
                        currentValue = selectedPath.Dequeue();
                    }
                    else if (selectedPath is not null)
                    {
                        currentValue.IsOpened = true;
                        if (moves.Count > 0)
                        {
                            selectedPath = new Queue<Valve>(FindShortestPath(currentValue, moves.Dequeue()));
                        }
                        else
                        {
                            selectedPath = null;
                        }
                    }
                }

                scan.ForEach(x => x.IsOpened = false);

                if (maxPreasure < releasedPreasure)
                {
                    maxPreasure = releasedPreasure;
                }
            }

            return maxPreasure.ToString();
            //var releasedPreasure = 0;

            //var currentValve = scan.First(x => x.Name.Equals("AA"));
            //var selectedPath = GetPath(currentValve,releasedPreasure,minutesToLeft,0, GetFlowForOpenedValves(scan));


            //for (int i = 0; i < minutesToLeft; i++)
            //{
            //    UpdateReleasedPreasure();
            //    if (selectedPath?.Count > 0)
            //    {
            //        currentValve = selectedPath.Dequeue();
            //    }
            //    else if (selectedPath is not null)
            //    {
            //        currentValve.IsOpened = true;
            //        selectedPath = GetPath(currentValve, releasedPreasure, minutesToLeft, i, GetFlowForOpenedValves(scan));
            //    }
            //}

            //return releasedPreasure.ToString();

            void UpdateReleasedPreasure()
            {
                //releasedPreasure += GetFlowForOpenedValves(scan);
            }

            Queue<Valve> GetPath(Valve start, int preasure, int limit, int minute, int flow)
            {
                var currentCostValue = preasure + (limit - minute) * flow;

                var paths = scan.Where(x => x.IsOpened == false && x.FlowRate > 0 && x != start)
                .Select(valve => (End: valve, Path: FindShortestPath(start, valve)))
                .Select(x => (End: x.End, Path: x.Path, Cost: CountCost(x.End, x.Path.Count)))
                .OrderByDescending(x => x.Cost);

                var selectedPath = paths?.FirstOrDefault().Path?.Where(x => x != start);

                int CountCost(Valve end, int count)
                {
                    var newMinute = minute + count;
                    var newPressure = preasure + (count * end.FlowRate);
                    var newFlow = flow + end.FlowRate;
                    var cost = newPressure + ((limit - newMinute) * newFlow);
                    return (cost - currentCostValue) / count;
                    //return end.FlowRate / count;
                }

                return selectedPath is not null ? new Queue<Valve>(selectedPath) : null;
            }
        }



        public async Task<string> PartTwo()
        {
            var input = await _textFileReader.ReadAllLinesAsync("./202216.txt");
            throw new NotImplementedException();
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
                                       match.Groups["valves"].Value.Split(",").Select(v => v.Trim()).ToList(),
                                       false));
            }

            foreach (var valve in result)
            {
                valve.AvailableValves = result.Where(x => valve.AvailableValvesNames.Contains(x.Name));
            }

            return result;
        }

        public int GetFlowForOpenedValves(List<Valve> valves)
        {
            return valves.Where(x => x.IsOpened).Sum(x => x.FlowRate);
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
            path.Add(start);

            path.Reverse();

            return path;
        }

        public record Valve
        {
            public Valve(string Name, int FlowRate, List<string> AvailableValvesNames, bool IsOpened)
            {
                this.Name = Name;
                this.FlowRate = FlowRate;
                this.AvailableValvesNames = AvailableValvesNames;
                this.IsOpened = IsOpened;
            }

            public string Name { get; set; }
            public int FlowRate { get; set; }
            public List<string> AvailableValvesNames { get; set; }
            public bool IsOpened { get; set; }
            public IEnumerable<Valve> AvailableValves { get; set; }
        }

        public void RotateRight<T>(IList<T> sequence, int count)
        {
            T tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }

        public IEnumerable<IList<T>> Permutate<T>(IList<T> sequence, int count)
        {
            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }
    }
}