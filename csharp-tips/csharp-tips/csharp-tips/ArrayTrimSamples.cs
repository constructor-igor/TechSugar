using System;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ArrayTrimSamples
    {
        [TestCase(0, 0, new byte[] { 1, 2, 3, 4 })]
        [TestCase(2, 0, new byte[] { 0, 0, 1, 2, 3, 4 })]
        [TestCase(0, 3, new byte[] { 1, 2, 3, 4, 0, 0, 0 })]
        [TestCase(2, 3, new byte[] { 0, 0, 1, 2, 3, 4, 0, 0, 0 })]
        [TestCase(0, 0, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        public void TestLongTrim(int expectedStart, int expectedFinish, byte[] buffer)
        {
            int start;
            int finish;
            LongFindTrim(out start, out finish, buffer);
            Assert.That(start, Is.EqualTo(expectedStart));
            Assert.That(finish, Is.EqualTo(expectedFinish));
        }

        [TestCase(0, 0, new byte[] { 1, 2, 3, 4 })]
        [TestCase(2, 0, new byte[] { 0, 0, 1, 2, 3, 4 })]
        [TestCase(0, 3, new byte[] { 1, 2, 3, 4, 0, 0, 0 })]
        [TestCase(2, 3, new byte[] { 0, 0, 1, 2, 3, 4, 0, 0, 0 })]
        [TestCase(0, 0, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        public void TestShortTrim(int expectedStart, int expectedFinish, byte[] buffer)
        {
            int start;
            int finish;
            ShortFindTrim(out start, out finish, buffer);
            Assert.That(start, Is.EqualTo(expectedStart));
            Assert.That(finish, Is.EqualTo(expectedFinish));
        }

        void LongFindTrim(out int start, out int end, byte[] buffer)
        {
            start = 0;
            end = 0;
            bool findStart = false;
            bool findEnd = false;
            byte start_ii = 0;
            byte end_ii = 0;
            int xxx;
            int yyy;
            for (int ii = 0; ii < buffer.Length; ii++)
            {
                yyy = buffer.Length;
                if (!findStart)
                {
                    start_ii = buffer[ii];
                }
                if (!findEnd)
                {
                    xxx = buffer.Length - ii - 1;
                    end_ii = buffer[buffer.Length - ii - 1];
                }
                if (start_ii > 0 && !findStart)
                {
                    //Found the first byte from the beginning that is not zero.
                    findStart = true;
                    start = ii;
                }
                if (end_ii > 0 && !findEnd)
                {
                    //Found the first byte from the end that is not zero.
                    findEnd = true;
                    end = ii;
                }
                if ((findStart && findEnd))
                {
                    //Stop looking
                    break;
                }
            }
        }
        private void ShortFindTrim(out int start, out int end, byte[] buffer)
        {
            int tempStart = Array.FindIndex(buffer, b => b > 0);
            start = tempStart == -1 ? 0 : tempStart;

            int tempEnd = Array.FindLastIndex(buffer, b => b > 0);
            end = buffer.Length - tempEnd - 1;
            if (end == buffer.Length)
                end = 0;
        }
    }
}