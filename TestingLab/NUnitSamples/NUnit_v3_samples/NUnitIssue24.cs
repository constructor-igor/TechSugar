using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue24
    {
        [Test]
        public void AssertDictionary()
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
        }

        private IResolveConstraint ContainsValue(string expectedvalue)
        {
            throw new System.NotImplementedException();
        }
    }
}