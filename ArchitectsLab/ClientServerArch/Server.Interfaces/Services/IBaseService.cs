using System.Threading;

namespace Ctor.Server.Interfaces.Services
{
    public interface IBaseService
    {
    }

    public interface IBaseServiceCancelled
    {
        CancellationToken CancellationToken { get; set; }
    }
}
