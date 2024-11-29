using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class AVLTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public int Height { get; set; }

            public Node(T value) 
            {
                Value = value;
                Height = 1; // Height of 1 indicates a leaf node
            }
            public override string ToString()
            {
                return $"Value: {Value} | " +
                       $"Left {(Left == null ? "null" : Left.Value)} | " +
                    $"Right: {(Right == null ? "null" : Right.Value)}" +
                    $"Height: {Height}";
            }
        }

        private Node? Root { get; set; }

        public AVLTree() => Root = null;

        public AVLTree(T value)
        {
            Root = new Node(value);
        }
        public void Insert(T value)
        {
            // We assign back to the root in case the root needs to be changed in the insertion
            Root = Insert(Root, value);
        }

        private Node Insert(Node? node, T value)
        {
            // Base Case
            if (node == null)
            {
                return new Node(value);
            }
            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)
            {
                node.Right = Insert(node.Right, value);
            }
            else // Duplicate value was found
            {
                return node;
            }

            UpdateHeight(node);
           
            return Balance(node);
        }

        private void UpdateHeight(Node node)
        {
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1; // + 1 is to include the node itself
        }

        private int GetHeight(Node? node) => node?.Height ?? 0;

        private int BalanceFactor(Node? node)
        {
            if (node == null)
            {
                return 0;
            }
            
            int balanceFactor = GetHeight(node.Left) - GetHeight(node.Right);
            return balanceFactor;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right!;
            Node z = y.Left!;
            
            y.Left = x;
            x.Right = z;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }
        private Node RotateRight(Node y)
        {
            Node x = y.Left!;
            Node z = x.Right!;

            x.Right = y;
            y.Left = z;

            // Y has to be updated first, as it is lower on the tree
            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }
        private Node Balance(Node node)
        {
            int balanceFactor = BalanceFactor(node);
            
            // Left-heavy Subtree
            if (balanceFactor > 1)
            {
                if (BalanceFactor(node.Left) < 0)
                {
                    node.Left = RotateLeft(node.Left!);
                }

                return RotateRight(node);
            }
            // Right-heavy Subtree
            else if (balanceFactor < -1) 
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    node.Right = RotateRight(node.Right!);
                }

                return RotateLeft(node);
            }
            // Sub-tree is balanced
            return node;
        }

        public bool Contains(T value)
        {
            if (Root == null)
            {
                return false;
            }

            Node? current = Root;

            while (current != null)
            {
                if (value.CompareTo(current.Value) == 0)
                {
                    return true;
                }

                // Traversal
                else if (value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return false;
        }

        public void Delete(T value)
        {
            Root = Delete(Root, value);
        }

        private Node? Delete(Node? node, T value)
        {
            // Base case
            // Bottom of the tree is reached & parent has no children
            if (node == null)
            {
                return null!;
            }
            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value.CompareTo(node.Value) < 0)
            {
                node.Right = Delete(node.Right, value);
            }
            else // Value to delete has been found
            {
                // Returning this results in its parent being overwritten by this right child
                if (node.Left == null)
                {
                    return node.Right!;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }

                // Current node has two children
                Node min = FindMin(node.Right);
                node.Value = min.Value;
                // Deletes the successor node after it has been moved into the original node to delete
                node.Right = Delete(node.Right, min.Value);
            }

            UpdateHeight(node);

            return Balance(node);
        }
        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }
        public override string ToString()
        {
            return ToString(Root, 0);
        }
        private String ToString(Node? node, int level)
        {
            // Base case
            if (node == null)
            {
                return String.Empty;
            }

            StringBuilder result = new StringBuilder();

            // Traverse right subtree first
            result.Append(ToString(node.Right, level + 1));

            // Tabs for each level
            result.Append(new String('\t', level));

            // Root Node
            result.Append(node.Value + "\n");

            // Traverse left subtree
            result.Append(ToString(node.Left, level + 1));

            return result.ToString();
        }

        public void Print()
        {
            Print(Root);
            Console.Write(Environment.NewLine);
        }

        // Node represents the root of any subtree
        private void Print(Node? node)
        {
            // Base case - cannot go left further
            if (node == null)
            {
                return;
            }

            // Left subtree
            Print(node.Left);

            Console.Write(node.Value + ", ");

            // Right subtree
            Print(node.Right);
        }
    }
}
