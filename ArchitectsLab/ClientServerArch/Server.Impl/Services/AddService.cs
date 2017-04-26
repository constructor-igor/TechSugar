using Ctor.Server.Interfaces.Services;

namespace Ctor.Server.Impl.Services
{
    public class AddService: IAddService
    {
        #region Implementation of IAddService
        public double Add(double x, double y)
        {
            return x + y;
        }
        #endregion
    }
}
