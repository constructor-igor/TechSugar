using System;
using NUnit.Framework;

namespace TreeAlgs
{
    [TestFixture]
    public class TreeAlgsTests
    {
        [Test]
        public void TestTreeDeepCount_0()
        {
            Tree tree = new Tree();
            Assert.That(Count(tree, 0), Is.EqualTo(0));
        }
        [Test]
        public void TestTreeDeepCount_1()
        {
            Tree treeLeft = new Tree(new Tree(), null);
            Assert.That(Count(treeLeft, 0), Is.EqualTo(1));

            Tree treeRight = new Tree(null, new Tree());
            Assert.That(Count(treeRight, 0), Is.EqualTo(1));

            Tree treeBooth = new Tree(new Tree(), new Tree());
            Assert.That(Count(treeBooth, 0), Is.EqualTo(1));
        }
        [Test]
        public void TestTreeDeepCount_2()
        {
            Tree treeLeftLeft = new Tree(new Tree(new Tree(), null), null);
            Assert.That(Count(treeLeftLeft, 0), Is.EqualTo(2));

            Tree treeLeftRight = new Tree(new Tree(null, new Tree()), null);
            Assert.That(Count(treeLeftRight, 0), Is.EqualTo(2));

            Tree treeRightLeft = new Tree(null, new Tree(new Tree(), null));
            Assert.That(Count(treeRightLeft, 0), Is.EqualTo(2));

            Tree treeRightRight = new Tree(null, new Tree(null, new Tree()));
            Assert.That(Count(treeRightRight, 0), Is.EqualTo(2));

        }

        int Count(Tree p, int c)
        {
            if (p.Left == null && p.Right == null)
                return c;

            int a=0, b=0;
            if (p.Left!=null) a = Count(p.Left, c + 1);
            if (p.Right!=null) b = Count(p.Right, c + 1);

            return Math.Max(a, b);
        }
    }

    public class Tree
    {
        public readonly Tree Left;
        public readonly Tree Right;

        public Tree(Tree left, Tree right)
        {
            Left = left;
            Right = right;
        }

        public Tree() : this(null, null)
        {
        }
    }
}
