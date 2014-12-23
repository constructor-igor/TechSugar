namespace Engine.Model
{
    public interface IAlgorithmCommand
    {
        string Name { get; set; }
        string Body { get; set; }
        void Run();
    }
}