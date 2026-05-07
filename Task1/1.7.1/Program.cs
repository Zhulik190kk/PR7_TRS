using System;
using System.Linq;
using System.Diagnostics;

namespace Task_1_7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizes = { 100_000, 1_000_000, 10_000_000 };
            
            foreach (int size in sizes)
            {
                Console.WriteLine($"--- Тестування для розміру: {size} ---");
                int[] arr = new int[size];
                for (int i = 0; i < arr.Length; i++) arr[i] = i;
                
                arr[size / 10] = -1;
                arr[size / 2] = -5;

                Stopwatch sw = new Stopwatch();                sw.Start();
                var seqQuery = (from val in arr
                               where val < 0
                               select val).ToList();
                sw.Stop();
                Console.WriteLine($"Послідовно: {sw.Elapsed.TotalMilliseconds} мс");

                sw.Restart();
                var parQuery = (from val in arr.AsParallel()
                               where val < 0
                               select val).ToList();
                sw.Stop();
                Console.WriteLine($"Паралельно (PLINQ): {sw.Elapsed.TotalMilliseconds} мс\n");
            }
        }
    }
}