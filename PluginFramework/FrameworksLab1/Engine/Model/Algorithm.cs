using System.Collections.Generic;

namespace Engine.Model
{
    public class Algorithm
    {
        public readonly List<IAlgorithmCommand> Commands = new List<IAlgorithmCommand>();
        public void AddCommand(IAlgorithmCommand command)
        {
            Commands.Add(command);
        }
    }
}