using AdventOfCode2022.Common.Extensions;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tests.Common
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void SplitByEmptyLine_ForThreeLineStringContainingAnEmptyLine_ShouldReturnTwoStringsCollection()
        {
            var input = "FirstLine\r\n" +
                        "\r\n" +
                        "SecondLine";

            var expectedResult = new[] { "FirstLine", "SecondLine" };

            var result = input.SplitByEmptyLine();

            CollectionAssert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SplitByEmptyLine_ForThreeLineString_ShouldReturnThreeStringCollection()
        {
            var input = "FirstLine\r\n" +
                        "\r\n" +
                        "SecondLine";

            var expectedResult = new[] { "FirstLine","", "SecondLine" };

            var result = input.SplitByLine();

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow("Monkey 0:", "Monkey (\\d+):","0")]
        [DataRow("Operation: new = old * 10", "Operation: new = old \\* (\\d+)", "10")]
        [DataRow("addx 4", "addx (\\d+)", "4")]
        [DataRow("dir brhvclj", "dir (\\w+)", "brhvclj")]
        [DataRow("$ ls", "\\$ ls", "$ ls")]
        public void TryMatchPattern_ForInputDataAndPattern_ShouldReturnExpectedResult(string line, string pattern, string expected)
        {
            var result = line.TryMatchPattern(pattern, out var argument);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, argument);
        }
    }
}
