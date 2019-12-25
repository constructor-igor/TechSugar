using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using Plugin.Demo.Interfaces;

/*
 * https://stackoverflow.com/questions/31783755/configuring-castle-windsor-using-xml-app-config
 * https://www.codementor.io/copperstarconsulting/getting-started-with-dependency-injection-using-castle-windsor-4meqzcsvh
 */

namespace AppCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string pluginsFolder = Path.GetFullPath(@"..\..\..\Plugins");
            AssemblyFilter assemblyFilter = new AssemblyFilter(pluginsFolder);
            var container = new WindsorContainer();
            container.Register(Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IDemoPlugin>().WithService.FromInterface());
            IDemoPlugin[] plugins = container.ResolveAll<IDemoPlugin>();
            Console.WriteLine($"In folder {pluginsFolder} found {plugins.Length} plugins.");

            foreach (IDemoPlugin plugin in plugins)
            {
                plugin.Run(null);
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static void Sample_01()
        {
            var container = new WindsorContainer(new XmlInterpreter());
            // Register the CompositionRoot type with the container
            //            container.Register(Component.For<ICompositionRoot>().ImplementedBy<CompositionRoot>());
            //            container.Register(Component.For<IConsoleWriter>().ImplementedBy<ConsoleWriter>());
            //            container.Register(Component.For<IConsoleWriter>().ImplementedBy<ColorConsoleWriter>()); 
            //            container.Register(Component.For<ISingletonDemo>().ImplementedBy<SingletonDemo>());
            //            container.Register(Component.For<ISingletonDemo>().ImplementedBy<SingletonDemo>().LifestyleTransient());

            // Resolve an object of type ICompositionRoot (ask the container for an instance)
            // This is analagous to calling new() in a non-IoC application.
            var root = container.Resolve<ICompositionRoot>();

            root.LogMessage("Hello from my very first resolved class!");

            // Wait for user input so they can check the program's output.
        }
    }
}
