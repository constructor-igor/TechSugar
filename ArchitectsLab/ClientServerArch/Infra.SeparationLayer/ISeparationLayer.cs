namespace Ctor.Infra.SeparationLayer
{
    public interface ISeparationLayer
    {
        void Register<T>(T service);
        T GetService<T>();
    }
}