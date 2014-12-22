using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandDescriptor : ICommandDescriptor
    {
        public string Unique { get; private set; }
        public string Name { get; private set; }

        public CommandDescriptor(string unique, string name)
        {
            Unique = unique;
            Name = name;
        }
    }
}