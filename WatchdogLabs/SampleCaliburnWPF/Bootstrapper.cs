using Caliburn.Micro;
using SampleCaliburnWPF.ViewModels;
using System;

namespace SampleCaliburnWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        readonly SimpleContainer container;

        public Bootstrapper()
        {
            container = new SimpleContainer();
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override void Configure()
        {
            Init();
        }

        private void Init()
        {
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            container.Singleton<ShellViewModel>();
            container.Singleton<LauncherMainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }
    }
}
