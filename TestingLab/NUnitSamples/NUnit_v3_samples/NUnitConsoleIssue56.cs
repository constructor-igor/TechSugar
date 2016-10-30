using NUnit.Framework;

namespace NUnit_v3_samples
{
    namespace Issue56
    {
        namespace NamepSpace1
        {
            [TestFixture]
            public class Categoty1Test
            {
                [Test]
                public void category1_test_1()
                {
                    Assert.Pass();
                }
                [Test]
                public void category1_test_2()
                {
                    Assert.Pass();
                }
            }            
        }

        namespace NameSpace21
        {
            namespace NameSpace22
            {
                [TestFixture]
                public class TestSuite1Test
                {
                    [Test]
                    public void testsuite1_test_1()
                    {
                        Assert.Pass();
                    }

                    [Test]
                    public void testsuite1_test_2()
                    {
                        Assert.Pass();
                    }

                    [Test]
                    public void testsuite1_test_3()
                    {
                        Assert.Pass();
                    }
                }
            }
        }
    }
}