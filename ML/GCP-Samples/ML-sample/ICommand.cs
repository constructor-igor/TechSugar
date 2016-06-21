namespace ML_sample
{
    public interface ICommand
    {
        ProjectModelId ProjectModelId { get; }
        string Command { get; }
        void Run();
    }
}