using AdventOfCode2022.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day._02
{
    internal class Solver : ISolver
    {
        const string rock = "ROCK";
        const string paper = "PAPER";
        const string scissors = "SCISSORS";

        const int rockPoints = 1;
        const int paperPoints = 2;
        const int scissorsPoints = 3;

        const int lostPoints = 0;
        const int drawPoints = 3;
        const int wonPoints = 6;

        static Dictionary<string, int> points = new Dictionary<string, int>()
        {
            {rock,rockPoints },
            {paper,paperPoints },
            {scissors,scissorsPoints},
        };

        public string Title => "2022 - Day 2: Rock Paper Scissors";

        public async Task<string> PartOne()
        {
            var codes = new Dictionary<string, string>()
            {
                {"A",rock },
                {"B",paper},
                {"C",scissors},
                {"X",rock},
                {"Y",paper},
                {"Z",scissors},
            };

            var firstPartAnswer = 0;

            foreach (var round in await File.ReadAllLinesAsync("./202202.txt"))
            {
                var symbols = round.Split(" ");
                firstPartAnswer += RoundPointsCalculator(symbols[0], symbols[1]);
            }

            return firstPartAnswer.ToString();

            int RoundPointsCalculator(string opponentCode, string myCode)
            {
                var opponentShape = codes[opponentCode];
                var myShape = codes[myCode];

                var myShapePoints = points[myShape];

                //Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
                return (opponentShape, myShape) switch
                {
                    { opponentShape: rock, myShape: rock } => myShapePoints + drawPoints,
                    { opponentShape: rock, myShape: paper } => myShapePoints + wonPoints,
                    { opponentShape: rock, myShape: scissors } => myShapePoints + lostPoints,

                    { opponentShape: paper, myShape: rock } => myShapePoints + lostPoints,
                    { opponentShape: paper, myShape: paper } => myShapePoints + drawPoints,
                    { opponentShape: paper, myShape: scissors } => myShapePoints + wonPoints,

                    { opponentShape: scissors, myShape: rock } => myShapePoints + wonPoints,
                    { opponentShape: scissors, myShape: paper } => myShapePoints + lostPoints,
                    { opponentShape: scissors, myShape: scissors } => myShapePoints + drawPoints,
                };
            }
        }

        public async Task<string> PartTwo()
        {
            const string loseSymbol = "X";
            const string drawSymbol = "Y";
            const string wonSymbol = "Z";

            var codes = new Dictionary<string, string>()
            {
                {"A",rock },
                {"B",paper},
                {"C",scissors},
            };

            var answer = 0;

            foreach (var round in await File.ReadAllLinesAsync("./202202.txt"))
            {
                var symbols = round.Split(" ");
                answer += RoundPointsCalculator(symbols[0], symbols[1]);
            }

            return answer.ToString();

            int RoundPointsCalculator(string opponentCode, string expectedResultCode)
            {
                var opponentShape = codes[opponentCode];
                var myShape = (opponentShape, expectedResultCode) switch
                {
                    { opponentShape: rock, expectedResultCode: loseSymbol } => scissors,
                    { opponentShape: rock, expectedResultCode: drawSymbol } => rock,
                    { opponentShape: rock, expectedResultCode: wonSymbol } => paper,

                    { opponentShape: paper, expectedResultCode: loseSymbol } => rock,
                    { opponentShape: paper, expectedResultCode: drawSymbol } => paper,
                    { opponentShape: paper, expectedResultCode: wonSymbol } => scissors,

                    { opponentShape: scissors, expectedResultCode: loseSymbol } => paper,
                    { opponentShape: scissors, expectedResultCode: drawSymbol } => scissors,
                    { opponentShape: scissors, expectedResultCode: wonSymbol } => rock,
                };

                var myShapePoints = points[myShape];

                //Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
                return (opponentShape, myShape) switch
                {
                    { opponentShape: rock, myShape: rock } => myShapePoints + drawPoints,
                    { opponentShape: rock, myShape: paper } => myShapePoints + wonPoints,
                    { opponentShape: rock, myShape: scissors } => myShapePoints + lostPoints,

                    { opponentShape: paper, myShape: rock } => myShapePoints + lostPoints,
                    { opponentShape: paper, myShape: paper } => myShapePoints + drawPoints,
                    { opponentShape: paper, myShape: scissors } => myShapePoints + wonPoints,

                    { opponentShape: scissors, myShape: rock } => myShapePoints + wonPoints,
                    { opponentShape: scissors, myShape: paper } => myShapePoints + lostPoints,
                    { opponentShape: scissors, myShape: scissors } => myShapePoints + drawPoints,
                };
            }
        }
    }
}
