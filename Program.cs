using System;
using System.IO;
using System.Linq;

namespace Day_16
{
    internal class Program
    {
        private static byte[] _cache;

        private static void Round(ref byte[] input, int from = 0)
        {
            var longsum = 0;

            for (int k = from; k < input.Length; k++)
            {
                longsum += input[k];
            }

            for (int i = from; i < input.Length; i++)
            {
                _cache[i] = (byte)(longsum % 10);
                longsum -= input[i];
            }

            var tmp = input;

            input = _cache;
            _cache = tmp;
        }

        private static void Main(string[] args)
        {

            var input = File.ReadAllText("input").Select(x => (byte)(x - '0')).ToArray();

            int repeats = 10_000;

            var adjustedArray = new byte[input.Length * repeats];

            for (int i = 0; i < repeats; i++)
            {
                Buffer.BlockCopy(input, 0, adjustedArray, input.Length * i, input.Length);
            }

            var offset = int.Parse(string.Join("", input.Take(7)));

            _cache = new byte[adjustedArray.Length];
            for (int i = 0; i < 100; i++)
            {
                Round(ref adjustedArray, offset);
            }

            Console.WriteLine(string.Join("", adjustedArray.Skip(offset).Take(8)));
        }
    }
}
