namespace Engine.Model
{
    public class AlgorithmExecuter
    {
        private readonly Model model;
        public AlgorithmExecuter(Model model)
        {
            this.model = model;
        }

        public void Run()
        {
            foreach (IAlgorithmCommand command in model.Algorithm.Commands)
            {
                command.Run();
            }
        }
    }
}