using System.Windows;
using System.Windows.Forms.Integration;
using Caliburn.Micro;

namespace WpfUserControlLibrary
{
    public class Bootstraper: BootstrapperBase
    {
        public Bootstraper(ElementHost elementHost): base(false)
        {
            Initialize();    
            //            // container is your preferred DI container
            //            var rootViewModel = container.Resolve();
            //            // ViewLocator is a Caliburn class for mapping views to view models
            //UserControl1 rootViewModel = new UserControl1();
            MainViewModel rootViewModel = new MainViewModel();
            var rootView = ViewLocator.LocateForModel(rootViewModel, null, null);
            ViewModelBinder.Bind(rootViewModel, rootView, null);
            // Set elementHost child as mentioned earlier
            elementHost.Child = rootView;
            ViewModelBinder.Bind(rootViewModel, rootView, null);
        }

        #region Overrides of BootstrapperBase
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
        #endregion
    }
}
