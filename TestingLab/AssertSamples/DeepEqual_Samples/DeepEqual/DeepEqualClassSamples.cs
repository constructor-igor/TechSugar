using System;
using System.Collections.Generic;
using DeepEqual.Syntax;
using NUnit.Framework;

/*
 * Checking open source project https://github.com/jamesfoster/DeepEqual
 * 
 * */

namespace DeepEqual_Samples
{
    public class Helper
    {
        static public void Wrapper(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }

    [TestFixture]
    public class DeepEqualClassSamples
    {
        [Test]
        [ExpectedException]
        public void CheckStringAssert()
        {
            Helper.Wrapper(() =>
            {
                const string expected = "Joe";
                const string actual = "Chandler";
                Assert.AreEqual(expected, actual);
            });
        }
        [Test]
        [ExpectedException]
        public void TestIgnore()
        {
            Helper.Wrapper(() =>
            {
                var expected = new Data { Id = 5, Name = "Joe" };
                var actual = new Data { Id = 5, Name = "Chandler" };
                actual.WithDeepEqual(expected)
                    .Assert();
            });
        }

        [Test]
        [Category("unexpected result")]
        [ExpectedException]
        public void TestIgnoreByType()
        {
            Helper.Wrapper(() =>
            {
                var expected = new Data { Id = 5, Name = "Joe" };
                expected.Items.Add(new DataItem { Name = "1", Tag = new Tag(1) });
                var actual = new Data { Id = 5, Name = "Joe" };
                actual.Items.Add(new DataItem { Name = "1", Tag = new Tag(2) });

                actual.WithDeepEqual(expected)
                    .Assert();

                //Assert.AreEqual(expected.Items[0].Tag.R, actual.Items[0].Tag.R);
            });
        }
    }

    public class Data
    {
        public int Id;
        public string Name { get; set; }
        public List<DataItem> Items = new List<DataItem>();
    }

    public class DataItem
    {
        public string Name { get; set; }
        public Tag Tag { get; set; }
    }

    public class Tag
    {
        static readonly Random random = new Random();
        public int R { get; set; }

        public Tag()
        {
            R = random.Next();
        }
        public Tag(int r)
        {
            R = r;
        }
    }
}