using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions
{
    public static class Utility
    {
        public static int[] toIntArray(this string str, string delimiter = "")
        {
            return str
                .Split(delimiter)
                .Where(n => int.TryParse(n, out int v))
                .Select(n => Convert.ToInt32(n))
                .ToArray();
        }

        public static string[] splitByNewLine(this string str, bool trim = false)
        {
            return str
                .Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => trim ? s.Trim() : s)
                .ToArray();
        }

        public static int[] toDigitArray(this int input)
        {
            List<int> digits = new List<int>();
            while (input > 0)
            {
                digits.Add(input % 10);
                input = input / 10;
            }
            digits.Reverse();
            return digits.ToArray();
        }

        public static string reverse(this string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
