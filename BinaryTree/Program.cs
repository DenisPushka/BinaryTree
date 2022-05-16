using System;
using System.Diagnostics;

namespace BinaryTree
{
    internal static class Program
    {
        public static void Main()
        {
            const int count = 10;
            var arrayData = new int [count]; //{2, 72, 60, 59, 50, 11, 3, 35, 61, 74, 65, 47, 1, 12, 8};
            var rnd = new Random();
            for (var i = 0; i < count; i++)
            {
                arrayData[i] = rnd.Next(0, 100);
                Console.Write(arrayData[i] + " ");
            }

            Console.WriteLine();
            var bTree = new BTree<int>(3);
            // var stopwatch = new Stopwatch();
            // stopwatch.Start();
            
            foreach (var i in arrayData) 
                bTree.AddRoot(i);
            
            // stopwatch.Stop();
            // Console.WriteLine(stopwatch.ElapsedMilliseconds);
            bTree.Show();
            // var tree = new Tree<int>(arrayData[arrayData.Length / 2]);
            // foreach (var i in arrayData)
            //     tree.Add(i);
            // tree.Print();
        }
    }
}
//    2000  5000 10000  50000
// b+ 1     1    3      17
// b  0     2    9      295