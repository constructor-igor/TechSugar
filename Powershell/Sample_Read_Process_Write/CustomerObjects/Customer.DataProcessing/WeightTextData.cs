using Customer.Interfaces;

namespace Customer.DataProcessing
{
    public class WeightTextData : IDataProcessing<int>
    {
        #region IDataProcessing
        public int Run(ITextData textData)
        {
            return textData.GetNumberOf("Black") + textData.GetNumberOf("White");
        }
        #endregion
    }
}