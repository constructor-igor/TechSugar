using System;
using Serilog;

namespace SeqSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Information("Hello, {Name}!", Environment.UserName);

            // Important to call at exit so that batched events are flushed.
            Log.CloseAndFlush();

            Console.ReadKey(true);
        }
    }
}
