using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using NUnit.Framework;

namespace Unity_vs_MEF
{
    [TestFixture]
    public class MEFSamples
    {
        [Test]
        public void RegisterInstanceAndResolve()
        {
            IService instanceOfService = new ServiceAdd();
            var catalog = new AggregateCatalog();
            //catalog.Catalogs.Add();                       // TODO how to register instance of object?
            using (CompositionContainer mefContainer = new CompositionContainer())
            {
                mefContainer.ComposeExportedValue<IService>("add", instanceOfService);

                IService service = mefContainer.GetExport<IService>().Value;
                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
        [Test]
        public void RegisterTypeAndResolve()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(typeof(ServiceAdd)));
            using (CompositionContainer mefContainer = new CompositionContainer(catalog))
            {
                IService service = mefContainer.GetExport<IService>().Value;
                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
    }
}