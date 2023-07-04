using System.Diagnostics;
using Caliburn.Micro;
using SampleCaliburnWPF.Watcher;

namespace SampleCaliburnWPF.ViewModels
{
    public class ProcessItemViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ProcessData process;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
        }

        private bool isRunning;
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                NotifyOfPropertyChange();
            }
        }

        public ProcessItemViewModel(IEventAggregator eventAggregator, ProcessData process)
        {
            this.eventAggregator = eventAggregator;
            this.process = process;

            Name = process.ProcessName;
        }

        public void RunProcess()
        {
            eventAggregator.PublishOnCurrentThreadAsync(new RunProcessMessage(process));
        }

        public void StopProcess()
        {
            eventAggregator.PublishOnCurrentThreadAsync(new StopProcessMessage(process));
        }
    }
}
