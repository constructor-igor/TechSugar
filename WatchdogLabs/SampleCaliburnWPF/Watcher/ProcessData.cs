using System.Diagnostics;

namespace SampleCaliburnWPF.Watcher
{
    public class RunProcessMessage
    {
        public ProcessData Process { get; }

        public RunProcessMessage(ProcessData process)
        {
            Process = process;
        }
    }

    public class StopProcessMessage
    {
        public ProcessData Process { get; }

        public StopProcessMessage(ProcessData process)
        {
            Process = process;
        }
    }

    public class ProcessData
    {
        public Process Process { get; }

        public string ProcessName { get ; }
        public bool IsRunning { get; set; }

        public ProcessData(Process process, string processName)
        {
            Process = process;
            ProcessName = processName;
        }
    }
}
