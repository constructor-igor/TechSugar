using System.AddIn.Pipeline;

namespace CalcAddInViews
{
    // The AddInBaseAttribute identifies this interface as the basis for 
    // the add-in view pipeline segment.
    [AddInBase]
    public interface ICalculator
    {
        double Add(double a, double b);
        double Subtract(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
    }
}