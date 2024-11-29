using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BSTDuplicates<T> : BinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            
            // Any time a duplicate value is in the tree, the Count will be incremented
            public int Count { get; set; }

            public Node(T value)
            {
                Value = value;
                // There will only be one instance of the value in the tree upon construction
                Count = 1;
            }
        }

        private Node? Root { get; set; }
        public override void Insert(T value)
        {
            if (Root == null)
            {
                Root = new Node(value);
                return;
            }

            Node current = Root;

            while (true)
            {
                // Structure of the tree will not change, it will only increase the count of the node that holds the duplicate value
                if (value.CompareTo(current.Value) == 0)
                {
                    current.Count++;
                    return;
                }
                // value is less than the current node's value
                else if (value.CompareTo(current.Value) < 0)
                {
                    // Leaf has been reached
                    if (current.Left == null)
                    {
                        current.Left = new Node(value);
                        return;
                    }

                    // Traverse the tree if the current node is not a leaf
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node(value);
                    }

                    current = current.Right;
                }
            }
        }

        // This will be called when using the public delete method, as the parent will look to the child first
        private Node Delete(Node? node, T value)
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
                // Duplicate value was specified as the node to be deleted
                if (node.Count > 1)
                {
                    node.Count--;
                    return node;
                }

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

            return node;
        }

        private Node FindMin(Node node)
        {
            while(node.Left != null)
            {
                node = node.Left;
            }

            return node;
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
    }
}
