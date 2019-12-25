using System;
using Plugin.Demo.Interfaces;

namespace Plugin.Demo.First
{
    public class PluginDemoFirst: IDemoPlugin
    {
        #region IDemoPlugin
        public void Run(IDemoApplication application)
        {
            Console.WriteLine("PluginDemoFirst");
        }
        #endregion
    }
}
