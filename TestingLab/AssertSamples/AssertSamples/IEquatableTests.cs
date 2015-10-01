using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AssertSamples
{
    [TestFixture]
    public class IEquatableTests
    {
        [Test]
        public void IObjectEquatable1_false()
        {
            IObjectEquatable1 objectA = new ObjectEquatable();
            IObjectEquatable1 objectB = new ObjectEquatable();

            IEqualityComparer<IObjectEquatable1> equalityComparer = EqualityComparer<IObjectEquatable1>.Default;
            Assert.IsFalse(equalityComparer.Equals(objectA, objectB));
        }
        [Test]
        public void IObjectEquatable1_true()
        {
            IObjectEquatable2 objectA = new ObjectEquatable();
            IObjectEquatable2 objectB = new ObjectEquatable();

            IEqualityComparer<IObjectEquatable2> equalityComparer = EqualityComparer<IObjectEquatable2>.Default;
            Assert.IsTrue(equalityComparer.Equals(objectA, objectB));
        }

        [Test]
        public void ObjectEquatable_false()
        {
            ObjectEquatable objectA = new ObjectEquatable();
            ObjectEquatable objectB = new ObjectEquatable();

            IEqualityComparer<ObjectEquatable> equalityComparer = EqualityComparer<ObjectEquatable>.Default;
            Assert.IsFalse(equalityComparer.Equals(objectA, objectB));
        }
    }

    public interface IObjectEquatable1 : IEquatable<IObjectEquatable1>
    {
        
    }
    public interface IObjectEquatable2 : IEquatable<IObjectEquatable2>
    {
        
    }

    public class ObjectEquatable : IObjectEquatable1, IObjectEquatable2
    {
        #region IEquatable<IObjectEquatable1>
        public bool Equals(IObjectEquatable1 other)
        {
            return false;
        }
        #endregion

        #region IEquatable<IObjectEquatable2>
        public bool Equals(IObjectEquatable2 other)
        {
            return true;
        }
        #endregion
    }


    public class ExampleInheritanceProblem<T> : ExampleInheritanceProblemBase<T>
    {
        [Test]
        public void MyTest()
        {
            Assert.Pass(_type.ToString());
        }
    }
    [TestFixture(typeof(int))]
    [TestFixture(typeof(double))]
    public abstract class ExampleInheritanceProblemBase<T>
    {
        protected Type _type;

        protected ExampleInheritanceProblemBase()
        {
            _type = typeof(T);
        }
    }
}