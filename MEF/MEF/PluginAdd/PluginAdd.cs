using System.ComponentModel.Composition;
using Plugin.Command.Interfaces;

namespace PluginAdd
{
    [Export(typeof(ICommandPlugin))]
    public class PluginAdd : ICommandPlugin
    {
        public string Name { get; private set; }
        public double Run(double X, double Y)
        {
            return X + Y;
        }

        public PluginAdd()
        {
            Name = "Add";
        }
    }
}
