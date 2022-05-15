using System;

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
            foreach (var i in arrayData) 
                bTree.AddRoot(i);
            bTree.Show();
            // var tree = new Tree<int>(arrayData[arrayData.Length / 2]);
            // foreach (var i in arrayData)
            //     tree.Add(i);
            // tree.Print();
        }
    }
}