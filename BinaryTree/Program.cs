using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayData = new[] {5, 15, 3, 10, 21, 20, 55};
            var tree = new Tree<int>(12);
            foreach (var i in arrayData)
                tree.Add(i);
            tree.Remove(20);
        }
    }
}