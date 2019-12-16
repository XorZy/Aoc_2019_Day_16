using System;
using System.IO;
using System.Linq;

namespace Day_16
{
    internal class Program
    {
        private static byte[] _cache;

        private static sbyte[] _basePattern = new sbyte[] { 0, 1, 0, -1 };

        private static void Round(ref byte[] input, int from = 0)
        {
            for (int i = from; i < input.Length; i++)
            {
                long sum = 0;

                for (int k = i; k < input.Length; k++)
                {
                    var factor = _basePattern[((k + 1) / (i + 1)) % _basePattern.Length];

                    sum += input[k] * factor;
                }

                _cache[i] = (byte)(Math.Abs(sum) % 10);
            }

            var tmp = input;

            input = _cache;
            _cache = tmp;
        }

        private static void Main(string[] args)
        {

            var input = File.ReadAllText("input3").Select(x => (byte)(x - '0')).ToArray();

            int repeats = 1;

            var adjustedArray = new byte[input.Length * repeats];

            for (int i = 0; i < repeats; i++)
            {
                Buffer.BlockCopy(input, 0, adjustedArray, input.Length * i, input.Length);
            }

            var offset = int.Parse(string.Join("", input.Take(7)));
            offset = 0;
            _cache = new byte[adjustedArray.Length];
            for (int i = 0; i < 100; i++)
            {
                // Console.WriteLine($"Round {i}");
                Round(ref adjustedArray, offset);
            }

            Console.WriteLine(string.Join("", adjustedArray.Skip(offset).Take(8)));
        }
    }
}
