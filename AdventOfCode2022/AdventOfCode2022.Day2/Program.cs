//https://adventofcode.com/2022/day/2

internal class Program
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

    private static async Task Main(string[] args)
    {
        await FirstPart();
        await SecondPart();
    }

    private static async Task FirstPart()
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

        foreach (var round in await File.ReadAllLinesAsync("./input.txt"))
        {
            var symbols = round.Split(" ");
            firstPartAnswer += RoundPointsCalculator(symbols[0], symbols[1]);
        }

        Console.WriteLine(firstPartAnswer);

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

    private static async Task SecondPart()
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

        foreach (var round in await File.ReadAllLinesAsync("./input.txt"))
        {
            var symbols = round.Split(" ");
            answer += RoundPointsCalculator(symbols[0], symbols[1]);
        }

        Console.WriteLine(answer);

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