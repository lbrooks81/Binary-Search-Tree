namespace BinarySearchTree
{
    public class Program
    {
        public static void Main()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            TestBST(bst);
            
            BSTDuplicates<int> bstd = new BSTDuplicates<int>();
            TestBST(bst);

            TestAVLTree();
        }

        private static void TestBST(BinarySearchTree<int> bst)
        {
            // Root
            bst.Insert(50);
            
            // Left Subtree
            bst.Insert(25);
            bst.Insert(10);
            bst.Insert(33);
            bst.Insert(33);
            bst.Insert(4);
            bst.Insert(11);
            bst.Insert(30);
            bst.Insert(40);
            
            // Right Subtree
            bst.Insert(75);
            bst.Insert(56);
            bst.Insert(89);
            bst.Insert(52);
            bst.Insert(61);
            bst.Insert(82);
            bst.Insert(95);

            Console.WriteLine("Starting tree...");
            Console.WriteLine(bst);


            Console.WriteLine("Printing tree in order...");
            bst.Print();

            PerformDeletion(bst, 4);
            PerformDeletion(bst, 10);
            PerformDeletion(bst, 56);

            Console.WriteLine("Adding 55...");
            bst.Insert(55);
            Console.WriteLine(bst);

            PerformDeletion(bst, 50);

        }

        private static void PerformDeletion(BinarySearchTree<int> bst, int value)
        
        {
            Console.WriteLine("\n==================");
            bst.Delete(value);
            Console.WriteLine($"Deleting {value}...");
            Console.WriteLine(bst);
            Console.WriteLine("==================\n");
        }

        private static void TestAVLTree()
        {
            Console.WriteLine("Testing AVL Tree...");
            AVLTree<int> avl = new AVLTree<int>();

            for(int i = 0; i < 3; i++)
            {
                avl.Insert(i + 1);
            }

            Console.WriteLine(avl);
            Console.WriteLine("\n===================\n");

            avl = new AVLTree<int>();

            avl.Insert(33);
            avl.Insert(13);
            avl.Insert(53);
            avl.Insert(11);
            avl.Insert(21);
            avl.Insert(61);
            avl.Insert(8);

            Console.WriteLine(avl);
            Console.WriteLine("\n===================\n");
            Console.WriteLine("Inserting 9...");
            avl.Insert(9);

            Console.WriteLine(avl);
            Console.WriteLine("\n===================\n");
            Console.WriteLine("Deleting 13...");
            avl.Delete(13);

            Console.WriteLine(avl);
            Console.WriteLine("\n===================\n");

        }
    }
}
