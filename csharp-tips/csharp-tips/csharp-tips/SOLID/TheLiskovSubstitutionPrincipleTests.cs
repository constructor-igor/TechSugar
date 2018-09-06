using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    public interface IShape
    {

    }

    public interface ICircle: IShape
    {

    }

    public interface IRectangle: IShape
    {

    }

    public class Circle : ICircle
    {

    }

    public class Rectangle : IRectangle
    {

    }

    [TestFixture]
    public class TheLiskovSubstitutionPrincipleTests
    {
        [Test]
        public void Test1()
        {
            List<ICircle> circles = new List<ICircle> {new Circle()};
            List<IShape> shapes = circles.Cast<IShape>().ToList();
            List<ICircle> circles2 = shapes.Cast<ICircle>().ToList();
            Assert.That(circles2, Is.Not.Null);

            IShape newItem = new Rectangle();
            Foo(shapes, newItem);

            List<ICircle> circles3 = shapes.Cast<ICircle>().ToList();
            Assert.That(circles3, Is.Not.Null);
        }
        [Test]
        public void Test2()
        {
            ICircle[] circles = {new Circle()};
            IShape[] shapes = circles;
            IShape rectangle = new Rectangle();
            Foo(shapes, rectangle);
        }

        void Foo(List<IShape> list, IShape newItem)
        {
            list.Add(newItem);
        }
        void Foo(IShape[] shapes, IShape newItem)
        {
            shapes[0] = newItem;
        }
    }
}