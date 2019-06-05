using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace WcfLogLibService
{
    /// <summary>
    /// Interaction logic for ServerControl.xaml
    /// </summary>
    public partial class ServerControl : UserControl, IServerControl
    {
        ServiceHost _host;

        bool _serverRunning = false;

        public ServerControl()
        {
            InitializeComponent();
        }

        public void LogMessage(string msg)
        {
            logger.Text +=
                System.Environment.NewLine +
                DateTime.Now.ToLocalTime().ToString() + ": " +
                msg;
        }

        void ClearMessages()
        {
            logger.Text = string.Empty;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _serverRunning = !_serverRunning;

                if (_serverRunning)
                {
                    LogService svc = new LogService(this);

                    _host = new ServiceHost(
                        svc,
                        new Uri[] { new Uri("net.pipe://localhost") });

                    _host.AddServiceEndpoint(
                        typeof(ILogService),
                        new NetNamedPipeBinding(), "LogSrv");

                    _host.Open();

                    ClearMessages();
                    LogMessage("Server started ...");
                }
                else
                {
                    _host.Close();
                    _host = null;

                    LogMessage("Server stopped");
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }
    }
}
