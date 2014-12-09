using System.ComponentModel.Composition;
using Plugin.Command.Interfaces;

namespace SomeBusinessLogic
{
    [Export(typeof(ICommandPlugin))]
    [ExportMetadata("Symbol", '*')]
    public class PluginMult : ICommandPlugin
    {
        public string Name { get; private set; }

        public double Run(double X, double Y)
        {
            return X * Y;
        }

        public PluginMult()
        {
            Name = "Mult";
        }
    }
}