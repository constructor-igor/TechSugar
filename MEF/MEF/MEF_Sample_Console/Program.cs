//
//
//  References:
//  - http://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx
//  - http://www.codeproject.com/Articles/188054/An-Introduction-to-Managed-Extensibility-Framework#10
//
//  - [Mefx] http://msdn.microsoft.com/en-us/library/ff576068(v=vs.110).aspx
//          - see exported types: mefx /dir:. /dir:..\..\..\@PluginBinaries /parts
//          - see exported types: mefx /dir:. /dir:..\..\..\@PluginBinaries /parts /verbose
//
//          - exports:  mefx /dir:. /dir:..\..\..\@PluginBinaries /exports

using System;
using System.Collections.Generic;
using System.Globalization;
using Plugin.Command.Framework;

namespace MEF_Sample_Console
{
    class Program
    {
        static void Main()
        {
            var framework = new PluginCommandFramework();

            Console.WriteLine("Loaded commands list:");
            foreach (PluginCommandDescriptor descriptor in framework.Descriptors)
            {
                IDictionary<string, object> metaData = descriptor.MetaData;
                string symbol = metaData.ContainsKey("Symbol") ? ((char)metaData["Symbol"]).ToString(CultureInfo.InvariantCulture) : "?";
                Console.WriteLine("command '{0}({1})' loaded", descriptor.Command.Name, symbol);
            }

//            Console.WriteLine("Loaded commands list:");
//            foreach (ICommandPlugin command in framework.Commands)
//            {
//                Console.WriteLine("command '{0}' loaded", command.Name);
//            }
//            Console.WriteLine("Loaded symbols list:");
//            foreach (IDictionary<string, object> metaData in framework.MetaData)
//            {
//                string symbol = metaData.ContainsKey("Symbol") ? ((char) metaData["Symbol"]).ToString(CultureInfo.InvariantCulture) : "?";
//                Console.WriteLine("symbol '{0}' loaded", symbol);
//            }
            Console.WriteLine();
        }
    }
}
