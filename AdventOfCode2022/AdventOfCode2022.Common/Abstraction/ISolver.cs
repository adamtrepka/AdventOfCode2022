using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Common.Abstraction
{
    public interface ISolver
    {
        public string Title { get; }
        public Task<string> PartOne();
        public Task<string> PartTwo();
    }
}
