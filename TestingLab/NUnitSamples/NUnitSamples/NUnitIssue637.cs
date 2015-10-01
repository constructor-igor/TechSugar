using System;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitIssue637
    {
        [TestCase("foo", ExpectedResult = false)]
        public bool Fail(String incoming)
        {
            return incoming.Contains("bar");
        }
    }
}