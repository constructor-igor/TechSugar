using System.Windows;
using WcfLogLibService;

namespace LoggerServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IServerControl serverControl = new ServerControl();
            InitializeComponent();
            //ContentControl.DataContext = serverControl;
            ContentControl.Content = serverControl;
        }
    }
}
