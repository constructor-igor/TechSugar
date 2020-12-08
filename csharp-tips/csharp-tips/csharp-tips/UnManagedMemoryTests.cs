using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace csharp_tips
{
    public class UnManagedMemoryTests
    {
        [Test]
        public void DemoIntPtr()
        {
            IntPtr hglobal = Marshal.AllocHGlobal(10);
            Marshal.FreeHGlobal(hglobal);
        }
        [Test]
        public void DemoUnmanagedContainer_using()
        {
            using (UnmanagedContainer container = new UnmanagedContainer(100))
            {
                Console.WriteLine(container.Pointer);
            }
        }
        [Test]
        public void DemoUnmanagedContainer_noDispose()
        {
            RunTest();
            GC.Collect(); // try to force running finalizer
        }

        private void RunTest()
        {
            UnmanagedContainer container = new UnmanagedContainer(200);
            Console.WriteLine(container.Pointer);
        }
    }

    public class UnmanagedContainer: IDisposable
    {
        private string m_id;
        public IntPtr Pointer { get; private set; }

        public UnmanagedContainer(int size)
        {
            Pointer = Marshal.AllocHGlobal(size);
            m_id = Guid.NewGuid().ToString();
            Console.WriteLine($"[UnmanagedContainer, {m_id}] ctor()");
        }
        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
        }

        ~UnmanagedContainer()
        {
            Dispose(false);
        }
        #endregion

        protected void Dispose(bool disposing)
        {
            Console.WriteLine($"[UnmanagedContainer, {m_id}] Dispose({disposing})");
            if (disposing)
                GC.SuppressFinalize(this);
            if (Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
            }
        }
    }
}