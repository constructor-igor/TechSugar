using System;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUpFixture.SetUp");
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("SetUpFixture.TearDown");
        }
    }

    [TestFixture]
    public class SetUpSample1
    {
        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TestFixtureSetUp");
        }
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TestFixtureTearDown");
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": SetUp");
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TearDown");
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": Test1");
        }
        [Test]
        public void Test2()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": Test2");
        }
    }
    [TestFixture]
    public class SetUpSample2
    {
        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TestFixtureSetUp");
        }
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TestFixtureTearDown");
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": SetUp");
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": TearDown");
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": Test1");
        }
        [Test]
        public void Test2()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.FullName + ": Test2");
        }
    }
}
