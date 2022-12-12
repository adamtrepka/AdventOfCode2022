using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day12
    {
        [TestMethod]
        public async Task Part1_ForImputFromExample_ShouldReturn31()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = new[] {
                    "Sabqponm",
                    "abcryxxl",
                    "accszExk",
                    "acctuvwj",
                    "abdefghi"
                };

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._12.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("31", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExample_ShouldReturn()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllTextAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = "";

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._12.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("", result);
        }
    }
}