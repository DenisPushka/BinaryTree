using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayData = new int[15] {2, 72, 60, 59, 50, 11, 3, 35, 61, 74, 65, 47, 1, 12, 8};
            // var rnd = new Random();
            // for (var i = 0; i < 15; i++)
            // {
            //     arrayData[i] = rnd.Next(0, 100);
            //     Console.Write(arrayData[i] + " ");
            // }

            var tree = new Tree<int>(arrayData[arrayData.Length / 2]);
            foreach (var i in arrayData)
                tree.Add(i);
            tree.Print();
        }
    }
}