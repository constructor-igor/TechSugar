using System.Windows;
using Caliburn.Micro;
using LocalizationMarkupExtensionExample.ViewModels;

namespace LocalizationMarkupExtensionExample
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<AppMainViewModel>();
        }
    }
}
