using System.ComponentModel.Composition;

namespace Unity_vs_MEF
{
    public interface IService
    {
        double Calc(double x, double y);
    }

    [Export(typeof(IService))]
    public class ServiceAdd: IService
    {
        public double Calc(double x, double y)
        {
            return x + y;
        }
    }
}