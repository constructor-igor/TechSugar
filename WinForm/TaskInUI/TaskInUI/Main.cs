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
            int epxectedDuration = 3;
            ToOutput("[Started]Start worker ({0} seconds)", epxectedDuration);

            button1.Enabled = false;
            Task.Factory.StartNew(() => service.Do(epxectedDuration))
                .ContinueWith((data) =>
                {
                    ToOutput("[Completed]Start worker");
                    button1.Enabled = true;
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ToOutput(string text, params object[] arguments)
        {
            outputTextBox.AppendText(String.Format(text, arguments) + Environment.NewLine);
        }
    }
}
