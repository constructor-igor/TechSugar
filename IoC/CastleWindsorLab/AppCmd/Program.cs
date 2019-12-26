using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Plugin.Demo.Interfaces;

/*
 * https://stackoverflow.com/questions/31783755/configuring-castle-windsor-using-xml-app-config
 * https://www.codementor.io/copperstarconsulting/getting-started-with-dependency-injection-using-castle-windsor-4meqzcsvh
 */

namespace AppCmd
{
    public class MyRootService
    {
        public void Run(string title)
        {
            Console.WriteLine($"[{title}] MyRootService.Run()");
        }
    }
    public class MyChildService
    {
        public void Run(string title)
        {
            Console.WriteLine($"[{title}] MyChildService.Run()");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string pluginsFolder = Path.GetFullPath(@"..\..\..\Plugins");
            AssemblyFilter assemblyFilter = new AssemblyFilter(pluginsFolder);
            var rootContainer = new WindsorContainer();
            rootContainer.Register(Component.For<MyRootService>());
            rootContainer.Register(Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IDemoPlugin>().WithService.FromInterface());
            IDemoPlugin[] plugins = rootContainer.ResolveAll<IDemoPlugin>();
            Console.WriteLine($"In folder {pluginsFolder} found {plugins.Length} plugins.");

            foreach (IDemoPlugin plugin in plugins)
            {
                plugin.Run(null);
            }
            rootContainer.Resolve<MyRootService>().Run("root");

            WindsorContainer childContainer = new WindsorContainer();
            childContainer.Register(Component.For<MyChildService>());
            rootContainer.AddChildContainer(childContainer);
            childContainer.Resolve<MyRootService>().Run("child");
            childContainer.Resolve<MyChildService>().Run("child");

            try
            {
                Console.WriteLine("trying to resolve MyChildContainer in root container...");
                rootContainer.Resolve<MyChildService>().Run("root");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot resolve in root: {e.Message}");
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
