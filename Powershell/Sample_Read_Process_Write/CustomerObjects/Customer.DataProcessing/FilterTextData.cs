using Customer.Interfaces;

namespace Customer.DataProcessing
{
    public class FilterTextData : IDataProcessing<bool>
    {
        private readonly int _minimumSymbols;

        public FilterTextData(int minimumSymbols)
        {
            _minimumSymbols = minimumSymbols;
        }
        #region IDataProcessing
        public bool Run(ITextData textData)
        {
            return (textData.TotalSymbols > _minimumSymbols);
        }
        #endregion
    }
}
