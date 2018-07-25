using System.IO;
using System.Reflection;

namespace ctor.location.tests
{
    public class TestsHelper
    {
        public const string TEST_DATA_FOLDER_ROOT = @"..\..\..\Data\";

        public static string GetTestDataFilePath(string fileName)
        {
            string dataSetFile = Path.Combine(GetDllFolder(), TEST_DATA_FOLDER_ROOT, fileName);
            return Path.GetFullPath(dataSetFile);
        }
        private static string GetDllFolder()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return assemblyFolder;
        }
    }
}