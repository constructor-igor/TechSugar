namespace Engine.Model
{
    public class AlgorithmCommand : IAlgorithmCommand
    {
        public string Name { get; set; }
        public string Body { get; set; }

        public AlgorithmCommand(string name, string body)
        {
            Name = name;
            Body = body;
        }
        public void Run()
        {
            
        }
    }
}