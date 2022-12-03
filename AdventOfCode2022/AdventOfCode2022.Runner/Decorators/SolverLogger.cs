using AdventOfCode2022.Common.Abstraction;
using AdventOfCode2022.Common.Infrastraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Runner.Decorators
{
    [Decorator]
    internal class SolverLoggerDecorator : ISolver
    {
        private readonly ISolver _solver;
        private readonly ILogger _logger;
        private readonly string? _solverType;

        public SolverLoggerDecorator(ISolver solver, ILoggerFactory loggerFactory)
        {
            _solver = solver ?? throw new ArgumentNullException(nameof(solver));
            _logger = loggerFactory.CreateLogger(solver.GetType());
            _solverType = solver.GetType().FullName;
        }
        public string Title => _solver.Title;

        public async Task<string> PartOne()
        {
            try
            {
                var answer = await _solver.PartOne();

                _logger.LogInformation($"{Title} - First Part: {answer}");

                return answer;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{_solverType} Error");

                return string.Empty;
            }
        }

        public async Task<string> PartTwo()
        {
            try
            {
                var answer = await _solver.PartTwo();

                _logger.LogInformation($"{Title} - Second Part: {answer}");

                return answer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_solverType} Error");

                return string.Empty;
            }
        }
    }
}
