using System;
using System.ServiceModel;
using System.Windows;
using WcfLogLibService;


namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ILogService _chatSrv;
        LogCallback _callback;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _callback = new LogCallback(this);

                DuplexChannelFactory<ILogService> factory =
                    new DuplexChannelFactory<ILogService>(
                        _callback,
                        new NetNamedPipeBinding(),
                        new EndpointAddress("net.pipe://localhost/LogSrv"));

                _chatSrv = factory.CreateChannel();

                bool res = _chatSrv.Subscribe();
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex.Message);
            }
        }

        public void LogPeerMessage(string msg)
        {
            LogMessage(msg);
        }
        void LogErrorMessage(string msg)
        {
            LogMessage(msg);
        }
        void LogMessage(string msg)
        {
            string newMsg =
                Environment.NewLine +
                DateTime.Now.ToLocalTime() + " - " + msg +
                Environment.NewLine;

            logger.AppendText(newMsg);
        }

        void LogUserMessage(string msg)
        {
            LogMessage(msg);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            LogUserMessage("You say: " + Environment.NewLine + tbMessage.Text);

            try
            {
                string msg = tbMessage.Text;
                tbMessage.Text = string.Empty;

                _chatSrv.SendMessage(tbUser.Text, msg);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex.Message);
            }
        }
    }
}
