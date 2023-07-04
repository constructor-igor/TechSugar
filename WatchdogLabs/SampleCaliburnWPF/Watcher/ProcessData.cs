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
        public ProcessDefinition ProcessDefinition { get; private set; }
        public Process Process { get; private set; }

        public string ProcessName { get { return ProcessDefinition.Name; } }
        public bool IsRunning { get; set; }

        public ProcessData(ProcessDefinition processDefinition)
        {
            ProcessDefinition = processDefinition;
        }
        public void SetRunningProcess(Process process)
        {
            Process = process;
            IsRunning = true;
        }
        public void Start()
        {
            if (!IsRunning)
            {
                Process = Process.Start(ProcessDefinition.ProcessPath);
                IsRunning = true;
            }
        }
        public void Stop()
        {
            if (IsRunning)
            {
                Process.Kill();
                IsRunning = false;
            }
        }
    }
}
