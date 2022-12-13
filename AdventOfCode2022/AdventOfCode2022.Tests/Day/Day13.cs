using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day13
    {
        [TestMethod]
        public async Task Part1_ForImputFromExample_ShouldReturn13()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllTextAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = "[1,1,3,1,1]\r\n" +
                             "[1,1,5,1,1]\r\n" +
                             "\r\n" +
                             "[[1],[2,3,4]]\r\n" +
                             "[[1],4]\r\n" +
                             "\r\n" +
                             "[9]\r\n" +
                             "[[8,7,6]]\r\n" +
                             "\r\n" +
                             "[[4,4],4,4]\r\n" +
                             "[[4,4],4,4,4]\r\n" +
                             "\r\n" +
                             "[7,7,7,7]\r\n" +
                             "[7,7,7]\r\n" +
                             "\r\n" +
                             "[]\r\n" +
                             "[3]\r\n" +
                             "\r\n" +
                             "[[[]]]\r\n" +
                             "[[]]\r\n" +
                             "\r\n" +
                             "[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n" +
                             "[1,[2,[3,[4,[5,6,0]]]],8,9]";

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._13.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("13", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExample_ShouldReturn140()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllTextAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = "[1,1,3,1,1]\r\n" +
                             "[1,1,5,1,1]\r\n" +
                             "\r\n" +
                             "[[1],[2,3,4]]\r\n" +
                             "[[1],4]\r\n" +
                             "\r\n" +
                             "[9]\r\n" +
                             "[[8,7,6]]\r\n" +
                             "\r\n" +
                             "[[4,4],4,4]\r\n" +
                             "[[4,4],4,4,4]\r\n" +
                             "\r\n" +
                             "[7,7,7,7]\r\n" +
                             "[7,7,7]\r\n" +
                             "\r\n" +
                             "[]\r\n" +
                             "[3]\r\n" +
                             "\r\n" +
                             "[[[]]]\r\n" +
                             "[[]]\r\n" +
                             "\r\n" +
                             "[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n" +
                             "[1,[2,[3,[4,[5,6,0]]]],8,9]";

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._13.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("140", result);
        }
    }
}