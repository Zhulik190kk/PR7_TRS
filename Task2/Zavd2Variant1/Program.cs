using System;
using System.Linq;
using System.Diagnostics;

namespace Zavd2_Variant1
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 100_000_000;
            int[] arr = new int[size]; 

            Random rand = new Random();
            for (int i = 0; i < 1000; i++) 
            {
                arr[rand.Next(0, size)] = 7;
            }

            Console.WriteLine($"Обробка масиву розміром {size}...");

            Stopwatch sw = new Stopwatch();

            sw.Start();
            int zeroCount = arr.AsParallel()
                               .Where(x => x == 0)
                               .Count();
            sw.Stop();

            Console.WriteLine($"Кількість нулів: {zeroCount}");
            Console.WriteLine($"Час паралельного виконання: {sw.Elapsed.TotalMilliseconds} мс");
        }
    }
}