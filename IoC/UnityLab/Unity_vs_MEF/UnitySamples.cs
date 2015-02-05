using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Unity_vs_MEF
{
    [TestFixture]
    public class UnitySamples
    {
        [Test]
        public void RegisterInstanceAndResolve()
        {
            using (UnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterInstance<IService>(new ServiceAdd());

                IService service = unityContainer.Resolve<IService>();

                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
        [Test]
        public void RegisterTypeAndResolve()
        {
            using (UnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterType<IService, ServiceAdd>();

                IService service = unityContainer.Resolve<IService>();

                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
        [Test]
        public void RegisterTypesAndResolveByName()
        {
            using (UnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterType<IService, ServiceAdd>("add");
                unityContainer.RegisterType<IService, ServiceSub>("sub");

                IService service = unityContainer.Resolve<IService>("add");

                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
    }
}
