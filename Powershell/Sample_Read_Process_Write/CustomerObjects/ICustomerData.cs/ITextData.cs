using System.Security.Cryptography.X509Certificates;

namespace Customer.Interfaces
{
    public interface ITextData
    {
        int TotalSymbols { get; }
        int GetNumberOf(string term);
    }

    public interface ICustomerTextDataFactory<out T>
    {
        T LoadFromFile(string filePath);
    }
}