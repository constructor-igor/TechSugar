using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class GenericTests
    {
        public interface IDataInterface
        {
            int X { get; }
            int Y { get; }
        }

        public class DataClass : IDataInterface
        {
            #region IDataInterface
            public int X { get; private set; }
            public int Y { get; private set; }
            #endregion
            public DataClass(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        internal struct TestStruct
        {
            internal int X;
            internal int Y;

            internal TestStruct(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        // only class or interface could be specified as constraint
        public class GenericNew<T> where T : new()
        {
            public T Data { get; private set; }

            public GenericNew(T data)
            {
                Data = data;
            }
        }
        public class GenericStruct<T> where T : struct
        {
            public T Data { get; private set; }

            public GenericStruct(T data)
            {
                Data = data;
            }
        }
        public class GenericClass<T> where T : class
        {
            public T Data { get; private set; }

            public GenericClass(T data)
            {
                Data = data;
            }
        }
        public class GenericInterface<T> where T : IDataInterface
        {
            public T Data { get; private set; }

            public GenericInterface(T data)
            {
                Data = data;
            }
        }
        public class GenericDataClass<T> where T : DataClass
        {
            public T Data { get; private set; }

            public GenericDataClass(T data)
            {
                Data = data;
            }
        }

        [Test]
        public void TestGenericNewInt()
        {
            var objectNewInt = new GenericNew<int>(10);
            Assert.That(objectNewInt.Data, Is.EqualTo(10));
        }
        [Test]
        public void TestGenericNewStruct()
        {
            TestStruct testStruct = new TestStruct(10, 20);
            var objectNewInt = new GenericNew<TestStruct>(testStruct);
            Assert.That(objectNewInt.Data, Is.Not.SameAs(testStruct));
            Assert.That(objectNewInt.Data.X, Is.EqualTo(testStruct.X));
            Assert.That(objectNewInt.Data.Y, Is.EqualTo(testStruct.Y));
        }

        [Test]
        public void TestGenericStruct()
        {
            TestStruct testStruct = new TestStruct(10, 20);
            var objectNewInt = new GenericStruct<TestStruct>(testStruct);
            Assert.That(objectNewInt.Data, Is.Not.SameAs(testStruct));
            Assert.That(objectNewInt.Data.X, Is.EqualTo(testStruct.X));
            Assert.That(objectNewInt.Data.Y, Is.EqualTo(testStruct.Y));

            Assert.That(new GenericStruct<int>(10).Data, Is.EqualTo(10));
        }
        [Test]
        public void TestGenericClass()
        {
            var dataObject = 10;
            var objectClass = new GenericClass<object>(dataObject);
            Assert.That(objectClass.Data, Is.EqualTo(dataObject));
        }
        [Test]
        public void TestGenericDataClass()
        {
            var dataObject = new DataClass(10, 20);
            var objectClass = new GenericClass<object>(dataObject);
            Assert.That(objectClass.Data, Is.EqualTo(dataObject));
            Assert.That(objectClass.Data, Is.SameAs(dataObject));
        }
        [Test]
        public void TestGenericDataClassViaInterface()
        {
            var dataObject = new DataClass(10, 20);
            var objectClass = new GenericClass<IDataInterface>(dataObject);
            Assert.That(objectClass.Data, Is.EqualTo(dataObject));
            Assert.That(objectClass.Data, Is.SameAs(dataObject));
        }

        [Test]
        public void TestGenericInterface()
        {
            var dataObject = new DataClass(10, 20);
            var objectClass = new GenericInterface<IDataInterface>(dataObject);
            Assert.That(objectClass.Data, Is.EqualTo(dataObject));
            Assert.That(objectClass.Data, Is.SameAs(dataObject));
        }

        [Test]
        public void TestGenericClassData()
        {
            var dataObject = new DataClass(10, 20);
            var objectClass = new GenericDataClass<DataClass>(dataObject);
            Assert.That(objectClass.Data, Is.EqualTo(dataObject));
            Assert.That(objectClass.Data, Is.SameAs(dataObject));
        }
    }
}