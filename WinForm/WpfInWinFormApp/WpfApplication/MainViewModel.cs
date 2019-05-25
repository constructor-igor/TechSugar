using Caliburn.Micro;

namespace WpfApplication
{
    public class MainViewModel: Screen
    {
        public MyViewModel MyViewModel { get; private set; }

        public MainViewModel()
        {
            MyViewModel = new MyViewModel();
        }
    }
}