using System;
using System.Text;

namespace BinaryTree
{
    public class Node
    {
        public int N;
        public readonly int[] Key;
        public readonly Node[] Child;
        public bool Leaf;

        public Node(int T)
        {
            Key = new int[2 * T - 1];
            Child = new Node[2 * T];
            Leaf = true;
            N = 0;
        }

        public int Find(int k)
        {
            for (var i = 0; i < this.N; i++)
                if (this.Key[i] == k)
                    return i;
            return -1;
        }
    }

    public class BTree<T>
    {
        private Node _root;
        private int Count;

        public BTree(int count)
        {
            Count = count;
            _root = new Node(Count);
        }

        public void AddRoot(int key)
        {
            var r = _root;
            if (r.N == 2 * Count - 1)
            {
                var s = new Node(Count);
                _root = s;
                s.Leaf = false;
                s.N = 0;
                s.Child[0] = r;
                Split(s, 0, r);
                AddValue(s, key);
            }
            else
                AddValue(r, key);
        }

        // Добавление узла
        private void AddValue(Node node, int key)
        {
            if (node.Leaf)
            {
                var i = 0;
                for (i = node.N - 1; i >= 0 && key < node.Key[i]; i--) 
                    node.Key[i + 1] = node.Key[i];

                node.Key[i + 1] = key;
                node.N += 1;
            }
            else
            {
                var i = 0;
                for (i = node.N - 1; i >= 0 && key < node.Key[i]; i--)
                {
                }

                i++;
                var tmp = node.Child[i];
                if (tmp.N == 2 * Count - 1)
                {
                    Split(node, i, tmp);
                    if (key > node.Key[i]) 
                        i++;
                }

                AddValue(node.Child[i], key);
            }
        }

        public bool Remove()
        {
            return false;
        }

        // Разбиение узла
        private void Split(Node node, int pos, Node nodeT)
        {
            var buff = new Node(Count)
            {
                Leaf = nodeT.Leaf,
                N = Count - 1
            };

            for (var i = 0; i < Count - 1; i++)
                buff.Key[i] = nodeT.Key[i + Count];

            if (!nodeT.Leaf)
            {
                for (var i = 0; i < Count; i++)
                    buff.Child[i] = nodeT.Child[i + Count];
            }

            nodeT.N = Count - 1;
            for (var i = node.N; i >= pos + 1; i--)
                node.Child[i + 1] = node.Child[i];
            
            node.Child[pos + 1] = buff;

            for (var i = node.N - 1; i >= pos; i--)
                node.Key[i + 1] = node.Key[i];
            
            node.Key[pos] = nodeT.Key[Count - 1];
            node.N += 1;
        }

        public Node Search(Node node, int key)
        {
            while (true)
            {
                int j;
                if (node == null) return null;
                for (j = 0; j < node.N; j++)
                {
                    if (key < node.Key[j]) break;
                    if (key == node.Key[j]) return node;
                }

                if (node.Leaf) return null;
                node = node.Child[j];
            }
        }

        public void Show() => Show(_root);

        private static void Show(Node node)
        {
            if (node == null) return;
            for (var i = 0; i < node.N; i++)
                Console.Write(node.Key[i] + " ");
            
            Console.Write(" | ");
            if (!node.Leaf)
            {
                Console.WriteLine();
                for (var i = 0; i < node.N + 1; i++)
                    Show(node.Child[i]);
            }
        }

        public bool Contain(int k) => Search(_root, k) != null;
    }
}