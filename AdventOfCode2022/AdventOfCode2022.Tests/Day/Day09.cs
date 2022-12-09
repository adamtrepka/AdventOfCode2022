using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day09
    {
        [TestMethod]
        public async Task Part1_ForInputFromExample_ShouldReturns13()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var input = new[]
                {
                    "R 4",
                    "U 4",
                    "L 3",
                    "D 1",
                    "R 4",
                    "D 1",
                    "L 5",
                    "R 2"
                };

                return Task.FromResult(input);
            });

            var solver = new AdventOfCode2022.Day._09.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("13", result);
        }

        [TestMethod]
        public async Task Part2_ForInputFromExample_ShouldReturns36()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllLinesAsync(It.IsAny<string>())).Returns(() =>
            {
                var input = new[]
                {
                    "R 5",
                    "U 8",
                    "L 8",
                    "D 3",
                    "R 17",
                    "D 10",
                    "L 25",
                    "U 20"
                };

                return Task.FromResult(input);
            });

            var solver = new AdventOfCode2022.Day._09.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("36", result);
        }
    }
}