using System.IO;

namespace SampleCaliburnWPF.Watcher
{
    public class ProcessDefinition
    {
        public readonly string Name;
        public readonly string ProcessPath;
        public ProcessDefinition(string processPath, string name = null)
        {
            ProcessPath = processPath;
            Name = name != null ? name : Path.GetFileNameWithoutExtension(processPath);
        }
    }
}
