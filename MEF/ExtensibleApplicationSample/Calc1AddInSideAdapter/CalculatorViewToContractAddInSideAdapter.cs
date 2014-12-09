using System.AddIn.Pipeline;
using CalcAddInViews;
using CalculatorContracts;

namespace Calc1AddInSideAdapter
{
    [AddInAdapterAttribute]
    public class CalculatorViewToContractAddInSideAdapter: ContractBase, ICalc1Contract
    {
        private ICalculator _view;

        public CalculatorViewToContractAddInSideAdapter(ICalculator view)
        {
            _view = view;
        }

        #region ICalc1Contract
        public double Add(double a, double b)
        {
            return _view.Add(a, b);
        }

        public double Subtract(double a, double b)
        {
            return _view.Subtract(a, b);
        }

        public double Multiply(double a, double b)
        {
            return _view.Multiply(a, b);
        }

        public double Divide(double a, double b)
        {
            return _view.Divide(a, b);
        }
        #endregion
    }
}
