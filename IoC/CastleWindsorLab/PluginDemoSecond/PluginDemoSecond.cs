using System;
using Plugin.Demo.Interfaces;

namespace PluginDemoSecond
{
    public class PluginDemoSecond: IDemoPlugin
    {
        #region IDemoPlugin
        public void Run(IDemoApplication application)
        {
            Console.WriteLine("PluginDemoSecond");
        }
        #endregion
    }
}
