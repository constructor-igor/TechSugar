using System.Windows;
using Caliburn.Micro;

namespace WpfApplication
{
    public class Bootstraper: BootstrapperBase
    {
        public Bootstraper()
        {
            Initialize();    
        }

        #region Overrides of BootstrapperBase
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
        #endregion
    }
}
