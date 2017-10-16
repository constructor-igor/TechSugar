using System;
using System.Threading;
using NUnit.Framework;
using TinyWorkflow;

namespace IntroToTinyWorkflow
{
    public class QuadraticEquationContext
    {
        public readonly double A;
        public readonly double B;
        public readonly double C;
        public double Discriminant { get; set; }
        public int ResultRootsNumber { get; set; }
        public double Root1 { get; set; }
        public double Root2 { get; set; }

        public QuadraticEquationContext(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
    }

    [TestFixture]
    public class QuadraticEquationTests
    {
        [TestCase(1, 2, 1)]
        [TestCase(2, 2, 3)]
        [TestCase(1, 4, 3)]
        public void Test(double a, double b, double c)
        {
            QuadraticEquationContext context = new QuadraticEquationContext(a, b, c);
            IWorkflow<QuadraticEquationContext> workflow = new Workflow<QuadraticEquationContext>()
                .Do(CalculateDiscriminant)
                .If(RootsChecking,
                    new Workflow<QuadraticEquationContext>()
                        .If(IfSameRoots,
                            new Workflow<QuadraticEquationContext>().Do(SameRoots), 
                            new Workflow<QuadraticEquationContext>().DoAsynch(CalculateRoot1, CalculateRoot2)),
                    new Workflow<QuadraticEquationContext>().Do(NoRoots)                
                )
                .Do(obj => Console.WriteLine("[{0}] finish", Thread.CurrentThread.ManagedThreadId));
            workflow.Start(context);
        }

        private void CalculateRoot1(QuadraticEquationContext context)
        {
            context.Root1 = (-context.B + Math.Sqrt(context.Discriminant)) / 2 * context.A;
            Console.WriteLine("[{0}] root1: {1}", Thread.CurrentThread.ManagedThreadId, context.Root2);
        }
        private void CalculateRoot2(QuadraticEquationContext context)
        {
            context.Root2 = (-context.B - Math.Sqrt(context.Discriminant)) / 2 * context.A;
            Console.WriteLine("[{0}] root2: {1}", Thread.CurrentThread.ManagedThreadId, context.Root2);
        }

        private void SameRoots(QuadraticEquationContext context)
        {
            double root = -context.B / 2 * context.A;
            context.Root1 = root;
            context.Root2 = root;
            Console.WriteLine("[{0} Same roots: {1}", Thread.CurrentThread.ManagedThreadId, root);
        }

        private bool IfSameRoots(QuadraticEquationContext context)
        {
            return context.Discriminant == 0;
        }

        private void CalculateDiscriminant(QuadraticEquationContext context)
        {
            double discriminant = Math.Pow(context.B, 2) - 4 * context.A * context.C;
            context.Discriminant = discriminant;
            Console.WriteLine("[{0}] Calculated discriminant = {1}", Thread.CurrentThread.ManagedThreadId, discriminant);
        }
        private bool RootsChecking(QuadraticEquationContext context)
        {
            return context.Discriminant >= 0;
        }
        private void NoRoots(QuadraticEquationContext context)
        {
            context.ResultRootsNumber = 0;
            Console.WriteLine("No Roots");
        }
    }
}