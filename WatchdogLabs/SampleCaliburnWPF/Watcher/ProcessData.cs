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

    public enum ProcessState
    {
        Running,
        Stopped
    }
    public class ProcessData
    {
        public ProcessDefinition ProcessDefinition { get; private set; }
        public ProcessState State { get; private set; }
        public Process Process { get; private set; }

        public string ProcessName { get { return ProcessDefinition.Name; } }
        public bool IsRunning { get; set; }

        public ProcessData(ProcessDefinition processDefinition)
        {
            ProcessDefinition = processDefinition;
            State = ProcessState.Stopped;
        }
        public void SetRunningProcess(Process process)
        {
            Process = process;
            State = ProcessState.Running;
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
                State = ProcessState.Stopped;
                IsRunning = false;
            }
        }
    }
}
