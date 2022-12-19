using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day16
    {
        [TestMethod]
        public async Task Part1_ForImputFromExample_ShouldReturn1651()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = new[]
                {
                    "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
                    "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
                    "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
                    "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
                    "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
                    "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
                    "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
                    "Valve HH has flow rate=22; tunnel leads to valve GG",
                    "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
                    "Valve JJ has flow rate=21; tunnel leads to valve II"
                };

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._16.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("1651", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExample_ShouldReturn1707()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = new[]
                {
                    "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
                    "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
                    "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
                    "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
                    "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
                    "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
                    "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
                    "Valve HH has flow rate=22; tunnel leads to valve GG",
                    "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
                    "Valve JJ has flow rate=21; tunnel leads to valve II"
                };

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._16.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("1707", result);
        }
    }
}