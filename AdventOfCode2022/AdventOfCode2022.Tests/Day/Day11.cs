using AdventOfCode2022.Common.Abstraction;
using Moq;

namespace AdventOfCode2022.Tests.Day
{
    [TestClass]
    public class Day11
    {
        [TestMethod]
        public async Task Part1_ForImputFromExample_ShouldReturn10605()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllTextAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = "Monkey 0:\r\n" +
                             "  Starting items: 79, 98\r\n" +
                             "  Operation: new = old * 19\r\n" +
                             "  Test: divisible by 23\r\n" +
                             "    If true: throw to monkey 2\r\n" +
                             "    If false: throw to monkey 3\r\n" +
                             "\r\n" +
                             "Monkey 1:\r\n" +
                             "  Starting items: 54, 65, 75, 74\r\n" +
                             "  Operation: new = old + 6\r\n" +
                             "  Test: divisible by 19\r\n" +
                             "    If true: throw to monkey 2\r\n" +
                             "    If false: throw to monkey 0\r\n" +
                             "\r\n" +
                             "Monkey 2:\r\n" +
                             "  Starting items: 79, 60, 97\r\n" +
                             "  Operation: new = old * old\r\n" +
                             "  Test: divisible by 13\r\n" +
                             "    If true: throw to monkey 1\r\n" +
                             "    If false: throw to monkey 3\r\n" +
                             "\r\n" +
                             "Monkey 3:\r\n" +
                             "  Starting items: 74\r\n" +
                             "  Operation: new = old + 3\r\n" +
                             "  Test: divisible by 17\r\n" +
                             "    If true: throw to monkey 0\r\n" +
                             "    If false: throw to monkey 1";

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._11.Solver(fileReaderMock.Object);

            var result = await solver.PartOne();

            Assert.AreEqual("10605", result);
        }

        [TestMethod]
        public async Task Part2_ForImputFromExample_ShouldReturn2713310158()
        {
            var fileReaderMock = new Mock<ITextFileReader>();
            fileReaderMock.Setup(reader => reader.ReadAllTextAsync(It.IsAny<string>())).Returns(() =>
            {
                var result = "Monkey 0:\r\n" +
                             "  Starting items: 79, 98\r\n" +
                             "  Operation: new = old * 19\r\n" +
                             "  Test: divisible by 23\r\n" +
                             "    If true: throw to monkey 2\r\n" +
                             "    If false: throw to monkey 3\r\n" +
                             "\r\n" +
                             "Monkey 1:\r\n" +
                             "  Starting items: 54, 65, 75, 74\r\n" +
                             "  Operation: new = old + 6\r\n" +
                             "  Test: divisible by 19\r\n" +
                             "    If true: throw to monkey 2\r\n" +
                             "    If false: throw to monkey 0\r\n" +
                             "\r\n" +
                             "Monkey 2:\r\n" +
                             "  Starting items: 79, 60, 97\r\n" +
                             "  Operation: new = old * old\r\n" +
                             "  Test: divisible by 13\r\n" +
                             "    If true: throw to monkey 1\r\n" +
                             "    If false: throw to monkey 3\r\n" +
                             "\r\n" +
                             "Monkey 3:\r\n" +
                             "  Starting items: 74\r\n" +
                             "  Operation: new = old + 3\r\n" +
                             "  Test: divisible by 17\r\n" +
                             "    If true: throw to monkey 0\r\n" +
                             "    If false: throw to monkey 1";

                return Task.FromResult(result);
            });

            var solver = new AdventOfCode2022.Day._11.Solver(fileReaderMock.Object);

            var result = await solver.PartTwo();

            Assert.AreEqual("2713310158", result);
        }
    }
}