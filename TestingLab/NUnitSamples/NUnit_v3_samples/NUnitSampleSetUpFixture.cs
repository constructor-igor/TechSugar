using System;
using System.Text;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    public static class Helper
    {
        public static StringBuilder LogContainer = new StringBuilder();

        public static void ToLog(string message)
        {
            LogContainer.AppendLine(message);
            Console.WriteLine(LogContainer.ToString());
        }
    }

    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Helper.ToLog("SetUpFixture.OneTimeSetUp");
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            Helper.ToLog("SetUpFixture.OneTimeTearDown");
        }
    }

    [TestFixture]
    public class SetUpSample1
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Helper.ToLog("SetUpSample1: OneTimeSetUp");
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Helper.ToLog("SetUpSample1: OneTimeTearDown");
        }

        [SetUp]
        public void SetUp()
        {
            Helper.ToLog("SetUpSample1: SetUp");
        }
        [TearDown]
        public void TearDown()
        {
            Helper.ToLog("SetUpSample1: TearDown");
        }

        [Test]
        public void Test1()
        {
            Helper.ToLog("SetUpSample1: Test1");
        }
        [Test]
        public void Test2()
        {
            Helper.ToLog("SetUpSample1: Test2");
        }
    }
    [TestFixture]
    public class SetUpSample2
    {
        [OneTimeSetUp]
        public void SetUpFixture()
        {
            Helper.ToLog("SetUpSample2: OneTimeSetUp");
        }
        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            Helper.ToLog("SetUpSample2: OneTimeTearDown");
        }

        [SetUp]
        public void SetUp()
        {
            Helper.ToLog("SetUpSample2: SetUp");
        }
        [TearDown]
        public void TearDown()
        {
            Helper.ToLog("SetUpSample2: TearDown");
        }

        [Test]
        public void Test1()
        {
            Helper.ToLog("SetUpSample2: Test1");
        }
        [Test]
        public void Test2()
        {
            Helper.ToLog("SetUpSample2: Test2");
        }
    }
}
