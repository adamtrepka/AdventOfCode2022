using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Common.Extensions
{
    public  static class StringExtensions
    {
        private const string NewLine = "\r\n";

        public static string[] SplitByEmptyLine(this string input)
        {
            return input.Split($"{NewLine}{NewLine}");
        }

        public static string[] SplitByLine(this string input)
        {
            return input.Split(NewLine);
        }

        public static bool TryMatchPattern(this string input,string pattern, out string arg)
        {
            var match = Regex.Match(input, pattern);
            if(match.Success)
            {
                arg = match.Groups[match.Groups.Count - 1].Value;
                return true;
            }
            else
            {
                arg = string.Empty;
                return false;
            }
        }
    }
}
