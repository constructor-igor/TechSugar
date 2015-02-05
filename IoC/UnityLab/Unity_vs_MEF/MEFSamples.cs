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
            ServiceAdd instanceOfService = new ServiceAdd();
            using (CompositionContainer mefContainer = new CompositionContainer())
            {
                mefContainer.ComposeExportedValue<IService>(instanceOfService);

                IService service = mefContainer.GetExportedValue<IService>();
                //IService service = mefContainer.GetExport<IService>().Value;
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
                //IService service = mefContainer.GetExport<IService>().Value;
                IService service = mefContainer.GetExportedValue<IService>();
                Assert.That(service.Calc(1, 2), Is.EqualTo(3));
            }
        }
    }
}