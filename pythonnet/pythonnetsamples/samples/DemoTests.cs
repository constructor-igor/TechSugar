using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Python.Runtime;

namespace samples
{
    [TestFixture]
    public class DemoTests
    {
        [Test]
        public void RunNumPy()
        {
            using (Py.GIL())
            {
                dynamic np = Py.Import("numpy");
                dynamic sin = np.sin;
                double value = sin(Math.PI / 2);
                Assert.That(value, Is.EqualTo(1.00));
            }
        }

        [Test]
        public void RunCustomMethod()
        {
            string moduleDirectory = GetDllLocation();
            using (Py.GIL())
            {
                dynamic syspy = Py.Import("sys");
                syspy.path.append(moduleDirectory);

                dynamic customModule = Py.Import("custom");
                dynamic run = customModule.run;
                string answer = run();
                Assert.That(answer, Is.EqualTo("Hello from python"));
            }
        }

        [Test]
        public void RunCustomWithNumpyMethod()
        {
            string moduleDirectory = GetDllLocation();
            using (Py.GIL())
            {
                dynamic syspy = Py.Import("sys");
                syspy.path.append(moduleDirectory);

                dynamic customModule = Py.Import("custom");
                dynamic run = customModule.run_np_cos;
                double result = run(0.0);
                Assert.That(result, Is.EqualTo(1.0));
            }
        }

        private string GetDllLocation()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return assemblyFolder;
        }
    }
}
