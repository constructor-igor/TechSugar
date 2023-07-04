using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace SampleCaliburnWPF.Watcher;

public class ProcessWatcher
{
    public List<ProcessData> Processes;
    public List<ProcessDefinition> Definitions = new List<ProcessDefinition>();
    public Timer timer = new Timer();

    public ProcessWatcher()
    {
        Definitions.Add(new ProcessDefinition(@"C:\Windows\System32\mspaint.exe"));
        Definitions.Add(new ProcessDefinition(@"C:\Program Files\Notepad++\notepad++.exe"));

        Processes = new List<ProcessData>();
        foreach (ProcessDefinition definition in Definitions)
        {
            Processes.Add(new ProcessData(definition));
        }

        CheckIfProcessesRun();

        timer.Interval = 5000;
        timer.Elapsed += TimerElapsed;
        timer.Start();
    }

    private void CheckIfProcessesRun()
    {        
        foreach (ProcessData processData in Processes)
        {
            Process[] foundProcesses = Process.GetProcessesByName(processData.ProcessName);
            bool found = foundProcesses.Length>0;
            processData.IsRunning = found;
            if (found)
            {
                processData.SetRunningProcess(foundProcesses[0]);
            }
            else if (processData.State == ProcessState.Running)
            {
                processData.Start();    
            }
        }
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        CheckIfProcessesRun();
    }

    public void RunProcess(ProcessData processData)
    {
        processData.Start();
    }

    public void StopProcess(ProcessData processData)
    {
        processData.Stop();
    }
}