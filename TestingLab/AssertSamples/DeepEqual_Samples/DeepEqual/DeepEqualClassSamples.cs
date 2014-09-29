using DeepEqual.Syntax;
using NUnit.Framework;

/*
 * Checking open source project https://github.com/jamesfoster/DeepEqual
 * 
 * */

namespace DeepEqual_Samples.DeepEqual
{
    [TestFixture]
    public class DeepEqualClassSamples
    {
        [Test]
        [ExpectedException]
        public void DifferentProperties_ExpectedException()
        {
            AssertHelper.Wrapper(() =>
            {
                var expected = new Data { Id = 5, Name = "Joe" };
                var actual = new Data { Id = 5, Name = "Chandler" };
                actual.WithDeepEqual(expected)
                    .Assert();
            });
        }

        [Test]
        [Category("unexpected result")]
        [Category("fixed by updated version (1.1.0.0) of 'DeepEqual'")]
        [ExpectedException]
        public void DifferentPropertiesOfListItem_ExpectedException()
        {
            AssertHelper.Wrapper(() =>
            {
                var expected = DataFactory.Create(1);
                var actual = DataFactory.Create(2);

                actual.WithDeepEqual(expected)
                    .Assert();
            });
        }

        [Test]
        public void Ignore_DifferentPropertiesOfListItem_Pass()
        {
            AssertHelper.Wrapper(() =>
            {
                var expected = DataFactory.Create(1);
                var actual = DataFactory.Create(2);

                actual.WithDeepEqual(expected)
                    .IgnoreProperty<DataItem>(x => x.Tag)
                    .Assert();
            });
        }

        [Test]
        [ExpectedException]
        public void DifferentPropertiesOfListItem_ExpectedException_Proof()
        {
            AssertHelper.Wrapper(() =>
            {
                var expected = DataFactory.Create(1);
                var actual = DataFactory.Create(2);

                Assert.AreEqual(expected.Items[0].Tag.R, actual.Items[0].Tag.R);
            });
        }
    }
}