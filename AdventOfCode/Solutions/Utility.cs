using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Solutions
{
    public static class Utility
    {
        public static int[] toIntArray(this string str, string delimiter = "")
        {
            if (delimiter == "")
            {
                var result = new List<int>();
                foreach (char c in str) if (int.TryParse(c.ToString(), out int n)) result.Add(n);
                return result.ToArray();
            }
            return str
                .Split(delimiter)
                .Where(n => int.TryParse(n, out int v))
                .Select(n => Convert.ToInt32(n))
                .ToArray();
        }

        public static long[] toLongArray(this string str, string delimiter = "")
        {
            return str
                .Split(delimiter)
                .Where(n => long.TryParse(n, out long v))
                .Select(n => Convert.ToInt64(n))
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

        public static IEnumerable<IEnumerable<T>> getPermutations<T>(this IEnumerable<T> list)
        {
            return permute(list, list.Count());
        }

        private static IEnumerable<IEnumerable<T>> permute<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return permute(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size)
        {
            for (int i = 0; i < array.Count() / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }

        public static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        public static double angleToVector(this Vector2 a, Vector2 b)
        {
            double angleInRadians = Math.Atan2(a.Y, a.X) - Math.Atan2(b.Y, b.X);
            if (angleInRadians < 0)
                angleInRadians = 2 * Math.PI - Math.Abs(angleInRadians);
            return angleInRadians;
        }

        public static int calculateManhattanDistance((int x, int y) pos1, (int x, int y) pos2)
        {
            return Math.Abs(pos2.x - pos1.x) + Math.Abs(pos2.y - pos1.y);
        }
    }
}
