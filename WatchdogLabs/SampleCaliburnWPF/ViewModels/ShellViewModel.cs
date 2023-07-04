using Caliburn.Micro;

namespace SampleCaliburnWPF.ViewModels
{
    public class ShellViewModel : Screen
    {
        public LauncherMainViewModel LauncherMainViewModel { get; set; }

        public ShellViewModel(LauncherMainViewModel launcherMainViewModel)
        {
            LauncherMainViewModel = launcherMainViewModel;
        }
    }
}
