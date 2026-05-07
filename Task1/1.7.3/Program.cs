using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Task_1_7_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 10_000_000;
            int[] arr = Enumerable.Range(0, size).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();
            Stopwatch sw = new Stopwatch();

            Console.WriteLine($"Запуск паралельного запиту з перериванням (розмір {size})...");

            Task.Run(() => {
                Thread.Sleep(20);
                cts.Cancel();
            });

            sw.Start();
            try
            {
                var query = arr.AsParallel()
                               .WithCancellation(cts.Token)
                               .Where(x => x > 0)
                               .Select(x => Math.Sqrt(x))
                               .ToList();
            }
            catch (OperationCanceledException)
            {
                sw.Stop();
                Console.WriteLine($"Запит успішно перервано. Час роботи: {sw.Elapsed.TotalMilliseconds} мс");
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}