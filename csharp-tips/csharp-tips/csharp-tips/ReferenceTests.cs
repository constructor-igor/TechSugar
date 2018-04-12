using System;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ReferenceTests
    {
        public class Point
        {
            public static Point Init = new Point();
            public double X;
            public double Y;

            public void PrintInit()
            {
                Console.WriteLine("X={0}, Init = ({1}, {2})", X, Init.X, Init.Y);
                PrintStatic();
            }

            private static void PrintStatic()
            {
                Console.WriteLine("Init = ({0}, {1})", Init.X, Init.Y);
            }
        }

        public struct PointStruct
        {
            public int X;
            public int Y;
        }

        [Test]
        public void Test()
        {
            object objectA = new object();

            function1(objectA);
            Assert.That(objectA, Is.Not.Null);

            function2(ref objectA);
            Assert.That(objectA, Is.Null);
        }

        [Test]
        public void PointTest()
        {
            Point p = new Point() {X = 1, Y = 2};
            Foo(p);
            Assert.That(p.X, Is.EqualTo(1));
            Foo2(p);
            Assert.That(p.X, Is.EqualTo(9));

            Point one = new Point() {X = 100};
            Point two = new Point() {X = 200};
            Point.Init.X = 10;
            one.PrintInit();
            two.PrintInit();
            Point.Init.X = 20;
            one.PrintInit();
            two.PrintInit();
        }
        [Test]
        public void PointTestWithRef()
        {
            Point p = new Point { X = 1, Y = 2 };
            FooRef(ref p);
            Assert.That(p.X, Is.EqualTo(10));
        }
        [Test]
        public void PointTestWithOut()
        {
            Point p = new Point { X = 1, Y = 2 };
            FooOut(out p);
            Assert.That(p.X, Is.EqualTo(10));
        }

        [Test]
        public void PointStructTest()
        {
            PointStruct p = new PointStruct() { X = 1, Y = 2 };
            FooStruct(p);
            Assert.That(p.X, Is.EqualTo(1));
            FooRefStruct(ref p);
            Assert.That(p.X, Is.EqualTo(100));
            FooStruct2(p);
            Assert.That(p.X, Is.EqualTo(100));
        }

        void FooStruct(PointStruct pstruct)
        {
            pstruct = new PointStruct() {X=10, Y=20};
        }
        void FooRefStruct(ref PointStruct pstruct)
        {
            pstruct = new PointStruct() { X = 100, Y = 200 };
        }
        void FooStruct2(PointStruct pstruct)
        {
            pstruct.X = 5;
        }

        [Test]
        public void TestString()
        {
            string line = "12345";
            line = line + "6";
            FooString(line);
            Assert.That(line, Is.EqualTo("123456"));
            FooRefString(ref line);
            Assert.That(line, Is.EqualTo("abc"));
        }

        [Test]
        public void TestInt()
        {
            int number = 12345;
            FooNumber(number);
            Assert.That(number, Is.EqualTo(12345));
            FooRefNumber(ref number);
            Assert.That(number, Is.EqualTo(10));
        }

        public void FooNumber(int pnumber)
        {
            pnumber = 10;
        }
        public void FooRefNumber(ref int pnumber)
        {
            pnumber = 10;
        }

        static string line = "12345";
        [Test]        
        public void TestStaticString()
        {            
            line = line + "6";
            FooString(line);
            Assert.That(line, Is.EqualTo("123456"));
            FooRefString(ref line);
            Assert.That(line, Is.EqualTo("abcd"));
        }

        void FooString(string pline)
        {
            pline = "abc";
        }
        void FooRefString(ref string pline)
        {
            pline = "abcd";
        }

        void Foo(Point p)
        {
            p = new Point() {X = 10, Y = 20};
        }
        void Foo2(Point p)
        {
            p.X = 9;
        }
        void FooRef(ref Point p)
        {
            p = new Point() { X = 10, Y = 20 };
        }
        void FooOut(out Point p)
        {
            p = new Point() { X = 10, Y = 20 };
        }

        void function1(object objectX)
        {
            objectX = null;
        }
        void function2(ref object objectX)
        {
            objectX = null;
        }
    }
}