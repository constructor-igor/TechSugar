using Customer.Interfaces;

namespace Customer.DataProcessing
{
    public interface IDataProcessing<T>
    {
        T Run(ITextData textData);
    }
}