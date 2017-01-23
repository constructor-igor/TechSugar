using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeSolution
{
    public class Tree
    {
        private INode _root;
        public void Init()
        {
            INode one = new Node("1", 0.1, null, null, null);
            _root = one;
            INode two = new Node("2", 0.2, null, null, _root);
            INode three = new Node("3", 0.3, null, null, _root);
            _root.SetLeftNode(two);
            _root.SetRightNode(three);

            INode four = new Node("4", 0.4, null, null, two);
            INode five = new Node("5", 0.5, null, null, two);
            two.SetLeftNode(four);
            two.SetRightNode(five);

            INode sixNode = new Node("6", 0.6, null, null, three);
            INode seven = new Node("7", 0.7, null, null, three);
            three.SetLeftNode(sixNode);
            three.SetRightNode(seven);

            INode eight = new Node("8", 0.8, null, null, five);
            five.SetLeftNode(eight);

            INode nine = new Node("9", 0.9, null, null, sixNode);
            sixNode.SetRightNode(nine);
        }

        public void PrintTreeWithRecursion()
        {
            INode node = _root;
            PrintNode(node);
        }

        private void PrintNode(INode node)
        {
            if (node == null) return;
            Console.WriteLine(node.ID);
            PrintNode(node.LeftNode);
            PrintNode(node.RightNode);
        }

        public void PrintTreeWithoutRecursion()
        {
            IList<INode> nodes = new List<INode>();
            IList<INode> waitingList = new List<INode>();

            INode node = _root;

            while (node != null)
            {
                waitingList.Remove(node);
                nodes.Add(node);

                INode left = GetLeftNode(node);
                INode right = GetRightNode(node);

                if (left != null)
                {
                    waitingList.Add(left);
                }
                if (right != null)
                {
                    waitingList.Add(right);

                }
                node = waitingList.Count > 0 ? waitingList.First() : null;
            }
            PrintList(nodes);
        }

        private INode GetRightNode(INode node)
        {
            return node.RightNode;
        }

        private INode GetLeftNode(INode node)
        {
            return node.LeftNode;
        }

        private static void PrintList(IList<INode> nodes)
        {
            foreach (INode n in nodes)
            {
                Console.WriteLine(n.ID);
            }
        }
    }
}
