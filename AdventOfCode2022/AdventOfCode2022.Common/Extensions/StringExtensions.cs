using System.Text.RegularExpressions;

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

        public static bool TryMatchPattern<TResult>(this string input, string pattern, Func<string,TResult> parsingMethod, out TResult arg)
        {
            var match = Regex.Match(input, pattern);
            if (match.Success)
            {
                var value = match.Groups[match.Groups.Count - 1].Value;
                arg = parsingMethod(value);

                return true;
            }
            else
            {
                arg = default;
                return false;
            }
        }

        public static bool TryMatchPattern<TResult>(this string input, string pattern, TryParse<TResult> parsingMethod, out TResult arg)
        {
            arg = default;

            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                var value = match.Groups[match.Groups.Count - 1].Value;

                return parsingMethod(value, out arg);
            }
            else
            {
                return false;
            }
        }

        public delegate bool TryParse<TResult>(string input, out TResult output);
    }
}
