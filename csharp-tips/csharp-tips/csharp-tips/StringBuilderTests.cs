using System;
using System.Text;
using NUnit.Framework;

/*
 * 
 * http://www.dotnetperls.com/stringbuilder-capacity
 * 
 * */

namespace csharp_tips
{
    [TestFixture]
    public class StringBuilderTests
    {
        [Test]
        [Repeat(1000)]
        public void Test_20()
        {
            StringBuilder builder = new StringBuilder(10);
            builder.AppendFormat("12345678901234567890");
            Assert.That(builder.Capacity, Is.EqualTo(20));
        }

        [Test]
        public void Test_11()
        {
            StringBuilder builder = new StringBuilder(5);
            builder.AppendFormat("123456789012");
            Assert.That(builder.Capacity, Is.EqualTo(20));
        }

        [Test]
        public void Loop()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= 256; i++)
            {
                Console.Write(builder.Capacity);    // 16, 32, 64, 128, 256, etc.
                Console.Write(",");
                Console.Write(builder.Length);
                Console.WriteLine();
                builder.Append("1"); // <-- Add one character
            }
        }
    }
}