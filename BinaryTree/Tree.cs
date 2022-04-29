using System;
using System.Collections.Generic;

namespace BinaryTree
{
    public class Tree<T> where T : IComparable<T>
    {
        private Tree<T> _parent, _left, _right;
        private T _val;
        private readonly List<T> _listForPrint = new();

        public Tree(T val, Tree<T> parent = null)
        {
            _val = val;
            _parent = parent;
        }

        public void Add(T val)
        {
            if (val.CompareTo(_val) < 0)
            {
                if (_left == null)
                    _left = new Tree<T>(val, this);
                else
                    _left.Add(val);
            }
            else
            {
                if (_right == null)
                    _right = new Tree<T>(val, this);
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

        private void Print(Tree<T> node)
        {
            if (node == null) return;
            Print(node._left);
            _listForPrint.Add(node._val);
            Console.Write(node + " ");
            if (node._right != null)
                Print(node._right);
        }

        public void Print()
        {
            _listForPrint.Clear();
            Print(this);
            Console.WriteLine();
        }

        public override string ToString() => _val.ToString();
    }
}