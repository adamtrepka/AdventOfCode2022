using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day14
    {
        [TestMethod]
        public async Task Part1_ForImputFromExample_ShouldReturn24()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = new[]
                {
                    "498,4 -> 498,6 -> 496,6",
                    "503,4 -> 502,4 -> 502,9 -> 494,9"
                };

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._14.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("24", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExample_ShouldReturn93()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = new[]
                {
                    "498,4 -> 498,6 -> 496,6",
                    "503,4 -> 502,4 -> 502,9 -> 494,9"
                };

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._14.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("93", result);
        }
    }
}