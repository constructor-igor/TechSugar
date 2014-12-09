using System.ComponentModel.Composition;
using Plugin.Command.Interfaces;

namespace PluginSub
{
    [Export(typeof(ICommandPlugin))]
    public class PluginSub: ICommandPlugin
    {
        public string Name { get; private set; }

        public double Run(double X, double Y)
        {
            return X - Y;
        }

        public PluginSub()
        {
            Name = "Sub";
        }
    }
}
