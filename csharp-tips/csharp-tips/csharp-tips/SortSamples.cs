using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class SortSamples
    {
        [Test]
        public void Test_OneColumn()
        {
            List<Point> pointsList = new List<Point>
            {
                new Point(0, 0),
                new Point(-2, 0),
                new Point(-1, 0)
            };
            List<Point> expectedList = new List<Point>
            {
                new Point(0, 0),
                new Point(-1, 0),
                new Point(-2, 0),
            };

            RunSort(pointsList);
            Assert.That(pointsList, Is.EquivalentTo(expectedList));
        }
        [Test]
        public void Test_TwoColumns()
        {
            List<Point> pointsList = new List<Point>
            {
                new Point(0, 1),
                new Point(-2, 0),
                new Point(-5, 1),
                new Point(-1, 0)
            };
            List<Point> expectedList = new List<Point>
            {
                new Point(-1, 0),
                new Point(-2, 0),
                new Point(0, 1),
                new Point(-5, 1),
            };

            RunSort(pointsList);
            Assert.That(pointsList, Is.EquivalentTo(expectedList));
        }

        public class Point
        {
            public double X;
            public double Y;
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
            public override bool Equals(object obj)
            {
                return this.X == (obj as Point).X && this.Y == (obj as Point).Y;
            }

            #region Overrides of Object
            public override string ToString()
            {
                return String.Format("x={0}, y={1}", X, Y);
            }
            #endregion
        }

        public void RunSort(List<Point> pointsList)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                Point minPoint = pointsList[i];
                for (int j = i + 1; j < pointsList.Count; j++)
                {
                    Point currentPoint = pointsList[j];
                    if (Less(currentPoint, minPoint))
                    {
                        pointsList[i] = currentPoint;
                        pointsList[j] = minPoint;
                        minPoint = currentPoint;
                    }
                }
            }
        }

        private const double DELTA = 0.001;
        public bool Less(Point point1, Point point2)
        {
            if (Math.Abs(point1.Y - point2.Y) > DELTA)
            {
                return point1.Y < point2.Y;
            }
            return point1.X > point2.X;
        }
    }
}