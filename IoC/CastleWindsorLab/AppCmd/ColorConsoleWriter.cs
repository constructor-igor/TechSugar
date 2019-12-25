using System;

namespace AppCmd
{
    public class ColorConsoleWriter : IConsoleWriter
    {
        readonly ISingletonDemo singletonDemo;

        public ColorConsoleWriter(ISingletonDemo singletonDemo)
        {
            this.singletonDemo = singletonDemo;
        }

        public void LogMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"ConsoleWriter.LogMessage:  singletonDemo.ObjectId={singletonDemo.ObjectId}");
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}