using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TreeAlgs
{
    [TestFixture]
    public class NodesAlgsTests
    {
        [Test]
        public void Test()
        {
            Node root = new Node(0, 
                new Node(1, 
                    Node.New(3), 
                    new Node(4, 
                        null, 
                        Node.New(6))), 
                new Node(2,
                        Node.New(5), 
                        null));
            PrintNodesRecursiveSorted(root);
        }

        private static void PrintNodesRecursive(Node root, List<Node> nodes, int counter)
        {
            if (root == null)
                return;
            nodes.Add(root);
            counter++;
            PrintNodesRecursive(root.RightNode, nodes, counter);
            PrintNodesRecursive(root.LeftNode, nodes, counter);
        }
        private static void PrintNodesRecursiveSorted(Node root)
        {
            List<Node> nodes = new List<Node>();
            PrintNodesRecursive(root, nodes, 0);
            nodes.Sort();
            foreach (Node node in nodes)
            {
                Console.WriteLine(node.Number);
            }
        }
        private static void PrintNodesRecursiveNotSorted(Node root)
        {
            if (root == null) return;

            Console.WriteLine(root.Number);

            PrintNodesRecursiveNotSorted(root.RightNode);
            PrintNodesRecursiveNotSorted(root.LeftNode);
        }
    }

    public class Node : IComparable<Node>
    {
        public static Node New(int number)
        {
            return new Node(number, null, null);
        }
        public Node(int number, Node leftNode, Node rightNode)
        {
            Number = number;
            LeftNode = leftNode;
            RightNode = rightNode;
            IsVisited = false;
        }

        public bool IsVisited { get; set; }
        public int Number { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public int CompareTo(Node other)
        {
            if (Number == other.Number)
                return 0;
            if (Number > other.Number)
                return 1;
            return -1;
        }
    }
}