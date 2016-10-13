using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ArraySamples
    {
        [TestCase(0, 2, new[] { '0', '1', '"', '2' })]
        [TestCase(1, 2, new[] { '0', '1', '"', '2' })]
        [TestCase(2, 4, new[] { '0', '1', '"', '2' })]
        [TestCase(3, 4, new[] { '0', '1', '"', '2' })]
        [TestCase(4, 5, new[] { '0', '1', '"', '2' })]
        [TestCase(5, 6, new[] { '0', '1', '"', '2' })]
        [TestCase(0, 4, new[] { '0', '1', '2', '3' })]
        public void Test(int startIndex, int expected, char[] argument)
        {
            Assert.That(FindItemWhile(startIndex, argument), Is.EqualTo(expected));
            Assert.That(FindItemLINQ(startIndex, argument), Is.EqualTo(expected));

        }

        int FindItemWhile(int index, char[] argument)
        {
            while (++index < argument.Length && argument[index] != '"')
                ;
            return index;
        }
        int FindItemLINQ(int index, char[] argument)
        {
            return argument.Skip(index+1).TakeWhile(c => c != '"').Count()+index+1;
        }
    }

    public static class LinqExtesnions
    {
        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
    }
}