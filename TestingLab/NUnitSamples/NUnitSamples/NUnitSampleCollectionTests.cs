using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitSampleCollectionTests
    {
        [Test]
        public void CollectionWithClass()
        {
            List<PairItem> actual = new List<PairItem>
            {
                new PairItem(10, 20),
                new PairItem(30, 40)
            };
            List<PairItem> expected = new List<PairItem>
            {
                new PairItem(10, 20),
                new PairItem(30, 40)
            };

            CollectionAssert.AreEquivalent(expected, actual);   //Assert.That(actual, Is.EquivalentTo(expected));            
        }
        [Test]
        public void CollectionWithTemplate()
        {
            List<Pair<int, int>> actual = new List<Pair<int, int>>
            {
                new Pair<int, int>(10, 20),
                new Pair<int, int>(30, 40)
            };
            List<Pair<int, int>> expected = new List<Pair<int, int>>
            {
                new Pair<int, int>(10, 20),
                new Pair<int, int>(30, 40)
            };

            CollectionAssert.AreEquivalent(expected, actual);   //Assert.That(actual, Is.EquivalentTo(expected));            
        }
    }

    public class PairItem
    {
        public int First;
        public int Second;
        public PairItem(int first, int second)
        {
            First = first;
            Second = second;
        }

        public override bool Equals(object obj)
        {
            PairItem other = (PairItem)obj;
            return First == other.First && Second == other.Second;
        }
        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }
    }
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            First = first;
            Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
        public override bool Equals(object obj)
        {
            Pair<T, U> other = (Pair<T, U>)obj;
            return First.Equals(other.First) && Second.Equals(other.Second);
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }
    };
}