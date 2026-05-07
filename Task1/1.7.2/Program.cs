using System;
using System.Linq;
using System.Diagnostics;

namespace Task_1_7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizes = { 100_000, 1_000_000, 10_000_000 };
            
            foreach (int size in sizes)
            {
                Console.WriteLine($"--- Тестування AsOrdered для розміру: {size} ---");
                int[] arr = Enumerable.Range(0, size).ToArray();
                arr[100] = -1; arr[size - 100] = -2;

                Stopwatch sw = new Stopwatch();

                sw.Start();
                var seq = arr.Where(x => x < 0).ToList();
                sw.Stop();
                Console.WriteLine($"Послідовно: {sw.Elapsed.TotalMilliseconds} мс");

                sw.Restart();
                var parOrdered = arr.AsParallel().AsOrdered().Where(x => x < 0).ToList();
                sw.Stop();
                Console.WriteLine($"Паралельно (AsOrdered): {sw.Elapsed.TotalMilliseconds} мс\n");
            }
        }
    }
}