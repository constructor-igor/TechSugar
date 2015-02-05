using System.ComponentModel.Composition;

namespace Unity_vs_MEF
{
    public interface IService
    {
        double Calc(double x, double y);
    }

    //[Export("add", typeof(IService))]
    [Export(typeof(IService))]
    public class ServiceAdd: IService
    {
        public double Calc(double x, double y)
        {
            return x + y;
        }
    }
    [Export(typeof(IService))]
    public class ServiceSub : IService
    {
        public double Calc(double x, double y)
        {
            return x - y;
        }
    }

    [Export(typeof(Manager))]
    public class Manager
    {
        public IService Service { get; private set; }
        public string Title { get; private set; }

        public Manager(IService service, string title)
        {
            Service = service;
            Title = title;
        }
    }
}