using Customer.TextData;

namespace MainConsole
{
    public class PathToFolder
    {
        public string Folder { get; private set; }
        public PathToFolder(string folder)
        {
            Folder = folder;
        }
    }
    public class PathToFile
    {
        public string File { get; private set; }

        public PathToFile(string pathToFile)
        {
            File = pathToFile;
        }
    }

    public class BuilderCustomerData
    {
        public PathToFile PathToFile { get; set; }
        public bool Enabled { get; set; }
        public CustomerTextData CustomerData { get; set; }
    }
}
