using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day15
    {
        [TestMethod]
        public async Task Part1_ForImputFromExampleAndYEquals10_ShouldReturn26()
        {
            var fileReaderMock = new Mock<ITextFileReader>();

            var solver = new AdventOfCode2022.Day._15.Solver(fileReaderMock.Object);

            var input = new[]
            {
                "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
                "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
                "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
                "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
                "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
                "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
                "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
                "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
                "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
                "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
                "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
                "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
                "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
                "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
            };

            var map = solver.ParseMap(input);

            var result = solver.CountPositionsThatCannotContainsBeacon(10,map).ToString();

            Assert.AreEqual("26", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExampleAndMaxiumumSizeEquals20_ShouldReturn56000011()
        {
            var fileReaderMock = new Mock<ITextFileReader>();

            var solver = new AdventOfCode2022.Day._15.Solver(fileReaderMock.Object);

            var input = new[]
            {
                "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
                "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
                "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
                "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
                "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
                "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
                "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
                "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
                "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
                "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
                "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
                "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
                "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
                "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
            };

            var map = solver.ParseMap(input);

            var result = solver.CountFrequencyForOnlyPossiblePositionOfBeacon(20, map).ToString();

            Assert.AreEqual("56000011", result);
        }
    }
}