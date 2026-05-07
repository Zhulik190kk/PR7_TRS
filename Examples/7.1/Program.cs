using System;
using System.Linq;

namespace Ex_7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10000000];
            for (int i = 0; i < arr.Length; i++) arr[i] = i;

            arr[1000] = -1;
            arr[14000] = -2;
            arr[15000] = -3;
            arr[676000] = -4;
            arr[8024540] = -5;
            arr[9908000] = -6;

            var negatives = from val in arr.AsParallel()
            where val < 0
            select val;
            foreach (var v in negatives)
            Console.Write(v + "");
            Console.WriteLine();
        }
    }
    
}
