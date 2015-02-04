using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnityLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterInstance(new Instance());
            }
            Console.WriteLine("unityContainer disposed");
        }
    }
    public class Instance : IDisposable
    {
        public Instance()
        {
            Console.WriteLine("Instance.ctor");
        }
        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("Instance.Dispose");
        }
        #endregion
    }
}
