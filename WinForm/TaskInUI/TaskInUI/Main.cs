using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskInUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Service service = new Service();
            int expectedDuration = 5;
            ToOutput("[Started] Worker ({0} seconds)", expectedDuration);

            button1.Enabled = false;
            Task.Factory.StartNew(() => service.Do(expectedDuration))
                .ContinueWith(data =>
                {
                    ToOutput("[Completed] Worker");
                    button1.Enabled = true;
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ToOutput(string text, params object[] arguments)
        {
            outputTextBox.AppendText(String.Format(text, arguments) + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Service service = new Service();
            int expectedDuration = 10;
            ToOutput("[Started] Worker with progress ({0} seconds)", expectedDuration);

            button1.Enabled = false;
            Task.Factory.StartNew(() => service.Do(expectedDuration))
                .ContinueWith(data =>
                {
                    ToOutput("[Completed] Worker with progress");
                    button1.Enabled = true;
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
