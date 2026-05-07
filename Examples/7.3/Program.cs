using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Ex_7_3
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancelTokSrc = new
                CancellationTokenSource();
            int[] arr = new int[10000000];
            for (int i = 0; i < arr.Length; i++) arr[i] = i;

            arr[1000] = -1;
            arr[14000] = -2;
            arr[15000] = -3;
            arr[676000] = -4;
            arr[8024540] = -5;
            arr[9908000] = -6;

            var negatives = from val in arr.AsParallel().
            WithCancellation(cancelTokSrc.Token)
                            where val < 0
                            select val;

            Task cancelTsk = Task.Factory.StartNew(() => {
                Thread.Sleep(50);
                cancelTokSrc.Cancel();
            });
            try
            {
                foreach (var v in negatives)
                    Console.Write(v + " ");
            }
            catch (OperationCanceledException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (AggregateException exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                cancelTsk.Wait();
                cancelTokSrc.Dispose();
                cancelTsk.Dispose();
            }
        
        }
    }
}