using System.IO;
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

    public class ItemReport
    {
        public string FilePath { get; set; }
        public bool Enabled { get; set; }
        public int Weight { get; set; }
    }
}