namespace Customer.Interfaces
{
    public interface ITextData
    {
        int TotalSymbols { get; }
        int GetNumberOf(string term);
    }
}