using Ctor.Infra.SeparationLayer;
using Ctor.Server.Impl.Services;
using Ctor.Server.Interfaces.Services;

namespace Ctor.Server.Impl
{
    public static class SeparationLayerFactory
    {
        public static ISeparationLayer Create()
        {
            ISeparationLayer separationLayer = new SeparationLayer();
            separationLayer.Register<IAddService>(new AddService());
            return separationLayer;
        }
    }
}