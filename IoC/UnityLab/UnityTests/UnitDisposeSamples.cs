using System;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace UnityTests
{
    [TestFixture]
    public class UnitDisposeSamples
    {
        [Test]
        public void ProblemDefinition()
        {
            using (IUnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterType<IService, BaseService>();
                unityContainer.RegisterType<IService, FacadeService>("facade");

                Enumerable.Range(0, 2).ForEach(i =>
                {
                    using (IUnityContainer childContainer = unityContainer.CreateChildContainer())
                    {
                        childContainer.RegisterType<IService, BaseService>(new ContainerControlledLifetimeManager());

                        IService service = childContainer.Resolve<IService>("facade");
                        service.Foo(String.Format("problemDefinitionTest({0})", i));
                        Console.WriteLine("before childContainer.Dispose()");
                    }
                });

                Console.WriteLine("before unityContainer.Dispose()");
            }
        }
    }

    public interface IService
    {
        string Foo(string message);
    }

    public class BaseService : IService, IDisposable
    {
        public BaseService()
        {
            Console.WriteLine("Created BaseService");
        }
        #region IService
        public string Foo(string message)
        {
            Console.WriteLine("    BaseService({0}", message);
            return message;            
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("BaseService.Dispose()");
        }
        #endregion
        ~BaseService()
        {
            Console.WriteLine("~BaseService()");
        }
    }

    public class FacadeService : IService
    {
        private readonly IService m_service;

        public FacadeService(IService service)
        {
            m_service = service;
            Console.WriteLine("Created FacadeService");
        }
        #region IService
        public string Foo(string message)
        {
            Console.WriteLine("    FacadeService({0}", message);
            return m_service.Foo(message);
        }
        #endregion
    }
}
