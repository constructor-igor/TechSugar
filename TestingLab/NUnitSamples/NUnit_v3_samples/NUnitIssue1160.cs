using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1160
    {
        [Test]
        public unsafe void CatchNullReference()
        {
            byte* p = null;
            Assert.That(() => *p, Throws.Exception.TypeOf<NullReferenceException>());
        }

        [Test]
        public unsafe void CatchAccessViolation()
        {
            byte* p = (byte*)Marshal.AllocHGlobal(100).ToPointer();
            Assert.That(() => *p, Throws.Nothing);
            Marshal.FreeHGlobal((IntPtr)p);
            Assert.That(() => *p, Throws.Exception.TypeOf<AccessViolationException>());
        }

        [Test]
        public unsafe void CatchAccessViolation_throw()
        {
            byte* p = (byte*)Marshal.AllocHGlobal(100).ToPointer();
            Assert.That(() => *p, Throws.Nothing);
            Marshal.FreeHGlobal((IntPtr)p);
            Assert.That(() => { throw new AccessViolationException(); }, Throws.Exception.TypeOf<AccessViolationException>());
        }

    }
}