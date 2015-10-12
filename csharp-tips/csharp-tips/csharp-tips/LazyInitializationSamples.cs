using System;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class LazyInitializationSamples
    {
        private TestFilter m_testFilter = null;
        private readonly Lazy<TestFilter> m_lazyTestFilter = new Lazy<TestFilter>(CreateTestFilter);
        [Test]
        public void SampleStandard()
        {
            if (m_testFilter==null)
                m_testFilter = new TestFilter();
            Assert.That(m_testFilter, Is.Not.Null);
        }
        [Test]
        public void SampleLazy()
        {
            Assert.That(m_lazyTestFilter.Value, Is.Not.Null);
            Assert.That(m_lazyTestFilter.Value, Is.Not.Null);
        }

        static private TestFilter CreateTestFilter()
        {
            Console.WriteLine("CreateTestFilter");
            return new TestFilter();
        }
    }

    public class TestFilter
    {
        public TestFilter()
        {
            Console.WriteLine("TestFilter.ctor()");
        }
    }
}
