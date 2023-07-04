using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace SampleCaliburnWPF.Watcher;

public class ProcessWatcher
{
    private string DEVICE_MANAGER = "DeviceManager";
    private string DEVICE_MANAGER_PATH = @"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\DeviceManager.exe";

    private string CONFIGURATION_SERVICE = "@C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\ConfigurationService.exe";
    private string CONFIGURATION_SERVICE_PATH = "ConfigurationService";

    private string DIAGNOSTICS_SERVICE = "DiagnosticsService";
    private string DIAGNOSTICS_SERVICE_PATH = @"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\DiagnosticsService.exe";

    private string JOBS_SERVICE = "JobsService";
    private string JOBS_SERVICE_PATH = @"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\JobsService.exe";

    private string IMAGE_PROCESS_PROVIDER_SERVICE = "ImageProcessProviderService";
    private string IMAGE_PROCESS_PROVIDER_SERVICE_PATH = @"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\ImageProcessProviderService.exe";

    public Dictionary<string, ProcessData> Processes;
    public List<ProcessDefinition> Definitions = new List<ProcessDefinition>();

    public ProcessWatcher()
    {
        //Definitions.Add(new ProcessDefinition(@"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\DeviceManager.exe"));
        //Definitions.Add(new ProcessDefinition(@"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\ConfigurationService.exe"));
        //Definitions.Add(new ProcessDefinition(@"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\DiagnosticsService.exe"));
        //Definitions.Add(new ProcessDefinition(@"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\JobsService.exe"));
        //Definitions.Add(new ProcessDefinition(@"C:\\RepositoryCore\\Infrastructure\\Common\\Bin\\Debug\\ImageProcessProviderServicev.exe"));
        Definitions.Add(new ProcessDefinition(@"C:\Windows\System32\mspaint.exe"));
        Definitions.Add(new ProcessDefinition(@"C:\Program Files\Notepad++\notepad++.exe"));

        Processes = new Dictionary<string, ProcessData>();
        foreach (ProcessDefinition definition in Definitions)
        {
            Process process = new Process();
            process.StartInfo.FileName = definition.ProcessPath;
            Processes.Add(definition.Name, new ProcessData(process, definition.Name));
        }

        CheckIfProcessesRun();

        Timer timer = new Timer();
        timer.Interval = 5000; 
        timer.Elapsed += TimerElapsed;
        timer.Start();
    }

    private void CheckIfProcessesRun()
    {
        Process[] currentProcesses = Process.GetProcesses();

        foreach (Process process in currentProcesses)
        {
            if (Processes.TryGetValue(process.ProcessName, out ProcessData processData))
            {
                processData.IsRunning = true;
            }
        }
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        CheckIfProcessesRun();
    }

    private bool IsProcessRunning(Process[] processes, string processName)
    {
        foreach (Process process in processes)
        {
            if (process.ProcessName == processName)
            {
                if (Processes.TryGetValue(processName, out ProcessData processData))
                {
                    processData.IsRunning = true;
                    return true;
                }
            }
        }
        if (Processes.TryGetValue(processName, out ProcessData procData))
        {
            procData.IsRunning = false;
        }
        return false;
    }

    public void RunProcess(ProcessData processData)
    {
        processData.Process.Start();
        //processData.Process.WaitForExit();
        if(Processes.TryGetValue(processData.ProcessName, out ProcessData data))
        {
            data.IsRunning = true;
        }
    }

    public void StopProcess(ProcessData process)
    {
        if (Processes.TryGetValue(process.ProcessName, out ProcessData data))
        {
            data.Process.Kill();
            data.IsRunning = false;
        }
    }
}