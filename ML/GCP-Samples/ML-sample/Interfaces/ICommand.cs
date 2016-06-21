namespace ML_sample.Interfaces
{
    public interface ICommand
    {
        ProjectModelId ProjectModelId { get; }
        string Command { get; }
        void Run();
    }
}