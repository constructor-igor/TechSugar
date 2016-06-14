using System.IO;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string dbFilePath = Path.Combine(Path.GetDirectoryName(location), "demo.db");

            var databaseContext = new DatabaseContext.DatabaseContext(dbFilePath);
        }
    }
}
