using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    public class Knot<T> where T : IComparable<T>
    {
        public Knot<T> Parent, Left, Right;
        public int Level;
        public T Val;
    }

    public class Tree<T> where T : IComparable<T>
    {
        private Tree<T> _parent, _left, _right;
        private int _level;
        private T _val;

        private StringBuilder[] _array;
        //private string[] array;
        // private Knot<T> knot;

        public Tree(T val, Tree<T> parent = null)
        {
            if (parent == null) _level = 0;
            // knot = new Knot<T>();
            _val = val;
            _parent = parent;
        }

        public void Add(T val)
        {
            if (val.CompareTo(_val) < 0)
            {
                if (_left == null)
                {
                    _left = new Tree<T>(val, this)
                    {
                        _level = _level + 1
                    };
                }
                else
                    _left.Add(val);
            }
            else
            {
                if (_right == null)
                {
                    _right = new Tree<T>(val, this)
                    {
                        _level = _level + 1
                    };
                }
                else
                    _right.Add(val);
            }
        }

        private static Tree<T> Search(Tree<T> tree, T val)
        {
            if (tree == null) return null;
            switch (val.CompareTo(tree._val))
            {
                case 1:
                    return Search(tree._right, val);
                case -1:
                    return Search(tree._left, val);
                case 0:
                    return tree;
                default:
                    return null;
            }
        }

        public Tree<T> Search(T val) => Search(this, val);

        private bool RemoveRoot(Tree<T> tree)
        {
            var curTree = tree._right != null ? tree._right : tree._left;

            while (curTree._left != null)
                curTree = curTree._left;

            var temp = curTree._val;
            Remove(temp);
            tree._val = temp;

            return true;
        }

        private static bool RemoveLeaves(Tree<T> tree)
        {
            if (tree == tree._parent._left)
                tree._parent._left = null;
            else
                tree._parent._right = null;

            return true;
        }

        private static bool RemoveRootLeft(Tree<T> tree)
        {
            tree._left._parent = tree._parent;
            if (tree == tree._parent._left)
                tree._parent._left = tree._left;
            else if (tree == tree._parent._right)
                tree._parent._right = tree._left;

            return true;
        }

        private static bool RemoveRootRight(Tree<T> tree)
        {
            tree._right._parent = tree._parent;
            if (tree == tree._parent._left)
                tree._parent._left = tree._right;
            else if (tree == tree._parent._right)
                tree._parent._right = tree._right;

            return true;
        }

        private static bool RemoveRootLR(Tree<T> tree)
        {
            var curTree = tree._right;

            while (curTree._left != null)
                curTree = curTree._left;

            if (curTree._parent == tree)
            {
                curTree._left = tree._left;
                tree._left._parent = curTree;
                curTree._parent = tree._parent;
                if (tree == tree._parent._left)
                    tree._parent._left = curTree;
                else if (tree == tree._parent._right)
                    tree._parent._right = curTree;

                return true;
            }

            if (curTree._right != null)
                curTree._right._parent = curTree._parent;

            curTree._parent._left = curTree._right;
            curTree._right = tree._right;
            curTree._left = tree._left;
            tree._left._parent = curTree;
            tree._right._parent = curTree;
            curTree._parent = tree._parent;

            if (tree == tree._parent._left)
                tree._parent._left = curTree;
            else if (tree == tree._parent._right)
                tree._parent._right = curTree;

            return true;
        }

        public bool Remove(T val)
        {
            var tree = Search(val);
            if (tree == null)
                return false;

            //Если удаляем корень
            if (tree == this)
                return RemoveRoot(tree);

            //Удаление листьев
            if (tree._left == null && tree._right == null && tree._parent != null)
                return RemoveLeaves(tree);

            //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
            if (tree._left != null && tree._right == null)
                return RemoveRootLeft(tree);

            //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
            if (tree._left == null && tree._right != null)
                return RemoveRootRight(tree);

            //Удаляем узел, имеющий поддеревья с обеих сторон
            if (tree._right != null && tree._left != null)
                return RemoveRootLR(tree);

            return false;
        }

        private int _allLevel;

        private void ScoreLevel(Tree<T> node)
        {
            if (node._left != null)
            {
                ScoreLevel(node._left);
                if (node._right != null)
                    ScoreLevel(node._right);
                else
                    CheckInequality(node);
            }
            else if (node._right != null)
                ScoreLevel(node._right);
            else
                CheckInequality(node);
        }

        private int CheckInequality(Tree<T> node) => _allLevel > node._level ? _allLevel : _allLevel = node._level;

        private void Print(Tree<T> node)
        {
            if (node == null) return;
            for (var i = 0; i < _array.Length; i++)
            {
                var buff = _array.Length - i * 0.75;
                var split = _array[i].ToString().Split();
                for (var f = 0; f < split.Length - 1; f++)
                {
                    for (var j = 0; j < Math.Pow(2, buff) - i; j++)
                        Console.Write(' ');
                    Console.Write(split[f]);
                    if (f % 2 != 0) Console.Write('|');
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void FillArray(Tree<T> node)
        {
            _array[node._level].Append(node._val).Append(' ');
            if (node._left == null && node._right == null)
            {
                FillGaps(node);
                FillGaps(node);
            }
            else if (node._left == null)
                FillGaps(node);

            if (node._left != null)
            {
                FillArray(node._left);
                if (node._right != null)
                    FillArray(node._right);
            }
            else if (node._right != null)
                FillArray(node._right);

            if (node._right == null && node._left != null)
                FillGaps(node);
        }

        private void FillGaps(Tree<T> node)
        {
            for (int i = node._level + 1, up = 0; i < _allLevel + 1; i++, up++)
            {
                for (var j = 0; j < Math.Pow(2, up); j++)
                    _array[i].Append("- ");
            }
        }

        public void Print()
        {
            ScoreLevel(this);
            _array = new StringBuilder[_allLevel + 1];
            for (var i = 0; i < _allLevel + 1; i++)
                _array[i] = new StringBuilder();

            FillArray(this);
            Print(this);
            Console.WriteLine();
        }

        public override string ToString() => _val.ToString();
    }
}