using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using SampleCaliburnWPF.Watcher;

namespace SampleCaliburnWPF.ViewModels
{
    public class LauncherMainViewModel : Screen, IHandle<RunProcessMessage>, IHandle<StopProcessMessage>
    {
        private IEventAggregator eventAggregator;
        private ProcessWatcher processWatcher;

        private ProcessItemViewModel selectedItem;

        public ProcessItemViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<ProcessItemViewModel> processItems;
        public ObservableCollection<ProcessItemViewModel> ProcessItems
        {
            get => processItems;
            set
            {
                processItems = value;
                NotifyOfPropertyChange();
            }
        }

        public LauncherMainViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnPublishedThread(this);

            ProcessItems = new ObservableCollection<ProcessItemViewModel>();

            processWatcher = new ProcessWatcher();
            
            UpdateProcessItems(eventAggregator);
        }

        private void UpdateProcessItems(IEventAggregator eventAggregator)
        {
            ProcessItems.Clear();

            foreach (ProcessData processData in processWatcher.Processes)
            {
                ProcessItems.Add(new ProcessItemViewModel(eventAggregator, processData));
            }
        }

        public void OnSelectedRowChanged(SelectionChangedEventArgs args)
        {

        }

        public Task HandleAsync(RunProcessMessage message, CancellationToken cancellationToken)
        {
            processWatcher.RunProcess(message.Process);
            return Task.FromResult(true);
        }

        public Task HandleAsync(StopProcessMessage message, CancellationToken cancellationToken)
        {
            processWatcher.StopProcess(message.Process);
            return Task.FromResult(true);
        }
    }
}
