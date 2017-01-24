using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips.LINQ
{
    [TestFixture]
    public class SelfImplementedWhereTests
    {
        [Test]
        public void Test()
        {
            List<int> data = Enumerable.Range(0, 100).ToList();
            List<int> evenData = data.WhereEven().ToList();
            int oddCount = data.WhereOdd().Count();

            Assert.That(data.WhereOdd().Count(), Is.EqualTo(50));
            Assert.That(data.WhereOdd2().Count(), Is.EqualTo(50));
            Assert.That(data.MyWhere(v=>v>=50).Count(), Is.EqualTo(50));
        }
    }
    public static class MyLinq
    {
        public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> source)
        {
            return source.Where(item => !IsOdd(Convert.ToInt32(item)));
        }
        public static IEnumerable<T> WhereOdd<T>(this IEnumerable<T> source)
        {
            return source.Where(item => IsOdd(Convert.ToInt32(item)));
        }
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public static IEnumerable<T> WhereOdd2<T>(this IEnumerable<T> source)
        {
            List<T> soureData = source.ToList();
            for (int i = 0; i < soureData.Count; i++)
            {
                if (IsOdd(Convert.ToInt32(soureData[i])))
                    yield return soureData[i];
            }
        }
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> whereFunc)
        {
            List<T> soureData = source.ToList();
            foreach (T dataValue in soureData)
            {
                if (whereFunc(dataValue))
                    yield return dataValue;
            }
        }
    }
}